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
using DVDRENTAL.Entities;
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

        public ObjectResponse<int> AddOrUpdate(int clientId, string lastName, string firstName, DateTime dateOfBirth)
        {
            try
            {
                if (clientId == 0)
                {
                    clientId = (_unitOfWork.ClientsRepository.GetAll().OrderByDescending(x => x.client_id).FirstOrDefault()?.client_id ?? 0) + 1;
                    var client = new client()
                    {
                        client_id = clientId,
                        first_name = firstName,
                        last_name = lastName,
                        birthday = dateOfBirth
                    };
                    _unitOfWork.ClientsRepository.Add(client);
                }
                else
                {
                    var client = _unitOfWork.ClientsRepository.GetFirstWhere(x => x.client_id == clientId);
                    if (client == null)
                    {
                        return new ObjectResponse<int> { Success = false, Error = $"no client with id {clientId} Found in DB", Info = DB_GET_ERROR };
                    }

                    client.first_name = firstName;
                    client.last_name = lastName;
                    client.birthday = dateOfBirth;
                    _unitOfWork.ClientsRepository.Update(client);
                  
                }

                return _unitOfWork.SaveChanges() > 0 ? new ObjectResponse<int> { Success = true, Data = clientId }
                        : new ObjectResponse<int> { Success = false, Error = "Failed to Save Client", Info = DB_SAVE_ERROR };

            }
            catch (Exception ex)
            {
                return new ObjectResponse<int> { Success = false, Error = ex.GetBaseException().Message, Info = DB_SAVE_ERROR };
            }
        }
    }
}
