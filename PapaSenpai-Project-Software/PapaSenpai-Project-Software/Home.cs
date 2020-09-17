using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            string[] row1 = new string[] { "Ivan Dimitrov", "9:00","17:00", "Cleaner"};
            string[] row2 = new string[] { "Pepi Georgiev", "9:00","17:00", "Salesman"};
            string[] row3 = new string[] { "Presqn Viktorov", "9:00","17:00", "Salesman"};
            string[] row4 = new string[] { "Alex Sashkov", "9:00","17:00", "Cashier"};
            string[] row5 = new string[] { "Georgi Dimitrov", "9:00","17:00", "Cashier"};
            string[] row6 = new string[] { "Deyan Bozhilov", "9:00","17:00", "Cashier"};
            string[] row7 = new string[] { "Pavel Kostadinov", "9:00","17:00", "Technical Support"};
            string[] row8 = new string[] { "Morsh Porsh", "9:00","17:00", "Technical Support"};


            string[] agenda1 = new string[] { "12/05/2020", "12", "Yes" };
            string[] agenda2 = new string[] { "11/05/2020", "12", "Yes" };
            string[] agenda3 = new string[] { "10/05/2020", "08", "No" };
            string[] agenda4 = new string[] { "09/05/2020", "12", "Yes" };
            string[] agenda5 = new string[] { "08/05/2020", "10", "No" };
            string[] agenda6 = new string[] { "07/05/2020", "10", "No" };
            string[] agenda7 = new string[] { "06/05/2020", "10", "No" };
            string[] agenda8 = new string[] { "05/05/2020", "10", "No" };
            string[] agenda9 = new string[] { "04/05/2020", "12", "Yes" };
            string[] agenda10 = new string[] { "03/05/2020", "12", "Yes" };
            string[] agenda11 = new string[] { "02/05/2020", "12", "Yes" };


            string[] staff1 = new string[] { "Ivan Dimitrov", "112", "Full", "Admin" };
            string[] staff2 = new string[] { "Georgi Ivanov", "43", "Full", "Manager" };
            string[] staff3 = new string[] { "Preslav Georgiev", "67", "Partly", "Employee" };
            string[] staff4 = new string[] { "Momchil Dragomirov", "21", "Hourly", "Employee" };
            string[] staff5 = new string[] { "Alex Petrov", "37", "Full", "Employee" };
            string[] staff6 = new string[] { "Rosen Rosenov", "40", "Full", "Employee" };
            string[] staff7 = new string[] { "Petio Petkov", "11", "Full", "Employee" };
            string[] staff8 = new string[] { "Momin Zlaten", "31", "Full", "Employee" };
            string[] staff9 = new string[] { "Rocky Balboa", "8", "Full", "Employee" };
            string[] staff10 = new string[] { "Vasko Vasilev", "1", "Full", "Employee" };
            
            object[] rows = new object[] { row1, row2, row3, row4, row5, row6, row7, row8};
            object[] agendaRows = new object[] { agenda1, agenda2, agenda3, agenda4, agenda5, agenda6, agenda7, agenda8, agenda9, agenda10, agenda11 };
            object[] staffRows = new object[] { staff1, staff2, staff3, staff4, staff5, staff6, staff7, staff8, staff9, staff10 };

            foreach (string[] rowArray in rows)
            {
                dataGridView1.Rows.Add(rowArray);
                dataGridView2.Rows.Add(rowArray);
            }

            foreach (string[] rowArray in agendaRows)
            {
                dataGridView3.Rows.Add(rowArray);
            }

            foreach (string[] rowArray in staffRows)
            {
                dataGridView4.Rows.Add(rowArray);
            }

 
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
            this.pnlViewStaff.Visible = false;
            this.pnlSchedules.Visible = false;
            this.pnlAddStaff.Visible = false;
            this.pnlAddSchedule.Visible = false;
        }

        private void btnViewStaff_Click(object sender, EventArgs e)
        {
            this.pnlDashBoard.Visible = false;
            this.pnlViewStaff.Visible = true;
            this.pnlSchedules.Visible = false;
            this.pnlAddStaff.Visible = false;
            this.pnlAddSchedule.Visible = false;
        }

        private void btnAddSchedule_Click(object sender, EventArgs e)
        {
            this.pnlDashBoard.Visible = false;
            this.pnlViewStaff.Visible = false;
            this.pnlSchedules.Visible = false;
            this.pnlAddStaff.Visible = false;
            this.pnlAddSchedule.Visible = true; 
        }

        private void btnViewSchedule_Click_1(object sender, EventArgs e)
        {
            this.pnlDashBoard.Visible = false;
            this.pnlViewStaff.Visible = false;
            this.pnlAddSchedule.Visible = false;
            this.pnlSchedules.Visible = true;
            this.pnlAddStaff.Visible = false;
        }

        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            this.pnlDashBoard.Visible = false;
            this.pnlViewStaff.Visible = false;
            this.pnlSchedules.Visible = false;
            this.pnlAddStaff.Visible = true;
            this.pnlAddSchedule.Visible = false;
        }

    }
}
