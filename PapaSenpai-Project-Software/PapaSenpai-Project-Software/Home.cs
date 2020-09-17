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
            
            object[] rows = new object[] { row1, row2, row3, row4, row5, row6, row7, row8};

            foreach (string[] rowArray in rows)
            {
                dataGridView1.Rows.Add(rowArray);
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
