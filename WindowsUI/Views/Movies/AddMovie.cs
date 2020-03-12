using BLL.Infrastructure.Shared.Interfaces;
using BLL.Models.DTO;
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
    public partial class AddMovie : Form
    {
        private readonly IMoviesLogic _moviesLogic;
        private readonly IActorsLogic _actorsLogic;
        private readonly int _movieId;
        public AddMovie(IMoviesLogic moviesLogic, IActorsLogic actorsLogic, int movieId = 0)
        {
            _actorsLogic = actorsLogic;
            _moviesLogic = moviesLogic;
            _movieId = movieId;
            InitializeComponent();
            InitialiseControls();
            FillForm();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AddMovie_Load(object sender, EventArgs e)
        {

        }

        private bool Validate(string title, float price, int year)
        {
            var isValid = (!string.IsNullOrWhiteSpace(title) && price > 0 && year > 0);
            if (isValid)
            {
                return true;
            }

            return false;
        }

        private void FillForm()
        {
            if (_movieId != 0)
            {
                var result = _moviesLogic.GetById(_movieId);
                if (!result.Success)
                {
                    MessageBox.Show(result.Error, result.Info, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                txtId.Text = result.Data.Id.ToString();
                txtPrice.Text = result.Data?.Price?.ToString();
                txtTitle.Text = result.Data?.Title?.ToString();
                nmYear.Value = (int)result.Data?.Year;
                nmAgeRestriction.Value = (int)(result.Data?.AgeRestriction ?? 0);
                if (result.Data.Copies.Any())
                {
                    FillCopies(result.Data.Copies);
                }
                if (result.Data.Actors.Any())
                {
                    FillCurrentActorsGrid(result.Data.Actors);
                }
                FillActorsGrid(result.Data.Actors);
                return;
            }
            FillActorsGrid();
        }

        private void InitialiseControls()
        {
            txtId.Clear();
            txtPrice.Clear();
            txtPrice.Clear();
            dgridActors.Rows.Clear();
            dgridMovieActors.Rows.Clear();
            dgrid.Rows.Clear();
        }

        private void FillCurrentActorsGrid(List<ActorDTO> actors)
        {
            foreach (var actor in actors)
            {
                dgridMovieActors.Rows.Add(1);
                dgridMovieActors.Rows[dgridMovieActors.Rows.Count - 1].Cells[0].Value = actor.Id;
                dgridMovieActors.Rows[dgridMovieActors.Rows.Count - 1].Cells[1].Value = actor.LastName;
                dgridMovieActors.Rows[dgridMovieActors.Rows.Count - 1].Cells[2].Value = actor.FirstName;
                dgridMovieActors.Rows[dgridMovieActors.Rows.Count - 1].Cells[3].Value = true;

            }
        }

        private void FillCopies(List<CopyDTO> copies)
        {
            foreach (var copy in copies)
            {
                dgrid.Rows.Add(1);
                dgrid.Rows[dgrid.Rows.Count - 1].Cells[0].Value = copy.Id;
                dgrid.Rows[dgrid.Rows.Count - 1].Cells[1].Value = copy.Available;

            }
        }

        private List<ActorDTO> LoadActors(List<ActorDTO> actors = null)
        {
            var result = _actorsLogic.GetAll();
            if (!result.Success)
            {
                return null;
            }
            if (actors != null)
            {
                var actorsId = actors.Select(x => x.Id).ToArray();
                result.Data = result.Data.Where(x => !actorsId.Contains(x.Id)).ToList();
            }
            return result.Data;
        }
        private void FillActorsGrid(List<ActorDTO> actors = null)
        {
            var result = LoadActors(actors);
            foreach (var actor in result)
            {
                dgridActors.Rows.Add(1);
                dgridActors.Rows[dgridActors.Rows.Count - 2].Cells[0].Value = actor.Id;
                dgridActors.Rows[dgridActors.Rows.Count - 2].Cells[1].Value = actor.LastName;
                dgridActors.Rows[dgridActors.Rows.Count - 2].Cells[2].Value = actor.FirstName;
                dgridActors.Rows[dgridActors.Rows.Count - 2].Cells[3].Value = false;

            }
        }

    }
}
