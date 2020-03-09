using BLL.Infrastructure.Shared.Abstracts;
using BLL.Infrastructure.Shared.Interfaces;
using BLL.Infrastructure.Shared.Responses;
using BLL.Models.DTO;
using DVDRENTAL.Entities;
using DVDRENTAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Logic
{
    public class MoviesLogic : BaseLogic, IMoviesLogic
    {
        private ICopiesLogic _copiesLogic;
        public MoviesLogic(IUnitOfwork unitOfWork, ICopiesLogic copiesLogic) : base(unitOfWork)
        {
            _copiesLogic = copiesLogic;
        }

        public ObjectResponse<bool> AddorUpdate(MovieDTO movieDTO)
        {
            try
            {
                if (movieDTO != null)
                {
                    var movie = _unitOfWork.MoviesRepository.GetFirstWhere(x => x.movie_id == movieDTO.Id);
                    if (movie != null)
                    {
                        movie.price = movieDTO.Price;
                        movie.title = movieDTO.Title;
                        movie.year = movieDTO.Year;
                    }
                    else
                    {
                        var id = (_unitOfWork.MoviesRepository.GetAll().OrderByDescending(x => x.movie_id).FirstOrDefault()?.movie_id + 1) ?? 1;
                        movie = new movy
                        {
                            movie_id = id,
                            age_restriction = movieDTO.AgeRestriction,
                            title = movieDTO.Title,
                            price = movieDTO.Price,
                            year = movieDTO.Year
                        };
                        _unitOfWork.MoviesRepository.Add(movie);
                    }

                    return AddMovieCopy(movie);

                }
                return new ObjectResponse<bool> { Success = false, Error = "Invalid Supplied Parameters", Info = DB_SAVE_ERROR };
            }
            catch (Exception ex)
            {
                return new ObjectResponse<bool> { Success = false, Error = ex.GetBaseException().Message, Info = DB_ERROR_INSERT };
            }
        }

        private ObjectResponse<bool> AddMovieCopy(movy movie)
        {
            var movieCopy = _copiesLogic.GetLastCopy();
            if (!movieCopy.Success)
            {
                return new ObjectResponse<bool> { Success = false, Error = movieCopy.Error, Info = movieCopy.Info };
            }

            var copyId = (movieCopy.Data?.Id + 1) ?? 1;
            var addCopyResult = _copiesLogic.AddOrUpdate(copyId, movie.movie_id, true);

            return addCopyResult.Success ? new ObjectResponse<bool> { Success = true, Data = true } :
                new ObjectResponse<bool> { Success = false, Error = addCopyResult.Error, Info = addCopyResult.Info };
        }

        public ObjectResponse<List<MovieDTO>> GetAll()
        {
            try
            {
                var response = _unitOfWork.MoviesRepository.GetAll().Include(t => t.copies).Include(z => z.actors).ToList();
                if (!response.Any())
                {
                    return new ObjectResponse<List<MovieDTO>> { Success = false, Error = "No Movies In Database", Info = DB_GET_ERROR };
                }
                return new ObjectResponse<List<MovieDTO>> { Success = true, Data = response.Select(x => MovieDTO.Create(x))?.ToList() };
            }
            catch (Exception ex)
            {
                return new ObjectResponse<List<MovieDTO>> { Success = false, Error = ex.GetBaseException().Message, Info = DB_GET_ERROR };
            }
        }

        public ObjectResponse<MovieDTO> GetById(int id)
        {
            try
            {
                var response = _unitOfWork.MoviesRepository.GetAllWhere(x => x.movie_id == id).Include(t => t.copies).Include(z => z.actors)?.FirstOrDefault();
                if (response != null)
                {
                    return new ObjectResponse<MovieDTO> { Success = true, Data = MovieDTO.Create(response) };

                }
                return new ObjectResponse<MovieDTO> { Success = false, Error = "No Movie Found", Info = DB_GET_ERROR };

            }
            catch (Exception ex)
            {
                return new ObjectResponse<MovieDTO> { Success = false, Error = ex.GetBaseException().Message, Info = DB_GET_ERROR };
            }
        }
    }
}
