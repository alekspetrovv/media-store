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
using PapaSenpai_Project_Software;

namespace PapaSenpai_Project_Software
{
    public partial class Home : MaterialSkin.Controls.MaterialForm
    {
        private DateTime currentScheduleDate;
        private EmployeeControl employeeControl;
        private AdminControl adminControl;
        private ScheduleControl scheduleControl;
        private ProductControl productControl;
        private OrdersControl ordersControl;
        public Home(AdminControl a)
        {
            this.employeeControl = new EmployeeControl();
            this.adminControl = new Logic.AdminControl();
            this.scheduleControl = new ScheduleControl();
            this.productControl = new ProductControl();
            this.ordersControl = new OrdersControl();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            this.pnlDashBoard.BringToFront();
            this.currentScheduleDate = DateTime.Now;
            this.adminControl = a;


            this.employeeControl.retrieveAllEmployees();
            this.scheduleControl.retrieveSchedules();
            this.adminControl.retrieveAllAdmins();
            this.productControl.retrieveAllProducts();
            this.renderStaffTable();
            this.renderAdminTable();
            this.renderScheduleMembers();
            this.renderDailySchedule();
            this.renderProductsTable();
            this.renderProductsForOrder();
        }


        private void Home_Load(object sender, EventArgs e)
        {
            ChangeLoginStyle();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            this.showPanel(this.pnlDashBoard);
        }


        private void btnViewStaff_Click(object sender, EventArgs e)
        {
            this.showPanel(pnlEmployee);
        }

        private void btnBackToEmployeePageFromDetails_Click_1(object sender, EventArgs e)
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
            this.valuesAreEmptyEmployee();
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
            this.clearAdminFields();
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

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            this.showPanel(this.pnlAddEditProduct);
            this.showButton(this.btnAddProductItem);
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            this.EditProduct();
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            this.DeleteProduct();
        }

        private void btnAddProductItem_Click(object sender, EventArgs e)
        {
            this.AddProduct();
        }

        private void btnUpdateProductItem_Click(object sender, EventArgs e)
        {
            this.UpdateProduct();
        }

        private void btnViewCart_Click(object sender, EventArgs e)
        {
            this.showPanel(pnlCart);
        }

        private void btnViewEmployeeDetails_Click(object sender, EventArgs e)
        {
            this.ShowEmployeeDetails();
        }

        private void btnViewProduct_Click(object sender, EventArgs e)
        {
            ShowProductDetails();
        }
        private void btnBackToUserPage_Click(object sender, EventArgs e)
        {
            this.showPanel(this.pnlViewUser);
        }
        private void btnBackToEmployeePage_Click(object sender, EventArgs e)
        {
            this.showPanel(this.pnlEmployee);
        }
        private void btnBackToProductPageFromAddEdit_Click(object sender, EventArgs e)
        {
            this.showPanel(this.pnlProducts);
            this.clearEmployeeFields();
        }

        private void btnBackToProductPage_Click(object sender, EventArgs e)
        {
            this.showPanel(this.pnlProducts);
        }

        private void btnBackToEmployeePageFromDetails_Click(object sender, EventArgs e)
        {
            this.showPanel(this.pnlEmployee);
        }

        private void renderProductsForOrder()
        {
            lbStoreProducts.Items.Clear();
            foreach (Product p in productControl.GetProducts())
            {
                if (p.Quantity > 0)
                {
                lbStoreProducts.Items.Add(p);
                }
            }

        }


