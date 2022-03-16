using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiuneComenzi
{
    public partial class Eticheta_Form : Form
    {
        public static Eticheta_Form instance = null;
        static int row;

        public Eticheta_Form()
        {
            this.Show();
            if (instance == null)
                instance = this;
            InitializeComponent();
        }

        public static Eticheta_Form getInstance()
        {
            return instance;
        }

        public void fillInfo()
        {
            

            string query = String.Empty;
            if (ClientForm.getInstance() == null)
            {
                row = AdminForm.getSelectedRow();

                if (row == -1)
                {
                    row = 0;
                }

                query = " SELECT Comanda.Data, Comanda.ID_Comanda, Comanda.Greutate, Clienti.[Nume Client], Clienti.[Persoana de contact], Clienti.[Nr Contact], Livrare.[Nume destinatar], Livrare.[Adresa destinatar], Livrare.[Oras destinatar], Comanda.Comentarii,Comanda.Data, Comanda.ID_Comanda, Comanda.Greutate, Clienti.[Nume Client], Clienti.[Persoana de contact], Clienti.[Nr Contact], Livrare.[Nume destinatar], Livrare.[Adresa destinatar], Livrare.[Oras destinatar], Comanda.Comentarii " +
                              " FROM [Servicii suplimentare] INNER JOIN(Livrare INNER JOIN(Colectare INNER JOIN(Clienti INNER JOIN Comanda ON Clienti.ID_Client = Comanda.ID_Client) ON Colectare.ID_Colectare = Comanda.ID_Colectare) ON Livrare.ID_Livrare = Comanda.ID_Livrare) ON Comanda.ID_Comanda = [Servicii Suplimentare].ID_Comanda " +
                              " WHERE (((Comanda.ID_Comanda) Like " + "\"" + AdminForm.getInstance().dataGridView2[2, row].Value.ToString() + "\"" + "));";
            }

            if(AdminForm.getInstance() == null)
            {
                row = ClientForm.getSelectedRow();

                if (row == -1)
                {
                    row = 0;
                }
                query = " SELECT Comanda.Data, Comanda.ID_Comanda, Comanda.Greutate, Clienti.[Nume Client], Clienti.[Persoana de contact], Clienti.[Nr Contact], Livrare.[Nume destinatar], Livrare.[Adresa destinatar], Livrare.[Oras destinatar], Comanda.Comentarii,Comanda.Data, Comanda.ID_Comanda, Comanda.Greutate, Clienti.[Nume Client], Clienti.[Persoana de contact], Clienti.[Nr Contact], Livrare.[Nume destinatar], Livrare.[Adresa destinatar], Livrare.[Oras destinatar], Comanda.Comentarii " +
                              " FROM [Servicii suplimentare] INNER JOIN(Livrare INNER JOIN(Colectare INNER JOIN(Clienti INNER JOIN Comanda ON Clienti.ID_Client = Comanda.ID_Client) ON Colectare.ID_Colectare = Comanda.ID_Colectare) ON Livrare.ID_Livrare = Comanda.ID_Livrare) ON Comanda.ID_Comanda = [Servicii Suplimentare].ID_Comanda " +
                              " WHERE (((Comanda.ID_Comanda) Like " + "\"" + ClientForm.getInstance().dataGridView2[2, row].Value.ToString() + "\"" + "));"; 
            }

            try
            {

                OleDbCommand command = new OleDbCommand(query, Database.con);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DataComanda.Text = DateTime.ParseExact(reader.GetValue(0).ToString(), "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture).GetDateTimeFormats()[4].ToString();
                    ID_Comanda.Text = reader.GetValue(1).ToString();
                    grPaleti.Text = reader.GetValue(2).ToString();
                    NumeClient.Text = reader.GetValue(3).ToString();
                    PersContact.Text = reader.GetValue(4).ToString();
                    TelContact.Text = reader.GetValue(5).ToString();
                    NumeDest.Text = reader.GetValue(6).ToString();
                    AdresaDest.Text = reader.GetValue(7).ToString();
                    OrasDest.Text = reader.GetValue(8).ToString();
                    comm.Text = reader.GetValue(9).ToString();
                    DataComanda2.Text = DateTime.ParseExact(reader.GetValue(10).ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture).GetDateTimeFormats()[4].ToString();
                    ID_Comanda2.Text = reader.GetValue(11).ToString();
                    grPaleti2.Text = reader.GetValue(12).ToString();
                    NumeClient2.Text = reader.GetValue(13).ToString();
                    PersContact2.Text = reader.GetValue(14).ToString();
                    TelContact2.Text = reader.GetValue(15).ToString();
                    NumeDest2.Text = reader.GetValue(16).ToString();
                    AdresaDest2.Text = reader.GetValue(17).ToString();
                    OrasDest2.Text = reader.GetValue(18).ToString();
                    comm2.Text = reader.GetValue(19).ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NumeDest2_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = Utils.DrawControlToBitmap(EtichetaImg);
            bitmap.Save("Eticheta.bmp");
            //dialog.FileName, ImageFormat.Jpeg
            System.Diagnostics.Process.Start("Eticheta.bmp");
        }
    }
}
