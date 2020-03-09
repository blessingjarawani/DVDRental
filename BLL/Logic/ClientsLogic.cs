using BLL.Infrastructure.Shared.Abstracts;
using BLL.Infrastructure.Shared.Interfaces;
using BLL.Infrastructure.Shared.Responses;
using BLL.Models.DTO;
using DVDRENTAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Logic
{
    public class ClientsLogic : BaseLogic, IClientsLogic
    {
        public ClientsLogic(IUnitOfwork unitOfwork) : base(unitOfwork)
        {
        }

        public ObjectResponse<List<ClientDTO>> GetClients(string searchText)
        {
            try
            {
                var response = _unitOfWork.ClientsRepository.GetAll();
                if (!String.IsNullOrWhiteSpace(searchText))
                {
                    var searchKey = searchText.ToLower();
                    response = response.Where(x => searchKey.Contains(x.client_id.ToString().ToLower())
                    || searchKey.Contains(x.first_name.ToLower()) || searchKey.Contains(x.last_name.ToLower()));
                }

                return new ObjectResponse<List<ClientDTO>> { Success = true, Data = response.ToList()?.Select(x => ClientDTO.Create(x))?.ToList() };

            }
            catch (Exception ex)
            {
                return new ObjectResponse<List<ClientDTO>> { Success = false, Error = ex.GetBaseException().Message, Info = DB_GET_ERROR };

            }
        }
    }
}
