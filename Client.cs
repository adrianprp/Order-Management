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
    public partial class ClientForm : Form
    {
        static ClientForm instance = null;
        public static int rowSelected = 0;
        public ClientForm()
        {
            instance = this;
            InitializeComponent();
            loadPartide();
            loadDoc();
            pack(dataGridView1);
            pack(dataGridView2);
          
        }

        public static ClientForm getInstance()
        {
            return instance;
        }
        public static int getSelectedRow()
        {
            return rowSelected;
        }

        public void refreshFormSize(DataGridView dataGrid)
        {
            int sumWidth = 0;
            for (int i = 0; i < dataGrid.Columns.Count; ++i)
            {
                sumWidth += dataGrid.Columns[i].Width;
            }
            this.Width = sumWidth + 95;
        }


        private int getTextSize(string text)
        {
            Font font = new Font("Candara", 10.0F);
            Image fakeImage = new Bitmap(1, 1);
            Graphics graphics = Graphics.FromImage(fakeImage);
            SizeF size = graphics.MeasureString(text, font);
            return (int)size.Width;
        }

        public void pack(DataGridView dataGrid)
        {
            int maxWidth;
            for (int i = 0; i < dataGrid.Columns.Count; i++)
            {
                maxWidth = getTextSize(dataGrid.Columns[i].Name);
                for (int j = 0; j < dataGrid.Rows.Count; j++)
                {
                    if (dataGrid[i, j].Value != null)
                    {
                        int textSize = getTextSize(dataGrid[i, j].Value.ToString());
                        if (textSize > maxWidth)
                        {
                            maxWidth = textSize;
                        }
                    }
                }
                dataGrid.Columns[i].Width = maxWidth+ 30;
            }
        }


        public void loadPartide() {
            ClientInfo client_info = new ClientInfo();
            string id = client_info.getClientID();

            string query = "SELECT Comanda.ID_Comanda, Comanda.TrackAndTrace, Colectare.[Nume expeditor], Colectare.[Judet expeditor], Colectare.[Oras expeditor], Colectare.[Adresa expeditor], Colectare.[Contact expeditor], Colectare.[Telefon expeditor], Livrare.[Nume destinatar], Livrare.[Judet destinatar], Livrare.[Oras destinatar], Livrare.[Adresa destinatar], Livrare.[Contact destinatar], Livrare.[Telefon destinatar], Comanda.[Numar paleti], Comanda.Greutate " +
                "FROM((Clienti INNER JOIN Comanda ON Clienti.ID_Client = Comanda.ID_Client) INNER JOIN Livrare ON Comanda.ID_Comanda = Livrare.ID_Livrare) " +
                "INNER JOIN Colectare ON Comanda.ID_Comanda = Colectare.ID_Colectare WHERE (((Clienti.ID_Client) Like " + id + "));";



            try
            {

                OleDbCommand command = new OleDbCommand(query, Database.con);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable table = new DataTable();
                da.Fill(table);
                dataGridView1.DataSource = table;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void loadDoc()
        {
            ClientInfo client_info = new ClientInfo();
            string id = client_info.getClientID();

            string query = "SELECT Clienti.ID_Client, Clienti.[Nume Client], Comanda.ID_Comanda, Comanda.Data, Livrare.[Nume destinatar], Colectare.[Nume expeditor], Comanda.Validat" +
                            " FROM Colectare INNER JOIN(Livrare INNER JOIN(Clienti INNER JOIN Comanda ON Clienti.ID_Client = Comanda.ID_Client) ON Livrare.ID_Livrare = Comanda.ID_Livrare) ON Colectare.ID_Colectare = Comanda.ID_Colectare "+
                            " WHERE(((Clienti.ID_Client)Like " + id + "));";

            try
            {

                OleDbCommand command = new OleDbCommand(query, Database.con);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable table = new DataTable();
                da.Fill(table);
                dataGridView2.DataSource = table;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdauga_Click(object sender, EventArgs e)
        {
            ClientAddForm addClientForm = new ClientAddForm();
            addClientForm.Show();
            instance.Hide();
        }

        private void ClientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void btnValidare_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            rowSelected = e.RowIndex;
            boolValidat();
        }


        private void boolValidat()

        {

           int row = ClientForm.getSelectedRow();

            if (row == -1)
            {
                row = 0;
            }

            string validat =Utils.getStringValue(ClientForm.getInstance().dataGridView2[6, row].Value.ToString());
         

            if (validat.Equals("True"))
            {
                btnCMR.Enabled = true;
                btnBord.Enabled = true;
                btnEti.Enabled = true;
            }
            else
            {
                btnCMR.Enabled = false;
                btnBord.Enabled = false;
                btnEti.Enabled = false;
            }


        }

        private void btnCMR_Click(object sender, EventArgs e)
        {
            CMR_Form cmr = new CMR_Form();
            if (cmr.IsDisposed == true)
                cmr = new CMR_Form();
            cmr.fillInfo();
            cmr.Show();
        

        }

        private void btnBord_Click(object sender, EventArgs e)
        {
            Borderou borderou =   new Borderou();
            if (borderou.IsDisposed == true)
                borderou = new Borderou();

            borderou.Show();
            borderou.fillInfo();
        }

        private void btnEti_Click(object sender, EventArgs e)
        {
           Eticheta_Form eticheta= new Eticheta_Form();
            if(eticheta.IsDisposed== true)
                eticheta= new Eticheta_Form();

            eticheta.Show();
            eticheta.fillInfo();
            
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {   
           
            this.Close(); 
            new LoginForm().Show();
        }
    }
}
