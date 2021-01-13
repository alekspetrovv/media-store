using PapaSenpai_Project_Software.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
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
            Permissions();

            // statistics
            DepartmentStatistics();

            // render
            GetDepartments();
            GetRoles();
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

        // buttons
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
            MessageBox.Show("Request/s has been sucessfully approved");
        }


        // dashboard buttons
        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            showPanel(pnlDashBoard);
        }



        // role buttons
        private void btnAddRole_Click(object sender, EventArgs e)
        {
            AddRole();
        }

        private void btnBackToRolePage_Click(object sender, EventArgs e)
        {
            showPanel(pnlRoles);
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




        // product buttons
        private void btnShowProductDetails_Click(object sender, EventArgs e)
        {
            ShowProductDetails();
        }

        private void btnAddProductPage_Click(object sender, EventArgs e)
        {
            showPanel(pnlAddEditProduct);
            showButton(btnAddProductItem);
            hideButton(btnUpdateProductItem);
        }

        private void btnEditProductPage_Click(object sender, EventArgs e)
        {
            showPanel(pnlAddEditProduct);
            hideButton(btnAddProductItem);
            showButton(btnUpdateProductItem);
            EditProduct();
        }

        private void btnShowProducts_Click(object sender, EventArgs e)
        {
            renderProductsTable();
        }

        private void btnViewProducts_Click(object sender, EventArgs e)
        {
            showPanel(pnlProducts);
        }

        private void btnViewProduct_Click(object sender, EventArgs e)
        {
            ShowProductDetails();
        }

        private void btnBackToProductPage_Click(object sender, EventArgs e)
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

        private void btnAddProductItem_Click(object sender, EventArgs e)
        {
            AddProduct();
        }

        private void btnUpdateProductItem_Click(object sender, EventArgs e)
        {
            UpdateProduct();
        }

        private void btnBackToProductPageFromAddEdit_Click(object sender, EventArgs e)
        {
            showPanel(pnlProducts);
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            DeleteProduct();
        }


        // admin buttons


        private void btnUsersPage_Click(object sender, EventArgs e)
        {
            showPanel(pnlAdmins);
        }


        private void btnAddAdmin_Click(object sender, EventArgs e)
        {
            CreateAdmin();
        }


        private void btnEditAdmins_Click(object sender, EventArgs e)
        {
            EditAdmin();
        }


        private void btnUpdateAdmin_Click(object sender, EventArgs e)
        {
            UpdateAdmin();
        }


        private void BtnAddAdmins_Click(object sender, EventArgs e)
        {
            showPanel(pnlAddEditAdmin);
            showButton(btnAddUser);
            hideButton(btnUpdateUser);
        }


        private void BtnDeleteAdmins_Click(object sender, EventArgs e)
        {
            DeleteAdmin();
        }

        private void btnBackToUserPage_Click(object sender, EventArgs e)
        {
            showPanel(pnlAdmins);
        }




        // schedule buttons

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

        private void BtnAddSchedule_Click(object sender, EventArgs e)
        {
            showPanel(pnlViewSchedule);
        }

        private void btnBackToSchedulesPage_Click(object sender, EventArgs e)
        {
            showPanel(pnlViewSchedule);
        }

        private void btnGoToAutomaticSchedulePage_Click(object sender, EventArgs e)
        {
            showPanel(pnlAutomaticSchedule);
        }

        private void btnAutomate_Click(object sender, EventArgs e)
        {
            AutomateSchedule();
        }



        // cart buttons

        private void btnViewCart_Click(object sender, EventArgs e)
        {
            showPanel(pnlCart);
        }

        private void tbAddToCart_Click(object sender, EventArgs e)
        {
            AddProductsToCart();
        }

        private void tbPurchase_Click(object sender, EventArgs e)
        {
            PurchaseProducts();
        }



        // restock buttons 

        private void btnAddExtraQuantity_Click(object sender, EventArgs e)
        {
            SendProductRequest();
        }


        // employee buttons
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            CreateEmployee();
        }

        private void btnBackToEmployeePage_Click(object sender, EventArgs e)
        {
            showPanel(pnlEmployee);
        }

        private void btnViewEmployeeDetails_Click(object sender, EventArgs e)
        {
            ShowEmployeeDetails();
        }

        private void btnShowEmployees_Click(object sender, EventArgs e)
        {
            renderStaffTable();
        }

        private void btnEditEmployee_Click(object sender, EventArgs e)
        {
            EditEmployee();
        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            DeleteEmployee();
        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            UpdateEmployee();
        }

        private void BtnAddStaff_Click(object sender, EventArgs e)
        {
            showPanel(pnlAddEditEmployee);
            hideButton(btnUpdateEmployee);
            showButton(btnAssignEmployee);
        }

        private void BtnViewStaff_Click(object sender, EventArgs e)
        {
            showPanel(pnlEmployee);
        }

        private void btnBackToEmployeePageFromDetails_Click(object sender, EventArgs e)
        {
            showPanel(pnlEmployee);
        }





        // department buttons
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

        private void BtnAddDepartment_Click(object sender, EventArgs e)
        {
            AddDepartment();
        }

        private void BtnBackToDepartmentPage_Click(object sender, EventArgs e)
        {
            showPanel(pnlDepartments);
        }

        private void btnBackToDepartmentPageFromStatistics_Click(object sender, EventArgs e)
        {
            showPanel(pnlDepartments);
        }

        private void btnUpdateDepartment_Click(object sender, EventArgs e)
        {
            UpdateDepartment();
        }

        private void btnEditDepartmentPage_Click(object sender, EventArgs e)
        {
            EditDepartment();
        }

        private void btnDeleteDepartment_Click(object sender, EventArgs e)
        {
            DeleteDepartment();
        }

        private void btnViewDepartment_Click(object sender, EventArgs e)
        {
            ShowDepartmentDetails();
        }

        private void btnShowDepartments_Click(object sender, EventArgs e)
        {
            renderDepartmentsTable();
        }


        // role buttons
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


        // Functions

        // Tables

        private void renderProductsForOrder()
        {
            lbStoreProducts.Items.Clear();
            lbStoreProducts.Items.Add("                           Current items in store:");
            lbStoreProducts.Items.Add("----------------------------------------------------------------------------");
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
            DepartmentStatistics();

            if (cbSelectedProductDepartment.Text == "All")
            {
                foreach (Product p in productControl.GetProducts())
                {
                    string refill = "No";
                    if (p.Quantity <= p.ThreshHold)
                    {
                        refill = "Yes";
                    }
                    dtPrd.Rows.Add(false, p.Id, p.getDepartmentName(), p.Title, p.Quantity, p.QuantityDepo, p.SellingPrice, p.BuyingPrice, refill, p.ThreshHold, p.OverallPrice);
                }
            }


            foreach (Product p in productControl.GetProducts(GetProductDepartment()))
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
            DepartmentStatistics();
            foreach (Admin admin in adminControl.getAdmins())
            {
                dtEmp.Rows.Add(false, admin.ID, admin.Username, admin.FirstName, admin.LastName, admin.getRoleName());
            }

            dtAdmins.DataSource = dtEmp;

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

            DepartmentStatistics();

            if (cbSelectDepartment.Text == "All")
            {
                foreach (Employee employee in employeeControl.getEmployees())
                {
                    dtEmp.Rows.Add(false, employee.ID, employee.UserName, employee.FirstName, employee.LastName, employee.getDepartmentName(), employee.Contract, employee.Wage);
                }
            }

            // show employees filtered by department
            foreach (Employee employee in employeeControl.getEmployees(GetEmployeeDepartment()))
            {
                dtEmp.Rows.Add(false, employee.ID, employee.UserName, employee.FirstName, employee.LastName, employee.getDepartmentName(), employee.Contract, employee.Wage);
            }
            dtEmployees.DataSource = dtEmp;
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

        private void renderRequestTable()
        {
            lbRestocking.Items.Clear();
            lbRestocking.Items.Add("                                                  Request/s to be approved:");
            lbRestocking.Items.Add("------------------------------------------------------------" +
                "-----------------------------------------------------------------------");
            foreach (Request request in requestControl.GetRequests())
            {
                lbRestocking.Items.Add(request);
            }
        }

        private void renderDepartmentsTable()
        {
            DataTable dtDep = new DataTable();
            dtDep.Columns.Add("Selected", typeof(bool));
            dtDep.Columns.Add("ID", typeof(int));
            dtDep.Columns.Add("Title", typeof(string));


            if (cbDepartments.Text == "All")
            {
                foreach (Department d in departmentControl.GetDepartments())
                {
                    dtDep.Rows.Add(false, d.Id, d.Title);
                }
            }


            foreach (Department d in departmentControl.GetDepartments(GetDepartment()))
            {
                dtDep.Rows.Add(false, d.Id, d.Title);
            }


            dtDepartments.DataSource = dtDep;
        }

        private void RenderRolesTable()
        {
            DataTable dtRole = new DataTable();
            dtRole.Columns.Add("Selected", typeof(bool));
            dtRole.Columns.Add("ID", typeof(string));
            dtRole.Columns.Add("Role", typeof(string));
            dtRole.Columns.Add("Department", typeof(string));

            foreach (Role role in roleControl.GetRoles())
            {
                dtRole.Rows.Add(false, role.Id, role.Title, role.getDepartmentName());
            }

            dtRoles.DataSource = dtRole;
        }


        // Get Departments and Roles
        private Department GetProductDepartment()
        {
            Admin admin = adminControl.getloggedUser();
            if (admin.Role.Department != null)
            {
                lblWelcomeToProductPage.Text = admin.ShowDepartmentInfo();
                return admin.Role.Department;
            }
            Department product = (Department)cbSelectedProductDepartment.SelectedItem;
            if (product != null)
            {
                return product;
            }
            return null;
        }

        private Department GetEmployeeDepartment()
        {
            Admin admin = adminControl.getloggedUser();
            if (admin.Role.Department != null)
            {
                lblWelcomeToEmployeesPage.Text = admin.ShowDepartmentInfo();
                return admin.Role.Department;
            }
            Department demployee = (Department)cbSelectDepartment.SelectedItem;
            if (demployee != null)
            {
                return demployee;
            }
            return null;
        }

        private Department GetDepartment()
        {
            Admin admin = adminControl.getloggedUser();
            if (admin.Role.Department != null)
            {
                return admin.Role.Department;
            }
            Department department = (Department)cbDepartments.SelectedItem;
            if (department != null)
            {
                return department;
            }
            return null;
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
            Department d = new Department(10, "All");
            cbEmployeeDepartment.Items.Clear();
            cbProductDepartment.Items.Clear();
            cbRoleDepartment.Items.Clear();
            cbSelectDepartment.Items.Clear();
            cbSelectedProductDepartment.Items.Clear();
            cbDepartments.Items.Clear();
            cbDepartments.Items.Add(d);
            cbSelectedProductDepartment.Items.Add(d);
            cbSelectDepartment.Items.Add(d);
            foreach (Department department in departmentControl.GetDepartments())
            {
                cbEmployeeDepartment.Items.Add(department);
                cbProductDepartment.Items.Add(department);
                cbRoleDepartment.Items.Add(department);
                cbSelectDepartment.Items.Add(department);
                cbSelectedProductDepartment.Items.Add(department);
                cbDepartments.Items.Add(department);
            }
        }


        // CRUD Product
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
                try
                {
                    int quantity = Convert.ToInt32(tbProductQuantity.Text);
                    int quantitydepo = Convert.ToInt32(tbProductQuantityDepo.Text);
                    double selling_price = Convert.ToDouble(tbProductSellingPrice.Text);
                    double buying_price = Convert.ToDouble(tbProductBuyingPrice.Text);
                    int threshold = Convert.ToInt32(tbProductThreshHold.Text);
                    Department department = (Department)cbProductDepartment.SelectedItem;
                    string[] product_bindings = {department.Id.ToString(), tbProductTitle.Text, tbProductDescription.Text, quantity.ToString(), quantitydepo.ToString(),
                 selling_price.ToString(), buying_price.ToString() , threshold.ToString()};
                    productControl.Create(product_bindings);
                    MessageBox.Show("you have successfully created a product!");
                    TraverseControlsAndSetTextEmpty(this);
                    this.renderProductsTable();
                    this.productControl.retrieveAllProducts();
                    ShowDepartmentDetails();
                    this.showPanel(pnlProducts);
                    return;
                }
                catch (Exception e)
                {
                    MessageBox.Show("There's been an exception: " + e.Message);
                }
            }
            foreach (string message in errors)
            {
                MessageBox.Show(message);
            }
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
                TraverseControlsAndSetTextEmpty(this);
                productControl.Update(productData);
                this.renderProductsTable();
                productControl.retrieveAllProducts();
                this.showPanel(this.pnlProducts);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
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
                        productControl.Delete(product);
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
                    tbProductTitleInfo.Text = product.Title;
                    tbProductDescriptionInfo.Text = product.Description;
                    tbProductQuantityInfo.Text = Convert.ToString(product.Quantity);
                    tbProductQuantityDepoInfo.Text = Convert.ToString(product.QuantityDepo);
                    tbProductSellingPriceInfo.Text = Convert.ToString(product.SellingPrice);
                    tbProductBuyingPriceInfo.Text = Convert.ToString(product.BuyingPrice);
                    tbProductPlaceHolderInfo.Text = Convert.ToString(product.ThreshHold);
                    tbProductIDInfo.Text = Convert.ToString(product.Id);
                    tbRevenueInfo.Text = Convert.ToString(product.OverallPrice);
                    productControl.retrieveAllProducts();
                    renderProductsTable();
                    showPanel(pnlProductInformation);
                }
            }
        }

        private void SendProductRequest()
        {
            if (MessageBox.Show("Do you want to send a request for this product?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    string id = tbProductIDInfo.Text;
                    string quantity = tbQuantityRequest.Text;
                    string[] requestBindings = { id, quantity };
                    MessageBox.Show("You request has been sent to the Store Manager!");
                    showPanel(pnlProducts);
                    requestControl.Create(requestBindings);
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


        // CRUD Admin
        private void CreateAdmin()
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
                    if (role == null)
                    {
                        MessageBox.Show("Please select a role in order to add an admin!");
                        return;
                    }
                    string[] admin_bindings = { tbAdminUserName.Text, tbAdminPassword.Text, tbAdminFirstName.Text, tbAdminLastName.Text, tbAdminEmail.Text, role.Id.ToString() };
                    adminControl.Create(admin_bindings);
                    MessageBox.Show("You have created a user!");
                    TraverseControlsAndSetTextEmpty(this);
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
                if (role == null)
                {
                    MessageBox.Show("Please select a role in order to update an admin!");
                    return;
                }
                string[] adminData = { tbAdminUserName.Text, tbAdminPassword.Text, tbAdminFirstName.Text, tbAdminLastName.Text, tbAdminEmail.Text, role.Id.ToString(), adminId };
                adminControl.Update(adminData);
                pnlAdmins.Visible = true;
                MessageBox.Show("You have succesfully update information for that admin!");
                TraverseControlsAndSetTextEmpty(this);
                renderAdminTable();
                showPanel(pnlAdmins);
            }
            catch (Exception e)
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
        }



        // Cart 
        private void AddProductsToCart()
        {

            Product product = (Product)lbStoreProducts.SelectedItem;
            int quantity = Convert.ToInt32(tbQuantity.Text);

            if (product == null)
            {
                MessageBox.Show("You need to select a product first before you add it to the cart!");
                return;
            }

            if (product.Quantity < quantity || quantity <= 0)
            {
                MessageBox.Show("The desired quanity of the product is not in stock/or the quantity you have entered is incorrect!");
                return;
            }
            ordersControl.addProduct(product, quantity);
            lbShoppingCart.Items.Add($"Product:  {product.Title}, Quantity:  {quantity}");
        }

        private void PurchaseProducts()
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
            lbShoppingCart.Items.Add("                     Current items in shopping cart:");
            lbShoppingCart.Items.Add("----------------------------------------------------------------------------");
            productControl.retrieveAllProducts();
            renderProductsForOrder();
            renderProductsTable();
            MessageBox.Show("You successfully made an order!");
        }


        // CRUD Employee
        private void CreateEmployee()
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
                Department department = (Department)cbEmployeeDepartment.SelectedItem;
                if (department == null)
                {
                    MessageBox.Show("Please enter a department!");
                    return;
                }
                int contract_id = cbEmployeeContract.SelectedIndex;
                contract_id++;
                string increased_contract_id = Convert.ToString(contract_id);
                string gender = Convert.ToString(cbEmployeeGender.Text);
                string[] employee_bindings = { tbEmployeeFirstName.Text, tbEmployeeLastName.Text, tbEmployeeAdress.Text,
                    tbEmployeeCity.Text, tbEmployeeCountry.Text, tbEmployeeWagePerHour.Text, tbEmployeePhoneNumber.Text,
                    gender, tbEmployeeEmail.Text,department.Id.ToString(),increased_contract_id,tbEmployeeUserName.Text,tbEmployeePassword.Text};
                employeeControl.Create(employee_bindings);
                MessageBox.Show("You have succesfully created an employee!");
                TraverseControlsAndSetTextEmpty(this);
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
                    cbEmployeeDepartment.Text = employee.getDepartmentName();
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
                if (department == null)
                {
                    MessageBox.Show("Please enter a department");
                    return;
                }
                int contract_id = cbEmployeeContract.SelectedIndex;
                contract_id++;
                string increased_contract_id = Convert.ToString(contract_id);
                string[] employeeData = {tbEmployeeFirstName.Text,tbEmployeeLastName.Text,tbEmployeeAdress.Text,
                    tbEmployeeCity.Text, tbEmployeeCountry.Text,
                    tbEmployeePhoneNumber.Text,gender,tbEmployeeEmail.Text,department.Id.ToString(),increased_contract_id,
                    tbEmployeeWagePerHour.Text,tbEmployeeUserName.Text,tbEmployeePassword.Text,employeeId};
                showPanel(pnlEmployee);
                employeeControl.Update(employeeData);
                MessageBox.Show("You have succesfully update information for that employee!");
                TraverseControlsAndSetTextEmpty(this);
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
                        employeeControl.Delete(getID);
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


        // Validation
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
                tbEmployeeCountry.Text == "" || tbEmployeeEmail.Text == "" || tbEmployeeWagePerHour.Text == "" || cbEmployeeContract.SelectedItem == null ||
                cbEmployeeGender.SelectedItem == null || tbEmployeePhoneNumber.Text == "")
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


        // Schedule
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

        private void calendarSchedule_DateSelected(object sender, DateRangeEventArgs e)
        {
            showPanel(pnlScheduleEmployees);
            currentScheduleDate = e.End;
            renderScheduleMembers();
        }

        private DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return result.AddDays(-3);
        }

        private void AutomateSchedule()
        {

            int employeeCount = Convert.ToInt32(tbEmployeesCount.Text);
            int week = Convert.ToInt32(cbSelectWeek.SelectedItem);
            int year = Convert.ToInt32(tbYear.Text);


            DateTime date = this.FirstDateOfWeekISO8601(year, week);

            for (int i = 1; i <= 5; i++)
            {
                Schedule exists = this.scheduleControl.getScheduleByDate(date);
                if (exists != null)
                {
                    //schedule exists
                    continue;
                }

                List<Employee> employees = new List<Employee>();

                foreach (Employee employee in this.employeeControl.getEmployees())
                {
                    Boolean shouldAdd = false;
                    switch (date.DayOfWeek.ToString())
                    {
                        case "Monday":
                            if (employee.Monday != 1 && employees.Count() <= employeeCount)
                            {
                                shouldAdd = true;
                            }
                            break;
                        case "Tuesday":
                            if (employee.Tuesday != 1 && employees.Count() <= employeeCount)
                            {
                                shouldAdd = true;
                            }
                            break;
                        case "Wednesday":
                            if (employee.Wednesday != 1 && employees.Count() <= employeeCount)
                            {
                                shouldAdd = true;
                            }
                            break;
                        case "Thursday":
                            if (employee.Thursday != 1 && employees.Count() <= employeeCount)
                            {
                                shouldAdd = true;
                            }
                            break;
                        case "Friday":
                            if (employee.Friday != 1 && employees.Count() <= employeeCount)
                            {
                                shouldAdd = true;
                            }
                            break;
                    }

                    if (shouldAdd == true)
                    {
                        employees.Add(employee);
                    }
                }

                if (employees.Count() < employeeCount)
                {
                    foreach (Employee employee in this.employeeControl.getEmployees().OrderBy(a => Guid.NewGuid()).ToList())
                    {

                        if (employees.Count() > employeeCount)
                        {
                            break;
                        }
                        employees.Add(employee);
                    }
                }

                string[] bindings = { "", date.ToString("MM-dd-yyyy") };
                int id = Convert.ToInt32(scheduleControl.Insert(bindings));

                foreach (Employee addingEmployee in employees)
                {

                    string from = date.ToString("MM-dd-yyyy") + " " + "9:00";
                    string to = date.ToString("MM-dd-yyyy") + " " + "17:00";
                    string[] member_data = { id.ToString(), addingEmployee.ID.ToString(), from, to, "8" };

                    //check if the datetime string is really a datetime and if yes safe the user to the db
                    scheduleControl.InsertMember(member_data);
                }


                //create schedule
                //add the employees
                date = date.AddDays(1);
            }

            MessageBox.Show("You successfully created the schedules");
            this.scheduleControl.retrieveSchedules();
            this.renderDailySchedule();
            this.renderScheduleMembers();
            showPanel(pnlScheduleEmployees);
            //get all days from monday to friday in that week where there is no schedule already
            //at least one working from each department
        }


        // Home controls
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
            this.pnlAutomaticSchedule.Visible = false;
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

        private void hideLabel(Label l)
        {
            l.Visible = false;
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


        // Permissions
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
            this.hideButton(btnAddDepartmentPage);
            this.hideButton(btnEditDepartmentPage);
            this.hideButton(btnDeleteDepartment);

            hideLabel(lblWelcomeToProductPage);
            this.hideButton(this.btnAddQuantity);
            this.tbQuantityRequest.Visible = false;
            this.hideLabel(this.materialLabel43);
            this.hideLabel(lblWelcomeToEmployeesPage);
            this.hideLabel(this.materialLabel75);
            this.hidePicture(iconPictureBox20);
            this.hidePicture(iconPictureBox23);
            this.hidePicture(iconPictureBox22);
            this.hidePicture(iconPictureBox24);
            this.hidePicture(iconPictureBox15);
            this.hidePicture(iconPictureBox10);
            this.hidePicture(iconPictureBox5);
            this.hidePicture(iconPictureBox6);
            this.hidePicture(iconPictureBox7);
            this.hidePicture(iconPictureBox11);
            this.hidePicture(iconPictureBox3);
            this.hidePicture(iconPictureBox16);
            this.hidePicture(iconPictureBox17);
        }

        private void DepartmentManagerPermissions()
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
            this.hideButton(btnDashboardPage);
            this.hideButton(btnShowEmployees);
            this.hideButton(btnShowProducts);
            this.hideButton(btnAddQuantity);
            this.hideButton(btnShowDepartments);
            this.hideButton(btnAddDepartmentPage);
            this.hideButton(btnEditDepartmentPage);
            this.hideButton(btnDeleteDepartment);
            this.showPanel(pnlEmployee);


            tbeTotalEmployees.Visible = false;
            tbTotalRevenue.Visible = false;
            tbTotalProducts.Visible = false;
            cbSelectDepartment.Visible = false;
            cbDepartments.Visible = false;
            cbSelectedProductDepartment.Visible = false;
            this.tbQuantityRequest.Visible = false;

            this.hideLabel(materialLabel53);
            this.hideLabel(materialLabel86);
            this.hideLabel(materialLabel92);
            this.hideLabel(materialLabel93);
            this.hideLabel(materialLabel112);
            this.hideLabel(materialLabel122);
            this.hideLabel(materialLabel85);
            this.hideLabel(materialLabel99);
            this.hideLabel(this.materialLabel43);
            this.hidePicture(iconPictureBox17);
            this.hidePicture(iconPictureBox20);
            this.hidePicture(iconPictureBox23);
            this.hidePicture(iconPictureBox22);
            this.hidePicture(iconPictureBox24);
            this.hidePicture(iconPictureBox15);
            this.hidePicture(iconPictureBox10);
            this.hidePicture(iconPictureBox5);
            this.hidePicture(iconPictureBox6);
            this.hidePicture(iconPictureBox7);
            this.hidePicture(iconPictureBox11);
            this.hidePicture(iconPictureBox3);
            this.hidePicture(iconPictureBox16);
            this.hidePicture(iconPictureBox1);
        }

        private void StoreManagerPermissions()
        {
            Admin admin = adminControl.getloggedUser();
            lblWelcomeWorker.Text = admin.ShowWorkerInfo();
            if (requestControl.GetRequests().Count == 0)
            {
                lblRequestStatus.Text = $"There are currently no request to be approved!";
            }
            else
            {
                lblRequestStatus.Text = $" Current Requests: " + requestControl.GetRequests().Count();
            }
            this.hideButton(btnSchedulesPage);
            this.hideButton(btnUsersPage);
            this.hideButton(btnCartPage);
            this.hideButton(btnDashboardPage);
            this.hideButton(btnEmployeesPage);
            this.hideButton(btnProductsPage);
            this.hideButton(btnRolesPage);
            this.hideButton(btnDepartmentPage);
            this.showPanel(pnlRestocking);
            this.hidePicture(iconPictureBox19);
            this.hidePicture(iconPictureBox20);
            this.hidePicture(iconPictureBox16);
            this.hidePicture(iconPictureBox8);
            this.hidePicture(iconPictureBox2);
            this.hidePicture(iconPictureBox1);
            this.hidePicture(iconPictureBox15);
            this.hidePicture(iconPictureBox10);
            this.hidePicture(iconPictureBox5);
            this.hidePicture(iconPictureBox6);
            this.hidePicture(iconPictureBox7);
            this.hidePicture(iconPictureBox18);
            this.hidePicture(iconPictureBox11);
            this.hidePicture(iconPictureBox3);
            this.hidePicture(iconPictureBox16);
        }

        private void Permissions()
        {
            Admin admin = adminControl.getloggedUser();
            if (admin.Role.Title == "Admin")
            {
                hideLabel(lblWelcomeToEmployeesPage);
                hideLabel(lblWelcomeToProductPage);
            }
            else if (admin.Role.Title == "Manager")
            {
                ManagerPermissions();
            }
            else if (admin.Role.Title == "Store Manager")
            {
                StoreManagerPermissions();
            }
            else
            {
                DepartmentManagerPermissions();
            }
        }

        // CRUD Department
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
                departmentControl.Create(department_bindings);
                MessageBox.Show("You have succesfully added a new department!");
                departmentControl.retrieveAllDepartments();
                renderDepartmentsTable();
                showPanel(pnlDepartments);
                GetDepartments();
            }
            catch (Exception e)
            {
                MessageBox.Show("There's been an exception: " + e.Message);
            }
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
                    tbTotalEmployeesDepartment.Text = d.EmployeesCount.ToString();
                    tbTotalProductsDepartment.Text = d.ProductsCount.ToString();
                    tbTotalRevenueDepartment.Text = d.TotalProductsRevenue.ToString();
                    tbStatisticsTitle.Text = d.Title;
                    tbStatisticsDepId.Text = Convert.ToString(d.Id);
                    departmentControl.retrieveAllDepartments();
                    renderDepartmentsTable();
                    showPanel(pnlDepartmentStats);
                }
            }

        }


        private void DepartmentStatistics()
        {
            // total employees 
            this.tbeTotalEmployees.Text = Convert.ToString(this.employeeControl.GetEmployeesCount());

            // product count
            tbTotalProducts.Text = productControl.GetProductsCount().ToString();

            // total revenue
            double totalRevenue = productControl.GetProducts().Sum(item => item.OverallPrice);
            tbTotalRevenue.Text = totalRevenue.ToString();
        }

        // CRUD Role
        public void AddRole()
        {
            hideButton(btnUpdateRole);
            showButton(btnAddRole);
            Department department = (Department)cbRoleDepartment.SelectedItem;
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
                if (department == null)
                {
                    string[] empty_role = { title };
                    roleControl.CreateWithoutDepartment(empty_role);
                }
                else
                {
                    string[] role_bindings = { title, department.Id.ToString() };
                    roleControl.Create(role_bindings);
                }
                MessageBox.Show("You have succesfully added a role!");
                roleControl.retrieveAllRoles();
                GetRoles();
                showPanel(pnlRoles);
                RenderRolesTable();
            }
            catch (Exception e)
            {
                MessageBox.Show("There's been an exception: " + e.Message);
            }
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
            Department department = (Department)cbRoleDepartment.SelectedItem;

            Role role = roleControl.GetRoleByDeparment(department);
            Role r = roleControl.GetRoleByTitle(tbRoleTitle.Text);

            if (r != null)
            {
                MessageBox.Show("Role with title: " + tbRoleTitle.Text + " Already exist!");
                return;
            }



            if (role != null)
            {
                MessageBox.Show($"Role with department: " + department.Title + " Already exist." + " Please select a different department!");
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
                if (department == null)
                {
                    string[] role_bindings = { title, tbRoleId.Text };
                    roleControl.UpdateWithoutDepartment(role_bindings);
                }
                else
                {
                    string[] role_bindings = { title, department.Id.ToString(), tbRoleId.Text };
                    roleControl.Update(role_bindings);
                }
                MessageBox.Show("You have succesfully updated the role!");
                roleControl.retrieveAllRoles();
                RenderRolesTable();
                showPanel(pnlRoles);
                GetRoles();
            }
            catch (Exception e)
            {
                MessageBox.Show("There's been an exception: " + e.Message);
            }
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
                    tbDepartmentRole.Text = role.getDepartmentName();
                    tbViewId.Text = Convert.ToString(role.Id);
                    roleControl.retrieveAllRoles();
                    RenderRolesTable();
                    showPanel(pnlRolesStats);
                }
            }

        }



    }
}
