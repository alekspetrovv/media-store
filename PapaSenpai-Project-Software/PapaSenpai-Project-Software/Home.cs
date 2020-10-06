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
            this.pnlAddEditAdmin.Visible = false;
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
            this.btnAddUser.Visible = true;
            this.btnUpdateEmployee.Visible = false;
        }


        private void btnViewAdmins_Click(object sender, EventArgs e)
        {
            this.showPanel(pnlAdmin);
            this.btnUpdateAdmin.Visible = false;
        }


        private void btnAddAdmins_Click(object sender, EventArgs e)
        {
            this.showPanel(pnlAddEditAdmin);
            this.btnAddAdmin.Visible = true;
            this.btnUpdateAdmin.Visible = false;
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


        private void btnEditAdmins_Click(object sender, EventArgs e)
        {
            EditAdmin();
        }


        private void btnEditEmployee_Click(object sender, EventArgs e)
        {
            EditEmployee();
        }


        private void UpdateAdmin()
        {
            for (int i = 0; i < dtAdmins.Rows.Count; ++i)
            {

                DataGridViewRow dataRow = dtAdmins.Rows[i];

                if (dataRow.IsNewRow)
                {
                    continue;
                }

                string adminId = (dataRow.Cells["ID"].Value.ToString());
                int roleIndex = this.cbAdminRole.SelectedIndex;
                roleIndex++;
                string roleID = Convert.ToString(roleIndex);
                string[] adminData = { this.tbAdminUserName.Text, this.tbAdminPassword.Text, this.tbAdminFirstName.Text, this.tbAdminLastName.Text, this.tbAdminEmail.Text, roleID, adminId };

                MySqlDataReader updateEmployee = DBcon.executeReader("UPDATE `admins` SET `username`= @usn,`password`= @password," +
                    "`first_name`= @firstname,`last_name`= @lastname,`email`= @email," +
                    "`role_id`= @roleid WHERE `id` = @id", adminData);
                this.pnlAdmin.Visible = true;
                this.showPanel(this.pnlAdmin);
                Admin.retrieveAllAdmins();
                this.renderAdminTable();
            }
        }


        private void UpdateEmployee()
        {
            for (int i = 0; i < dtEmployees.Rows.Count; ++i)
            {
                DataGridViewRow dataRow = dtEmployees.Rows[i];

                if (dataRow.IsNewRow)
                {
                    continue;
                }

                string employeeId = (dataRow.Cells["ID"].Value.ToString());
                string gender = (string)this.cbEmployeeGender.SelectedItem;
                int department_id = this.cbEmployeeDepartment.SelectedIndex;
                department_id++;
                string increased_department_id = Convert.ToString(department_id);
                string[] employeeData = {this.tbEmployeeFirstName.Text,this.tbEmployeeLastName.Text,this.tbEmployeeAdress.Text,
                    this.tbEmployeeCity.Text, this.tbEmployeeCountry.Text,
                    this.tbEmployeePhoneNumber.Text,gender,this.tbEmployeeEmail.Text,increased_department_id,employeeId};
                this.pnlEmployee.Visible = true;
                DBcon.executeNonQuery("UPDATE `employees` SET `first_name`= @firstname,`last_name`= @secondname," +
                   "`address`= @adress,`city`= @city,`country`= @country,`phone_number`=@phonenumber,`gender`=@gender,`email`=@email" +
                   ",`department_id`= @departmentid WHERE id = @id", employeeData);
                this.showPanel(this.pnlEmployee);
                Employee.retrieveAllEmployees();
                this.renderStaffTable();
            }

        }


        private void EditEmployee()
        {
            this.btnUpdateEmployee.Visible = true;
            this.btnAddUser.Visible = false;
            for (int i = 0; i < dtEmployees.Rows.Count; ++i)
            {


                DataGridViewRow dataRow = dtEmployees.Rows[i];

                if (dataRow.IsNewRow)
                {
                    continue;
                }

                bool selectedEmployee = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());
                if (selectedEmployee)
                {
                    string employeeId = (dataRow.Cells["ID"].Value.ToString());
                    Employee employee = StoreControl.getEmployeeById(Convert.ToInt32(employeeId));
                    this.tbEmployeeFirstName.Text = employee.FirstName;
                    this.tbEmployeeLastName.Text = employee.LastName;
                    this.tbEmployeeEmail.Text = employee.Email;
                    this.tbEmployeeAdress.Text = employee.Adress;
                    this.tbEmployeePhoneNumber.Text = employee.PhoneNumber;
                    this.tbEmployeeCity.Text = employee.City;
                    this.tbEmployeeCountry.Text = employee.Country;
                    this.tbEmployeeWagePerHour.Text = employee.Wage;
                    this.cbEmployeeDepartment.SelectedItem = employee.Department;
                    this.cbEmployeeGender.SelectedItem = employee.Gender;
                    this.showPanel(this.pnlAddStaff);
                    Employee.retrieveAllEmployees();
                    this.renderStaffTable();
                }
            }


        }


        private void EditAdmin()
        {
            this.btnAddAdmin.Visible = false;
            this.btnUpdateAdmin.Visible = true;
            for (int i = 0; i < dtAdmins.Rows.Count; ++i)
            {


                DataGridViewRow dataRow = dtAdmins.Rows[i];

                if (dataRow.IsNewRow)
                {
                    continue;
                }

                bool selectedAdmins = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());
                if (selectedAdmins)
                {
                    string id = dataRow.Cells["ID"].Value.ToString();
                    Admin admin = StoreControl.getAdminById(Convert.ToInt32(id));
                    this.tbAdminUserName.Text = admin.Username;
                    this.tbAdminFirstName.Text = admin.FirstName;
                    this.tbAdminLastName.Text = admin.LastName;
                    this.tbAdminEmail.Text = admin.Email;
                    this.tbAdminPassword.Text = admin.Password;
                    this.cbAdminRole.SelectedItem = admin.Role;
                    Admin.retrieveAllAdmins();
                    this.renderAdminTable();
                    this.showPanel(pnlAddEditAdmin);
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
            bool found = false;
            for (int i = 0; i < dtAdmins.Rows.Count; ++i)
            {
                DataGridViewRow dataRow = dtAdmins.Rows[i];

                bool selectedAdmin = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());
                if (dataRow.IsNewRow || !selectedAdmin)
                {
                    continue;
                }

                string adminId = (dataRow.Cells["ID"].Value.ToString());
                string[] adminID = { adminId };
                DBcon.executeNonQuery("DELETE FROM `admins` WHERE `id` = @id", adminID);
                found = true;
            }

            Admin.retrieveAllAdmins();
            this.renderAdminTable();


            if (!found)
            {
                MessageBox.Show("You need to tick the selected box to delete a admin");
            }

        }


        private void DeleteEmployee()
        {
            bool found = false;
            for (int i = 0; i < dtEmployees.Rows.Count; ++i)
            {

                DataGridViewRow dataRow = dtEmployees.Rows[i];
                bool selectedUser = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());

                if (dataRow.IsNewRow || !selectedUser)
                {
                    continue;
                }

                string id = (dataRow.Cells["ID"].Value.ToString());
                string[] getID = { id };
                DBcon.executeNonQuery("DELETE FROM `employees` WHERE `id` = @id", getID);
                found = true;
            }

            Employee.retrieveAllEmployees();
            this.renderStaffTable();


            if (!found)
            {
                MessageBox.Show("You need to tick the selected box to delete a employee");
            }
        }


        private void AddAdmin()
        {
            List<String> errors = new List<string>();

            if (!NullCheckerAdmin())
            {
                errors.Add("You can't create a user without entering all the fields");
            }

            if (StoreControl.getAdminByUsername(this.tbAdminUserName.Text) != null)
            {
                errors.Add("You can't add an admin with the same username");
            }

            if (!errors.Any())
            {
                int role_id = this.cbAdminRole.SelectedIndex;
                role_id++;
                string increased_role_id = Convert.ToString(role_id);
                string[] admin_bindings = { this.tbAdminUserName.Text, this.tbAdminPassword.Text, this.tbAdminFirstName.Text, this.tbAdminLastName.Text, this.tbAdminEmail.Text,
                    increased_role_id };
                DBcon.executeReader("INSERT INTO `admins`(`username`, `password`, `first_name`, `last_name`, `email`, `role_id`)" +
                       "VALUES(@username,@password,@first_name,@last_name,@email,@role_id)", admin_bindings);
                Admin.retrieveAllAdmins();
                this.renderAdminTable();
                this.showPanel(pnlAdmin);
                return;
            }

            foreach (string message in errors)
            {
                MessageBox.Show(message);
            }


        }


        private void AddEmployee()
        {
            this.btnAddUser.Visible = true;
            this.btnUpdateEmployee.Visible = false;
            List<String> errors = new List<string>();

            if (!NullCheckerEmployee())
            {
                errors.Add("You can't create a user without entering all the fields");
            }
            if (StoreControl.GetEmployeeByEmail(this.tbEmployeeEmail.Text) != null)
            {
                errors.Add("You can't add an user with the same email");
            }

            if (!errors.Any())
            {
                int department_id = this.cbEmployeeDepartment.SelectedIndex;
                department_id++;
                string increased_department_id = Convert.ToString(department_id);
                string gender = (string)this.cbEmployeeGender.SelectedItem;
                string[] employee_bindings = { this.tbEmployeeFirstName.Text, this.tbEmployeeLastName.Text, this.tbEmployeeAdress.Text,
                    this.tbEmployeeCity.Text, this.tbEmployeeCountry.Text, this.tbEmployeePhoneNumber.Text,
                    gender, this.tbEmployeeEmail.Text,increased_department_id};
                DBcon.executeNonQuery("INSERT INTO `employees`(`first_name`, `last_name`, `address`, `city`, `country`,`phone_number`, `gender`, `email`,`department_id`) " +
                               "VALUES(@first_name,@last_name,@address,@city,@country,@phone_number,@gender,@email,@department_id)", employee_bindings);
                Employee.retrieveAllEmployees();
                this.renderStaffTable();
                this.showPanel(pnlEmployee);
                return;

            }

            foreach (string message in errors)
            {
                MessageBox.Show(message);
            }

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

            //delete schedule if working employees is equal 0
            if (user_count == 0)
            {
                DBcon.executeNonQuery("DELETE FROM `schedules` WHERE id = @id", delete_bindings);

            }

            Schedule.retrieveSchedules();
            this.renderScheduleMembers();

            calendarSchedule.SelectionRange.End = this.currentScheduleDate;

        }


        private bool NullCheckerEmployee()
        {
            if (this.tbEmployeeFirstName.Text == "" || this.tbEmployeeLastName.Text == "" ||
                this.tbEmployeeAdress.Text == "" || this.tbEmployeeCity.Text == "" ||
                this.tbEmployeeCountry.Text == "" || this.tbEmployeeEmail.Text == "" || this.cbEmployeeGender.SelectedItem == null || this.tbEmployeePhoneNumber == null)
            {
                return false;
            }
            return true;
        }


        private bool NullCheckerAdmin()
        {
            if (this.tbAdminUserName.Text == "" || this.tbAdminFirstName.Text == "" || this.tbAdminLastName.Text == "" || this.tbAdminPassword.Text == "" || this.tbAdminEmail.Text == "")
            {
                return false;
            }
            return true;
        }


        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            UpdateEmployee();
        }


        private void btnUpdateAdmin_Click(object sender, EventArgs e)
        {
            UpdateAdmin();
        }

    }
}
