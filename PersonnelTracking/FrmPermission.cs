using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonnelTracking
{
    public partial class FrmPermission : Form
    {
        public FrmPermission()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        TimeSpan PermissionDay;
        private void FrmPermission_Load(object sender, EventArgs e)
        {
            txtUserNo.Text = UserStatic.UserNo.ToString();
        }

        private void dtStart_ValueChanged(object sender, EventArgs e)
        {
            PermissionDay = dtEnd.Value.Date - dtStart.Value.Date;
            txtDayAmount.Text = PermissionDay.TotalDays.ToString();
        }

        private void dtEnd_ValueChanged(object sender, EventArgs e)
        {
            PermissionDay = dtEnd.Value.Date - dtStart.Value.Date;
            txtDayAmount.Text = PermissionDay.TotalDays.ToString();
        }
    }
}
