using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiuneComenzi
{
    class Utils
    {
        public static string getLastID(string colName, string table)
        {

            string id = null;
            string query = "SELECT MAX(" + colName + ") FROM " + table;

            try
            {

                OleDbCommand command = new OleDbCommand(query, Database.con);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = reader.GetValue(0).ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return (Int32.Parse(id)).ToString();
        }

        public static string getStringValue(string text)
        {
            return text.Equals(String.Empty) ? "" : text;
        }
        public static string getBoolValue(bool value)
        {
            return value ? "1" : "0";
        }

        public static string convertBoolValidare(string value)
        {
            return value.Equals("True") ? "0" : "1";
        }


        public static string getIntValue(string text)
        {
            return text.Equals(String.Empty) ? "null" : text;
        }
        public static string getNextID(string colName, string table)
        {

            string id = null;
            string query = "SELECT MAX(" + colName + ") FROM " + table;

            try
            {

                OleDbCommand command = new OleDbCommand(query, Database.con);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = reader.GetValue(0).ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return (Int32.Parse(id) + 1).ToString();
        }

        public static string getClientID(ComboBox cbClient)
        {
            string id = null;
            string client = cbClient.Text;
            try
            {
                string query = "SELECT Clienti.ID_Client FROM Clienti WHERE(((Clienti.[Nume Client]) Like " + "\"" + client + "\"" + "));";

                // "SELECT Clienti.ID_Client FROM Clienti WHERE(((Clienti.[Nume Client]) Like "+"\"SIKA ROMANIA\"" +"));";

                OleDbCommand command = new OleDbCommand(query, Database.con);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = reader.GetValue(0).ToString();
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return id;
        }

        public static string getPrimaryKey(string table, string primaryCol, string refCol, string refValue)
        {

            string id = null;
            string query = "SELECT " + primaryCol + " FROM " + table + " WHERE " + refCol + " like " + refValue;

            try
            {

                OleDbCommand command = new OleDbCommand(query, Database.con);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = reader.GetValue(0).ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return id;
        }

        public static float tarifare(string judet, string nrPaleti)
        {
            Dictionary<string, float> tarife = new Dictionary<string, float>();

            tarife.Add("Argeș", 110);
            tarife.Add("Bucuresti", 102);
            tarife.Add("Cluj", 114);
            tarife.Add("Dolj", 117);
            tarife.Add("Gorj", 77);
            tarife.Add("Iași", 158);
            tarife.Add("Mureș", 138);
            tarife.Add("Neamț", 138);
            tarife.Add("Olt", 119);
            tarife.Add("Sibiu", 105);
            tarife.Add("Timiș", 119);

            return tarife[judet] * Convert.ToInt32(nrPaleti);
        }


        public static Bitmap DrawControlToBitmap(Control control)
        {
            Bitmap bitmap = new Bitmap(control.Width, control.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            Rectangle rect = control.RectangleToScreen(control.ClientRectangle);
            graphics.CopyFromScreen(rect.Location, Point.Empty, control.Size);
            return bitmap;


        }

    }

    
}
