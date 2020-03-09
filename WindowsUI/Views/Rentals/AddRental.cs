using BLL.Infrastructure.Shared.Interfaces;
using BLL.Infrastructure.Shared.Responses;
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
    public partial class AddRental : Form
    {
        private readonly IRentalsLogic _rentalsLogic;
        private readonly int _clientId;
        private readonly bool _addNewCopy;
        public AddRental(IRentalsLogic rentalsLogic, int clientId, bool addNewCopy)
        {
            _rentalsLogic = rentalsLogic;
            _clientId = clientId;
            _addNewCopy = addNewCopy;
            InitializeComponent();
        }

        private void Save(int copyId)
        {
            if ((_clientId != 0) && (copyId != 0))
            {
                var responseResult = _addNewCopy ? _rentalsLogic.AddRental(_clientId, copyId) : _rentalsLogic.ReturnCopy(_clientId, copyId);
                if (!responseResult.Success)
                {
                    MessageBox.Show(responseResult.Error, responseResult.Info, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Sucessfully Added", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var copyId = int.Parse(txtRental.Text.ToString());
            Save(copyId);
        }
    }
}
