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

        private void btnEditAdmins_Click(object sender, EventArgs e)
        {
            EditAdmin();
        }


        private void btnEditEmployee_Click(object sender, EventArgs e)
        {
            EditEmployee();
        }

        private void UpdateEmployee()
        {
            this.pnlDashBoard.Visible = false;
            this.pnlEmployee.Visible = true;
            this.pnlAddStaff.Visible = false;
            this.pnlAddSchedule.Visible = false;
            this.pnlAdmin.Visible = false;
            this.pnlAddAdmin.Visible = false;
            this.btnAddUser.Visible = false;
            this.btnUpdateEmployee.Visible = false;
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
                    string gender = (string)this.cbEmployeeGender.SelectedItem;
                    int department_id = this.cbEmployeeDepartment.SelectedIndex;
                    department_id++;
                    string increased_department_id = Convert.ToString(department_id);

                    string[] employeeData = {this.tbEmployeeFirstName.Text,this.tbEmployeeLastName.Text,this.tbEmployeeAdress.Text,
                    this.tbEmployeeCity.Text, this.tbEmployeeCountry.Text,
                    this.tbEmployeePhoneNumber.Text,gender,this.tbEmployeeEmail.Text,increased_department_id,employeeId};

                    MySqlDataReader updateEmployee = DBcon.executeReader("UPDATE `employees` SET `first_name`= @firstname,`last_name`= @secondname," +
                        "`address`= @adress,`city`= @city,`country`= @country,`phone_number`=@phonenumber,`gender`=@gender,`email`=@email" +
                        ",`department_id`= @departmentid WHERE id = @id",employeeData);
                     this.retrieveAllEmployees();
                    this.renderStaffTable();
                }
            }

        }


        private void EditEmployee()
        {
            this.pnlDashBoard.Visible = false;
            this.pnlEmployee.Visible = false;
            this.pnlAddStaff.Visible = true;
            this.pnlAddSchedule.Visible = false;
            this.pnlAdmin.Visible = false;
            this.pnlAddAdmin.Visible = false;
            this.btnAddUser.Visible = false;
            this.btnUpdateEmployee.Visible = true;
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
                    this.cbEmployeeDepartment.SelectedItem = employee.Department;
                    this.cbEmployeeGender.SelectedItem = employee.Gender;
                }
            }


        }

        private void EditAdmin()
        {


        }


        private void retrieveAllEmployees()
        {
            MySqlDataReader employees = DBcon.executeReader("SELECT employees.*, departments.title as department FROM `employees` " +
                "INNER JOIN departments ON departments.id = employees.department_id GROUP by employees.id");
            StoreControl.emptyUsers();
            if (employees.HasRows)
            {

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
            StoreControl.emptyAdmins();
            if (admins.HasRows)
            {
                while (admins.Read())
                {
                    Admin admin = new Admin(Convert.ToInt32(admins["id"]), admins["username"].ToString(),
                        admins["role_title"].ToString(), admins["first_name"].ToString()
                        , admins["last_name"].ToString(), admins["email"].ToString());

                    StoreControl.addAdmin(admin);
                }
            }
        }

        private void retrieveSchedules()
        {
            MySqlDataReader schedules = DBcon.executeReader("SELECT schedules.* from schedules");

            if (schedules.HasRows)
            {
                StoreControl.emptySchedules();
                while (schedules.Read())
                {

                    Schedule schedule = new Schedule(Convert.ToInt32(schedules["id"]), schedules["notes"].ToString(), schedules["date"].ToString());

                    string[] bindings = { schedule.ID.ToString() };
                    MySqlDataReader employees_ids_q = DBcon.executeReader("SELECT employee_id as id ,from_hour, to_hour FROM schedules_employees WHERE" +
                    " schedule_id = @schedule_id", bindings);

                    if (employees_ids_q.HasRows)
                    {
                        while (employees_ids_q.Read())
                        {
                            int id = Convert.ToInt32(employees_ids_q["id"]);
                            Employee foundEmployee = StoreControl.getEmployeeById(id);

                            if (foundEmployee != null)
                            {
                                ScheduleMember member = new ScheduleMember(foundEmployee, employees_ids_q["from_hour"].ToString(), employees_ids_q["to_hour"].ToString());
                                schedule.addMember(member);
                            }
                        }
                    }

                    StoreControl.addSchedule(schedule);
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

                bool selectedAdmin = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());
                if (selectedAdmin)
                {
                    string adminId = (dataRow.Cells["ID"].Value.ToString());
                    string[] adminID = { adminId };
                    MySqlDataReader delete_admin = DBcon.executeReader("DELETE FROM `admins` WHERE `id` =" + adminID);
                    this.retrieveAllAdmins();
                    this.renderAdminTable();
                    DBcon.CloseConnection(delete_admin);
                }
                else
                {
                    MessageBox.Show("You need to tick the selected box to delete a customer");
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

                bool selectedUser = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());
                if (selectedUser)
                {
                    string id = (dataRow.Cells["ID"].Value.ToString());
                    string[] getID = { id };
                    MySqlDataReader delete_users = DBcon.executeReader("DELETE FROM `employees` WHERE `id` = @id",getID);
                    this.retrieveAllEmployees();    
                    this.renderStaffTable();
                    DBcon.CloseConnection(delete_users);
                }
                else
                {
                    MessageBox.Show("You need to tick the selected box to delete a employee");
                }
            }

        }

        private void AddAdmin()
        {
            this.pnlDashBoard.Visible = false;
            this.pnlEmployee.Visible = false;
            this.pnlAddStaff.Visible = false;
            this.pnlAddSchedule.Visible = false;
            this.pnlAdmin.Visible = false;
            this.pnlAddAdmin.Visible = true;
            if (NullCheckerAdmin())
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
                    this.retrieveAllAdmins();
                    this.renderAdminTable();
                    this.pnlAdmin.Visible = true;
                    DBcon.CloseConnection(add_admin);
                }
                else
                {
                    MessageBox.Show("You can't add an admin with the same username");
                }
            }
            else
            {
                MessageBox.Show("You can't create a user without entering all the fields");
            }

        }
        private void AddEmployee()
        {
            this.pnlDashBoard.Visible = false;
            this.pnlEmployee.Visible = false;
            this.pnlAddStaff.Visible = true;
            this.pnlAddSchedule.Visible = false;
            this.pnlAdmin.Visible = false;
            this.pnlAddAdmin.Visible = false;
            this.btnUpdateEmployee.Visible = false;
            if (NullCheckerEmployee())
            {
                if (StoreControl.GetEmployeeByEmail(this.tbEmployeeEmail.Text) == null)
                {
                    int department_id = this.cbEmployeeDepartment.SelectedIndex;
                    department_id++;
                    string increased_department_id = Convert.ToString(department_id);
                    string gender = (string)this.cbEmployeeGender.SelectedItem;
                    string[] employee_bindings = { this.tbEmployeeFirstName.Text, this.tbEmployeeLastName.Text, this.tbEmployeeAdress.Text,
                    this.tbEmployeeCity.Text, this.tbEmployeeCountry.Text, this.tbEmployeePhoneNumber.Text,
                    gender, this.tbEmployeeEmail.Text,increased_department_id};
                    MySqlDataReader add_employee = DBcon.executeReader("INSERT INTO `employees`(`first_name`, `last_name`, `address`, `city`, `country`,`phone_number`, `gender`, `email`,`department_id`) " +
                    "VALUES(@first_name,@last_name,@address,@city,@country,@phone_number,@gender,@email,@department_id)", employee_bindings);
                    this.retrieveAllEmployees();
                    this.renderStaffTable();
                    this.pnlEmployee.Visible = true;
                    DBcon.CloseConnection(add_employee);
                }
                else
                {
                    MessageBox.Show("You can't add an employee with the same email");
                }
            }
            else
            {
                MessageBox.Show("You can't create an employee without entering all the fields");
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
    }
}
