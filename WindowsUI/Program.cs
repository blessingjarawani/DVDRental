using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Infrastructure.Shared.Interfaces;
using BLL.Logic;
using DVDRENTAL;
using DVDRENTAL.Entities;
using DVDRENTAL.Interfaces;
using SimpleInjector;
namespace WindowsUI
{
    static class Program
    {
        private static Container container;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            InjectServices();
            Application.Run(container.GetInstance<Main>());
        }

        private static void  InjectServices()
        {
            container = new Container();
            container.Register<rentalEntities>(Lifestyle.Singleton);
            container.Register<IUnitOfwork, UnitOfwork>(Lifestyle.Singleton);
            container.Register<IMoviesLogic, MoviesLogic>(Lifestyle.Singleton);
            container.Register<ICopiesLogic, CopiesLogic>(Lifestyle.Singleton);
            container.Register<IRentalsLogic, RentalsLogic>(Lifestyle.Singleton);
            container.Register<IClientsLogic, ClientsLogic>(Lifestyle.Singleton);
            container.Register<IActorsLogic, ActorsLogic>(Lifestyle.Singleton);
            container.Register<Main>(Lifestyle.Singleton);
            container.Verify();
        }
    }
}
