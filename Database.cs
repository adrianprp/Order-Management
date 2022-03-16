using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiuneComenzi
{
    public class Database
    {
        public static OleDbConnection con;

        public void connect()
        {
            try
            {
                con = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\Mojo\\source\\repos\\GestiuneComenzi\\GestiuneComenzi\\DB.accdb");
                con.Open();
                MessageBox.Show("Conexiune Reusita");
            }
            catch (Exception e)
            {
                MessageBox.Show("Eroare" + e);
            }
        }

        public void disconnect()
        {
            con.Close();
        }

        public string login(string username, string password)
        {
            string id = String.Empty;
            string name = String.Empty;
            string query = "Select * FROM Utilizatori where Username='" + username + "' and Parola='" + password + "'";

            try
            {
 
                OleDbCommand command = new OleDbCommand(query, con);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = reader.GetValue(4).ToString();
                    name = reader.GetValue(1).ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (!id.Equals(String.Empty))
            {
                new ClientInfo(name, id);
                return id.Equals("0") ? "admin" : "client";
            } 
            else
            {
                return String.Empty;
            }

        }
    }
}
