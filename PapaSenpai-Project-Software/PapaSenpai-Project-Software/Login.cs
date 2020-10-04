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
        public Login()
        {
            InitializeComponent();
        }


        private void Login_Load(object sender, EventArgs e)
        {
            ChangeLoginStyle();
        }


        private void ChangeLoginStyle()
        {
            // using MaterialSkin for the project design
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
            userLogin();
        }


        private void userLogin()
        {

            string[] bindings = { this.tbUserName.Text, this.tbUserPassword.Text };

            MySqlDataReader user = DBcon.executeReader("SELECT admins.*, roles.title as role_title FROM `admins` " +
                "INNER JOIN roles ON roles.id = admins.role_id " +
                "WHERE `username` = @usn AND password = @pass", bindings
                );

            user.Read();

            //check if user exist if yes show the home page else don't show error message
            if (user.HasRows)
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

            DBcon.CloseConnection(user);
        }
    }
}
