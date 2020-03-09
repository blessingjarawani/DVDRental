using DVDRENTAL.Entities;
using DVDRENTAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDRENTAL.Interfaces
{
    public interface IUnitOfwork
    {

        rentalEntities DbContext { get; }
        int SaveChanges();

        BaseRepository<actor> ActorsRepository { get; }
        BaseRepository<movy> MoviesRepository { get; }
        BaseRepository<copy> CopiesRepository { get; }
        BaseRepository<client> ClientsRepository { get; }
        BaseRepository<employee> EmployeesRepository { get; }
        BaseRepository<rental> RentalsRepository { get; }



    }
}
