using BLL.Infrastructure.Shared.Responses;
using BLL.Models.DTO;
using DVDRENTAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure.Shared.Interfaces
{
    public interface ICopiesLogic
    {
        ObjectResponse<List<CopyDTO>> GetAll();
        ObjectResponse<bool> AddOrUpdate(int copyId, int? movieId, bool? availability = true);
        ObjectResponse<CopyDTO> GetCopy(System.Linq.Expressions.Expression<Func<copy, bool>> func);
        ObjectResponse<CopyDTO> GetLastCopy();
    }
}
