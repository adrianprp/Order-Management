using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiuneComenzi
{
    public partial class Borderou : Form
    {
        public static Borderou instance = null;
        int row = -1;
        public Borderou()
        {
            this.Show();
            if (instance == null)
                instance = this;
            InitializeComponent();
        }


        public static Borderou getInstance()
        {
            return instance;
        }



        public void fillInfo()
        {
            

           

            if (ClientForm.getInstance() == null)
            {
                row = AdminForm.getSelectedRow();

                if (row == -1)
                {
                    row = 0;
                }

                string query = " SELECT Clienti.[Nr Contact], Clienti.[Nume Client],  Comanda.ID_Comanda, Livrare.[Nume destinatar], Livrare.[Adresa destinatar], Livrare.[Oras destinatar], Comanda.[Numar paleti], Comanda.Greutate, Comanda.Data " +
                          " FROM [Servicii suplimentare] INNER JOIN(Livrare INNER JOIN(Colectare INNER JOIN(Clienti INNER JOIN Comanda ON Clienti.ID_Client = Comanda.ID_Client) ON Colectare.ID_Colectare = Comanda.ID_Colectare) ON Livrare.ID_Livrare = Comanda.ID_Livrare) ON Comanda.ID_Comanda = [Servicii Suplimentare].ID_Comanda " +
                          " WHERE (((Comanda.ID_Comanda) Like " + "\"" + AdminForm.getInstance().dataGridView2[2, row].Value.ToString() + "\"" + "));";


                try
                {

                    OleDbCommand command = new OleDbCommand(query, Database.con);
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        txtNrContact.Text = reader.GetValue(0).ToString();
                        txtNumeClient.Text = reader.GetValue(1).ToString();
                        txtIDComanda.Text = reader.GetValue(2).ToString();
                        txtNumeDest.Text = reader.GetValue(3).ToString();
                        txtAdresaDest.Text = reader.GetValue(4).ToString();
                        txtOrasDest.Text = reader.GetValue(5).ToString();
                        txtNrPaleti.Text = reader.GetValue(6).ToString();
                        txtGrPaleti.Text = reader.GetValue(7).ToString();
                        txtNrPaleti2.Text = reader.GetValue(6).ToString();
                        txtGrPaleti2.Text = reader.GetValue(7).ToString();
                        txtData.Text = reader.GetValue(8).ToString();


                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (AdminForm.getInstance() == null)
            {
                row = ClientForm.getSelectedRow();

                if (row == -1)
                {
                    row = 0;
                }
                string query = " SELECT Clienti.[Nr Contact], Clienti.[Nume Client],  Comanda.ID_Comanda, Livrare.[Nume destinatar], Livrare.[Adresa destinatar], Livrare.[Oras destinatar], Comanda.[Numar paleti], Comanda.Greutate, Comanda.Data " +
                          " FROM [Servicii suplimentare] INNER JOIN(Livrare INNER JOIN(Colectare INNER JOIN(Clienti INNER JOIN Comanda ON Clienti.ID_Client = Comanda.ID_Client) ON Colectare.ID_Colectare = Comanda.ID_Colectare) ON Livrare.ID_Livrare = Comanda.ID_Livrare) ON Comanda.ID_Comanda = [Servicii Suplimentare].ID_Comanda " +
                          " WHERE (((Comanda.ID_Comanda) Like " + "\"" + ClientForm.getInstance().dataGridView2[2, row].Value.ToString() + "\"" + "));";


                try
                {

                    OleDbCommand command = new OleDbCommand(query, Database.con);
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        txtNrContact.Text = reader.GetValue(0).ToString();
                        txtNumeClient.Text = reader.GetValue(1).ToString();
                        txtIDComanda.Text = reader.GetValue(2).ToString();
                        txtNumeDest.Text = reader.GetValue(3).ToString();
                        txtAdresaDest.Text = reader.GetValue(4).ToString();
                        txtOrasDest.Text = reader.GetValue(5).ToString();
                        txtNrPaleti.Text = reader.GetValue(6).ToString();
                        txtGrPaleti.Text = reader.GetValue(7).ToString();
                        txtNrPaleti2.Text = reader.GetValue(6).ToString();
                        txtGrPaleti2.Text = reader.GetValue(7).ToString();
                        txtData.Text = reader.GetValue(8).ToString();


                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            }

        private void btnSave_Click(object sender, EventArgs e)
        {

            Bitmap bitmap = Utils.DrawControlToBitmap(BorderouImg);
            bitmap.Save("Borderou.bmp");
            //dialog.FileName, ImageFormat.Jpeg
            System.Diagnostics.Process.Start("Borderou.bmp");
        }
    }
}
