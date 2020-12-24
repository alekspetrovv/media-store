using PapaSenpai_Project_Software.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
        private RequestsControl requestControl;
        private DepartmentControl departmentControl;
        private RoleControl roleControl;

        public Home(AdminControl a)
        {
            InitializeComponent();
            // initialize
            this.employeeControl = new EmployeeControl();
            this.adminControl = new Logic.AdminControl();
            this.scheduleControl = new ScheduleControl();
            this.productControl = new ProductControl();
            this.ordersControl = new OrdersControl();
            this.requestControl = new RequestsControl();
            this.departmentControl = new DepartmentControl();
            this.roleControl = new RoleControl();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.pnlDashBoard.BringToFront();
            this.currentScheduleDate = DateTime.Now;
            this.adminControl = a;

            // retrieve 
            this.employeeControl.retrieveAllEmployees();
            this.scheduleControl.retrieveSchedules();
            this.adminControl.retrieveAllAdmins();
            this.productControl.retrieveAllProducts();
            this.requestControl.retrieveAllRequests();
            this.departmentControl.retrieveAllDepartments();
            this.roleControl.retrieveAllRoles();

            // permissions
            Admin admin = adminControl.getloggedUser();

            if (admin.Role.Title == "Manager")
            {
                ManagerPermissions();
            }

            if (admin.Role.Title == "Store Manager")
            {
                StoreManagerPermissions();
            }

            if (admin.Role.Title == "IT Manager" || admin.Role.Title == "Marketing Manager" || admin.Role.Title == "Finance Manager")
            {
                DepartmentPermissions();
            }

            // render
            this.renderRequestTable();
            this.renderStaffTable();
            this.renderAdminTable();
            this.renderScheduleMembers();
            this.renderDepartmentsTable();
            this.RenderRolesTable();
            this.renderDailySchedule();
            this.renderProductsTable();
            this.renderProductsForOrder();
        }


        private void Home_Load(object sender, EventArgs e)
        {
            ChangeHomeStyle();
        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            showPanel(pnlDashBoard);
        }


        private void BtnViewStaff_Click(object sender, EventArgs e)
        {
            showPanel(pnlEmployee);
        }

        private void BtnBackToEmployeePageFromDetails_Click_1(object sender, EventArgs e)
        {
            showPanel(pnlEmployee);
            TraverseControlsAndSetTextEmpty(this);
        }


        private void BtnAddSchedule_Click(object sender, EventArgs e)
        {
            showPanel(pnlViewSchedule);
        }

        private void BtnAddStaff_Click(object sender, EventArgs e)
        {
            showPanel(pnlAddEditEmployee);
            hideButton(btnUpdateEmployee);
            showButton(btnAssignEmployee);
            TraverseControlsAndSetTextEmpty(this);
        }

        private void BtnAddAdmins_Click(object sender, EventArgs e)
        {
            showPanel(pnlAddEditAdmin);
            showButton(btnAddUser);
            hideButton(btnUpdateUser);
            TraverseControlsAndSetTextEmpty(this);
        }


        private void BtnDeleteAdmins_Click(object sender, EventArgs e)
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
            showPanel(pnlScheduleEmployees);
            currentScheduleDate = e.End;
            renderScheduleMembers();
        }

        private void btnViewSchedule_Click(object sender, EventArgs e)
        {
            showPanel(pnlViewSchedule);
        }

        private void btnUpdateSchedule_Click(object sender, EventArgs e)
        {
            updateSchedule();
            showPanel(pnlViewSchedule);
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            showPanel(pnlViewSchedule);
        }

        private void btnViewAllSchedules_Click(object sender, EventArgs e)
        {
            showPanel(pnlViewSchedule);
        }

        private void btnViewProducts_Click(object sender, EventArgs e)
        {
            showPanel(pnlProducts);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            showPanel(this.pnlAddEditProduct);
            btnAddProductItem.Visible = true;
            hideButton(btnUpdateProductItem);
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            EditProduct();
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            DeleteProduct();
        }

        private void btnAddProductItem_Click(object sender, EventArgs e)
        {
            AddProduct();
        }

        private void btnUpdateProductItem_Click(object sender, EventArgs e)
        {
            UpdateProduct();
        }

        private void btnViewCart_Click(object sender, EventArgs e)
        {
            showPanel(pnlCart);
        }

        private void btnViewEmployeeDetails_Click(object sender, EventArgs e)
        {
            ShowEmployeeDetails();
        }

        private void btnViewProduct_Click(object sender, EventArgs e)
        {
            ShowProductDetails();
        }

        private void btnBackToUserPage_Click(object sender, EventArgs e)
        {
            showPanel(pnlAdmins);
            TraverseControlsAndSetTextEmpty(this);
        }

        private void btnBackToEmployeePage_Click(object sender, EventArgs e)
        {
            showPanel(pnlEmployee);
            TraverseControlsAndSetTextEmpty(this);
        }
        private void btnBackToProductPageFromAddEdit_Click(object sender, EventArgs e)
        {
            showPanel(pnlProducts);
            TraverseControlsAndSetTextEmpty(this);
        }

        private void btnBackToProductPage_Click(object sender, EventArgs e)
        {
            showPanel(pnlProducts);
            TraverseControlsAndSetTextEmpty(this);
        }

        private void btnBackToEmployeePageFromDetails_Click(object sender, EventArgs e)
        {
            showPanel(pnlEmployee);
            TraverseControlsAndSetTextEmpty(this);
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
            dtPrd.Columns.Add("Department", typeof(string));
            dtPrd.Columns.Add("Title", typeof(string));
            dtPrd.Columns.Add("Quantity", typeof(string));
            dtPrd.Columns.Add("QuantityDepo", typeof(string));
            dtPrd.Columns.Add("Selling Price", typeof(string));
            dtPrd.Columns.Add("Buying Price", typeof(string));
            dtPrd.Columns.Add("Needs Refill", typeof(string));
            dtPrd.Columns.Add("Threshold", typeof(string));
            dtPrd.Columns.Add("Revenue", typeof(string));
            GetDepartments();

            // product count
            tbTotalProducts.Text = productControl.GetProductsCount().ToString();

            // total revenue
            double totalRevenue = productControl.GetProducts().Sum(item => item.OverallPrice);
            tbTotalRevenue.Text = totalRevenue.ToString();

            // max revenue
            double mostRevenue = productControl.GetProducts().Max(item => item.OverallPrice);
            tbMostRevenue.Text = mostRevenue.ToString();

            foreach (Product p in productControl.GetProducts())
            {
                string refill = "No";
                if (p.Quantity <= p.ThreshHold)
                {
                    refill = "Yes";
                }
                dtPrd.Rows.Add(false, p.Id, p.getDepartmentName(), p.Title, p.Quantity, p.QuantityDepo, p.SellingPrice, p.BuyingPrice, refill, p.ThreshHold, p.OverallPrice);
            }
            dtProducts.DataSource = dtPrd;
        }

        private bool ViewSelectedDepartmentOfProduct()
        {
            Department d = (Department)cbSelectedDepartmentProducts.SelectedItem;
            Admin admin = adminControl.getloggedUser();
            if (admin.Role.Title == "IT Manager")
            {
                if (d.Id == 2 || d.Id == 3)
                {
                    MessageBox.Show("You don't have permission to view this department!");
                    return false;
                }
                return true;
            }
            if (admin.Role.Title == "Marketing Manager")
            {
                if (d.Id == 1 || d.Id == 3)
                {
                    MessageBox.Show("You don't have permission to view this department!");
                    return false;
                }
                return true;
            }
            if (admin.Role.Title == "Finance Manager")
            {
                if (d.Id == 1 || d.Id == 2)
                {
                    MessageBox.Show("You don't have permission to view this department!");
                    return false;
                }
                return true;
            }
            return true;
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
                int quantity = Convert.ToInt32(tbProductQuantity.Text);
                int quantitydepo = Convert.ToInt32(tbProductQuantityDepo.Text);
                double selling_price = Convert.ToDouble(tbProductSellingPrice.Text);
                double buying_price = Convert.ToDouble(tbProductBuyingPrice.Text);
                int threshold = Convert.ToInt32(tbProductThreshHold.Text);
                Department department = (Department)cbProductDepartment.SelectedItem;
                string[] product_bindings = {department.Id.ToString(), tbProductTitle.Text, tbProductDescription.Text, quantity.ToString(), quantitydepo.ToString(),
                selling_price.ToString(), buying_price.ToString() , threshold.ToString()};
                productControl.AddProduct(product_bindings);
                MessageBox.Show("you have successfully created a product!");
                this.renderProductsTable();
                this.productControl.retrieveAllProducts();
                this.showPanel(pnlProducts);
                return;
            }
            foreach (string message in errors)
            {
                MessageBox.Show(message);
            }
            TraverseControlsAndSetTextEmpty(this);
        }


        private void EditProduct()
        {
            btnAddProductItem.Visible = false;
            btnUpdateProductItem.Visible = true;
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
                        tbProductTitle.Text = product.Title;
                        tbProductDescription.Text = product.Description;
                        tbProductQuantity.Text = Convert.ToString(product.Quantity);
                        tbProductQuantityDepo.Text = Convert.ToString(product.QuantityDepo);
                        tbProductSellingPrice.Text = Convert.ToString(product.SellingPrice);
                        tbProductBuyingPrice.Text = Convert.ToString(product.BuyingPrice);
                        tbProductThreshHold.Text = Convert.ToString(product.ThreshHold);
                        tbProductId.Text = Convert.ToString(product.Id);
                        cbProductDepartment.SelectedItem = product.getDepartmentName();
                        productControl.retrieveAllProducts();
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

                string productID = tbProductId.Text;
                Department department = (Department)cbProductDepartment.SelectedItem;
                string[] productData = {department.Id.ToString(), tbProductTitle.Text,tbProductDescription.Text,tbProductQuantity.Text,tbProductQuantityDepo.Text,
                tbProductSellingPrice.Text,tbProductBuyingPrice.Text,tbProductThreshHold.Text, productID };
                MessageBox.Show("You have succesfully updated information for that product!");
                productControl.UpdateProduct(productData);
                this.renderProductsTable();
                productControl.retrieveAllProducts();
                this.showPanel(this.pnlProducts);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            TraverseControlsAndSetTextEmpty(this);
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
                        productControl.DeleteProduct(product);
                        found = true;
                    }

                    productControl.retrieveAllProducts();
                    renderProductsTable();


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
                    lblProductTitle.Text = product.Title;
                    lblProductDescription.Text = product.Description;
                    lblProductQuantity.Text = Convert.ToString(product.Quantity);
                    lblProductQuantityDepo.Text = Convert.ToString(product.QuantityDepo);
                    lblProductSellingPrice.Text = Convert.ToString(product.SellingPrice);
                    lblProductBuyingPrice.Text = Convert.ToString(product.BuyingPrice);
                    lblProductPlaceHolder.Text = Convert.ToString(product.ThreshHold);
                    lblProductID.Text = Convert.ToString(product.Id);
                    lblRevue.Text = Convert.ToString(product.OverallPrice);
                    productControl.retrieveAllProducts();
                    renderProductsTable();
                    showPanel(pnlProductInformation);
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
            GetRoles();
            foreach (Admin admin in adminControl.getAdmins())
            {
                dtEmp.Rows.Add(false, admin.ID, admin.Username, admin.FirstName, admin.LastName, admin.getRoleName());
            }

            dtAdmins.DataSource = dtEmp;

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
                try
                {
                    Role role = (Role)cbAdminRole.SelectedItem;
                    string[] admin_bindings = { tbAdminUserName.Text, tbAdminPassword.Text, tbAdminFirstName.Text, tbAdminLastName.Text, tbAdminEmail.Text, role.Id.ToString() };
                    adminControl.Insert(admin_bindings);
                    MessageBox.Show("You have created a user!");
                    renderAdminTable();
                    showPanel(pnlAdmins);
                    return;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }

            foreach (string message in errors)
            {
                MessageBox.Show(message);
            }

            TraverseControlsAndSetTextEmpty(this);
        }



        private void EditAdmin()
        {
            btnAddUser.Visible = false;
            btnUpdateUser.Visible = true;
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
                    Admin admin = adminControl.getAdminById(Convert.ToInt32(id));
                    tbAdminUserName.Text = admin.Username;
                    tbAdminFirstName.Text = admin.FirstName;
                    tbAdminLastName.Text = admin.LastName;
                    tbAdminEmail.Text = admin.Email;
                    tbAdminPassword.Text = admin.Password;
                    cbAdminRole.SelectedItem = admin.Role;
                    tbAdminId.Text = Convert.ToString(admin.ID);
                    adminControl.retrieveAllAdmins();
                    renderAdminTable();
                    showPanel(pnlAddEditAdmin);
                }
            }

        }



        private void UpdateAdmin()
        {
            try
            {
                string adminId = this.tbAdminId.Text;
                Role role = (Role)cbAdminRole.SelectedItem;
                string[] adminData = { tbAdminUserName.Text, tbAdminPassword.Text, tbAdminFirstName.Text, tbAdminLastName.Text, tbAdminEmail.Text, role.Id.ToString(), adminId };
                adminControl.Update(adminData);
                pnlAdmins.Visible = true;
                MessageBox.Show("You have succesfully update information for that admin!");
                renderAdminTable();
                showPanel(pnlAdmins);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            TraverseControlsAndSetTextEmpty(this);
        }



        private void DeleteAdmin()
        {
            try
            {
                if (MessageBox.Show("Do you want to delete this user?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                        MessageBox.Show("Admin/s have been succesfully deleted");
                        adminControl.Delete(adminID);
                        found = true;
                    }

                    adminControl.retrieveAllAdmins();
                    renderAdminTable();


                    if (!found)
                    {
                        MessageBox.Show("You need to tick the selected box to delete a admin/s");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Can't delete that");
            }

            TraverseControlsAndSetTextEmpty(this);
        }




        private void renderStaffTable()
        {
            DataTable dtEmp = new DataTable();
            dtEmp.Columns.Add("Selected", typeof(bool));
            dtEmp.Columns.Add("ID", typeof(int));
            dtEmp.Columns.Add("Username", typeof(string));
            dtEmp.Columns.Add("First Name", typeof(string));
            dtEmp.Columns.Add("Last Name", typeof(string));
            dtEmp.Columns.Add("Department", typeof(string));
            dtEmp.Columns.Add("Contract", typeof(string));
            dtEmp.Columns.Add("Wage per hour", typeof(string));
            GetDepartments();

            // total employees 
            this.tbeTotalEmployees.Text = Convert.ToString(this.employeeControl.GetEmployeesCount());

            // total hours of working 
            double total = this.employeeControl.getEmployees().Sum(item => item.HoursWorked);
            this.tbeHoursWorkedEmployee.Text = total.ToString();

            // salary to be paid
            double salaryToBePaid = this.employeeControl.getEmployees().Sum(item => item.getSalary());
            this.tbeSalaryToBePaidEmployee.Text = salaryToBePaid.ToString();


            // show employees filtered by department
            foreach (Employee employee in employeeControl.getEmployees(FilterByDepartment()))
            {
                Console.WriteLine(FilterByDepartment());
                dtEmp.Rows.Add(false, employee.ID, employee.UserName, employee.FirstName, employee.LastName, employee.getDepartmentName(), employee.Contract, employee.Wage);
            }
            dtEmployees.DataSource = dtEmp;
        }

        private Department FilterByDepartment()
        {
            Department d = (Department)cbSelectDepartment.SelectedItem;
            if (d != null)
            {
                if (ViewSelectedDepartment())
                {
                    string[] filterDepartment = { d.Id.ToString() };
                    employeeControl.Filter(filterDepartment);
                }
                return d;
            }
            return null;
        }


        private bool ViewSelectedDepartment()
        {
            Department d = (Department)cbSelectDepartment.SelectedItem;
            Admin admin = adminControl.getloggedUser();
            if (admin.Role.Title == "IT Manager")
            {
                if (d.Id == 2 || d.Id == 3)
                {
                    MessageBox.Show("You don't have permission to view this department!");
                    return false;
                }
                return true;
            }
            if (admin.Role.Title == "Marketing Manager")
            {
                if (d.Id == 1 || d.Id == 3)
                {
                    MessageBox.Show("You don't have permission to view this department!");
                    return false;
                }
                return true;
            }
            if (admin.Role.Title == "Finance Manager")
            {
                if (d.Id == 1 || d.Id == 2)
                {
                    MessageBox.Show("You don't have permission to view this department!");
                    return false;
                }
                return true;
            }
            return true;
        }

        private void btnShowEmployees_Click(object sender, EventArgs e)
        {
            FilterByDepartment();
        }


        private void AddEmployee()
        {
            showButton(btnAssignEmployee);
            hideButton(btnUpdateEmployee);
            List<String> errors = new List<string>();
            if (!valuesAreEmptyEmployee())
            {
                errors.Add("You can't create a user without entering all the fields");
            }
            if (employeeControl.GetEmployeeByEmail(tbEmployeeEmail.Text) != null)
            {
                errors.Add("You can't add an user with the same email");
            }

            if (!errors.Any())
            {
                try
                {
                    Department department = (Department)cbEmployeeDepartment.SelectedItem;
                    int contract_id = cbEmployeeContract.SelectedIndex;
                    contract_id++;
                    string increased_contract_id = Convert.ToString(contract_id);
                    string gender = Convert.ToString(cbEmployeeGender.Text);
                    string[] employee_bindings = { tbEmployeeFirstName.Text, tbEmployeeLastName.Text, tbEmployeeAdress.Text,
                    tbEmployeeCity.Text, tbEmployeeCountry.Text, tbEmployeeWagePerHour.Text, tbEmployeePhoneNumber.Text,
                    gender, tbEmployeeEmail.Text,department.Id.ToString(),increased_contract_id,tbEmployeeUserName.Text,tbEmployeePassword.Text};
                    employeeControl.AddEmployee(employee_bindings);
                    MessageBox.Show("You have succesfully created an employee!");
                    this.renderScheduleMembers();
                    this.renderStaffTable();
                    this.showPanel(pnlEmployee);
                    return;

                }
                catch (Exception e)
                {
                    MessageBox.Show("There's been an exception:  " + e.Message);
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
                    string employeeId = (dataRow.Cells["ID"].Value.ToString());
                    Employee employee = employeeControl.getEmployeeById(Convert.ToInt32(employeeId));
                    tbEmployeeUserName.Text = employee.UserName;
                    tbEmployeePassword.Text = employee.Password;
                    tbEmployeeFirstName.Text = employee.FirstName;
                    tbEmployeeLastName.Text = employee.LastName;
                    tbEmployeeEmail.Text = employee.Email;
                    tbEmployeeAdress.Text = employee.Adress;
                    tbEmployeePhoneNumber.Text = employee.PhoneNumber;
                    tbEmployeeCity.Text = employee.City;
                    tbEmployeeId.Text = Convert.ToString(employee.ID);
                    tbEmployeeCountry.Text = employee.Country;
                    tbEmployeeWagePerHour.Text = employee.Wage.ToString();
                    cbEmployeeDepartment.SelectedItem = employee.Department.Title;
                    cbEmployeeContract.SelectedItem = Convert.ToString(employee.Contract);
                    cbEmployeeGender.SelectedItem = Convert.ToString(employee.Gender);
                    showPanel(pnlAddEditEmployee);
                    employeeControl.retrieveAllEmployees();
                    renderStaffTable();
                    renderScheduleMembers();
                }
            }


        }


        private void UpdateEmployee()
        {

            if (!valuesAreEmptyEmployee())
            {
                MessageBox.Show("Please enter all fields for updating employee!");
                return;
            }
            try
            {
                string employeeId = tbEmployeeId.Text;
                string gender = Convert.ToString(this.cbEmployeeGender.SelectedItem);
                Department department = (Department)cbEmployeeDepartment.SelectedItem;
                int contract_id = cbEmployeeContract.SelectedIndex;
                contract_id++;
                string increased_contract_id = Convert.ToString(contract_id);
                string[] employeeData = {tbEmployeeFirstName.Text,tbEmployeeLastName.Text,tbEmployeeAdress.Text,
                    tbEmployeeCity.Text, tbEmployeeCountry.Text,
                    tbEmployeePhoneNumber.Text,gender,tbEmployeeEmail.Text,department.Id.ToString(),increased_contract_id,
                    tbEmployeeWagePerHour.Text,tbEmployeeUserName.Text,tbEmployeePassword.Text,employeeId};
                showPanel(pnlEmployee);
                employeeControl.UpdateEmployee(employeeData);
                MessageBox.Show("You have succesfully update information for that employee!");
                renderStaffTable();
                renderScheduleMembers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void DeleteEmployee()
        {
            try
            {
                if (MessageBox.Show("Do you want to delete this employee/s?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                    renderStaffTable();
                    renderScheduleMembers();

                    if (!found)
                    {
                        MessageBox.Show("You need to tick the selected box to delete an employee");
                    }
                }
            }
            catch (Exception e)
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
                        tbeDetailsUserName.Text = employee.UserName;
                        tbeDetailsFirstName.Text = employee.FirstName;
                        tbeDetailsLastName.Text = employee.LastName;
                        tbeDetailsEmail.Text = employee.Email;
                        tbeDetailsAddress.Text = employee.Adress;
                        tbeDetailsCity.Text = employee.City;
                        tbeDetailsCountry.Text = employee.Country;
                        tbeDetailsPhone.Text = employee.PhoneNumber;
                        tbeDetailsTotalHours.Text = employee.HoursWorked.ToString();
                        tbeDetailsWage.Text = employee.Wage.ToString();
                        tbeDetailsShiftsTaken.Text = employee.ShiftsTaken.ToString();
                        tbeDetailsTotalSalary.Text = employee.getSalary().ToString();
                        tbeDetailsContact.Text = Convert.ToString(employee.Contract);
                        tbeDetailsGender.Text = Convert.ToString(employee.Gender);
                        tbeDetailsDepartment.Text = Convert.ToString(employee.Department);
                        tbeDetailsId.Text = Convert.ToString(employee.ID);
                        employeeControl.retrieveAllEmployees();
                        renderStaffTable();
                        showPanel(this.pnlViewEmployeeDetails);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
        }

        private void updateSchedule()
        {
            //get if there is a schedule about this date
            //if yes remove all of the database members and add new ones

            //add the new ones
            //if not create schedule in db with the employees in related table

            Schedule schedule = scheduleControl.getScheduleByDate(currentScheduleDate);

            if (schedule != null)
            {

                string[] delete_bindings = { schedule.ID.ToString() };
                scheduleControl.Delete(delete_bindings);
            }

            string[] bindings = { "", currentScheduleDate.ToString("MM-dd-yyyy") };
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
                string from = currentScheduleDate.ToString("MM-dd-yyyy") + " " + dataRow.Cells["From"].Value.ToString();
                string to = currentScheduleDate.ToString("MM-dd-yyyy") + " " + dataRow.Cells["To"].Value.ToString();

                if (userChecked)
                {

                    DateTime from_date;
                    DateTime to_date;
                    string member_id = (dataRow.Cells["ID"].Value.ToString());
                    if (DateTime.TryParse(from, out from_date) && DateTime.TryParse(to, out to_date))
                    {
                        string[] member_data = { id.ToString(), member_id.ToString(), from, to, (to_date.Hour - from_date.Hour).ToString() };

                        //check if the datetime string is really a datetime and if yes safe the user to the db
                        scheduleControl.InsertMember(member_data);
                        user_count++;
                    }
                    else
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
            employeeControl.retrieveAllEmployees();
            renderScheduleMembers();
            renderDailySchedule();
            renderStaffTable();

            MessageBox.Show("You successfully updated the schedule");

            calendarSchedule.SelectionRange.End = currentScheduleDate;

        }


        private void renderScheduleMembers()
        {

            Schedule schedule = scheduleControl.getScheduleByDate(currentScheduleDate);
            calendarSchedule.AnnuallyBoldedDates = null;
            List<DateTime> coloredDates = new List<DateTime>();

            foreach (Schedule found_schedule in scheduleControl.getSchedules())
            {
                if (found_schedule.Members.Count() >= 5)
                {
                    coloredDates.Add(found_schedule.Date);
                }
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
            if (tbProductDescription.Text == "" || tbProductDescription.Text == "" || tbProductTitle.Text == "" ||
                tbProductQuantity.Text == "" || tbProductSellingPrice.Text == "" || tbProductBuyingPrice.Text == "" || tbProductThreshHold.Text == "")
            {
                return false;
            }
            return true;
        }


        private bool valuesAreEmptyEmployee()
        {
            if (tbEmployeeFirstName.Text == "" || tbEmployeeLastName.Text == "" ||
                tbEmployeeAdress.Text == "" || tbEmployeeCity.Text == "" ||
                tbEmployeeCountry.Text == "" || tbEmployeeEmail.Text == "" || cbEmployeeContract == null ||
                cbEmployeeGender.SelectedItem == null || tbEmployeePhoneNumber == null)
            {
                return false;
            }
            return true;
        }


        private bool valuesAreEmptyAdmin()
        {
            if (tbAdminUserName.Text == "" || tbAdminFirstName.Text == "" ||
                tbAdminLastName.Text == "" || tbAdminPassword.Text == "" ||
                tbAdminEmail.Text == "")
            {
                return false;
            }
            return true;
        }


        private void ChangeHomeStyle()
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
            this.pnlAdmins.Visible = false;
            this.pnlAddEditAdmin.Visible = false;
            this.pnlScheduleEmployees.Visible = false;
            this.pnlProducts.Visible = false;
            this.pnlAddEditProduct.Visible = false;
            this.pnlCart.Visible = false;
            this.pnlProductInformation.Visible = false;
            this.pnlViewEmployeeDetails.Visible = false;
            this.pnlRestocking.Visible = false;
            this.pnlDepartments.Visible = false;
            this.pnlAddEditDepartment.Visible = false;
            this.pnlDepartmentStats.Visible = false;
            this.pnlRoles.Visible = false;
            this.pnlAddEditRole.Visible = false;
            this.pnlRolesStats.Visible = false;
            panel.Visible = true;

        }

        private void hideButton(Button button)
        {
            button.Visible = false;
        }

        private void showButton(Button button)
        {
            button.Visible = true;
        }

        private void hidePicture(PictureBox p)
        {
            p.Visible = false;
        }

        private void ManagerPermissions()
        {
            this.hideButton(btnSchedulesPage);
            this.hideButton(btnViewAllSchedules);
            this.hideButton(btnAddEmployee);
            this.hideButton(btnEditEmployee);
            this.hideButton(btnDeleteEmployee);
            this.hideButton(btnAddProductPage);
            this.hideButton(btnEditProductPage);
            this.hideButton(btnDeleteProduct);
            this.hideButton(btnUsersPage);
            this.hideButton(btnCartPage);
            this.hideButton(btnRolesPage);
            this.hideButton(btnDepartmentPage);

            this.hideButton(this.btnAddQuantity);
            this.tbQuantityRequest.Visible = false;
            this.hideLabel(this.materialLabel43);
            this.hideLabel(this.materialLabel75);
            this.hidePicture(iconPictureBox19);
            this.hidePicture(iconPictureBox20);
            this.hidePicture(iconPictureBox15);
            this.hidePicture(iconPictureBox10);
            this.hidePicture(iconPictureBox9);
            this.hidePicture(iconPictureBox5);
            this.hidePicture(iconPictureBox6);
            this.hidePicture(iconPictureBox7);
            this.hidePicture(iconPictureBox18);
            this.hidePicture(iconPictureBox11);
            this.hidePicture(iconPictureBox3);
            this.hidePicture(iconPictureBox16);
        }

        private void DepartmentPermissions()
        {
            this.hideButton(btnSchedulesPage);
            this.hideButton(btnViewAllSchedules);
            this.hideButton(btnAddEmployee);
            this.hideButton(btnEditEmployee);
            this.hideButton(btnDeleteEmployee);
            this.hideButton(btnAddProductPage);
            this.hideButton(btnEditProductPage);
            this.hideButton(btnDeleteProduct);
            this.hideButton(btnUsersPage);
            this.hideButton(btnCartPage);
            this.hideButton(btnRolesPage);
            this.hideButton(btnDepartmentPage);
            this.hideButton(btnDashboardPage);

            this.hidePicture(iconPictureBox19);
            this.hidePicture(iconPictureBox20);
            this.hidePicture(iconPictureBox15);
            this.hidePicture(iconPictureBox10);
            this.hidePicture(iconPictureBox9);
            this.hidePicture(iconPictureBox5);
            this.hidePicture(iconPictureBox6);
            this.hidePicture(iconPictureBox7);
            this.hidePicture(iconPictureBox18);
            this.hidePicture(iconPictureBox11);
            this.hidePicture(iconPictureBox3);
            this.hidePicture(iconPictureBox16);
            this.hidePicture(iconPictureBox1);
        }

        private void StoreManagerPermissions()
        {

            this.hideButton(btnSchedulesPage);
            this.hideButton(btnViewAllSchedules);
            this.hideButton(btnAddEmployee);
            this.hideButton(btnEditEmployee);
            this.hideButton(btnDeleteEmployee);
            this.hideButton(btnAddProductPage);
            this.hideButton(btnEditProductPage);
            this.hideButton(btnDeleteProduct);
            this.hideButton(btnUsersPage);
            this.hideButton(btnCartPage);
            this.hideButton(btnDashboardPage);
            this.hideButton(btnEmployeesPage);
            this.hideButton(btnProductsPage);
            this.showPanel(pnlRestocking);

            gpRequestProduct.Visible = false;
            this.hideButton(btnRolesPage);
            this.hideButton(btnDepartmentPage);

            this.hidePicture(iconPictureBox19);
            this.hidePicture(iconPictureBox20);

            this.hidePicture(iconPictureBox16);
            this.hidePicture(iconPictureBox8);
            this.hidePicture(iconPictureBox2);
            this.hidePicture(iconPictureBox1);
            this.hidePicture(iconPictureBox15);
            this.hidePicture(iconPictureBox10);
            this.hidePicture(iconPictureBox9);
            this.hidePicture(iconPictureBox5);
            this.hidePicture(iconPictureBox6);
            this.hidePicture(iconPictureBox7);
            this.hidePicture(iconPictureBox18);
            this.hidePicture(iconPictureBox11);
            this.hidePicture(iconPictureBox3);
            this.hidePicture(iconPictureBox16);
        }

        private void tbAddToCart_Click(object sender, EventArgs e)
        {
            Product product = (Product)lbStoreProducts.SelectedItem;
            int quantity = Convert.ToInt32(tbQuantity.Text);
            if (product.Quantity < quantity || quantity <= 0)
            {
                MessageBox.Show("The desired quanity of the product is not in stock");
                return;
            }

            ordersControl.addProduct(product, quantity);
            lbShoppingCart.Items.Add($"{product.Title} - {quantity}");
        }

        private void tbPurchase_Click_2(object sender, EventArgs e)
        {
            Admin admin = adminControl.getloggedUser();

            if (ordersControl.Products.Count() == 0)
            {
                MessageBox.Show("Your basket is empty");
                return;
            }

            string[] bindings = { admin.ID.ToString() };
            ordersControl.Buy(bindings);
            lbShoppingCart.Items.Clear();
            productControl.retrieveAllProducts();
            renderProductsForOrder();
            renderProductsTable();
            MessageBox.Show("You successfully made an order!");

        }

        private void btnAddExtraQuantity_Click(object sender, EventArgs e)
        {
            this.SendProductRequest();
        }

        private void renderRequestTable()
        {
            lbRestocking.Items.Clear();
            foreach (Request request in requestControl.GetRequests())
            {
                lbRestocking.Items.Add(request);
            }
        }

        private void SendProductRequest()
        {
            if (MessageBox.Show("Do you want to send a request for this product?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    string id = lblProductID.Text;
                    string quantity = tbQuantityRequest.Text;

                    string[] requestBindings = { id, quantity };

                    showPanel(pnlProducts);
                    requestControl.Insert(requestBindings);
                    requestControl.retrieveAllRequests();
                    renderRequestTable();
                }
                catch (Exception e)
                {
                    MessageBox.Show("There's been an exception: " + e.Message);
                }
            }
            else
            {
                MessageBox.Show("You pressed no!");
            }
        }

        private void hideLabel(Label l)
        {
            l.Visible = false;
        }

        private void btnRestockProducts_Click(object sender, EventArgs e)
        {
            Request request = (Request)lbRestocking.SelectedItem;
            Product product = productControl.GetProductById(Convert.ToInt32(request.ProductId));
            string[] product_bindings = { (Convert.ToInt32(request.Quantity) + Convert.ToInt32(product.Quantity)).ToString(), request.ProductId };
            string[] deleteBindings = { request.Id.ToString() };
            requestControl.Approve(product_bindings);
            requestControl.Delete(deleteBindings);
            requestControl.retrieveAllRequests();
            renderRequestTable();
            MessageBox.Show("Request sucessfully approved");
        }

        private void dtProducts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in this.dtProducts.Rows)
            {
                string status = row.Cells[8].Value.ToString();

                if (status == "Yes")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(220, 20, 60);
                }
            }
        }

        private void btnDepartmentPage_Click(object sender, EventArgs e)
        {
            showPanel(pnlDepartments);
        }

        private void btnAddDepartmentPage_Click(object sender, EventArgs e)
        {
            showPanel(pnlAddEditDepartment);
            btnAddDepartment.Visible = true;
            hideButton(btnUpdateDepartment);
        }


        public void AddDepartment()
        {
            hideButton(btnUpdateDepartment);

            Department d = departmentControl.GetDepartmentsByTitle(tbDepartmentTitle.Text);

            if (d != null)
            {
                MessageBox.Show("Department with title: " + tbDepartmentTitle.Text + " Already exist!");
                return;
            }

            if (tbDepartmentTitle.Text == "")
            {
                MessageBox.Show("You cant leave the title of department empty!");
                return;
            }

            try
            {
                string title = tbDepartmentTitle.Text;
                string[] department_bindings = { title };
                departmentControl.Insert(department_bindings);
                departmentControl.retrieveAllDepartments();
                renderDepartmentsTable();
                showPanel(pnlDepartments);
                GetDepartments();
            }
            catch (Exception e)
            {
                MessageBox.Show("There's been an exception: " + e.Message);
            }
            TraverseControlsAndSetTextEmpty(this);
        }


        public void EditDepartment()
        {
            hideButton(btnAddDepartment);
            showButton(btnUpdateDepartment);
            for (int i = 0; i < dtDepartments.Rows.Count; ++i)
            {
                DataGridViewRow dataRow = dtDepartments.Rows[i];

                if (dataRow.IsNewRow)
                {
                    continue;
                }

                bool selectedDepartment = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());
                if (selectedDepartment)
                {
                    string id = dataRow.Cells["ID"].Value.ToString();
                    Department department = departmentControl.GetDepartmentsByID(Convert.ToInt32(id));
                    tbDepartmentTitle.Text = department.Title;
                    tbDepartmentId.Text = Convert.ToString(department.Id);
                    departmentControl.retrieveAllDepartments();
                    renderDepartmentsTable();
                    showPanel(pnlAddEditDepartment);
                }
            }
        }


        public void UpdateDepartment()
        {
            hideButton(btnAddDepartment);

            Department d = departmentControl.GetDepartmentsByTitle(tbDepartmentTitle.Text);

            if (d != null)
            {
                MessageBox.Show("Department with title: " + tbDepartmentTitle.Text + " Already exist!");
                return;
            }

            if (tbDepartmentTitle.Text == "")
            {
                MessageBox.Show("You cant leave the title of department empty!");
                return;
            }


            try
            {
                string title = tbDepartmentTitle.Text;
                string[] department_bindings = { title, tbDepartmentId.Text };
                departmentControl.Update(department_bindings);
                departmentControl.retrieveAllDepartments();
                renderDepartmentsTable();
                showPanel(pnlDepartments);
                MessageBox.Show("You have succesfully updated the department!");
                GetDepartments();
            }
            catch (Exception e)
            {
                MessageBox.Show("There's been an exception: " + e.Message);
            }
            TraverseControlsAndSetTextEmpty(this);
        }


        private void DeleteDepartment()
        {
            try
            {
                if (MessageBox.Show("Do you want to delete this department?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool found = false;
                    for (int i = 0; i < dtDepartments.Rows.Count; ++i)
                    {

                        DataGridViewRow dataRow = dtDepartments.Rows[i];
                        bool selectedDepartment = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());

                        if (dataRow.IsNewRow || !selectedDepartment)
                        {
                            continue;
                        }

                        string id = (dataRow.Cells["ID"].Value.ToString());
                        string[] getID = { id };
                        departmentControl.Delete(getID);
                        GetDepartments();
                        found = true;
                    }

                    if (found)
                    {
                        MessageBox.Show("Department have been succesfully deleted");
                    }

                    productControl.retrieveAllProducts();
                    renderDepartmentsTable();

                    if (!found)
                    {
                        MessageBox.Show("You need to tick the selected box to delete a department");
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("There's been an exception!" + e.Message);
            }
        }


        private void ShowDepartmentDetails()
        {
            for (int i = 0; i < dtDepartments.Rows.Count; ++i)
            {
                DataGridViewRow dataRow = dtDepartments.Rows[i];

                if (dataRow.IsNewRow)
                {
                    continue;
                }

                bool selectedDepartment = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());
                if (selectedDepartment)
                {
                    string id = dataRow.Cells["ID"].Value.ToString();
                    Department d = departmentControl.GetDepartmentsByID(Convert.ToInt32(id));
                    tbStatisticsTitle.Text = d.Title;
                    tbStatisticsDepId.Text = Convert.ToString(d.Id);
                    departmentControl.retrieveAllDepartments();
                    renderDepartmentsTable();
                    showPanel(pnlDepartmentStats);
                }
            }

        }


        private void renderDepartmentsTable()
        {
            DataTable dtDep = new DataTable();
            dtDep.Columns.Add("Selected", typeof(bool));
            dtDep.Columns.Add("ID", typeof(int));
            dtDep.Columns.Add("Title", typeof(string));
            foreach (Department d in departmentControl.GetDepartments())
            {
                dtDep.Rows.Add(false, d.Id, d.Title);
            }

            dtDepartments.DataSource = dtDep;
        }

        private void BtnAddDepartment_Click(object sender, EventArgs e)
        {
            AddDepartment();
        }

        private void BtnBackToDepartmentPage_Click(object sender, EventArgs e)
        {
            showPanel(pnlDepartments);
            TraverseControlsAndSetTextEmpty(this);
        }

        private void btnBackToDepartmentPageFromStatistics_Click(object sender, EventArgs e)
        {
            showPanel(pnlDepartments);
            TraverseControlsAndSetTextEmpty(this);
        }

        private void btnUpdateDepartment_Click(object sender, EventArgs e)
        {
            UpdateDepartment();
        }

        private void btnEditDepartmentPage_Click(object sender, EventArgs e)
        {
            EditDepartment();
        }

        private void TraverseControlsAndSetTextEmpty(Control control)
        {
            foreach (Control c in control.Controls)
            {
                var box = c as TextBox;
                if (box != null)
                {
                    box.Text = string.Empty;
                }

                this.TraverseControlsAndSetTextEmpty(c);
            }
        }

        private void btnDeleteDepartment_Click(object sender, EventArgs e)
        {
            DeleteDepartment();
        }

        private void btnViewDepartment_Click(object sender, EventArgs e)
        {
            ShowDepartmentDetails();
        }

        private void btnRolesPage_Click(object sender, EventArgs e)
        {
            showPanel(pnlRoles);
        }

        private void btnAddRolePage_Click(object sender, EventArgs e)
        {
            showPanel(pnlAddEditRole);
            hideButton(btnUpdateRole);
            showButton(btnAddRole);
        }

        private void btnViewRolePage_Click(object sender, EventArgs e)
        {
            ShowRoleDetails();
        }

        private void btnEditRolePage_Click(object sender, EventArgs e)
        {
            EditRole();
        }

        private void RenderRolesTable()
        {
            DataTable dtRole = new DataTable();
            dtRole.Columns.Add("Selected", typeof(bool));
            dtRole.Columns.Add("ID", typeof(string));
            dtRole.Columns.Add("Role", typeof(string));
            dtRole.Columns.Add("Department", typeof(string));
            GetDepartments();
            foreach (Role role in roleControl.GetRoles())
            {
                dtRole.Rows.Add(false, role.Id, role.Title, role.getDepartmentName());
            }

            dtRoles.DataSource = dtRole;
        }

        private void GetRoles()
        {
            cbAdminRole.Items.Clear();
            foreach (Role roles in roleControl.GetRoles())
            {
                cbAdminRole.Items.Add(roles);
            }
        }

        private void GetDepartments()
        {
            cbEmployeeDepartment.Items.Clear();
            cbProductDepartment.Items.Clear();
            cbRoleDepartment.Items.Clear();
            cbSelectDepartment.Items.Clear();
            cbSelectedDepartmentProducts.Items.Clear();
            foreach (Department department in departmentControl.GetDepartments())
            {
                cbEmployeeDepartment.Items.Add(department);
                cbProductDepartment.Items.Add(department);
                cbRoleDepartment.Items.Add(department);
                cbSelectDepartment.Items.Add(department);
                cbSelectedDepartmentProducts.Items.Add(department);
            }
        }

        public void AddRole()
        {
            hideButton(btnUpdateRole);
            showButton(btnAddRole);
            Role r = roleControl.GetRoleByTitle(tbRoleTitle.Text);


            if (r != null)
            {
                MessageBox.Show("Role with title: " + tbRoleTitle.Text + " Already exist!");
                return;
            }

            if (tbRoleTitle.Text == "")
            {
                MessageBox.Show("You cant leave the title of role empty!");
                return;
            }

            try
            {
                string title = tbRoleTitle.Text;
                Department department = (Department)cbRoleDepartment.SelectedItem;
                string[] role_bindings = { title, department.Id.ToString() };
                MessageBox.Show("You have succesfully added a role!");
                roleControl.Insert(role_bindings);
                roleControl.retrieveAllRoles();
                GetRoles();
                showPanel(pnlRoles);
                RenderRolesTable();
            }
            catch (Exception e)
            {
                MessageBox.Show("There's been an exception: " + e.Message);
            }
            TraverseControlsAndSetTextEmpty(this);
        }

        public void EditRole()
        {
            hideButton(btnAddRole);
            showButton(btnUpdateRole);
            for (int i = 0; i < dtRoles.Rows.Count; ++i)
            {
                DataGridViewRow dataRow = dtRoles.Rows[i];

                if (dataRow.IsNewRow)
                {
                    continue;
                }

                bool selectedRole = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());
                if (selectedRole)
                {
                    string id = dataRow.Cells["ID"].Value.ToString();
                    Role role = roleControl.GetRoleById(Convert.ToInt32(id));
                    tbRoleTitle.Text = role.Title;
                    tbRoleId.Text = Convert.ToString(role.Id);
                    roleControl.retrieveAllRoles();
                    RenderRolesTable();
                    showPanel(pnlAddEditRole);
                }
            }
        }

        public void UpdateRole()
        {
            hideButton(btnAddRole);
            showButton(btnUpdateRole);

            Role r = roleControl.GetRoleByTitle(tbRoleTitle.Text);


            if (r != null)
            {
                MessageBox.Show("Role with title: " + tbRoleTitle.Text + " Already exist!");
                return;
            }

            if (tbRoleTitle.Text == "")
            {
                MessageBox.Show("You cant leave the title of role empty!");
                return;
            }


            try
            {
                string title = tbRoleTitle.Text;
                Department department = (Department)cbRoleDepartment.SelectedItem;
                string[] role_bindings = { title, department.Id.ToString(), tbRoleId.Text };
                roleControl.Update(role_bindings);
                roleControl.retrieveAllRoles();
                RenderRolesTable();
                showPanel(pnlRoles);
                GetRoles();
                MessageBox.Show("You have succesfully updated the role!");
            }
            catch (Exception e)
            {
                MessageBox.Show("There's been an exception: " + e.Message);
            }
            TraverseControlsAndSetTextEmpty(this);
        }

        public void DeleteRole()
        {
            try
            {
                if (MessageBox.Show("Do you want to delete this role/s?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool found = false;
                    for (int i = 0; i < dtRoles.Rows.Count; ++i)
                    {

                        DataGridViewRow dataRow = dtRoles.Rows[i];
                        bool selectedRole = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());

                        if (dataRow.IsNewRow || !selectedRole)
                        {
                            continue;
                        }

                        try
                        {
                            string id = (dataRow.Cells["ID"].Value.ToString());
                            string[] getID = { id };
                            roleControl.Delete(getID);
                            found = true;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("You can't delete a role which is already assigned to a person!");
                        }
                    }

                    if (found)
                    {
                        MessageBox.Show("Role/s have been succesfully deleted");
                    }
                    roleControl.retrieveAllRoles();
                    RenderRolesTable();
                    GetRoles();
                    if (!found)
                    {
                        MessageBox.Show("You need to tick the selected box to delete a role");
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("There's been an exception!" + e.Message);
            }
        }

        private void ShowRoleDetails()
        {
            for (int i = 0; i < dtRoles.Rows.Count; ++i)
            {
                DataGridViewRow dataRow = dtRoles.Rows[i];

                if (dataRow.IsNewRow)
                {
                    continue;
                }

                bool selectedRole = Convert.ToBoolean(dataRow.Cells["Selected"].Value.ToString());
                if (selectedRole)
                {
                    string id = dataRow.Cells["ID"].Value.ToString();
                    Role role = roleControl.GetRoleById(Convert.ToInt32(id));
                    tbViewTitle.Text = role.Title;
                    tbViewId.Text = Convert.ToString(role.Id);
                    roleControl.retrieveAllRoles();
                    RenderRolesTable();
                    showPanel(pnlRolesStats);
                }
            }

        }

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            AddRole();
        }

        private void btnBackToRolePage_Click(object sender, EventArgs e)
        {
            showPanel(pnlRoles);
            TraverseControlsAndSetTextEmpty(this);
        }

        private void btnUpdateRole_Click(object sender, EventArgs e)
        {
            UpdateRole();
        }

        private void btnDeleteRole_Click(object sender, EventArgs e)
        {
            DeleteRole();
        }

        private void btnBackToRolePageFromView_Click(object sender, EventArgs e)
        {
            showPanel(pnlRoles);
        }

        private void btnUsersPage_Click(object sender, EventArgs e)
        {
            showPanel(pnlAdmins);
        }

        
    }
}
