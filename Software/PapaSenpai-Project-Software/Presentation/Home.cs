using MySql.Data.MySqlClient;
using MySql.Data.Types;
using PapaSenpai_Project_Software.Logic;
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
        private EmployeeControl employeeControl;
        private Logic.UserControl adminControl;
        private ScheduleControl scheduleControl;
        public Home()
        {
            this.employeeControl = new EmployeeControl();
            this.adminControl = new Logic.UserControl();
            this.scheduleControl = new ScheduleControl();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            this.pnlDashBoard.BringToFront();
            this.currentScheduleDate = DateTime.Now;

            this.employeeControl.retrieveAllEmployees();
            this.scheduleControl.retrieveSchedules();
            this.adminControl.retrieveAllAdmins();

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
            this.pnlAddEditEmployee.Visible = false;
            this.pnlViewSchedule.Visible = false;
            this.pnlViewUser.Visible = false;
            this.pnlAddEditAdmin.Visible = false;
            this.pnlScheduleEmployees.Visible = false;
            this.pnlProducts.Visible = false;
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
            this.showPanel(pnlViewSchedule);
        }
        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            this.showPanel(pnlAddEditEmployee);
            this.btnAssignEmployee.Visible = true;
            this.btnUpdateEmployee.Visible = false;
        }


        private void btnViewAdmins_Click(object sender, EventArgs e)
        {
            this.showPanel(pnlViewUser);
            this.btnUpdateUser.Visible = false;
        }


        private void btnAddAdmins_Click(object sender, EventArgs e)
        {
            this.showPanel(pnlAddEditAdmin);
            this.btnAddUser.Visible = true;
            this.btnUpdateUser.Visible = false;
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
            try
            {

                string adminId = this.tbAdminId.Text;
                int roleIndex = this.cbAdminRole.SelectedIndex;
                roleIndex++;
                string roleID = Convert.ToString(roleIndex);
                string[] adminData = { this.tbAdminUserName.Text, this.tbAdminPassword.Text, this.tbAdminFirstName.Text, this.tbAdminLastName.Text, this.tbAdminEmail.Text, roleID, adminId };
                adminControl.UpdateAdmin(adminData);
                this.pnlViewUser.Visible = true;
                MessageBox.Show("You have succesfully update information for that user!");
                this.renderAdminTable();
                this.showPanel(this.pnlViewUser);
            }
            catch
            {
                MessageBox.Show("You need to enter all details for updating admin");
            }
               
      
        }


        private void UpdateEmployee()
        {
            if (NullCheckerEmployee())
            {
                string employeeId = this.tbEmployeeId.Text;
                string gender = (string)this.cbEmployeeGender.SelectedItem;
                int department_id = Convert.ToInt32(this.cbEmployeeDepartment.SelectedIndex);
                department_id++;
                string department = Convert.ToString(department_id);
                string[] employeeData = {this.tbEmployeeFirstName.Text,this.tbEmployeeLastName.Text,this.tbEmployeeAdress.Text,
                    this.tbEmployeeCity.Text, this.tbEmployeeCountry.Text,
                    this.tbEmployeePhoneNumber.Text,gender,this.tbEmployeeEmail.Text,department,this.tbEmployeeWagePerHour.Text,this.tbEmployeeUserName.Text,this.tbEmployeePassword.Text,employeeId};
                this.pnlEmployee.Visible = true;
                employeeControl.UpdateEmployee(employeeData);
                this.showPanel(this.pnlEmployee);
                this.renderStaffTable();
            }
            else
            {
                MessageBox.Show("You need to enter all the details for updating an employee!");
            }

        }


        private void EditEmployee()
        {
            this.btnUpdateEmployee.Visible = true;
            this.btnAssignEmployee.Visible = false;
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
                    Employee employee = employeeControl.getEmployeeById(Convert.ToInt32(employeeId));
                    this.tbEmployeeUserName.Text = employee.UserName;
                    this.tbEmployeePassword.Text = employee.Password;
                    this.tbEmployeeFirstName.Text = employee.FirstName;
                    this.tbEmployeeLastName.Text = employee.LastName;
                    this.tbEmployeeEmail.Text = employee.Email;
                    this.tbEmployeeAdress.Text = employee.Adress;
                    this.tbEmployeePhoneNumber.Text = employee.PhoneNumber;
                    this.tbEmployeeCity.Text = employee.City;
                    this.tbEmployeeId.Text = Convert.ToString(employee.ID);
                    this.tbEmployeeCountry.Text = employee.Country;
                    this.tbEmployeeWagePerHour.Text = employee.Wage;
                    this.cbEmployeeDepartment.SelectedItem = employee.Department;
                    this.cbEmployeeGender.SelectedItem = employee.Gender;
                    this.showPanel(this.pnlAddEditEmployee);
                    employeeControl.retrieveAllEmployees();
                    this.renderStaffTable();
                    this.renderScheduleMembers();
                }
            }


        }


        private void EditAdmin()
        {
            this.btnAddUser.Visible = false;
            this.btnUpdateUser.Visible = true;
            for (int i = 0; i < dtUsers.Rows.Count; ++i)
            {
                DataGridViewRow dataRow = dtUsers.Rows[i];

                if (dataRow.IsNewRow)
                {
                    continue;
                }

                bool selectedAdmins = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());
                if (selectedAdmins)
                {
                    string id = dataRow.Cells["ID"].Value.ToString();
                    Admin admin = adminControl.getAdminById(Convert.ToInt32(id));
                    this.tbAdminUserName.Text = admin.Username;
                    this.tbAdminFirstName.Text = admin.FirstName;
                    this.tbAdminLastName.Text = admin.LastName;
                    this.tbAdminEmail.Text = admin.Email;
                    this.tbAdminPassword.Text = admin.Password;
                    this.cbAdminRole.SelectedItem = admin.Role;
                    this.tbAdminId.Text = Convert.ToString(admin.ID);
                    adminControl.retrieveAllAdmins();
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


            foreach (Admin admin in adminControl.getAdmins())
            {
                dtEmp.Rows.Add(false, admin.ID, admin.Username, admin.FirstName, admin.LastName, admin.Role, admin.Email);
            }

            dtUsers.DataSource = dtEmp;

        }


        private void renderStaffTable()
        {
            DataTable dtEmp = new DataTable();
            dtEmp.Columns.Add("Selected", typeof(bool));
            dtEmp.Columns.Add("ID", typeof(int));
            dtEmp.Columns.Add("Username", typeof(string));
            dtEmp.Columns.Add("Password", typeof(string));
            dtEmp.Columns.Add("First Name", typeof(string));
            dtEmp.Columns.Add("Last Name", typeof(string));
            dtEmp.Columns.Add("Gender", typeof(string));
            dtEmp.Columns.Add("Phone", typeof(string));
            dtEmp.Columns.Add("Country", typeof(string));
            dtEmp.Columns.Add("City", typeof(string));
            dtEmp.Columns.Add("Adress", typeof(string));
            dtEmp.Columns.Add("Email", typeof(string));
            dtEmp.Columns.Add("Deparment", typeof(string));
            dtEmp.Columns.Add("Wage per hour", typeof(string));
            dtEmp.Columns.Add("Salary for the shift", typeof(string));

            foreach (Employee employee in employeeControl.getEmployees())
            {
                dtEmp.Rows.Add(false, employee.ID, employee.UserName, employee.Password, employee.FirstName, employee.LastName, employee.Gender, employee.PhoneNumber, employee.Country, employee.City, employee.Adress, employee.Email, employee.Department, employee.Wage, "10");
            }

            dtEmployees.DataSource = dtEmp;

        }


        private void renderScheduleMembers()
        {

            Schedule schedule = scheduleControl.getScheduleByDate(this.currentScheduleDate);
            calendarSchedule.AnnuallyBoldedDates = null;
            List<DateTime> coloredDates = new List<DateTime>();

            foreach (Schedule found_schedule in scheduleControl.getSchedules())
            {
                Console.WriteLine(found_schedule.Date);
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

            foreach (Employee employee in employeeControl.getEmployees())
            {
                ScheduleMember foundMember = null;
                if (schedule != null)
                {
                    foreach (ScheduleMember member in scheduleControl.Members)
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

            dtAssignShift.DataSource = dtEmp;
        }


        private void renderDailySchedule()
        {

            Schedule schedule = scheduleControl.getScheduleByDate(DateTime.Now);

            DataTable dtEmp = new DataTable();

            dtEmp.Columns.Add("ID", typeof(string));
            dtEmp.Columns.Add("Name", typeof(string));
            dtEmp.Columns.Add("From", typeof(string));
            dtEmp.Columns.Add("To", typeof(string));
            dtEmp.Columns.Add("Department", typeof(string));

            if (schedule != null)
            {
                foreach (ScheduleMember member in scheduleControl.Members)
                {

                    dtEmp.Rows.Add(member.Employee.ID, member.Employee.getFullName(), member.StartTime.ToString("HH:mm"), member.EndTime.ToString("HH:mm"), member.Employee.Department);

                }
            }


            dtTodaySchedule.DataSource = dtEmp;
        }


        private void DeleteAdmin()
        {
            try
            {
                if(MessageBox.Show("Do you want to delete this user?","Confirm",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool found = false;
                    for (int i = 0; i < dtUsers.Rows.Count; ++i)
                    {
                        DataGridViewRow dataRow = dtUsers.Rows[i];

                        bool selectedAdmin = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());
                        if (dataRow.IsNewRow || !selectedAdmin)
                        {
                            continue;
                        }
                        string adminId = (dataRow.Cells["ID"].Value.ToString());
                        string[] adminID = { adminId };
                        adminControl.DeleteAdmin(adminID);
                        found = true;
                    }

                    adminControl.retrieveAllAdmins();
                    this.renderAdminTable();


                    if (!found)
                    {
                        MessageBox.Show("You need to tick the selected box to delete a admin");
                    }
                }   
            }
            catch
            {
                MessageBox.Show("Can't delete that");
            }
          

        }


        private void DeleteEmployee()
        {
            try
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
                    employeeControl.DeleteEmployee(getID);
                    found = true;
                }

                if (found)
                {
                    MessageBox.Show("Employee/s have been succesfully deleted");
                }

                employeeControl.retrieveAllEmployees();
                this.renderStaffTable();
                this.renderScheduleMembers();


                if (!found)
                {
                    MessageBox.Show("You need to tick the selected box to delete a employee");
                }
            }
            catch
            {
                MessageBox.Show("Couldn't delete employee because it's already assigned to schedule!");
            }
           
        }


        private void AddAdmin()
        {
            List<String> errors = new List<string>();

            if (!NullCheckerAdmin())
            {
                errors.Add("You can't create a user without entering all the fields");
            }

            if (adminControl.getAdminByUsername(this.tbAdminUserName.Text) != null)
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
                adminControl.AddAdmin(admin_bindings); 
                MessageBox.Show("You have created a user!");
                this.renderAdminTable();
                this.showPanel(pnlViewUser);
                return;
            }

            foreach (string message in errors)
            {
                MessageBox.Show(message);
            }


        }


        private void AddEmployee()
        {
            this.btnAssignEmployee.Visible = true;
            this.btnUpdateEmployee.Visible = false;
            List<String> errors = new List<string>();

            if (!NullCheckerEmployee())
            {
                errors.Add("You can't create a user without entering all the fields");
            }
            if (employeeControl.GetEmployeeByEmail(this.tbEmployeeEmail.Text) != null)
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
                    this.tbEmployeeCity.Text, this.tbEmployeeCountry.Text, this.tbEmployeeWagePerHour.Text, this.tbEmployeePhoneNumber.Text,
                    gender, this.tbEmployeeEmail.Text,increased_department_id,this.tbEmployeeUserName.Text,this.tbEmployeePassword.Text};
                 employeeControl.AddEmployee(employee_bindings);
                this.renderScheduleMembers();
                this.renderStaffTable();
                this.showPanel(pnlEmployee);
                return;
            }

            foreach (string message in errors)
            {
                MessageBox.Show(message);
            }

        }


        private void updateSchedule()
        {
            //get if there is a schedule about this date
            //if yes remove all of the database members and add new ones

            //add the new ones
            //if not create schedule in db with the employees in related table

            Schedule schedule = scheduleControl.getScheduleByDate(this.currentScheduleDate);

            int id;
            if (schedule == null)
            {
                string[] bindings = { "", this.currentScheduleDate.ToString("MM-dd-yyyy") };
                // to do - George
                id = Convert.ToInt32(scheduleControl.UpdateSchedule(bindings) );

            }
            else
            {
                id = schedule.ID;
            }

            //delete all of the attached users for new/pervious schedules
            string[] delete_bindings = { id.ToString() };
            scheduleControl.DeleteSchedule(delete_bindings);

            int user_count = 0;

            for (int i = 0; i < dtAssignShift.Rows.Count; ++i)
            {

                DataGridViewRow dataRow = dtAssignShift.Rows[i];

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
                    scheduleControl.AssignSchedule(member_data);
                    user_count++;
                }
            }

            //delete schedule if working employees is equal 0
            if (user_count == 0)
            {
               scheduleControl.DeleteSchedule(delete_bindings);
            }

            scheduleControl.retrieveSchedules();
            this.renderScheduleMembers();
            this.renderDailySchedule();

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

        private void calendarSchedule_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.showPanel(pnlScheduleEmployees);
            this.currentScheduleDate = e.End;
            this.renderScheduleMembers();
        }

        private void btnViewSchedule_Click(object sender, EventArgs e)
        {
            this.showPanel(pnlViewSchedule);
        }

        private void btnUpdateSchedule_Click(object sender, EventArgs e)
        {
            updateSchedule();
            UpdateEmployee();
            this.showPanel(pnlViewSchedule);
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.showPanel(pnlViewSchedule);
        }

        private void btnViewAllSchedules_Click(object sender, EventArgs e)
        {
            this.showPanel(pnlViewSchedule);
        }

        private void btnViewProducts_Click(object sender, EventArgs e)
        {
            this.showPanel(pnlProducts);
        }

    }
}
