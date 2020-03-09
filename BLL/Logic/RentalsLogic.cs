using BLL.Infrastructure.Shared.Abstracts;
using BLL.Infrastructure.Shared.Interfaces;
using BLL.Infrastructure.Shared.Responses;
using BLL.Models;
using BLL.Models.DTO;
using DVDRENTAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVDRENTAL.Entities;
namespace BLL.Logic
{
    public class RentalsLogic : BaseLogic, IRentalsLogic
    {
        private ICopiesLogic _copiesLogic;
        public RentalsLogic(IUnitOfwork unitOfwork, ICopiesLogic copiesLogic) : base(unitOfwork)
        {
            _copiesLogic = copiesLogic;
        }

        public ObjectResponse<bool> AddRental(int clientId, int copyId, DateTime dateOfReturn)
        {
            try
            {
                var copy = _copiesLogic.GetCopy(x => x.copy_id == copyId && x.available == true);
                if (!copy.Success)
                {
                    return new ObjectResponse<bool> { Success = false, Error = "Copy Does not Exist In Shop" };
                }

                var rental = new rental
                {
                    client_id = clientId,
                    copy_id = copyId,
                    date_of_rental = DateTime.Now,
                    date_of_return = dateOfReturn
                };

                _unitOfWork.RentalsRepository.Add(rental);
                var result = _copiesLogic.AddOrUpdate(copy.Data.Id, copy.Data.Movie.Id, false);

                return result.Success ? new ObjectResponse<bool> { Success = true, Data = true } :
                    new ObjectResponse<bool> { Success = false, Error = result.Error, Info = result.Info };


            }
            catch (Exception ex)
            {
                return new ObjectResponse<bool> { Success = false, Error = ex.GetBaseException().Message, Info = DB_SAVE_ERROR };
            }
        }

       
        public ObjectResponse<List<RentalDTO>> GetRentals(RentalFilters rentalFilters)
        {
            try
            {
                var response = _unitOfWork.RentalsRepository.GetAll().Include(x=>x.copy).Include(x=>x.copy.movy).Include(y=>y.client);
                if (rentalFilters.ClientId.HasValue)
                {
                    response = response.Where(x => x.client_id == rentalFilters.ClientId.Value);
                }

                if (rentalFilters.RentalDateFrom.HasValue)
                {
                    var datefrom = rentalFilters.RentalDateFrom.Value.Date;
                    response = response.Where(x => x.date_of_rental.Value.CompareTo(datefrom) >= 0);
                }

                if (rentalFilters.RentalDateTo.HasValue)
                {
                    var dateTo = rentalFilters.RentalDateFrom.Value.Date.AddDays(1);
                    response = response.Where(x => x.date_of_rental.Value.CompareTo(dateTo) < 0);
                }

                return new ObjectResponse<List<RentalDTO>> { Success = true, Data = response.ToList()?.Select(x => RentalDTO.Create(x))?.ToList() };
            }
            catch (Exception ex)
            {
                return new ObjectResponse<List<RentalDTO>> { Success = false, Error = ex.GetBaseException().Message, Info = DB_GET_ERROR };
            }
        }

        public ObjectResponse<bool> ReturnCopy(int clientId, int copyId)
        {
            try
            {
                var response = _copiesLogic.GetCopy(x => x.copy_id == copyId && x.available == false);
                if (!response.Success)
                {
                    return new ObjectResponse<bool> { Success = false, Error = response.Error, Info = response.Info };
                }

                var rental = _unitOfWork.RentalsRepository.GetFirstWhere(x => x.copy_id == response.Data.Id && x.client_id == clientId);
                if (rental != null)
                {
                    var result = _copiesLogic.AddOrUpdate(response.Data.Id, response.Data.Movie.Id, true);

                    return result.Success ? new ObjectResponse<bool> { Success = true, Data = true } :
                          new ObjectResponse<bool> { Success = false, Error = result.Error, Info = result.Info };
                }
                return new ObjectResponse<bool> { Success = false, Error = "Invalid Rental", Info = DB_GET_ERROR };
            }
            catch (Exception ex)
            {
                return new ObjectResponse<bool> { Success = false, Error = ex.GetBaseException().Message };
            }
        }
    }
}
