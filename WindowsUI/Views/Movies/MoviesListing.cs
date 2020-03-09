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

namespace WindowsUI.Views.Movies
{
    public partial class MoviesListing : Form
    {
        private readonly IMoviesLogic _moviesLogic;
        public MoviesListing(IMoviesLogic moviesLogic)
        {
            this._moviesLogic = moviesLogic;
            InitializeComponent();
            this.FillForm();
        }

        private void MoviesListing_Load(object sender, EventArgs e)
        {

        }
        private void FillForm()
        {
            dgrid.Rows?.Clear();
            var moviesResponse = _moviesLogic.GetAll();
            if (!moviesResponse.Success)
            {

            }
            foreach (var movie in moviesResponse.Data)
            {
                dgrid.Rows.Add(1);
                dgrid.Rows[dgrid.Rows.Count - 1].Cells[0].Value = movie.Id;
                dgrid.Rows[dgrid.Rows.Count - 1].Cells[1].Value = movie.Title;
                dgrid.Rows[dgrid.Rows.Count - 1].Cells[2].Value = movie.Year;
                dgrid.Rows[dgrid.Rows.Count - 1].Cells[3].Value = movie.Price;
                dgrid.Rows[dgrid.Rows.Count - 1].Cells[4].Value = movie.Copies.Count(x => x.Available == true);
                dgrid.Rows[dgrid.Rows.Count - 1].Cells[5].Value = movie.Copies.Count(x => x.Available == false);

            }
        }
    }
}
