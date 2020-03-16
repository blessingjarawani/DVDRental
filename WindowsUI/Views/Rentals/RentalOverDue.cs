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
    public partial class RentalOverDue : Form
    {
        private readonly IRentalsLogic _rentalsLogic;
        public RentalOverDue(IRentalsLogic rentalsLogic)
        {
            _rentalsLogic = rentalsLogic;
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            FillForm();
        }
        private void FillForm()
        {
            try
            {
                var result = _rentalsLogic.GetRentals(new RentalFilters { DaysOverDue = int.Parse(txtNo.Text) });
                if (!result.Success)
                {
                    MessageBox.Show(result.Error, result.Info, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                FillRentalsGrid(result.Data);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RentalOverDue_Load(object sender, EventArgs e)
        {

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

        private void txtNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
             (e.KeyChar != '.'))
            {
                e.Handled = true;
            }


            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
