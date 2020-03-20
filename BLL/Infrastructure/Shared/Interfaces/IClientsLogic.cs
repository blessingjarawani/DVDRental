using BLL.Infrastructure.Shared.Responses;
using BLL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure.Shared.Interfaces
{
    public interface IClientsLogic
    {
        ObjectResponse<List<ClientDTO>> GetClients(string SearchText);
        ObjectResponse<int> AddOrUpdate(int clientId, string lastName, string firstName, DateTime dateOfBirth);
        
    }
}
