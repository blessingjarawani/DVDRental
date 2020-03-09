using BLL.Infrastructure.Shared.Responses;
using BLL.Models;
using BLL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure.Shared.Interfaces
{
    public interface IRentalsLogic
    {
     
        ObjectResponse<bool> AddRental(int clientId, int copyId, DateTime dateOfReturn);
        
        ObjectResponse<List<RentalDTO>> GetRentals(RentalFilters rentalFilters);
        ObjectResponse<bool> ReturnCopy(int clientId, int copyId); 
    }
}
