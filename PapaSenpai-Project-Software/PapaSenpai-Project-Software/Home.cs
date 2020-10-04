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
            this.pnlSchedules.Visible = false;
            this.pnlAddStaff.Visible = false;
            this.pnlAddSchedule.Visible = false;
            this.pnlAdmin.Visible = false;
            this.pnlAddAdmin.Visible = false;
        }

        private void btnViewStaff_Click(object sender, EventArgs e)
        {
            this.pnlDashBoard.Visible = false;
            this.pnlEmployee.Visible = true;
            this.pnlSchedules.Visible = false;
            this.pnlAddStaff.Visible = false;
            this.pnlAddSchedule.Visible = false;
            this.pnlAdmin.Visible = false;
            this.pnlAddAdmin.Visible = false;
        }

        private void btnAddSchedule_Click(object sender, EventArgs e)
        {
            this.pnlDashBoard.Visible = false;
            this.pnlEmployee.Visible = false;
            this.pnlSchedules.Visible = false;
            this.pnlAddStaff.Visible = false;
            this.pnlAddSchedule.Visible = true;
            this.pnlAdmin.Visible = false;
            this.pnlAddAdmin.Visible = false;
        }

        private void btnViewSchedule_Click_1(object sender, EventArgs e)
        {
            this.pnlDashBoard.Visible = false;
            this.pnlEmployee.Visible = false;
            this.pnlAddSchedule.Visible = false;
            this.pnlSchedules.Visible = true;
            this.pnlAddStaff.Visible = false;
            this.pnlAdmin.Visible = false;
            this.pnlAddAdmin.Visible = false;
        }

        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            this.pnlDashBoard.Visible = false;
            this.pnlEmployee.Visible = false;
            this.pnlSchedules.Visible = false;
            this.pnlAddStaff.Visible = true;
            this.pnlAddSchedule.Visible = false;
            this.pnlAdmin.Visible = false;
            this.pnlAddAdmin.Visible = false;
        }
        private void btnViewAdmins_Click(object sender, EventArgs e)
        {
            this.pnlDashBoard.Visible = false;
            this.pnlEmployee.Visible = false;
            this.pnlSchedules.Visible = false;
            this.pnlAddStaff.Visible = false;
            this.pnlAddSchedule.Visible = false;
            this.pnlAdmin.Visible = true;
            this.pnlAddAdmin.Visible = false;
        }

        private void btnAddAdmins_Click(object sender, EventArgs e)
        {
            this.pnlDashBoard.Visible = false;
            this.pnlEmployee.Visible = false;
            this.pnlSchedules.Visible = false;
            this.pnlAddStaff.Visible = false;
            this.pnlAddSchedule.Visible = false;
            this.pnlAdmin.Visible = false;
            this.pnlAddAdmin.Visible = true;
        }
    }
}
