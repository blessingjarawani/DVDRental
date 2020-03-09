using BLL.Infrastructure.Shared.Responses;
using BLL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure.Shared.Interfaces
{
    public interface IMoviesLogic
    {
        ObjectResponse<List<MovieDTO>> GetAll();
        ObjectResponse<MovieDTO> GetById(int id);
        ObjectResponse<bool> AddorUpdate(MovieDTO movieDTO);
    }
}
