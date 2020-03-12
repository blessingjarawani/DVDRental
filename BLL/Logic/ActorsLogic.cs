using BLL.Infrastructure.Shared.Abstracts;
using BLL.Infrastructure.Shared.Interfaces;
using BLL.Infrastructure.Shared.Responses;
using BLL.Models.DTO;
using DVDRENTAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Logic
{
    public class ActorsLogic : BaseLogic, IActorsLogic
    {
        private readonly IMoviesLogic _moviesLogic;
        public ActorsLogic(IUnitOfwork unitOfwork, IMoviesLogic moviesLogic) : base(unitOfwork)
        {
            _moviesLogic = moviesLogic;
        }


        public ObjectResponse<List<ActorDTO>> GetAll()
        {
            try
            {
                var response = _unitOfWork.ActorsRepository.GetAll().ToList();
                if (!response.Any())
                {
                    return new ObjectResponse<List<ActorDTO>> { Success = false, Error = "No Movies Found", Info = DB_GET_ERROR };
                }

                return new ObjectResponse<List<ActorDTO>> { Success = true, Data = response?.Select(x => ActorDTO.Create(x))?.ToList() };
            }
            catch (Exception ex)
            {
                return new ObjectResponse<List<ActorDTO>> { Success = false, Error = ex.GetBaseException().Message, Info = DB_GET_ERROR };
            }
        }
        public ObjectResponse<bool> AddOrUpdate(ActorDTO actor)
        {
            try
            {
                if (actor != null)
                {
                    if (!actor.Movies.Any())
                    {
                        return new ObjectResponse<bool> { Success = false, Error = "No Movie Selected", Info = DB_SAVE_ERROR };
                    }

                    foreach (var movie in actor.Movies)
                    {
                        if (!(movie.Id > 0))
                        {
                            continue;
                        }

                        var currentMovie = _moviesLogic.GetById(movie.Id);
                        if (!currentMovie.Success)
                        {
                            return new ObjectResponse<bool> { Success = false, Error = currentMovie.Error, Info = currentMovie.Info};
                        }



                        return new ObjectResponse<bool> { Success = false, Error = currentMovie.Error, Info = currentMovie.Info };
                    }
                }
                return new ObjectResponse<bool> { Success = false};
            }
            catch (Exception ex)
            {
                return new ObjectResponse<bool> { Success = false, Error = ex.GetBaseException().Message, Info = DB_SAVE_ERROR };
            }
        }


    }
}
