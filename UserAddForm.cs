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
    public partial class UserAddForm : Form
    {
        public UserAddForm()
        {
            InitializeComponent();
            loadClienti();
        }
        public void loadClienti()
        {

            try
            {
                string query = "SELECT  [Nume Client]  from Clienti";
                OleDbCommand command = new OleDbCommand(query, Database.con);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    cbClient.Items.Add(reader[0]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected virtual void saveUser()
        {
            string id = Utils.getClientID(cbClient);
            //// Marfa
            string query = "INSERT INTO Utilizatori ( Username, Email, Parola, ID_Client ) VALUES ('" + Utils.getIntValue(textUser.Text) + "','" + Utils.getIntValue(textEmail.Text) + "','" + Utils.getStringValue(textPass.Text) + "','" + id + "')";

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
            saveUser();
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
