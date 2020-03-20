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

namespace WindowsUI.Views.Clients
{
    public partial class AddClient : Form
    {
        private IClientsLogic _clientsLogic;
        public AddClient(IClientsLogic clientsLogic)
        {
            _clientsLogic = clientsLogic;
            InitializeComponent();
            dpDOB.Value = DateTime.Now.AddYears(-10);
        }

        private bool Validate(string firstName, string lastName, DateTime dateOfBirth)
        {
            var notValid = (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || dateOfBirth.Date > DateTime.Now.Date);
            if (notValid)
            {
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Validate(txtFirstName.Text, txtLastName.Text, dpDOB.Value))
                {
                    MessageBox.Show("Please Enter Client Correct Details");
                    return;
                }

                var result = _clientsLogic.AddOrUpdate(int.Parse(txtId.Text), txtLastName.Text, txtFirstName.Text, dpDOB.Value);
                if (!result.Success)
                {
                    MessageBox.Show(result.Error);
                    return;
                }
                txtId.Text = result.Data.ToString();
                var addnew = MessageBox.Show("Do you want to Add Another Client", "Client Sucessfully Saved", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                if (addnew)
                {
                    ClearControls();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }

        }

        private void ClearControls()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtId.Text = "0";
            dpDOB.Value = DateTime.Now.AddYears(-10);
        }
    }
}
