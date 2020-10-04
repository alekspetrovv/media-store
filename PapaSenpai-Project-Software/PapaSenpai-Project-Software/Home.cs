using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PapaSenpai_Project_Software
{
    public partial class Home : MaterialSkin.Controls.MaterialForm
    {
        public Home()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            this.pnlDashBoard.BringToFront();
            this.lblMenu.Text = StoreControl.getloggedUser().getFullName();

            retrieveAllEmployees();
            retrieveAllAdmins();

            renderStaffTable();
            renderStaffTable();
            renderAdminTable();

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

        private void Home_Load(object sender, EventArgs e)
        {
            ChangeLoginStyle();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            this.pnlDashBoard.Visible = true;
            this.pnlEmployee.Visible = false;
            this.pnlAddStaff.Visible = false;
            this.pnlAddSchedule.Visible = false;
            this.pnlAdmin.Visible = false;
            this.pnlAddAdmin.Visible = false;
        }

        private void btnViewStaff_Click(object sender, EventArgs e)
        {
            this.pnlDashBoard.Visible = false;
            this.pnlEmployee.Visible = true;
            this.pnlAddStaff.Visible = false;
            this.pnlAddSchedule.Visible = false;
            this.pnlAdmin.Visible = false;
            this.pnlAddAdmin.Visible = false;
        }

        private void btnAddSchedule_Click(object sender, EventArgs e)
        {
            this.pnlDashBoard.Visible = false;
            this.pnlEmployee.Visible = false;
            this.pnlAddStaff.Visible = false;
            this.pnlAddSchedule.Visible = true;
            this.pnlAdmin.Visible = false;
            this.pnlAddAdmin.Visible = false;
        }

        private void btnViewSchedule_Click_1(object sender, EventArgs e)
        {
            this.pnlDashBoard.Visible = false;
            this.pnlEmployee.Visible = false;
            this.pnlAddSchedule.Visible = true;
            this.pnlAddStaff.Visible = false;
            this.pnlAdmin.Visible = false;
            this.pnlAddAdmin.Visible = false;
        }

        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            this.pnlDashBoard.Visible = false;
            this.pnlEmployee.Visible = false;
            this.pnlAddStaff.Visible = true;
            this.pnlAddSchedule.Visible = false;
            this.pnlAdmin.Visible = false;
            this.pnlAddAdmin.Visible = false;
        }

        private void btnViewAdmins_Click(object sender, EventArgs e)
        {
            this.pnlDashBoard.Visible = false;
            this.pnlEmployee.Visible = false;
            this.pnlAddStaff.Visible = false;
            this.pnlAddSchedule.Visible = false;
            this.pnlAdmin.Visible = true;
            this.pnlAddAdmin.Visible = false;
        }

        private void btnAddAdmins_Click(object sender, EventArgs e)
        {
            this.pnlDashBoard.Visible = false;
            this.pnlEmployee.Visible = false;
            this.pnlAddStaff.Visible = false;
            this.pnlAddSchedule.Visible = false;
            this.pnlAdmin.Visible = false;
            this.pnlAddAdmin.Visible = true;
        }


        private void retrieveAllEmployees()
        {
            MySqlDataReader employees = DBcon.executeReader("SELECT employees.*, departments.title as department FROM `employees` " +
                "INNER JOIN employees_departments ON employees_departments.employee_id = employees.id " +
                "INNER JOIN departments ON departments.id = employees_departments.department_id GROUP by employees.id");

            if (employees.HasRows)
            {
                StoreControl.emptyUsers();
                while (employees.Read())
                {
                    Employee employee = new Employee(Convert.ToInt32(employees["id"]), employees["first_name"].ToString(),
                        employees["last_name"].ToString(), employees["email"].ToString()
                        , employees["address"].ToString(), employees["city"].ToString(), employees["country"].ToString(),
                        employees["phone_number"].ToString(), employees["gender"].ToString(), employees["department"].ToString());

                    StoreControl.addEmployee(employee);
                }
            }
        }

        private void retrieveAllAdmins()
        {
            MySqlDataReader admins = DBcon.executeReader("SELECT admins.*, roles.title as role_title FROM `admins` " +
                "INNER JOIN roles ON roles.id = admins.role_id GROUP by admins.id");

            if (admins.HasRows)
            {
                StoreControl.emptyAdmins();
                while (admins.Read())
                {
                    Admin admin = new Admin(Convert.ToInt32(admins["id"]), admins["username"].ToString(),
                        admins["role_title"].ToString(), admins["first_name"].ToString()
                        , admins["last_name"].ToString(), admins["email"].ToString());

                    StoreControl.addAdmin(admin);
                }
            }
        }

        private void renderAdminTable()
        {
            DataTable dtEmp = new DataTable();
            // add column to datatable  
            dtEmp.Columns.Add("Selected", typeof(bool));
            dtEmp.Columns.Add("ID", typeof(int));
            dtEmp.Columns.Add("Username", typeof(string));
            dtEmp.Columns.Add("First Name", typeof(string));
            dtEmp.Columns.Add("Last Name", typeof(string));
            dtEmp.Columns.Add("Role", typeof(string));
            dtEmp.Columns.Add("Email", typeof(string));

            foreach (Admin admin in StoreControl.getAdmins())
            {
                dtEmp.Rows.Add(false, admin.ID, admin.Username, admin.FirstName, admin.LastName, admin.Role, admin.Email);
            }

            dtAdmins.DataSource = dtEmp;

        }


        private void renderStaffTable()
        {
            DataTable dtEmp = new DataTable();
            dtEmp.Columns.Add("Selected", typeof(bool));
            dtEmp.Columns.Add("ID", typeof(int));
            dtEmp.Columns.Add("First Name", typeof(string));
            dtEmp.Columns.Add("Last Name", typeof(string));
            dtEmp.Columns.Add("Gender", typeof(string));
            dtEmp.Columns.Add("Phone", typeof(string));
            dtEmp.Columns.Add("Email", typeof(string));
            //            dtEmp.Columns.Add("Wage (h)", typeof(double));
            dtEmp.Columns.Add("Deparment", typeof(string));

            foreach (Employee employee in StoreControl.getUsers())
            {
                dtEmp.Rows.Add(false, employee.ID, employee.FirstName, employee.LastName, employee.Gender, employee.PhoneNumber, employee.Email, employee.Department);
            }

            dtEmployees.DataSource = dtEmp;

        }

        private void renderScheduleMembers()
        {
            //get all members
            //get members from that schedule
            //foreach all members and fill them in the datable
            //if found in members of schedule make selected true and and fill the hours worked
        }


        private void btnAddUser_Click(object sender, EventArgs e)
        {
            string gender = (string)this.cbEmployeeGender.SelectedItem;
            string[] employee_bindings = { this.tbEmployeeFirstName.Text, this.tbEmployeeLastName.Text, this.tbEmployeeAdress.Text,
                this.tbEmployeeCity.Text, this.tbEmployeeCountry.Text, this.tbEmployeePhoneNumber.Text,
                gender, this.tbEmployeeEmail.Text};
            MySqlDataReader add_employee = DBcon.executeReader("INSERT INTO `employees`(`first_name`, `last_name`, `address`, `city`, `country`,`phone_number`, `gender`, `email`) " +
            "VALUES(@first_name,@last_name,@address,@city,@country,@phone_number,@gender,@email)", employee_bindings);

            DBcon.CloseConnection(add_employee);
        }

        private void btnAddAdmin_Click(object sender, EventArgs e)
        {
            int role_id = this.cbAdminRole.SelectedIndex;
            role_id++;
            string increase_role_id = Convert.ToString(role_id);
            Console.WriteLine(increase_role_id);
            string[] admin_bindings = { this.tbAdminUserName.Text, this.tbAdminPassword.Text, this.tbAdminFirstName.Text, this.tbAdminLastName.Text, this.tbAdminEmail.Text, increase_role_id };
            MySqlDataReader add_admin = DBcon.executeReader("INSERT INTO `admins`(`username`, `password`, `first_name`, `last_name`, `email`, `role_id`)" + "VALUES(@username,@password,@first_name,@last_name,@email,@role_id)", admin_bindings);


            DBcon.CloseConnection(add_admin);
        }

        private void shit()
        {
            for (int i = 0; i < dtAdmins.Rows.Count; ++i)
            {


                DataGridViewRow dataRow = dtAdmins.Rows[i];

                if (dataRow.IsNewRow)
                {
                    continue;
                }

                string dr = dataRow.Cells["ID"].Value.ToString();
                Console.WriteLine(dr);
            }

        }

        private void btnDeleteAdmins_Click(object sender, EventArgs e)
        {


        }
    }
}
