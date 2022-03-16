using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiuneComenzi
{
    public partial class CMR_Form : Form
    {
        public static CMR_Form instance = null;
        

        int row = -1;
        public CMR_Form()
        {
            this.Show();
            if(instance == null)
                instance = this;
            InitializeComponent();
           
            
            CMR.Controls.Add(NumeClient);
            CMR.Controls.Add(AdresaSediu);
            CMR.Controls.Add(OrasSediu);
            CMR.Controls.Add(NrContact);
            CMR.Controls.Add(PersContact);
            CMR.Controls.Add(NumeDest);
            CMR.Controls.Add(AdresaDest);
            CMR.Controls.Add(JudetDest);
            CMR.Controls.Add(OrasDest);
            CMR.Controls.Add(TelDest);
            CMR.Controls.Add(ContactDest);
            CMR.Controls.Add(OraDesLiv);
            CMR.Controls.Add(OraIncLiv);
            CMR.Controls.Add(NumeExp);
            CMR.Controls.Add(AdresaExp);
            CMR.Controls.Add(JudetExp);
            CMR.Controls.Add(OrasExp);
            CMR.Controls.Add(TelExp);
            CMR.Controls.Add(ContactExp);
            CMR.Controls.Add(OraDesCol);
            CMR.Controls.Add(OraIncCol);
            CMR.Controls.Add(DataColectare);
            CMR.Controls.Add(DataLivrare);
            CMR.Controls.Add(ID_Com);
            CMR.Controls.Add(nrPaleti);
            CMR.Controls.Add(grPaleti);
            CMR.Controls.Add(Asigurare);
            CMR.Controls.Add(comm);





          //  print();
        }

        public static CMR_Form getInstance()
        {
            return instance;
        }

       

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
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

                query = " SELECT Clienti.[Nume Client], Clienti.[Adresă sediu social], Clienti.[Oraș sediu social], Clienti.[Nr Contact], Clienti.[Persoana de contact], Livrare.[Nume destinatar], Livrare.[Adresa destinatar], Livrare.[Judet destinatar], Livrare.[Oras destinatar], Livrare.[Telefon destinatar], Livrare.[Contact destinatar], Livrare.[Ora deschidere locatie], Livrare.[Ora inchidere locatie], Colectare.[Nume expeditor], Colectare.[Adresa expeditor], Colectare.[Judet expeditor], Colectare.[Oras expeditor], Colectare.[Telefon expeditor], Colectare.[Contact expeditor], Colectare.[Ora deschidere locatie], Colectare.[Ora inchidere locatie], Colectare.[Data colectare], Livrare.[Data livrare], Comanda.ID_Comanda, Comanda.[Numar paleti], Comanda.Greutate, [Servicii suplimentare].[Valoare marfa], Comanda.Comentarii " +
                          " FROM [Servicii suplimentare] INNER JOIN(Livrare INNER JOIN(Colectare INNER JOIN(Clienti INNER JOIN Comanda ON Clienti.ID_Client = Comanda.ID_Client) ON Colectare.ID_Colectare = Comanda.ID_Colectare) ON Livrare.ID_Livrare = Comanda.ID_Livrare) ON Comanda.ID_Comanda = [Servicii Suplimentare].ID_Comanda " +
                          " WHERE (((Comanda.ID_Comanda) Like " + "\"" + AdminForm.getInstance().dataGridView2[2, row].Value.ToString() + "\"" + "));";


            }
            if (AdminForm.getInstance() == null)
            {

                row = ClientForm.getSelectedRow();

                if (row == -1)
                {
                    row = 0;
                }
                 query = " SELECT Clienti.[Nume Client], Clienti.[Adresă sediu social], Clienti.[Oraș sediu social], Clienti.[Nr Contact], Clienti.[Persoana de contact], Livrare.[Nume destinatar], Livrare.[Adresa destinatar], Livrare.[Judet destinatar], Livrare.[Oras destinatar], Livrare.[Telefon destinatar], Livrare.[Contact destinatar], Livrare.[Ora deschidere locatie], Livrare.[Ora inchidere locatie], Colectare.[Nume expeditor], Colectare.[Adresa expeditor], Colectare.[Judet expeditor], Colectare.[Oras expeditor], Colectare.[Telefon expeditor], Colectare.[Contact expeditor], Colectare.[Ora deschidere locatie], Colectare.[Ora inchidere locatie], Colectare.[Data colectare], Livrare.[Data livrare], Comanda.ID_Comanda, Comanda.[Numar paleti], Comanda.Greutate, [Servicii suplimentare].[Valoare marfa], Comanda.Comentarii " +
                            " FROM [Servicii suplimentare] INNER JOIN(Livrare INNER JOIN(Colectare INNER JOIN(Clienti INNER JOIN Comanda ON Clienti.ID_Client = Comanda.ID_Client) ON Colectare.ID_Colectare = Comanda.ID_Colectare) ON Livrare.ID_Livrare = Comanda.ID_Livrare) ON Comanda.ID_Comanda = [Servicii Suplimentare].ID_Comanda " +
                            " WHERE (((Comanda.ID_Comanda) Like " + "\"" + ClientForm.getInstance().dataGridView2[2, row].Value.ToString() + "\"" + "));";

            }
                try
                {

                    OleDbCommand command = new OleDbCommand(query, Database.con);
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        NumeClient.Text = reader.GetValue(0).ToString();
                        AdresaSediu.Text = reader.GetValue(1).ToString();
                        OrasSediu.Text = reader.GetValue(2).ToString();
                        NrContact.Text = reader.GetValue(3).ToString();
                        PersContact.Text = reader.GetValue(4).ToString();
                        NumeDest.Text = reader.GetValue(5).ToString();
                        AdresaDest.Text = reader.GetValue(6).ToString();
                        JudetDest.Text = reader.GetValue(7).ToString();
                        OrasDest.Text = reader.GetValue(8).ToString();
                        TelDest.Text = reader.GetValue(9).ToString();
                        ContactDest.Text = reader.GetValue(10).ToString();
                        OraDesLiv.Text = DateTime.ParseExact(reader.GetValue(11).ToString(), "MM/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture).GetDateTimeFormats()[106].ToString();
                        OraIncLiv.Text = DateTime.ParseExact(reader.GetValue(12).ToString(), "MM/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture).GetDateTimeFormats()[106].ToString();
                        NumeExp.Text = reader.GetValue(13).ToString();
                        AdresaExp.Text = reader.GetValue(14).ToString();
                        JudetExp.Text = reader.GetValue(15).ToString();
                        OrasExp.Text = reader.GetValue(16).ToString();
                        TelExp.Text = reader.GetValue(17).ToString();
                        ContactExp.Text = reader.GetValue(18).ToString();
                        OraDesCol.Text = DateTime.ParseExact(reader.GetValue(19).ToString(), "MM/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture).GetDateTimeFormats()[106].ToString();
                        OraIncCol.Text = DateTime.ParseExact(reader.GetValue(20).ToString(), "MM/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture).GetDateTimeFormats()[106].ToString();
                        DataColectare.Text = DateTime.ParseExact(reader.GetValue(21).ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture).GetDateTimeFormats()[4].ToString();
                        DataLivrare.Text = DateTime.ParseExact(reader.GetValue(22).ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture).GetDateTimeFormats()[4].ToString();
                        ID_Com.Text = reader.GetValue(23).ToString();
                        nrPaleti.Text = reader.GetValue(24).ToString();
                        grPaleti.Text = reader.GetValue(25).ToString();
                        Asigurare.Text = reader.GetValue(26).ToString();
                        comm.Text = reader.GetValue(27).ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = Utils.DrawControlToBitmap(CMR);
            bitmap.Save("CMR.bmp");
            //dialog.FileName, ImageFormat.Jpeg
            System.Diagnostics.Process.Start("CMR.bmp");
        }
    }
}
