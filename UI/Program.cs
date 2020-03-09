using BLL.Infrastructure.Shared.Interfaces;
using BLL.Logic;
using DVDRENTAL;
using DVDRENTAL.Entities;
using DVDRENTAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnitOfwork unitOfwork = new UnitOfwork(new rentalEntities());
            ICopiesLogic moviesLogic = new CopiesLogic(unitOfwork);

            var result = moviesLogic.GetAll();


        }
    }
}
