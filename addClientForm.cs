using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiuneComenzi
{
    public partial class addClientForm : Form
    {
        public addClientForm()
        {
            InitializeComponent();
        }

        private void addClientForm_Load(object sender, EventArgs e)
        {

        }
        protected virtual void saveClient()
        {
           // string id = Utils.getClientID(cbClient);
            //// Marfa
            string query = "INSERT INTO Clienti ( [Nume Client], [Cod Fiscal], [Nr Registru Comert], [Persoana de contact],[Nr Contact], [Adresă sediu social], [Oraș sediu social] ) VALUES ('" + Utils.getStringValue(textNume.Text) + "','" + Utils.getStringValue(textCod.Text) + "','" + Utils.getStringValue(textNrReg.Text) + "','" + Utils.getStringValue(textPers.Text) + "','" + Utils.getStringValue(textNrContact.Text)  + "','" + Utils.getStringValue(textAdresa.Text) + "','" +  Utils.getStringValue(textOrasAdresa.Text) + "')";

            try
            {
                OleDbCommand command = new OleDbCommand(query, Database.con);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveClient();
            AdminForm.getInstance().refreshTable();
            goBack();
        }
        protected void goBack()
        {
            this.Dispose();
            AdminForm.instance.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            goBack(); 
        }
    }
}
