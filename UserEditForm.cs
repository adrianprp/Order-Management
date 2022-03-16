using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiuneComenzi
{
    public partial class UserEditForm : UserAddForm
    {
        public UserEditForm()
        {
            InitializeComponent();
        }

        private void UserEditForm_Load(object sender, EventArgs e)
        {

        }

        protected override void saveUser()
        {
            string query = "UPDATE Utilizatori SET Utilizatori.Username = '" + Utils.getStringValue(textUser.Text) + "'," +
                                              "Utilizatori.Email = '" + Utils.getStringValue(textEmail.Text) + "'," +
                                              "Utilizatori.Parola = '" + Utils.getStringValue(textPass.Text) + "'" +
                                              " WHERE(( (Utilizatori.ID_Utilizatori) Like " + "\"" + AdminForm.getInstance().dataGridView1[0, AdminForm.getSelectedRow()].Value.ToString() + "\"" + "));";

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

        public void fillInfo()
        {
            int row = AdminForm.getSelectedRow();
            if (row == -1) row = 0;
            textUser.Text = AdminForm.getInstance().dataGridView3[1,row].Value.ToString();
            textEmail.Text = AdminForm.getInstance().dataGridView3[2, row].Value.ToString();
            textPass.Text = AdminForm.getInstance().dataGridView3[3, row].Value.ToString();

         


        }

    }
}
