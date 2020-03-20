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

namespace WindowsUI.Views.Clients
{
    public partial class ClientsListing : Form
    {
        private readonly IClientsLogic _clientsLogic;
        private readonly IRentalsLogic _rentalsLogic;
        private const int VIEW_RENTAL_COL = 4;
        public ClientsListing(IClientsLogic clientsLogic, IRentalsLogic rentalsLogic)
        {
            this._clientsLogic = clientsLogic;
            this._rentalsLogic = rentalsLogic;
            InitializeComponent();
        }

        private void ClientsListing_Load(object sender, EventArgs e)
        {

        }


        private void FillForm(string searchText)
        {
            dgrid.Rows.Clear();
            var clientsResponse = _clientsLogic.GetClients(searchText);
            if (!clientsResponse.Success)
            {
                return;
            }

            foreach (var client in clientsResponse.Data)
            {
                dgrid.Rows.Add(1);
                dgrid.Rows[dgrid.Rows.Count - 1].Cells[0].Value = client.Id;
                dgrid.Rows[dgrid.Rows.Count - 1].Cells[1].Value = client.FirstName;
                dgrid.Rows[dgrid.Rows.Count - 1].Cells[2].Value = client.LastName;
                dgrid.Rows[dgrid.Rows.Count - 1].Cells[3].Value = client.Birthday.GetValueOrDefault().Date.ToShortDateString();
                dgrid.Rows[dgrid.Rows.Count - 1].Cells[4].Value = "...";
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.FillForm(txtSearchText.Text);
        }

        private void dgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == VIEW_RENTAL_COL)
            {
                if (!string.IsNullOrWhiteSpace(dgrid.CurrentRow.Cells[0].Value?.ToString()))
                {
                    var client = new ClientDTO
                    {
                        FirstName = dgrid.CurrentRow.Cells[1].Value?.ToString(),
                        LastName = dgrid.CurrentRow.Cells[2].Value?.ToString(),
                        Id = (int)dgrid.CurrentRow.Cells[0].Value
                    };
                    var frmRental = new Client(_rentalsLogic, client);
                    frmRental.Show();

                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var frmAddNew = new AddClient(_clientsLogic);
            frmAddNew.ShowDialog();
            FillForm(string.Empty);
        }
    }
}
