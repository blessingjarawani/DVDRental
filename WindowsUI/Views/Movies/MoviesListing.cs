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
        private readonly IActorsLogic _actorsLogic;

        public MoviesListing(IMoviesLogic moviesLogic, IActorsLogic actorsLogic)
        {
            this._moviesLogic = moviesLogic;
            this._actorsLogic = actorsLogic;
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
                return;
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
                dgrid.Rows[dgrid.Rows.Count - 1].Cells[6].Value = "...";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var frmMovie = new AddMovie(_moviesLogic, _actorsLogic);
            frmMovie.ShowDialog();
        }

        private void dgrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                if (!string.IsNullOrEmpty(dgrid.CurrentRow.Cells[0].Value?.ToString()))
                {
                    var movieId = int.Parse(dgrid.CurrentRow.Cells[0].Value.ToString());
                    var frmMovie = new AddMovie(_moviesLogic,_actorsLogic, movieId);
                    frmMovie.ShowDialog();
                }
            }
        }
    }
}