        private void renderProductsTable()
        {
            DataTable dtPrd = new DataTable();
            // add column to datatable  
            dtPrd.Columns.Add("Selected", typeof(bool));
            dtPrd.Columns.Add("ID", typeof(int));
            dtPrd.Columns.Add("Title", typeof(string));
            dtPrd.Columns.Add("Description", typeof(string));
            dtPrd.Columns.Add("Quantity", typeof(string));
            dtPrd.Columns.Add("QuantityDepo", typeof(string));
            dtPrd.Columns.Add("Selling Price", typeof(string));
            dtPrd.Columns.Add("Buying Price", typeof(string));
            dtPrd.Columns.Add("Needs Refill", typeof(string));
            dtPrd.Columns.Add("Threshold", typeof(string));


            foreach (Product p in productControl.GetProducts())
            {
                string refill = "No";
                if (p.Quantity <= p.ThreshHold) {
                    refill = "Yes";
                }
                dtPrd.Rows.Add(false, p.Id, p.Title, p.Description, p.Quantity, p.QuantityDepo, p.SellingPrice, p.BuyingPrice, refill, p.ThreshHold);
            }

            dtProducts.DataSource = dtPrd;
        }


        private void AddProduct()
        {
            List<String> errors = new List<string>();

            if (!valuesAreEmptyProduct())
            {
                errors.Add("You can't create a product without entering all the fields");
            }

            if (this.productControl.GetProductByTitle(this.tbProductTitle.Text) != null)
            {
                errors.Add("You can't add a product with the same title");
            }

            if (!errors.Any())
            {
                int quantity = Convert.ToInt32(this.tbProductQuantity.Text);
                int quantitydepo = Convert.ToInt32(this.tbProductQuantityDepo.Text);
                double selling_price = Convert.ToDouble(this.tbProductSellingPrice.Text);
                double buying_price = Convert.ToDouble(this.tbProductBuyingPrice.Text);
                int threshold = Convert.ToInt32(this.tbProductThreshHold.Text);
                string[] product_bindings = { this.tbProductTitle.Text, this.tbProductDescription.Text, quantity.ToString(), quantitydepo.ToString(),
                selling_price.ToString(), buying_price.ToString() , threshold.ToString()};
                this.productControl.AddProduct(product_bindings);
                MessageBox.Show("you have successfully created a product!");
                this.renderProductsTable();
                this.showPanel(pnlProducts);
                return;
            }

            foreach (string message in errors)
            {
                MessageBox.Show(message);
            }


        }


