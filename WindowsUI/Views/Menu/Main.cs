using BLL.Infrastructure.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsUI.Views.Clients;
using WindowsUI.Views.Movies;

namespace WindowsUI
{
    public partial class Main : Form
    {
        private readonly IMoviesLogic _moviesLogic;
        private readonly IClientsLogic _clientsLogic;
        private readonly IRentalsLogic _rentalsLogic;
        public Main(IMoviesLogic moviesLogic, IClientsLogic clientsLogic, IRentalsLogic rentalsLogic)
        {
            this._moviesLogic = moviesLogic;
            this._clientsLogic = clientsLogic;
            this._rentalsLogic = rentalsLogic;
          InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
          
        }

        private void mnuMoviesDirectory_Click(object sender, EventArgs e)
        {
            var frmMoviesList = new MoviesListing(_moviesLogic);
            frmMoviesList.Show();

        }

        private void mnuClientsDirectory_Click(object sender, EventArgs e)
        {
            var frmClientsListing = new ClientsListing(_clientsLogic,_rentalsLogic);
            frmClientsListing.Show();
        }
    }
}
