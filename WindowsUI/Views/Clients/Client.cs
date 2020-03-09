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
using WindowsUI.Views.Rentals;

namespace WindowsUI.Views.Clients
{
    public partial class Client : Form
    {
        private readonly IRentalsLogic _rentalsLogic;
        private readonly ClientDTO _client;
        public Client(IRentalsLogic rentalsLogic, ClientDTO client)
        {
            _rentalsLogic = rentalsLogic;
            _client = _client ?? new ClientDTO();
            _client = client;
            InitializeComponent();
            FillForm();
        }

        private void Client_Load(object sender, EventArgs e)
        {

        }

        private void FillForm()
        {
            if (_client.Id > 0)
            {
                this.Text = $"Client {_client.FirstName} {_client.LastName}";
                var rentalResponse = _rentalsLogic.GetRentals(new RentalFilters { ClientId = _client.Id });
                if (!rentalResponse.Success)
                {
                    return;
                }

                var activeRentals = rentalResponse.Data?.Where(x => !x.DateOfReturn.HasValue);
                var inActiveRentals = rentalResponse.Data?.Where(x => x.DateOfReturn.HasValue);
                FillGrid(dgridCurrent, activeRentals);
                FillGrid(dgridHistory, inActiveRentals);
            }
        }
        private void FillGrid(DataGridView dgrid, IEnumerable<RentalDTO> rentals)
        {
            dgrid.Rows.Clear();
            foreach (var rental in rentals)
            {
                dgrid.Rows.Add(1);
                dgrid.Rows[dgrid.Rows.Count - 1].Cells[0].Value = rental.Copy?.Movie?.Title;
                dgrid.Rows[dgrid.Rows.Count - 1].Cells[1].Value = rental.Copy?.Id;
                dgrid.Rows[dgrid.Rows.Count - 1].Cells[2].Value = rental.DateOfRental?.Date.ToShortDateString();
                dgrid.Rows[dgrid.Rows.Count - 1].Cells[3].Value = rental.DateOfReturn?.Date.ToShortDateString();
            }

        }

        private void btnRentalAdd_Click(object sender, EventArgs e)
        {
            if (_client != null)
            {
                var frmAddnewRental = new AddRental(_rentalsLogic, _client.Id, true);
                frmAddnewRental.ShowDialog();
                FillForm();
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            var frmAddnewRental = new AddRental(_rentalsLogic, _client.Id, false);
            frmAddnewRental.ShowDialog();
            FillForm();
        }
    }
}