        private void EditProduct()
        {
            this.btnAddProductItem.Visible = false;
            this.btnUpdateProductItem.Visible = true;
            try
            {
                for (int i = 0; i < dtProducts.Rows.Count; ++i)
                {
                    DataGridViewRow dataRow = dtProducts.Rows[i];

                    if (dataRow.IsNewRow)
                    {
                        continue;
                    }

                    bool selectedProduct = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());
                    if (selectedProduct)
                    {
                        string id = dataRow.Cells["ID"].Value.ToString();
                        Product product = productControl.GetProductById(Convert.ToInt32(id));
                        this.tbProductTitle.Text = product.Title;
                        this.tbProductDescription.Text = product.Description;
                        this.tbProductQuantity.Text = Convert.ToString(product.Quantity);
                        this.tbProductQuantityDepo.Text = Convert.ToString(product.QuantityDepo);
                        this.tbProductSellingPrice.Text = Convert.ToString(product.SellingPrice);
                        this.tbProductBuyingPrice.Text = Convert.ToString(product.BuyingPrice);
                        this.tbProductThreshHold.Text = Convert.ToString(product.ThreshHold);
                        this.tbProductId.Text = Convert.ToString(product.Id);
                        this.productControl.retrieveAllProducts();
                        this.renderProductsTable();
                        this.showPanel(pnlAddEditProduct);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("There's been an error: " + e.Message);
            }
        }


        private void UpdateProduct()
        {
            try
            {

                string productID = this.tbProductId.Text;
                string[] productData = { this.tbProductTitle.Text,this.tbProductDescription.Text,this.tbProductQuantity.Text,this.tbProductQuantityDepo.Text,
                this.tbProductSellingPrice.Text,this.tbProductBuyingPrice.Text,this.tbProductThreshHold.Text, productID };
                this.productControl.UpdateProduct(productData);
                MessageBox.Show("You have succesfully updated information for that product!");
                this.renderProductsTable();
                this.showPanel(this.pnlProducts);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            this.clearProductFields();
        }


        private void DeleteProduct()
        {
            try
            {
                if (MessageBox.Show("Do you want to delete this product?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool found = false;
                    for (int i = 0; i < dtProducts.Rows.Count; ++i)
                    {
                        DataGridViewRow dataRow = dtProducts.Rows[i];

                        bool selectedProduct = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());
                        if (dataRow.IsNewRow || !selectedProduct)
                        {
                            continue;
                        }
                        string productId = (dataRow.Cells["ID"].Value.ToString());
                        string[] product = { productId };
                        this.productControl.DeleteProduct(product);
                        found = true;
                    }

                    this.productControl.retrieveAllProducts();
                    this.renderProductsTable();


                    if (!found)
                    {
                        MessageBox.Show("You need to tick the selected box to delete a product");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Can't delete that");
            }

        }


        private void ShowProductDetails()
        {
            for (int i = 0; i < dtProducts.Rows.Count; ++i)
            {
                DataGridViewRow dataRow = dtProducts.Rows[i];

                if (dataRow.IsNewRow)
                {
                    continue;
                }

                bool selectedProduct = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());
                if (selectedProduct)
                {
                    string id = dataRow.Cells["ID"].Value.ToString();
                    Product product = productControl.GetProductById(Convert.ToInt32(id));
                    this.lblProductTitle.Text = product.Title;
                    this.lblProductDescription.Text = product.Description;
                    this.lblProductQuantity.Text = Convert.ToString(product.Quantity);
                    this.lblProductQuantityDepo.Text = Convert.ToString(product.QuantityDepo);
                    this.lblProductSellingPrice.Text = Convert.ToString(product.SellingPrice);
                    this.lblProductBuyingPrice.Text = Convert.ToString(product.BuyingPrice);
                    this.lblProductPlaceHolder.Text = Convert.ToString(product.ThreshHold);
                    this.lblProductID.Text = Convert.ToString(product.Id);
                    this.productControl.retrieveAllProducts();
                    this.renderProductsTable();
                    this.showPanel(pnlProductInformation);
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


        private void AddAdmin()
        {
            List<String> errors = new List<string>();

            if (!valuesAreEmptyAdmin())
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
                adminControl.Add(admin_bindings);
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
                    this.cbAdminRole.SelectedItem = Convert.ToString(admin.Role);
                    this.tbAdminId.Text = Convert.ToString(admin.ID);
                    adminControl.retrieveAllAdmins();
                    this.renderAdminTable();
                    this.showPanel(pnlAddEditAdmin);
                }
            }

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
                adminControl.Update(adminData);
                this.pnlViewUser.Visible = true;
                MessageBox.Show("You have succesfully update information for that user!");
                this.renderAdminTable();
                this.showPanel(this.pnlViewUser);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }




        private void DeleteAdmin()
        {
            try
            {
                if (MessageBox.Show("Do you want to delete this user?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                        adminControl.Delete(adminID);
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


        private void ShowAdminDetails()
        {

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
            dtEmp.Columns.Add("Contract", typeof(string));
            dtEmp.Columns.Add("Wage per hour", typeof(string));
            dtEmp.Columns.Add("Salary for the shift", typeof(string));

            foreach (Employee employee in employeeControl.getEmployees())
            {
                dtEmp.Rows.Add(false, employee.ID, employee.UserName, employee.Password, employee.FirstName,
                    employee.LastName, employee.Gender, employee.PhoneNumber, employee.Country, employee.City, 
                    employee.Adress, employee.Email, employee.Department,employee.Contract,employee.Wage, "10");
            }

            dtEmployees.DataSource = dtEmp;

        }



        private void AddEmployee()
        {
            this.btnAssignEmployee.Visible = true;
            this.btnUpdateEmployee.Visible = false;
            List<String> errors = new List<string>();

            if (!valuesAreEmptyEmployee())
            {
                errors.Add("You can't create a user without entering all the fields");
            }
            if (employeeControl.GetEmployeeByEmail(this.tbEmployeeEmail.Text) != null)
            {
                errors.Add("You can't add an user with the same email");
            }

            if (!errors.Any())
            {
                try
                {
                    int department_id = this.cbEmployeeDepartment.SelectedIndex;
                    department_id++;
                    string increased_department_id = Convert.ToString(department_id);
                    int contract_id = this.cbEmployeeContract.SelectedIndex;
                    contract_id++;
                    string increased_contract_id = Convert.ToString(contract_id);
                    Console.WriteLine(increased_contract_id);
                    string gender = Convert.ToString(this.cbEmployeeGender.Text);
                    string[] employee_bindings = { this.tbEmployeeFirstName.Text, this.tbEmployeeLastName.Text, this.tbEmployeeAdress.Text,
                    this.tbEmployeeCity.Text, this.tbEmployeeCountry.Text, this.tbEmployeeWagePerHour.Text, this.tbEmployeePhoneNumber.Text,
                    gender, this.tbEmployeeEmail.Text,increased_department_id,increased_contract_id,this.tbEmployeeUserName.Text,this.tbEmployeePassword.Text};
                    employeeControl.AddEmployee(employee_bindings);
                    this.renderScheduleMembers();
                    this.renderStaffTable();
                    this.showPanel(pnlEmployee);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

            foreach (string message in errors)
            {
                MessageBox.Show(message);
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
                    try
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
                        this.cbEmployeeDepartment.SelectedItem = Convert.ToString(employee.Department);
                        this.cbEmployeeContract.SelectedItem = Convert.ToString(employee.Contract);
                        this.cbEmployeeGender.SelectedItem = Convert.ToString(employee.Gender);
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show("There's been an exception: " + e.Message);
                    }
                    this.showPanel(this.pnlAddEditEmployee);
                    employeeControl.retrieveAllEmployees();
                    this.renderStaffTable();
                    this.renderScheduleMembers();
                }
            }


        }



        private void UpdateEmployee()
        {
            try
            {
                string employeeId = this.tbEmployeeId.Text;
                string gender = Convert.ToString(this.cbEmployeeGender.SelectedItem);
                int department_id = this.cbEmployeeDepartment.SelectedIndex;
                department_id++;
                string increased_department_id = Convert.ToString(department_id);
                int contract_id = this.cbEmployeeContract.SelectedIndex;
                contract_id++;
                string increased_contract_id = Convert.ToString(contract_id);
                string[] employeeData = {this.tbEmployeeFirstName.Text,this.tbEmployeeLastName.Text,this.tbEmployeeAdress.Text,
                    this.tbEmployeeCity.Text, this.tbEmployeeCountry.Text,
                    this.tbEmployeePhoneNumber.Text,gender,this.tbEmployeeEmail.Text,increased_department_id,increased_contract_id,
                    this.tbEmployeeWagePerHour.Text,this.tbEmployeeUserName.Text,this.tbEmployeePassword.Text,employeeId};
                this.pnlEmployee.Visible = true;
                employeeControl.UpdateEmployee(employeeData);
                MessageBox.Show("You have succesfully update information for that employee!");
                this.showPanel(this.pnlEmployee);
                this.renderStaffTable();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }



        private void DeleteEmployee()
        {
            try
            {
                if (MessageBox.Show("Do you want to delete this employee?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
            }
            catch(Exception e)
            {
                MessageBox.Show("There's been an exception!" + e.Message);
            }

        }


        private void ShowEmployeeDetails()
        {

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
                    try
                    {
                        string id = dataRow.Cells["ID"].Value.ToString();
                        Employee employee = employeeControl.getEmployeeById(Convert.ToInt32(id));
                        this.tbeUserName.Text = employee.UserName;
                        this.tbeFirstName.Text = employee.FirstName;
                        this.tbeLastName.Text = employee.LastName;
                        this.tbeEmail.Text = employee.Email;
                        this.tbePassword.Text = employee.Password;
                        this.tbeAddress.Text = employee.Adress;
                        this.tbeCity.Text = employee.City;
                        this.tbePhoneNumber.Text = employee.PhoneNumber;
                        //this.tbeSalary.Text = // to do;
                        //this.tbeTotalHours.Text = // to do;
                        this.tbeGender.Text = Convert.ToString(employee.Gender);
                        this.tbeDepartment.Text = Convert.ToString(employee.Department);
                        this.tbeId.Text = Convert.ToString(employee.ID);
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    adminControl.retrieveAllAdmins();
                    this.renderAdminTable();
                    this.showPanel(this.pnlViewEmployeeDetails);
                }
            }
        }

        private void updateSchedule()
        {
            //get if there is a schedule about this date
            //if yes remove all of the database members and add new ones

            //add the new ones
            //if not create schedule in db with the employees in related table

            Schedule schedule = scheduleControl.getScheduleByDate(this.currentScheduleDate);

            if (schedule != null)
            {

                string[] delete_bindings = { schedule.ID.ToString() };
                scheduleControl.Delete(delete_bindings);
            }

            string[] bindings = { "", this.currentScheduleDate.ToString("MM-dd-yyyy") };
            int id = Convert.ToInt32(scheduleControl.Insert(bindings));



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
                    DateTime test;

                    //check if the datetime string is really a datetime and if yes safe the user to the db
                    if (DateTime.TryParse(from, out test) && DateTime.TryParse(to, out test))
                    {
                         scheduleControl.InsertMember(member_data);
                         user_count++;
                    } else
                    {
                        MessageBox.Show("There was a problem with the timestamp given for user with ID: " + member_id);
                    }
                }
            }

            //delete schedule if working employees is equal 0
            if (user_count == 0)
            {
                string[] delete_bindings = { id.ToString() };
                scheduleControl.Delete(delete_bindings);
            }

            scheduleControl.retrieveSchedules();
            this.renderScheduleMembers();
            this.renderDailySchedule();

            calendarSchedule.SelectionRange.End = this.currentScheduleDate;

        }


        private void renderScheduleMembers()
        {

            Schedule schedule = scheduleControl.getScheduleByDate(this.currentScheduleDate);
            calendarSchedule.AnnuallyBoldedDates = null;
            List<DateTime> coloredDates = new List<DateTime>();

            foreach (Schedule found_schedule in scheduleControl.getSchedules())
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
            
            
            foreach (Employee employee in employeeControl.getEmployees())
            {
                ScheduleMember foundMember = null;
                if (schedule != null)
                {
                    foundMember = schedule.findEmployeeMemberById(employee.ID);
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
                foreach (ScheduleMember member in schedule.Members)
                {
                    Employee foundEmployee = this.employeeControl.getEmployeeById(member.EmployeeId);
                    dtEmp.Rows.Add(member.EmployeeId, foundEmployee.getFullName(), member.StartTime.ToString("HH:mm"), member.EndTime.ToString("HH:mm"), foundEmployee.Department);

                }
            }


            dtTodaySchedule.DataSource = dtEmp;
        }


        private bool valuesAreEmptyProduct()
        {
            if (this.tbProductDescription.Text == "" || this.tbProductDescription.Text == "" || this.tbProductTitle.Text == "" ||
                this.tbProductQuantity.Text == "" || this.tbProductSellingPrice.Text == "" || this.tbProductBuyingPrice.Text == "" || this.tbProductThreshHold.Text == "") 
            {
                return false;
            }
            return true;
        }


        private bool valuesAreEmptyEmployee()
        {
            if (this.tbEmployeeFirstName.Text == "" || this.tbEmployeeLastName.Text == "" ||
                this.tbEmployeeAdress.Text == "" || this.tbEmployeeCity.Text == "" ||
                this.tbEmployeeCountry.Text == "" || this.tbEmployeeEmail.Text == "" || this.cbEmployeeContract == null ||
                this.cbEmployeeGender.SelectedItem == null || this.tbEmployeePhoneNumber == null)
            {
                return false;
            }
            return true;
        }


        private bool valuesAreEmptyAdmin()
        {
            if (this.tbAdminUserName.Text == "" || this.tbAdminFirstName.Text == "" ||
                this.tbAdminLastName.Text == "" || this.tbAdminPassword.Text == "" ||
                this.tbAdminEmail.Text == "")
            {
                return false;
            }
            return true;
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
            this.pnlAddEditProduct.Visible = false;
            this.pnlCart.Visible = false;
            this.pnlProductInformation.Visible = false;
            this.pnlViewEmployeeDetails.Visible = false;
            panel.Visible = true;

        }

        private void showButton(Button button)
        {
            this.btnUpdateProductItem.Visible = false;
            button.Visible = true;
        }


        private void ManagerPermissions()
        {
            this.btnDashboard.Visible = false;
            this.btnViewEmployees.Visible = true;
            this.btnAddEmployee.Visible = false;
            this.btnEditEmployee.Visible = false;
            this.btnDeleteEmployee.Visible = false;
            
            this.btnViewUsers.Visible = false;
            this.btnDashboard.Visible = false;
            this.btnViewEmployees.Visible = false;

        }

        private void clearProductFields()
        {
            this.tbProductTitle.Clear();
            this.tbProductDescription.Clear();
            this.tbProductQuantity.Clear();
            this.tbProductQuantityDepo.Clear();
            this.tbProductSellingPrice.Clear();
            this.tbProductThreshHold.Clear();
            this.tbProductBuyingPrice.Clear();
        }

        private void clearEmployeeFields()
        {
            this.tbEmployeeAdress.Clear();
            this.tbEmployeeCountry.Clear();
            this.tbEmployeeCity.Clear();
            this.tbEmployeeEmail.Clear();
            this.tbEmployeeLastName.Clear();
            this.tbEmployeePassword.Clear();
            this.tbEmployeePhoneNumber.Clear();
            this.tbEmployeeWagePerHour.Clear();
            this.tbEmployeeFirstName.Clear();
            this.tbEmployeeUserName.Clear();
        }

        private void clearAdminFields()
        {
            this.tbAdminEmail.Clear();
            this.tbAdminFirstName.Clear();
            this.tbAdminLastName.Clear();
            this.tbAdminUserName.Clear();
            this.tbAdminPassword.Clear();
        }

        private void tbeDepartment_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel23_Click(object sender, EventArgs e)
        {

        }

        private void pnlAddEditEmployee_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tbAddToCart_Click(object sender, EventArgs e)
        {
            Product product = (Product) lbStoreProducts.SelectedItem;
            int quantity = Convert.ToInt32(this.tbQuantity.Text);
            if (product.Quantity < quantity || quantity <= 0)
            {
                MessageBox.Show("The desired quanity of the product is not in stock");
                return;
            }

            this.ordersControl.addProduct(product, quantity);
            this.lbShoppingCart.Items.Add($"{product.Title} - {quantity}");
        }

        private void tbPurchase_Click(object sender, EventArgs e)
        {

        }

        private void tbPurchase_Click_1(object sender, EventArgs e)
        {
                  }

        private void tbPurchase_Click_2(object sender, EventArgs e)
        {
             Admin admin = this.adminControl.getloggedUser();

            if (this.ordersControl.Products.Count() == 0 )
            {
                MessageBox.Show("Your basket is empty");
                return;
            }

            string[] bindings = { admin.ID.ToString() };
            this.ordersControl.Buy(bindings);
            this.lbShoppingCart.Items.Clear();
            this.productControl.retrieveAllProducts();
            this.renderProductsForOrder();
            this.renderProductsTable();
            MessageBox.Show("You successfully made an order!");

        }
    }
}
