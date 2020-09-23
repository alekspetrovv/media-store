using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PapaSenpai_Project_Software
{
    public partial class Login : MaterialSkin.Controls.MaterialForm
    {
        MySqlConnection con;
        public Login()
        {
            InitializeComponent();
            con = new MySqlConnection(@"server=localhost;user id=root;password = 123456;database=papasenpai");
        }
        private void Login_Load(object sender, EventArgs e)
        {
            ChangeLoginStyle();
        }
        private void ChangeLoginStyle()
        {
            MaterialSkin.MaterialSkinManager skinManager = MaterialSkin.MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.DARK;
            skinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Green300,
                MaterialSkin.Primary.Green300,
                MaterialSkin.Primary.Blue500,
                MaterialSkin.Accent.Orange700,
                MaterialSkin.TextShade.WHITE);
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // check if user exist if yes show the home page else don't show error message
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM `users` WHERE `name` = @usn AND password = @pass";
            cmd.Parameters.Add("@usn", MySqlDbType.VarChar).Value = this.tbUserName.Text;
            cmd.Parameters.Add("@pass", MySqlDbType.VarChar).Value = this.tbUserPassword.Text;
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("User logged in successfully!");
                this.Hide();
                Home h = new Home();
                h.Show();
            }
            else
            {
                MessageBox.Show("User credetentials are wrong!");
            }
            con.Close();
        }
    }
}
