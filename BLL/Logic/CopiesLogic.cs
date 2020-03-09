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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Logic
{
    public class CopiesLogic : BaseLogic, ICopiesLogic
    {
        public CopiesLogic(IUnitOfwork unitOfwork) : base(unitOfwork)
        {
        }

        public ObjectResponse<bool> AddOrUpdate(int copyId, int? movieId, bool? available = true)
        {
            try
            {
                if (!movieId.HasValue)
                {
                    return new ObjectResponse<bool> { Success = false, Error = "Invalid Movie Id", Info = DB_SAVE_ERROR };
                }

                var response = _unitOfWork.CopiesRepository.GetFirstWhere(x => x.copy_id == copyId);
                response = InitialiseCopy(copyId, movieId, available, response);

                return _unitOfWork.DbContext.SaveChanges() > 0 ?
                    new ObjectResponse<bool> { Success = true, Data = true } : new ObjectResponse<bool> { Success = false, Error = DB_SAVE_ERROR };
            }
            catch (Exception ex)
            {
                return new ObjectResponse<bool> { Success = false, Error = ex.GetBaseException().Message, Info = DB_SAVE_ERROR };
            }
        }

        private copy InitialiseCopy(int copyId, int? movieId, bool? available, copy response)
        {
            if (response != null)
            {
                if (response.movie_id != movieId)
                {
                    response = CreateCopy(copyId, movieId.Value, available);
                }
                else
                {
                    response.available = available;
                }

            }
            else
            {
                response = CreateCopy(copyId, movieId.Value, available);
                _unitOfWork.CopiesRepository.Add(response);
            }

            return response;
        }

        private copy CreateCopy(int copyId, int movieId, bool? available)
        {
            var response = new copy
            {
                available = available,
                copy_id = copyId,
                movie_id = movieId
            };
            _unitOfWork.CopiesRepository.Add(response);
            return response;
        }

        public ObjectResponse<List<CopyDTO>> GetAll()
        {
            try
            {
                var response = _unitOfWork.CopiesRepository.GetAll().Include(t => t.movy).Include(y => y.rentals).ToList();
                if (!response.Any())
                {
                    return new ObjectResponse<List<CopyDTO>> { Success = false, Error = "No Copies In System" };
                }

                return new ObjectResponse<List<CopyDTO>> { Success = true, Data = response.Select(x => CopyDTO.Create(x))?.ToList() };
            }
            catch (Exception ex)
            {
                return new ObjectResponse<List<CopyDTO>> { Success = false, Error = ex.GetBaseException().Message, Info = DB_GET_ERROR };
            }
        }

        public ObjectResponse<CopyDTO> GetLastCopy()
        {
            try
            {
                CopyDTO copyDTO = null;
                var response = _unitOfWork.CopiesRepository.GetAll().OrderByDescending(x => x.copy_id).FirstOrDefault();
                if (response != null)
                {
                    copyDTO = new CopyDTO
                    {
                        Id = response.copy_id,
                        Available = response.available,
                    };
                }
                return new ObjectResponse<CopyDTO> { Success = true, Data = copyDTO };
            }
            catch (Exception ex)
            {
                return new ObjectResponse<CopyDTO> { Success = false, Error = ex.GetBaseException().Message, Info = DB_GET_ERROR };
            }
        }

        public ObjectResponse<CopyDTO> GetCopy(Expression<Func<copy, bool>> func)
        {
            try
            {
                var response = _unitOfWork.CopiesRepository.GetFirstWhere(func);
                if (response != null)
                {
                    return new ObjectResponse<CopyDTO> { Success = true, Data = CopyDTO.Create(response) };
                }
                return new ObjectResponse<CopyDTO> { Success = false, Error = "Copy Is Not Available Now" };

            }
            catch (Exception ex)
            {
                return new ObjectResponse<CopyDTO> { Success = false, Error = ex.GetBaseException().Message, Info = DB_GET_ERROR };
            }
        }
    }
}
