using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace GestiuneComenzi
{

    public partial class LoginForm : Form
    {

        
        public LoginForm()
        
        {
            Database db = new Database();
            db.connect();
            InitializeComponent();
            textUser.Text = "Utilizator";
            textPassword.Text = "Parola";
            textPassword.PasswordChar = '*';
          
        }

        

        private void textUser_MouseClick(object sender, MouseEventArgs e)
        {
            textUser.Text = "";
        }

        private void textParola_Mouseclick(object sender, MouseEventArgs e)
        {
            textPassword.Text = "";
          
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Database db = new Database();

            switch(db.login(textUser.Text, textPassword.Text))
            {
                case "admin":      
                    AdminForm adminForm = new AdminForm();
                    adminForm.Show();
                    this.Hide();
                    break;
                case "client":
                    ClientForm clientForm = new ClientForm();
                    clientForm.Show();
                    this.Hide();
                    break;
                default:
                    MessageBox.Show("Autentificare nereusita !");
                    break;

            }

    
           
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
