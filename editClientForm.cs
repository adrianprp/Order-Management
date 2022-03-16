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
    public partial class editClientForm : addClientForm
    {
        public editClientForm()
        {
            InitializeComponent();
        }

        private void editClientForm_Load(object sender, EventArgs e)
        {

        }

        protected override void saveClient()
        {
            string query = "UPDATE Clienti SET Clienti.[Nume Client] = '" + Utils.getStringValue(textNume.Text) + "'," +
                                              "Clienti.[Cod Fiscal] = '" + Utils.getStringValue(textCod.Text) + "'," +
                                              "Clienti.[Nr Registru Comert] = '" + Utils.getStringValue(textNrReg.Text) + "'," +
                                              "Clienti.[Persoana de contact] = '" + Utils.getStringValue(textPers.Text) + "'," +
                                               "Clienti.[Nr Contact] = '" + Utils.getStringValue(textNrContact.Text) + "'," +
                                                "Clienti.[Adresă sediu social] = '" + Utils.getStringValue(textAdresa.Text) + "'," +
                                                 "Clienti.[Oraș sediu social] = '" + Utils.getStringValue(textOrasAdresa.Text) + "'" +
                                              " WHERE(( (Clienti.ID_Client) Like " + "\"" + AdminForm.getInstance().dataGridView4[0, AdminForm.getSelectedRow()].Value.ToString() + "\"" + "));";

            try
            {
                OleDbCommand cmd1 = new OleDbCommand(query, Database.con);
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int row = -1;
        public void fillInfo()

        {
          
            row = AdminForm.getSelectedRow();
            if (row == -1) row = 0;
            textNume.Text = AdminForm.getInstance().dataGridView4[1, row].Value.ToString();
            textCod.Text = AdminForm.getInstance().dataGridView4[2, row].Value.ToString();
            textNrReg.Text = AdminForm.getInstance().dataGridView4[3, row].Value.ToString();
            textPers.Text = AdminForm.getInstance().dataGridView4[4, row].Value.ToString();
            textNrContact.Text = AdminForm.getInstance().dataGridView4[5, row].Value.ToString();
            textAdresa.Text = AdminForm.getInstance().dataGridView4[6, row].Value.ToString();
            textOrasAdresa.Text = AdminForm.getInstance().dataGridView4[7, row].Value.ToString();
        }
    }
}
