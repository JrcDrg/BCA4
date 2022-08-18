using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace PracticeApp
{
    public partial class LogInForm : Form
    {
        MySqlConnection con = new MySqlConnection();
        public LogInForm()
        {
            InitializeComponent();
        }
        public void ConnectionDB()
        {
            string server = "localhost";
            string database = "supermarket";
            string username = "root";
            string password = "";
            string connectstring = "SERVER=" + server + ";" + "DATABASE =" + database + ";"
                                + "UID =" + username + ";" + "PASSWORD =" + password + ";";
            con = new MySqlConnection(connectstring);
            con.Open();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtBxPW_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            String uname = txtBxUName.Text;
            String pword = txtBxPW.Text;
            

            if (uname == "" || pword == "")
                {
                
                    if (uname == "" && pword == "") 
                    {
                    LblUNPWMsg.Text = "Please fill both username and password";
                    LblUNPWMsg.Visible = true;
                    txtBxUName.Focus();
                    }
                    else if (uname == "")
                     {
                    LblUNPWMsg.Text = "Please fill Username";
                    LblUNPWMsg.Visible = true;
                    txtBxUName.Focus();
                    }
                    else
                    {
                    LblUNPWMsg.Text = "Please fill Password";
                    LblUNPWMsg.Visible = true;
                    txtBxPW.Focus();
                    }
            }
            else
            {
                try
                {
                    //con.Open();
                    ConnectionDB();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sorry Could not connect to the database, Please Connect to The Database then try Again" + ex.Message);
                    Application.Restart();
                }

                String query = $"SELECT * from login WHERE UName='{uname}' AND Password = '{pword}';";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
               

                if (reader.Read())
                {
                    String varuname=reader.GetString(1);
                    String varpword=reader.GetString(2);
                    String varactive=reader.GetString(3);

                    //int varcolumncount = reader.FieldCount;  //to count number of columns in a table
                    reader.Close();
                    String rowquery= $"SELECT COUNT(*) FROM LOGIN UName='{uname}' AND Password = '{pword}'; ";
                    cmd =new MySqlCommand(rowquery, con);
                    int varrowcount = Convert.ToInt32(cmd.ExecuteScalar());  // to count number of rows in a table
                    
                    MessageBox.Show(varuname + " " + varpword + "   " + varactive + "          " + varrowcount);

                    //open another form if user is active
                    if (varactive == "Y")
                    {
                        ProgressForm pf = new ProgressForm();
                        pf.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("User Exist But is currently Inactive.");
                        txtBxPW.Text = "";
                        txtBxPW.Text = "";
                        txtBxUName.Focus();
                        con.Close();
                        con.Dispose();
                    }

                }

                else
                {
                    MessageBox.Show("Invalid UserName and or Password. Please Enter Correct values!!!");
                    txtBxUName.Text = "";
                    txtBxPW.Text = "";
                    txtBxUName.Focus();
                    con.Close();
                }
            }
        }

        private void LogInForm_Load(object sender, EventArgs e)
        {
           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBxUName.Text = "";
            txtBxPW.Text = "";
            txtBxUName.Focus();
        }

        private void LogInForm_UnLoad(object sender, EventArgs e)
        {
            con.Close();
        }
    }
}