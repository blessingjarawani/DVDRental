using BLL.Infrastructure.Shared.Interfaces;
using BLL.Models;
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

namespace WindowsUI.Views.Rentals
{
    public partial class RentalsList : Form
    {
        private readonly IRentalsLogic _rentalsLogic;
       
        public RentalsList(IRentalsLogic rentalsLogic)
        {
            _rentalsLogic = rentalsLogic;
            InitializeComponent();
        }

        private void RentalsList_Load(object sender, EventArgs e)
        {
            dpFrom.Value = DateTime.Now.AddMonths(-1);
            dpTo.Value = DateTime.Now;
        }


        private void FillForm()
        {
            try
            {
                var result = _rentalsLogic.GetRentals(new RentalFilters { RentalDateFrom = dpFrom.Value, RentalDateTo = dpTo.Value });
                if (!result.Success)
                {
                    MessageBox.Show(result.Error,result.Info, MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

                FillRentalsGrid(result.Data);
                FillStatisticsGrids(result.Data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void FillRentalsGrid(List<RentalDTO> rentals)
        {
            dgridRentalList.Rows.Clear();
            foreach (var rental in rentals)
            {
                dgridRentalList.Rows.Add(1);
                dgridRentalList.Rows[dgridRentalList.Rows.Count - 1].Cells[0].Value = rental.Copy.Movie.Title;
                dgridRentalList.Rows[dgridRentalList.Rows.Count - 1].Cells[1].Value = rental.DateOfRental?.ToShortDateString();
                dgridRentalList.Rows[dgridRentalList.Rows.Count - 1].Cells[2].Value = rental.DateOfReturn?.ToShortDateString();
              
            }
        }

        void FillStatisticsGrids(List<RentalDTO> rentals)
        {
            var groupResult = rentals.GroupBy(x => x.Copy.Movie.Id);
            dgridRentalStats.Rows.Clear();
            foreach (var rental in groupResult)
            {
                dgridRentalStats.Rows.Add(1);
                dgridRentalStats.Rows[dgridRentalStats.Rows.Count - 1].Cells[0].Value = rental.FirstOrDefault().Copy.Movie.Title;
                dgridRentalStats.Rows[dgridRentalStats.Rows.Count - 1].Cells[1].Value = rental.Count();
                dgridRentalStats.Rows[dgridRentalStats.Rows.Count - 1].Cells[2].Value = rental.Sum(x => x.Copy.Movie.Price);

            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            FillForm();
        }
    }
}
