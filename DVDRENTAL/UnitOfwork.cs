using DVDRENTAL.Entities;
using DVDRENTAL.Interfaces;
using DVDRENTAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDRENTAL
{
   public class UnitOfwork: IUnitOfwork
    {
        private readonly rentalEntities _context;

        private BaseRepository<actor> _actorsRepository;
        private BaseRepository<movy> _moviesRepository;
        private BaseRepository<copy> _copiesRepository;
        private BaseRepository<client> _clientsRepository;
        private BaseRepository<employee> _employeesRepository;
        private BaseRepository<rental> _rentalsRepository;
        public rentalEntities DbContext => _context;
        public UnitOfwork(rentalEntities rentalEntities)
        {
            _context = rentalEntities ?? throw new ArgumentException(nameof(rentalEntities));
        }

        public int SaveChanges() => _context.SaveChanges();

        public BaseRepository<actor> ActorsRepository => _actorsRepository ?? new BaseRepository<actor>(_context);
        public BaseRepository<movy> MoviesRepository => _moviesRepository ?? new BaseRepository<movy>(_context);
        public BaseRepository<copy> CopiesRepository => _copiesRepository ?? new BaseRepository<copy>(_context);
        public BaseRepository<client> ClientsRepository => _clientsRepository ?? new BaseRepository<client>(_context);
        public BaseRepository<employee> EmployeesRepository => _employeesRepository ?? new BaseRepository<employee>(_context);
        public BaseRepository<rental> RentalsRepository => _rentalsRepository ?? new BaseRepository<rental>(_context);

       

     
    }
}
