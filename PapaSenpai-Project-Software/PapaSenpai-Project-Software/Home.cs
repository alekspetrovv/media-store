﻿using MySql.Data.MySqlClient;
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

        private DateTime currentScheduleDate;
        public Home()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            this.pnlDashBoard.BringToFront();
            this.currentScheduleDate = DateTime.Now;
            this.lblMenu.Text = StoreControl.getloggedUser().getFullName();

            Employee.retrieveAllEmployees();
            Schedule.retrieveSchedules();
            Admin.retrieveAllAdmins();

            renderStaffTable();
            renderAdminTable();
            renderScheduleMembers();
            renderDailySchedule();

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

        private void showPanel(Panel panel)
        {
            this.pnlDashBoard.Visible = false;
            this.pnlEmployee.Visible = false;
            this.pnlAddStaff.Visible = false;
            this.pnlAddSchedule.Visible = false;
            this.pnlAdmin.Visible = false;
            this.pnlAddAdmin.Visible = false;
            panel.Visible = true;

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            this.showPanel(this.pnlDashBoard);
        }

        private void btnViewStaff_Click(object sender, EventArgs e)
        {
            this.showPanel(pnlEmployee);
        }

        private void btnAddSchedule_Click(object sender, EventArgs e)
        {
            this.showPanel(pnlAddSchedule);
        }

        private void btnViewSchedule_Click_1(object sender, EventArgs e)
        {
            this.showPanel(pnlAddSchedule);
        }

        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            this.showPanel(pnlAddStaff);
        }

        private void btnViewAdmins_Click(object sender, EventArgs e)
        {
            this.showPanel(pnlAdmin);
        }

        private void btnAddAdmins_Click(object sender, EventArgs e)
        {
            this.showPanel(pnlAddAdmin);
        }

        private void btnDeleteAdmins_Click(object sender, EventArgs e)
        {
            DeleteAdmin();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            AddEmployee();
        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            DeleteEmployee();
        }

        private void btnAddAdmin_Click(object sender, EventArgs e)
        {
            AddAdmin();
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

            Schedule schedule = StoreControl.getScheduleByDate(this.currentScheduleDate);
            calendarSchedule.AnnuallyBoldedDates = null;
            List<DateTime> coloredDates = new List<DateTime>();

            foreach (Schedule found_schedule in StoreControl.getSchedules())
            {
                coloredDates.Add(found_schedule.Date);
            }

            calendarSchedule.AnnuallyBoldedDates = coloredDates.ToArray();

            DataTable dtEmp = new DataTable();

            dtEmp.Columns.Add("Selected", typeof(bool));
            dtEmp.Columns.Add("ID", typeof(string));
            dtEmp.Columns.Add("Name", typeof(string));
            dtEmp.Columns.Add("From", typeof(string));
            dtEmp.Columns.Add("To", typeof(string));
            dtEmp.Columns.Add("Department", typeof(string));

            foreach (Employee employee in StoreControl.getUsers())
            {
                ScheduleMember foundMember = null;
                if (schedule != null)
                {
                    foreach (ScheduleMember member in schedule.Members)
                    {
                        if (member.Employee.ID == employee.ID)
                        {
                            foundMember = member;
                        }
                    }
                }
                if (foundMember != null)
                {
                    dtEmp.Rows.Add(true, employee.ID, employee.getFullName(), foundMember.StartTime.ToString("HH:mm"), foundMember.EndTime.ToString("HH:mm"), employee.Department);
                }
                else
                {
                    dtEmp.Rows.Add(false, employee.ID, employee.getFullName(), "9:00", "17:00", employee.Department);
                }
            }

            dtAddSchedule.DataSource = dtEmp;
        }

        private void renderDailySchedule()
        {

            Schedule schedule = StoreControl.getScheduleByDate(DateTime.Now);

            DataTable dtEmp = new DataTable();

            dtEmp.Columns.Add("ID", typeof(string));
            dtEmp.Columns.Add("Name", typeof(string));
            dtEmp.Columns.Add("From", typeof(string));
            dtEmp.Columns.Add("To", typeof(string));
            dtEmp.Columns.Add("Department", typeof(string));

            foreach (Employee employee in StoreControl.getUsers())
            {
                ScheduleMember foundMember = null;
                if (schedule != null)
                {
                    foreach (ScheduleMember member in schedule.Members)
                    {
                        if (member.Employee.ID == employee.ID)
                        {
                            foundMember = member;
                        }
                    }
                }
                if (foundMember != null)
                {
                    dtEmp.Rows.Add(employee.ID, employee.getFullName(), foundMember.StartTime.ToString("HH:mm"), foundMember.EndTime.ToString("HH:mm"), employee.Department);
                }
                else
                {
                    dtEmp.Rows.Add(employee.ID, employee.getFullName(), "9:00", "17:00", employee.Department);
                }
            }

            dtTodaySchedule.DataSource = dtEmp;
        }





        private void DeleteAdmin()
        {
            for (int i = 0; i < dtAdmins.Rows.Count; ++i)
            {


                DataGridViewRow dataRow = dtAdmins.Rows[i];

                if (dataRow.IsNewRow)
                {
                    continue;
                }

                bool deleteAdmin = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());
                if (deleteAdmin)
                {
                    string id = (dataRow.Cells["ID"].Value.ToString());
                    MySqlDataReader delete_admin = DBcon.executeReader("DELETE FROM `admins` WHERE `id` =" + id);
                    Admin.retrieveAllAdmins();
                    this.renderAdminTable();
                    DBcon.CloseConnection(delete_admin);
                }
            }

        }
        private void DeleteEmployee()
        {
            for (int i = 0; i < dtEmployees.Rows.Count; ++i)
            {


                DataGridViewRow dataRow = dtEmployees.Rows[i];

                if (dataRow.IsNewRow)
                {
                    continue;
                }

                bool deleteUser = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());
                if (deleteUser)
                {
                    string id = (dataRow.Cells["ID"].Value.ToString());
                    MySqlDataReader delete_users = DBcon.executeReader("DELETE FROM `employees` WHERE `id` =" + id);
                    Employee.retrieveAllEmployees();
                    this.renderStaffTable();
                    DBcon.CloseConnection(delete_users);
                }
            }

        }

        private void AddAdmin()
        {
            if (StoreControl.GetCreatedAdmin(this.tbAdminUserName.Text) == null)
            {
                int role_id = this.cbAdminRole.SelectedIndex;
                role_id++;
                string increased_role_id = Convert.ToString(role_id);
                string[] admin_bindings = { this.tbAdminUserName.Text, this.tbAdminPassword.Text, this.tbAdminFirstName.Text, this.tbAdminLastName.Text, this.tbAdminEmail.Text,
                    increased_role_id };
                MySqlDataReader add_admin = DBcon.executeReader("INSERT INTO `admins`(`username`, `password`, `first_name`, `last_name`, `email`, `role_id`)" +
                    "VALUES(@username,@password,@first_name,@last_name,@email,@role_id)", admin_bindings);
                DBcon.CloseConnection(add_admin);
                Admin.retrieveAllAdmins();
                this.renderAdminTable();

            }
            else
            {
                MessageBox.Show("You can't add an admin with the same username");
            }
        }
        private void AddEmployee()
        {
            if (StoreControl.GetCreatedUsers(this.tbEmployeeEmail.Text) == null)
            {
                int department_id = this.cbEmployeeDepartment.SelectedIndex;
                department_id++;
                string increased_department_id = Convert.ToString(department_id);
                string gender = (string)this.cbEmployeeGender.SelectedItem;
                string[] employee_bindings = { this.tbEmployeeFirstName.Text, this.tbEmployeeLastName.Text, this.tbEmployeeAdress.Text,
                this.tbEmployeeCity.Text, this.tbEmployeeCountry.Text, this.tbEmployeePhoneNumber.Text,
                gender, this.tbEmployeeEmail.Text,increased_department_id, this.tbEmployeeWagePerHour.Text};
                MySqlDataReader add_employee = DBcon.executeReader("INSERT INTO `employees`(`first_name`, `last_name`, `address`, `city`, `country`,`phone_number`, `gender`, `email`,`department_id`, `wage_per_hour`) " +
                "VALUES(@first_name,@last_name,@address,@city,@country,@phone_number,@gender,@email,@department_id, @wage)", employee_bindings);
                DBcon.CloseConnection(add_employee);
                Employee.retrieveAllEmployees();
                this.renderStaffTable();
            }
            else
            {
                MessageBox.Show("You can't add an employee with the same email");
            }
        }

        private void btnEditEmployee_Click(object sender, EventArgs e)
        {
            this.pnlEmployee.Visible = true;
        }

        private void calendarSchedule_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.currentScheduleDate = e.End;
            this.renderScheduleMembers();
        }

        private void btnUpdateSchedule_Click(object sender, EventArgs e)
        {
            updateSchedule();
        }

        private void updateSchedule()
        {
            //get if there is a schedule about this date
            //if yes remove all of the database members and add new ones

            //add the new ones
            //if not create schedule in db with the employees in related table

            Schedule schedule = StoreControl.getScheduleByDate(this.currentScheduleDate);

            int id;
            if (schedule == null)
            {
                string[] bindings = { "", this.currentScheduleDate.ToString("MM-dd-yyyy") };
                id = Convert.ToInt32(DBcon.executeScalar("INSERT INTO `schedules`(`notes`, `date`) VALUES (@notes,@date); SELECT LAST_INSERT_ID()", bindings));
            }
            else
            {
                id = schedule.ID;
            }

            //delete all of the attached users for new/pervious schedules
            string[] delete_bindings = { id.ToString() };
            DBcon.executeNonQuery("DELETE FROM `schedules_employees` WHERE schedule_id = @id", delete_bindings);

            int user_count = 0;

            for (int i = 0; i < dtAddSchedule.Rows.Count; ++i)
            {

                DataGridViewRow dataRow = dtAddSchedule.Rows[i];

                if (dataRow.IsNewRow)
                {
                    continue;
                }

                bool userChecked = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());
                string from = this.currentScheduleDate.ToString("MM-dd-yyyy") + " " + dataRow.Cells["From"].Value.ToString();
                string to = this.currentScheduleDate.ToString("MM-dd-yyyy") + " " + dataRow.Cells["To"].Value.ToString();

                if (userChecked)
                {
                    string member_id = (dataRow.Cells["ID"].Value.ToString());
                    string[] member_data = { id.ToString(), member_id.ToString(), from, to };
                    DBcon.executeNonQuery("INSERT INTO `schedules_employees`(`schedule_id`, `employee_id`, `from_hour`, `to_hour`)" +
                        " VALUES (@schedule_id, @member_id, @from_hour, @to_hour)", member_data);
                    user_count++;
                }
            }

            if (user_count == 0)
            {
                DBcon.executeNonQuery("DELETE FROM `schedules` WHERE id = @id", delete_bindings);

            }

            Schedule.retrieveSchedules();
            this.renderScheduleMembers();

            calendarSchedule.SelectionRange.End = this.currentScheduleDate;

        }
    }
}
