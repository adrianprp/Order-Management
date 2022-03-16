using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiuneComenzi
{
    public partial class AdminForm : Form
    {
        public static AdminForm instance = null;
        public static int rowSelected = 0;
        public static int columnSelected = 0;
        public static bool timeValueFrom=false;
        public static bool timeValueTill = false;
        Bitmap bitmap; 
        
        public AdminForm()
        {
            if (instance == null)
                instance = this;

            InitializeComponent();
            dateTimeFrom.Value = DateTime.Today;
            dateTimeTill.Value = DateTime.Today;
           
            loadPartide();
            loadUtilizatori();
            loadClienti();
            loadDoc();
            loadCBClienti();
           
        }

           
        public static AdminForm getInstance()
        {
            return instance;
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
            Font font = new Font("Courier New", 10.0F);
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
                dataGrid.Columns[i].Width = maxWidth;
            }
        }

        public void refreshTable()
        {
            loadPartide();
            loadUtilizatori();
            loadClienti();
            loadDoc();
        }

        public void loadCBClienti()
        {
            cbClienti.Items.Add("");
            try
            {
                string query = "SELECT  [Nume Client]  from Clienti";
                OleDbCommand command = new OleDbCommand(query, Database.con);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    cbClienti.Items.Add(reader[0]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void loadPartide()
        {
            string query = "SELECT Comanda.ID_Comanda, Comanda.TrackAndTrace, Colectare.[Nume expeditor], Colectare.[Judet expeditor], Colectare.[Oras expeditor], Colectare.[Adresa expeditor], Colectare.[Contact expeditor], Colectare.[Telefon expeditor], Livrare.[Nume destinatar], Livrare.[Judet destinatar], Livrare.[Oras destinatar], Livrare.[Adresa destinatar], Livrare.[Contact destinatar], Livrare.[Telefon destinatar], Comanda.[Numar paleti], Comanda.Greutate FROM((Clienti INNER JOIN Comanda ON Clienti.ID_Client = Comanda.ID_Client) INNER JOIN Livrare ON Comanda.ID_Comanda = Livrare.ID_Livrare) INNER JOIN Colectare ON Comanda.ID_Comanda = Colectare.ID_Colectare;";

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
        public void loadUtilizatori()
        {
            string query = "SELECT Utilizatori.ID_Utilizatori, Utilizatori.Username, Utilizatori.Email, Clienti.[Nume Client] FROM Clienti INNER JOIN Utilizatori ON Clienti.[ID_Client] = Utilizatori.[ID_Client] WHERE(((Clienti.ID_Client) =[Utilizatori].[ID_Client]));";

            try
            {
                OleDbCommand command = new OleDbCommand(query, Database.con);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable table = new DataTable();
                da.Fill(table);
                dataGridView3.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        public void loadClienti()
        {
            string query = "SELECT Clienti.ID_Client, Clienti.[Nume Client], Clienti.[Cod Fiscal], Clienti.[Nr Registru Comert] ,Clienti.[Persoana de contact], Clienti.[Nr Contact], Clienti.[Adresă sediu social], Clienti.[Oraș sediu social]  FROM Clienti;";

            try
            {
                OleDbCommand command = new OleDbCommand(query, Database.con);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable table = new DataTable();
                da.Fill(table);
                dataGridView4.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void loadDoc()
        {
            string query = "SELECT Clienti.ID_Client, Clienti.[Nume Client], Comanda.ID_Comanda, Comanda.Data, Livrare.[Nume destinatar], Colectare.[Nume expeditor], Comanda.Validat" +
                           " FROM Colectare INNER JOIN(Livrare INNER JOIN(Clienti INNER JOIN Comanda ON Clienti.ID_Client = Comanda.ID_Client) ON Livrare.ID_Livrare = Comanda.ID_Livrare) ON Colectare.ID_Colectare = Comanda.ID_Colectare; ";

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


        public static int getSelectedRow()
        {
            return rowSelected;
        }

        public static int getSelectedColumn()
        {
            return columnSelected;
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }

        private void ad_btnAdd_Click(object sender, EventArgs e)
        {
            AdminAddForm addAdminForm = new AdminAddForm();
            addAdminForm.Show();
            instance.Hide();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            AdminEditForm editAdminForm = new AdminEditForm();
            editAdminForm.Show();
            editAdminForm.fillInfo();
            instance.Hide();
        }

        public void getIDEvent(object sender, DataGridViewCellMouseEventArgs e)
        {
            rowSelected = e.RowIndex;
            columnSelected = e.ColumnIndex;
        }

        private void u_btnAdd_Click(object sender, EventArgs e)
        {
            UserAddForm addUserForm = new UserAddForm();
            addUserForm.Show();
            instance.Hide();
        }

        public string getID(DataGridView table, int row)
        {
            return "\"" + table[0, row].Value.ToString() + "\"";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string q1 = " DELETE[Servicii suplimentare].*, Comanda.*, Livrare.*, Colectare.*, Livrare.ID_Livrare, Comanda.ID_Comanda, Colectare.ID_Colectare, [Servicii suplimentare].ID_Comanda" +
                        " FROM(Livrare INNER JOIN(Colectare INNER JOIN Comanda ON Colectare.ID_Colectare = Comanda.ID_Colectare) ON Livrare.ID_Livrare = Comanda.ID_Livrare) INNER JOIN[Servicii suplimentare] ON Comanda.ID_Comanda = [Servicii suplimentare].ID_Comanda" +
                        " WHERE(( (Livrare.ID_Livrare) Like " + getID(AdminForm.getInstance().dataGridView1, getSelectedRow()) + ") " +
                                                            "AND ( (Comanda.ID_Comanda) Like " + getID(AdminForm.getInstance().dataGridView1, getSelectedRow()) + ") " +
                                                            "AND ( (Colectare.ID_Colectare) Like " + getID(AdminForm.getInstance().dataGridView1, getSelectedRow()) + ") " +
                                                            "AND ( ([Servicii suplimentare].ID_Comanda) Like " + getID(AdminForm.getInstance().dataGridView1, getSelectedRow()) + "));";

            try
            {
                OleDbCommand cmd1 = new OleDbCommand(q1, Database.con);
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            AdminForm.getInstance().refreshTable();
            tabControl1.SelectTab("tabPage3");

        }

        private void u_btnEdit_Click(object sender, EventArgs e)
        {
            UserEditForm editUserForm = new UserEditForm();
            editUserForm.Show();
            editUserForm.fillInfo();
            instance.Hide();
        }

        private void onCellClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            rowSelected = e.RowIndex;
            columnSelected = e.ColumnIndex;
        }

        private void _btnDelete_Click(object sender, EventArgs e)
        {
            string query = "DELETE Utilizatori.* FROM Utilizatori WHERE(( (Utilizatori.ID_Utilizatori) Like " + getID(AdminForm.getInstance().dataGridView3, getSelectedRow()) + "))";

            try
            {
                OleDbCommand cmd1 = new OleDbCommand(query, Database.con);
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            AdminForm.getInstance().refreshTable();
            tabControl1.SelectTab("tabPage1");
        }

        private void c_btnAdd_Click(object sender, EventArgs e)
        {
            addClientForm addClientform = new addClientForm();
            addClientform.Show();
            instance.Hide();
        }

        private void c_btnEdit_Click(object sender, EventArgs e)
        {
            editClientForm editClientForm = new editClientForm();
            editClientForm.Show();
            editClientForm.fillInfo();
            instance.Hide();
        }

        private void c_btnDelete_Click(object sender, EventArgs e)
        {


            string query = "DELETE Clienti.*, Comanda.*, [Servicii suplimentare].*, Colectare.*, Livrare.* " +
                          " FROM(Livrare INNER JOIN(Colectare INNER JOIN(Clienti INNER JOIN Comanda ON Clienti.ID_Client = Comanda.ID_Client) ON Colectare.ID_Colectare = Comanda.ID_Colectare) ON Livrare.ID_Livrare = Comanda.ID_Livrare) INNER JOIN[Servicii suplimentare] ON Comanda.ID_Comanda = [Servicii suplimentare].ID_Comanda" +
                          " WHERE(([Clienti].[ID_Client] Like" + getID(AdminForm.getInstance().dataGridView4, getSelectedRow()) + "));";

         
            try
            {
                OleDbCommand cmd1 = new OleDbCommand(query, Database.con);
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            AdminForm.getInstance().refreshTable();
            tabControl1.SelectTab("tabPage2");
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
       

        private void btnValidare_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Comanda SET Comanda.Validat = '" + Utils.convertBoolValidare(Utils.getStringValue(AdminForm.getInstance().dataGridView2[6, getSelectedRow()].Value.ToString())) + "' " +
                           " WHERE(( (Comanda.ID_Comanda) Like " + "\"" + AdminForm.getInstance().dataGridView2[2, getSelectedRow()].Value.ToString() + "\"" + "));";


            //" WHERE(( (Comanda.ID_Comanda) Like " + "\"" + Utils.getPrimaryKey("Comanda", "ID_Comanda", "ID_Client", dataGridView2[0, getSelectedRow()].Value.ToString()) + "\"" + "));";
            try
            {
                OleDbCommand cmd1 = new OleDbCommand(query, Database.con);
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            loadDoc();

        }

        

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            rowSelected = e.RowIndex;
        }

        private void printCmr_Click(object sender, EventArgs e)
        {
          



            CMR_Form cmr = new CMR_Form();
            if (cmr.IsDisposed == true)
                cmr = new CMR_Form();

            cmr.fillInfo();
            cmr.Show();

        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
          
        }

       

       

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {

            //if (e.TabPage.Name.Equals("tabPage1"))
            //{
            //    refreshFormSize(dataGridView3);
            //}

            //if (e.TabPage.Name.Equals("tabPage2"))
            //{
            //    refreshFormSize(dataGridView4);
            //}
            //if (e.TabPage.Name.Equals("tabPage3"))
            //{
            //    refreshFormSize(dataGridView1);
            //}

            //if (e.TabPage.Name.Equals("tabPage4"))
            //{
            //    refreshFormSize(dataGridView2);
            //}
        }

        private void printEticheta_Click(object sender, EventArgs e)
        {
            Eticheta_Form eticheta = new Eticheta_Form();
            if (eticheta.IsDisposed == true)
                eticheta = new Eticheta_Form();

            eticheta.Show();
            eticheta.fillInfo();
        }

        private void printBorderou_Click(object sender, EventArgs e)
        {
            Borderou borderou = new Borderou();
            if (borderou.IsDisposed == true)
                borderou = new Borderou();
            borderou.Show();
            borderou.fillInfo();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            new LoginForm().Show();
        }

        private void dataGridView4_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            rowSelected = e.RowIndex;
        }



     public void loadReport()
        {
            string report = queryReport();
                try
                {
                    OleDbCommand command = new OleDbCommand(report, Database.con);
                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    DataTable table = new DataTable();
                    da.Fill(table);
                    dataGridView5.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void btnAfisare_Click(object sender, EventArgs e)
        {
            dataGridView5.Visible = true;
            loadReport();
        }


        public string queryReport()
        {
           
            string report = "";

            //cbClienti cbJudetExp cbJudetDest dateTimeFrom dateTimeTill

            if (cbClienti.Text.Equals(String.Empty) && cbJudetExp.Text.Equals(String.Empty) && cbJudetDest.Text.Equals(String.Empty) && dateTimeFrom.Value == DateTime.Today && dateTimeTill.Value == DateTime.Today)
            {
                

                //  CLIENTI GENERAL
                report = "SELECT  Clienti.[Nume Client], Count(Comanda.ID_Comanda) AS [Număr Comenzi], Sum(Comanda.[Numar paleti]) AS[Număr paleti], Sum(Comanda.Greutate) AS [Total Greutate], Sum(Comanda.Tarif) AS [Tarif total], Sum([Servicii suplimentare].[Valoare marfa]) AS[Total valoare asigurată]" +
                         " FROM(Clienti INNER JOIN Comanda ON Clienti.ID_Client = Comanda.ID_Client) INNER JOIN[Servicii suplimentare] ON Comanda.ID_Comanda = [Servicii suplimentare].ID_Comanda" +
                         " GROUP BY Clienti.[Nume Client];";

            }
            else if (cbClienti.Text!=null && cbJudetExp.Text.Equals(String.Empty) && cbJudetDest.Text.Equals(String.Empty) && dateTimeFrom.Value == DateTime.Today && dateTimeTill.Value == DateTime.Today)
            {
                //   PER CLIENT
                report = "SELECT  Clienti.[Nume Client], Count(Comanda.ID_Comanda) AS [Număr Comenzi], Sum(Comanda.[Numar paleti]) AS[Număr paleti], Sum(Comanda.Greutate) AS [Total Greutate], Sum(Comanda.Tarif) AS [Tarif total], Sum([Servicii suplimentare].[Valoare marfa]) AS[Total valoare asigurată]" +
                         " FROM(Clienti INNER JOIN Comanda ON Clienti.ID_Client = Comanda.ID_Client) INNER JOIN[Servicii suplimentare] ON Comanda.ID_Comanda = [Servicii suplimentare].ID_Comanda" +
                         " GROUP BY Clienti.[Nume Client]" +
                         " HAVING(((Clienti.[Nume Client])Like '" + cbClienti.Text + "')) ";
            }
            else if (cbJudetExp.Text != null && cbJudetDest.Text != null && cbClienti.Text.Equals(String.Empty) && dateTimeFrom.Value == DateTime.Today && dateTimeTill.Value == DateTime.Today)
            {
                //  PER JUDETE
                report = "SELECT Clienti.[Nume Client], Count(Comanda.ID_Comanda) AS [Număr Comenzi], Colectare.[Judet expeditor], Livrare.[Judet destinatar], Sum(Comanda.[Numar paleti]) AS[Număr paleti], Sum(Comanda.Greutate) AS [Total Greutate], Sum(Comanda.Tarif) AS [Tarif total], Sum([Servicii suplimentare].[Valoare marfa]) AS[Total valoare asigurată]" +
                         " FROM(Livrare INNER JOIN(Colectare INNER JOIN(Clienti INNER JOIN Comanda ON Clienti.ID_Client = Comanda.ID_Client) ON Colectare.ID_Colectare = Comanda.ID_Colectare) ON Livrare.ID_Livrare = Comanda.ID_Livrare) INNER JOIN[Servicii suplimentare] ON Comanda.ID_Comanda = [Servicii suplimentare].ID_Comanda" +
                         " GROUP BY Clienti.[Nume Client], Colectare.[Judet expeditor], Livrare.[Judet destinatar]" +
                         " HAVING(((Colectare.[Judet expeditor]) Like'" + cbJudetExp.Text + "') AND (( Livrare.[Judet destinatar]) Like'" + cbJudetDest.Text + "'));";
            }
            else if (cbJudetExp.Text!=null && cbJudetDest.Text!=null && cbClienti.Text != null   && dateTimeFrom.Value == DateTime.Today && dateTimeTill.Value == DateTime.Today)
            {

                //  PER CLIENT PLUS JUDETE
                report = "SELECT Clienti.[Nume Client], Count(Comanda.ID_Comanda) AS [Număr Comenzi], Colectare.[Judet expeditor], Livrare.[Judet destinatar], Sum(Comanda.[Numar paleti]) AS[Număr paleti], Sum(Comanda.Greutate) AS [Total Greutate], Sum(Comanda.Tarif) AS [Tarif total], Sum([Servicii suplimentare].[Valoare marfa]) AS[Total valoare asigurată]" +
                         " FROM(Livrare INNER JOIN(Colectare INNER JOIN(Clienti INNER JOIN Comanda ON Clienti.ID_Client = Comanda.ID_Client) ON Colectare.ID_Colectare = Comanda.ID_Colectare) ON Livrare.ID_Livrare = Comanda.ID_Livrare) INNER JOIN[Servicii suplimentare] ON Comanda.ID_Comanda = [Servicii suplimentare].ID_Comanda" +
                         " GROUP BY Clienti.[Nume Client], Colectare.[Judet expeditor], Livrare.[Judet destinatar]" +
                         " HAVING(((Clienti.[Nume Client]) Like '" + cbClienti.Text + "') AND ((Colectare.[Judet expeditor]) Like'" + cbJudetExp.Text + "') AND (( Livrare.[Judet destinatar]) Like'" + cbJudetDest.Text + "'));";
            }
            else if (cbClienti.Text.Equals(String.Empty) && cbJudetExp.Text.Equals(String.Empty) && cbJudetDest.Text.Equals(String.Empty) && timeValueFrom==true && timeValueTill == true)
            {
                //   INTRE DATE
                report = "SELECT Clienti.[Nume Client], Count(Comanda.ID_Comanda) AS [Număr Comenzi], Sum(Comanda.[Numar paleti]) AS [Număr paleți], Sum(Comanda.Greutate) AS [Total Greutate], Sum(Comanda.Tarif) AS [Tarif total], Sum([Servicii suplimentare].[Valoare marfa]) AS [Total valoare asigurată]" +
                         " FROM Livrare INNER JOIN((Colectare INNER JOIN (Clienti INNER JOIN Comanda ON Clienti.ID_Client = Comanda.ID_Client) ON Colectare.ID_Colectare = Comanda.ID_Colectare) INNER JOIN[Servicii suplimentare] ON Comanda.ID_Comanda = [Servicii suplimentare].ID_Comanda) ON Livrare.ID_Livrare = Comanda.ID_Livrare" +
                         " WHERE(((Comanda.Data)Between #" + dateTimeFrom.Value + "# And #" + dateTimeTill.Value + "#))" +
                         " GROUP BY Clienti.[Nume Client];";
            }
            else if (cbClienti.Text!=null && dateTimeFrom.Value!= DateTime.Today && dateTimeTill.Value != DateTime.Today && cbJudetExp.Text.Equals(String.Empty) && cbJudetDest.Text.Equals(String.Empty))
            {
                //  INTRE DATE PER CLIENT
                report = "SELECT Count(Comanda.ID_Comanda) AS [Număr Comenzi], Sum(Comanda.[Numar paleti]) AS[Număr paleți], Sum(Comanda.Greutate) AS [Total Greutate], Sum(Comanda.Tarif) AS [Tarif total], Sum([Servicii suplimentare].[Valoare marfa]) AS[Total valoare asigurată]" +
                          " FROM Livrare INNER JOIN((Colectare INNER JOIN (Clienti INNER JOIN Comanda ON Clienti.ID_Client = Comanda.ID_Client) ON Colectare.ID_Colectare = Comanda.ID_Colectare) INNER JOIN[Servicii suplimentare] ON Comanda.ID_Comanda = [Servicii suplimentare].ID_Comanda) ON Livrare.ID_Livrare = Comanda.ID_Livrare" +
                          " WHERE(((Clienti.[Nume Client]) Like '" + cbClienti.Text + "') AND((Comanda.Data) Between #" + dateTimeFrom.Value + "# And #" + dateTimeTill.Value + "#));";
            }
            else if(cbClienti.Text != null && dateTimeFrom.Value != DateTime.Today && dateTimeTill.Value != DateTime.Today && cbJudetExp.Text!=null && cbJudetDest.Text != null)
            {
                // INTRE DATE PER CLIENT PLUS JUDET
                report = "SELECT Count(Comanda.ID_Comanda) AS [Număr Comenzi], Sum(Comanda.[Numar paleti]) AS[Număr paleți], Sum(Comanda.Greutate) AS [Total Greutate], Sum(Comanda.Tarif) AS [Tarif total], Sum([Servicii suplimentare].[Valoare marfa]) AS[Total valoare asigurată], Colectare.[Judet expeditor], Livrare.[Judet destinatar]" +
                         " FROM Livrare INNER JOIN((Colectare INNER JOIN (Clienti INNER JOIN Comanda ON Clienti.ID_Client = Comanda.ID_Client) ON Colectare.ID_Colectare = Comanda.ID_Colectare) INNER JOIN[Servicii suplimentare] ON Comanda.ID_Comanda = [Servicii suplimentare].ID_Comanda) ON Livrare.ID_Livrare = Comanda.ID_Livrare" +
                         " WHERE(((Clienti.[Nume Client]) Like '" + cbClienti.Text + "') AND((Comanda.Data) Between #" + dateTimeFrom.Value + "# And #" + dateTimeTill.Value + "#))" +
                         " GROUP BY Colectare.[Judet expeditor], Livrare.[Judet destinatar];";
            }
            return report;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {

            copyAlltoClipboard();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
        }

        private void copyAlltoClipboard()
        {
            dataGridView5.SelectAll();
            DataObject dataObj = dataGridView5.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void dateTimeFrom_ValueChanged(object sender, EventArgs e)
        {
              timeValueFrom = true;
    }

        private void dateTimeTill_ValueChanged(object sender, EventArgs e)
        {
            timeValueTill = true;
        }
    }
}
