using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading;
using System.Transactions;
using MySql.Data.Common;
using MySql.Data.MySqlClient.Replication;
using MySql.Data.MySqlClient;


namespace PracticeMySql
{
    
    public partial class Form1 : Form
    {
        MySqlConnection conn = new MySqlConnection();
        public Form1()
        {
            
            InitializeComponent();

        }

        public void ConnectionDB()
        {
            string server = "localhost";
            string database = "supermarket";
            string username = "root";
            string password = "";
            string connectstring = "SERVER=" + server + ";" + "DATABASE =" + database + ";" +
                                "UID =" + username + ";" + "PASSWORD =" + password + ";";
            conn = new MySqlConnection(connectstring);
            try
            {
                conn.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Restart();
            }
            

        }

        public void gridviewLoad()
        {
            ConnectionDB();
            MySqlDataAdapter MyDA = new MySqlDataAdapter();
            string query = "SELECT * from login";
            MyDA.SelectCommand = new MySqlCommand(query, conn);

            DataTable table = new DataTable();
            MyDA.Fill(table);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = table;

            dataGridView1.DataSource = bSource;
            conn.Close();
        }

              
         private void button1_Click(object sender, EventArgs e)
        {
            ConnectionDB();
            string query = "INSERT INTO `login` (`UName`, `PWord`, `UActive`) VALUES ('" + textBoxUName.Text + "', '" + textBoxPW.Text + "', '" + textBoxActive.Text + "')";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            int value = cmd.ExecuteNonQuery();
            
            if(value==1)              
            {
                MessageBox.Show("Record Inserted into Database Successfully");

            }
            else
            {

                MessageBox.Show("Record cannot be Inserted into Database");
            }
            gridviewLoad();
            textBoxId.Text = "";
            textBoxUName.Text = "";
            textBoxPW.Text = "";
            textBoxActive.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gridviewLoad();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            textBoxId.Text = selectedRow.Cells[0].Value.ToString();
            textBoxUName.Text = selectedRow.Cells[1].Value.ToString();
            textBoxPW.Text = selectedRow.Cells[2].Value.ToString();
            textBoxActive.Text = selectedRow.Cells[3].Value.ToString();

            btnInsert.Enabled = false;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ConnectionDB();
            //UPDATE table_name
            //SET column1 = value1, column2 = value2, ...
            //WHERE condition;
            //string id = textBoxId.Text;
            string uname = textBoxUName.Text;
            string password = textBoxPW.Text;
            string active = textBoxActive.Text;

            string queryupdate = "update login set UName='" + uname + "', PWord='" + password + "',UActive='" + active + "' where Id='" + id + "';";

            MySqlCommand cmd = new MySqlCommand(queryupdate, conn);
            int value = cmd.ExecuteNonQuery();
            //MessageBox.Show(value.ToString());
            if (value == 1)
            {
                MessageBox.Show("Record Updated into Database Successfully");
            }
            else
            {

                MessageBox.Show("Record cannot be Updated into Database");
            }
            gridviewLoad();
            textBoxId.Text = "";
            textBoxUName.Text = "";
            textBoxPW.Text = "";
            textBoxActive.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ConnectionDB();

            string id = textBoxId.Text;

            string querydelete = "delete from login where UId='" + id + "';";

            MySqlCommand cmd = new MySqlCommand(querydelete, conn);
            int value = cmd.ExecuteNonQuery();
            //MessageBox.Show(value.ToString());
            if (value == 1)
            {
                MessageBox.Show("Record Deleted from Database Successfully");
            }
            else
            {

                MessageBox.Show("Record cannot be Deleted From Database");
            }
            gridviewLoad();
            textBoxId.Text = "";
            textBoxUName.Text = "";
            textBoxPW.Text = "";
            textBoxActive.Text = "";
        }
    }
}