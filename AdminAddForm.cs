using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiuneComenzi
{
    public partial class AdminAddForm : Form
    {

        public static float tarif;
        public AdminAddForm()
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

        private void ad_saveBtn_Click(object sender, EventArgs e)
        {
            savePartida();
            AdminForm.getInstance().refreshTable();
            goBack();
        }

        private void ad_cancelBtn_Click(object sender, EventArgs e)
        {
            goBack();
        }
       protected void goBack()
        {
            this.Dispose();
            AdminForm.instance.Show();
        }
        public virtual void  savePartida()
        {

            if (cbTip.Text.Equals("Livrare"))
            {
             tarif = Utils.tarifare(l_JudetDestinatar.Text , m_nrPaleti.Text);
            }else if (cbTip.Text.Equals("Colectare"))
            {
             tarif = Utils.tarifare(c_Judetexpeditor.Text, m_nrPaleti.Text);
            }
            
            
            
            
            
            
            string today = DateTime.Now.ToString("d/M/yyyy"); ;
            string id_cmd = Utils.getNextID("ID_Colectare", "Colectare");
            string id = Utils.getClientID(cbClient);
            string queryColectare = "INSERT INTO Colectare ( [Nume expeditor], [Judet expeditor], [Oras expeditor], [Adresa expeditor], [Contact expeditor], [Telefon expeditor], [Data colectare], [Ora deschidere locatie], [Ora inchidere locatie])" +
                " VALUES ('" + Utils.getStringValue(c_NumeExpeditor.Text) + "','" + Utils.getStringValue(c_Judetexpeditor.Text) + "','" + Utils.getStringValue(c_OrasExpeditor.Text) + "','" + Utils.getStringValue(c_AdresaExpeditor.Text) + "','" + Utils.getStringValue(c_ContactExpeditor.Text) + "','" + Utils.getStringValue(c_telefonExpeditor.Text) + "','" + c_DataColectare.Value + "','" + c_OraDeschidere.Value + "','" + c_OraInchidere.Value + "')";

            // LIVRARE
            string queryLivrare = "INSERT INTO Livrare ( [Nume destinatar], [Judet destinatar], [Oras destinatar], [Adresa destinatar], [Contact destinatar], [Telefon destinatar], [Data livrare], [Ora deschidere locatie], [Ora inchidere locatie])" +
                " VALUES ('" + Utils.getStringValue(l_NumeDestinatar.Text) + "','" + Utils.getStringValue(l_JudetDestinatar.Text) + "','" + Utils.getStringValue(l_OrasDestinatar.Text) + "','" + Utils.getStringValue(l_AdresaDestinatar.Text) + "','" + Utils.getStringValue(l_ContactDestinatar.Text) + "','" + Utils.getStringValue(l_telefonDestinatar.Text) + "','" + l_DataLivrare.Value + "','" + l_OraDeschidere.Value + "','" + l_OraInchidere.Value + "')";

            // Servicii
            string queryServicii = "INSERT INTO[Servicii suplimentare] ( [Retur documente], [Marfuri periculoase], [Retur paleti], [Infoliere / etichetare], Depaletare, [Livrare în sediu], [Cu asigurare], [Valoare marfa], ID_Comanda) VALUES" +
              "(" + Utils.getBoolValue(checkBox1.Checked) + "," + Utils.getBoolValue(checkBox2.Checked) + "," + Utils.getBoolValue(checkBox3.Checked) + "," + Utils.getBoolValue(checkBox4.Checked) + "," + Utils.getBoolValue(checkBox5.Checked) + "," + Utils.getBoolValue(checkBox6.Checked) + "," + Utils.getBoolValue(checkBox7.Checked) + "," + Utils.getIntValue(s_valMarfa.Text) + ",'" + id_cmd + "')";

            // Marfa
            string queryComanda = "INSERT INTO Comanda ( Data, [Numar paleti], Greutate, Comentarii, ID_Client, ID_Colectare, ID_Livrare, Tarif ) VALUES ('" + today + "'," + Utils.getIntValue(m_nrPaleti.Text) + "," + Utils.getIntValue(m_grPaleti.Text) + ",'" + Utils.getStringValue(com_comentarii.Text) + "','" + id + "','" + id_cmd + "','" + id_cmd + "','" + tarif +"')";

            try
            {
                OleDbCommand command_colectare = new OleDbCommand(queryColectare, Database.con);
                command_colectare.ExecuteNonQuery();

                OleDbCommand command_livrare = new OleDbCommand(queryLivrare, Database.con);
                command_livrare.ExecuteNonQuery();
                
                OleDbCommand command_comanda = new OleDbCommand(queryComanda, Database.con);
                command_comanda.ExecuteNonQuery();
                
                OleDbCommand command_servicii = new OleDbCommand(queryServicii, Database.con);
                command_servicii.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    
            public void tipPartida(string tip)
        {
            if (tip.Equals("Livrare"))
            {

                
                c_Judetexpeditor.Items.Add("Vâlcea");
                l_JudetDestinatar.Items.Add("Argeș");
                l_JudetDestinatar.Items.Add("Bucuresti");
                l_JudetDestinatar.Items.Add("Cluj");
                l_JudetDestinatar.Items.Add("Dolj");
                l_JudetDestinatar.Items.Add("Gorj"); 
                l_JudetDestinatar.Items.Add("Iași");  
                l_JudetDestinatar.Items.Add("Mureș");
                l_JudetDestinatar.Items.Add("Neamț");
                l_JudetDestinatar.Items.Add("Olt");
                l_JudetDestinatar.Items.Add("Sibiu");
                l_JudetDestinatar.Items.Add("Timiș");
            

            }
            else if (tip.Equals("Colectare"))
            {
               
                l_JudetDestinatar.Items.Add("Valcea");
                c_Judetexpeditor.Items.Add("Argeș");
                c_Judetexpeditor.Items.Add("Bucuresti");
                c_Judetexpeditor.Items.Add("Cluj");
                c_Judetexpeditor.Items.Add("Dolj"); 
                c_Judetexpeditor.Items.Add("Gorj"); 
                c_Judetexpeditor.Items.Add("Iași");
                c_Judetexpeditor.Items.Add("Mureș");
                c_Judetexpeditor.Items.Add("Neamț");
                c_Judetexpeditor.Items.Add("Olt");
                c_Judetexpeditor.Items.Add("Sibiu");
                c_Judetexpeditor.Items.Add("Timiș");
            }
        }

        private void cbTip_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipPartida(cbTip.Text);
        }

       

        
    }

       


}
