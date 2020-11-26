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
using PapaSenpai_Project_Software.Data;
using PapaSenpai_Project_Software.Logic;

namespace PapaSenpai_Project_Software
{
    public partial class Login : MaterialSkin.Controls.MaterialForm
    {
        private Logic.AdminControl a;
        private AdminDAL adminDAL;
        public Login()
        {
            a = new Logic.AdminControl();
            InitializeComponent();
            adminDAL = new AdminDAL();
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

            MySqlDataReader user = adminDAL.Login(bindings);

            user.Read();
            //check if user exist if yes show the home page else don't show error message
            if (user.HasRows)
            {
                Admin admin = new Admin(Convert.ToInt32(user["id"]),user["username"].ToString(),
                    user["role_title"].ToString(), user["first_name"].ToString(), user["last_name"].ToString(),
                    user["email"].ToString(), user["password"].ToString());
                a.logUser(admin);

                this.Hide();
                Home h = new Home(this.a);
                h.Show();
            }
            else
            {
                MessageBox.Show("User credetentials are wrong!");
            }

            adminDAL.CloseConnection(user);
        }
    }
}
