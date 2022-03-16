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
    public partial class AdminEditForm : AdminAddForm
    {
        int row = -1;
        public AdminEditForm()
        {
            InitializeComponent();
        }

        private void AdminEditForm_Load(object sender, EventArgs e)
        {

        }
        private void ad_saveBtn_Click(object sender, EventArgs e)
        {
            savePartida();
            AdminForm.getInstance().refreshTable();
            goBack();
        }

        public void fillInfo()
        {
            row = AdminForm.getSelectedRow();

            if (row == -1) {
                row = 0;
            }

            if (row >= Int32.Parse(Utils.getLastID("ID_Livrare", "Livrare")))
            {
                MessageBox.Show("ai depasit");
            }

            c_NumeExpeditor.Text = AdminForm.getInstance().dataGridView1[2,row].Value.ToString();
            c_Judetexpeditor.Text = AdminForm.getInstance().dataGridView1[3, row].Value.ToString();
            c_OrasExpeditor.Text = AdminForm.getInstance().dataGridView1[4, row].Value.ToString();
            c_AdresaExpeditor.Text = AdminForm.getInstance().dataGridView1[5, row].Value.ToString();
            c_ContactExpeditor.Text = AdminForm.getInstance().dataGridView1[6, row].Value.ToString();
            c_telefonExpeditor.Text = AdminForm.getInstance().dataGridView1[7, row].Value.ToString();

            l_NumeDestinatar.Text = AdminForm.getInstance().dataGridView1[8,row].Value.ToString();
            l_JudetDestinatar.Text = AdminForm.getInstance().dataGridView1[9,row].Value.ToString();
            l_OrasDestinatar.Text = AdminForm.getInstance().dataGridView1[10,row].Value.ToString();
            l_AdresaDestinatar.Text = AdminForm.getInstance().dataGridView1[11,row].Value.ToString();
            l_ContactDestinatar.Text = AdminForm.getInstance().dataGridView1[12,row].Value.ToString();
            l_telefonDestinatar.Text = AdminForm.getInstance().dataGridView1[13,row].Value.ToString();

            m_nrPaleti.Text = AdminForm.getInstance().dataGridView1[14, row].Value.ToString();
            m_grPaleti.Text = AdminForm.getInstance().dataGridView1[15, row].Value.ToString();

            string queryServicii = "SELECT  [Servicii suplimentare].[Retur documente], "+
                                            "[Servicii suplimentare].[Retur paleti], " +
                                            "[Servicii suplimentare].[Infoliere / etichetare], " +
                                            "[Servicii suplimentare].[Depaletare], " +
                                            "[Servicii suplimentare].[Livrare în sediu], " +
                                            "[Servicii suplimentare].[Marfuri periculoase], " +
                                            "[Servicii suplimentare].[Cu asigurare], " +
                                            "[Servicii suplimentare].[Valoare marfa], " +
                                            "Comanda.Comentarii " +
                                            "FROM [Servicii suplimentare] INNER JOIN Comanda ON [Servicii suplimentare].[ID_Comanda] = Comanda.[ID_Comanda] " +
                                            "WHERE ((([Servicii suplimentare].ID_Serviciu) Like " + "\"" + AdminForm.getInstance().dataGridView1[0, row].Value.ToString() + "\"" + "));";
            
            try
            {

                OleDbCommand command = new OleDbCommand(queryServicii, Database.con);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    checkBox1.Checked = (bool)reader.GetValue(0);
                    checkBox2.Checked = (bool)reader.GetValue(1);
                    checkBox3.Checked = (bool)reader.GetValue(2);
                    checkBox4.Checked = (bool)reader.GetValue(3);
                    checkBox5.Checked = (bool)reader.GetValue(4);
                    checkBox6.Checked = (bool)reader.GetValue(5);
                    checkBox7.Checked = (bool)reader.GetValue(6);

                    s_valMarfa.Text = reader.GetValue(7).ToString();
                    com_comentarii.Text = reader.GetValue(8).ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public override void savePartida()
        {
            AdminForm af = new AdminForm();
            string q1 = "UPDATE Colectare SET Colectare.[Nume expeditor] = '" + Utils.getStringValue(c_NumeExpeditor.Text) + "'," +
                                              "Colectare.[Judet expeditor] = '" + Utils.getStringValue(c_Judetexpeditor.Text) + "'," +
                                              "Colectare.[Oras expeditor] = '" + Utils.getStringValue(c_OrasExpeditor.Text) + "'," +
                                              "Colectare.[Adresa expeditor] = '" + Utils.getStringValue(c_AdresaExpeditor.Text) + "'," +
                                              "Colectare.[Contact expeditor] = '" + Utils.getStringValue(c_ContactExpeditor.Text) + "'," +
                                              " Colectare.[Telefon expeditor] = '" + Utils.getStringValue(c_telefonExpeditor.Text) + "'," +
                                              "Colectare.[Data colectare] = '" + Utils.getStringValue(c_DataColectare.Value.ToString()) + "'," +
                                              "Colectare.[Ora deschidere locatie] = '" + Utils.getStringValue(c_OraDeschidere.Value.ToString()) + "'," +
                                              "Colectare.[Ora inchidere locatie] = '" + Utils.getStringValue(c_OraInchidere.Value.ToString()) + "'" +
                                              " WHERE(((Colectare.ID_Colectare) Like " + "\"" + AdminForm.getInstance().dataGridView1[0, row].Value.ToString() + "\"" + "));";

            string q2 = "UPDATE Livrare SET Livrare.[Nume destinatar] = '" + Utils.getStringValue(l_NumeDestinatar.Text) + "'," +
                                             "Livrare.[Judet destinatar] = '" + Utils.getStringValue(l_JudetDestinatar.Text) + "'," +
                                             "Livrare.[Oras destinatar] = '" + Utils.getStringValue(l_OrasDestinatar.Text) + "'," +
                                             "Livrare.[Adresa destinatar] = '" + Utils.getStringValue(l_AdresaDestinatar.Text) + "'," +
                                             "Livrare.[Contact destinatar] = '" + Utils.getStringValue(l_ContactDestinatar.Text) + "'," +
                                             " Livrare.[Telefon destinatar] = '" + Utils.getStringValue(l_telefonDestinatar.Text) + "'," +
                                             "Livrare.[Data livrare] = '" + Utils.getStringValue(l_DataLivrare.Value.ToString()) + "'," +
                                             "Livrare.[Ora deschidere locatie] = '" + Utils.getStringValue(l_OraDeschidere.Value.ToString()) + "'," +
                                             "Livrare.[Ora inchidere locatie] = '" + Utils.getStringValue(l_OraInchidere.Value.ToString()) + "'" +
                                             " WHERE(((Livrare.ID_Livrare) Like " + "\"" + AdminForm.getInstance().dataGridView1[0, row].Value.ToString() + "\"" + "));";

            string q3 = "UPDATE [Servicii suplimentare] SET  [Servicii suplimentare].[Retur documente] = '" + Utils.getBoolValue(checkBox1.Checked) + "'," +
                                                            "[Servicii suplimentare].[Retur paleti] = '" + Utils.getBoolValue(checkBox2.Checked) + "'," +
                                                            "[Servicii suplimentare].[Infoliere / etichetare] = '" + Utils.getBoolValue(checkBox3.Checked) + "'," +
                                                            "[Servicii suplimentare].[Depaletare] = '" + Utils.getBoolValue(checkBox4.Checked) + "'," +
                                                            "[Servicii suplimentare].[Livrare în sediu] = '" + Utils.getBoolValue(checkBox5.Checked) + "'," +
                                                            "[Servicii suplimentare].[Marfuri periculoase] = '" + Utils.getBoolValue(checkBox6.Checked) + "'," +
                                                            "[Servicii suplimentare].[Cu asigurare] = '" + Utils.getBoolValue(checkBox7.Checked) + "'," +
                                                            "[Servicii suplimentare].[Valoare marfa] = " + Utils.getIntValue(s_valMarfa.Text) +  "" +
                                                            " WHERE((([Servicii suplimentare].ID_Serviciu) Like " + "\"" + AdminForm.getInstance().dataGridView1[0, row].Value.ToString() + "\"" + "));";

            string q4 = "UPDATE Comanda SET Comanda.[Numar paleti] = " + Utils.getIntValue(m_nrPaleti.Text) + "," +
                                              "Comanda.Greutate = " + Utils.getIntValue(m_grPaleti.Text) + "," +
                                              "Comanda.Comentarii = '" + Utils.getStringValue(com_comentarii.Text) + "'" +
                                              " WHERE(( (Comanda.ID_Comanda) Like " + "\"" + AdminForm.getInstance().dataGridView1[0, row].Value.ToString() + "\"" + "));";


            try
            {
                OleDbCommand cmd1 = new OleDbCommand(q1, Database.con);
                cmd1.ExecuteNonQuery();

                OleDbCommand cmd2 = new OleDbCommand(q2, Database.con);
                cmd2.ExecuteNonQuery();

                OleDbCommand cmd3 = new OleDbCommand(q3, Database.con);
                cmd3.ExecuteNonQuery();

                OleDbCommand cmd4 = new OleDbCommand(q4, Database.con);
                cmd4.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
