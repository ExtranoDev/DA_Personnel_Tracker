using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;

namespace PersonnelTracking
{
    public partial class FrmDepartment : Form
    {
        public FrmDepartment()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDepartment.Text.Trim() == "") {
                MessageBox.Show("Please fill the department field");
            } else
            {
                Department dept = new Department();
                if (!isUpdate)
                {
                    dept.DepartmentName = txtDepartment.Text;
                    BLL.DepartmentBLL.AddDepartment(dept);
                    MessageBox.Show("Department Added successfully");
                    txtDepartment.Text = "";
                } else
                {
                    DialogResult result = MessageBox.Show("Are you sure?", "warning!!!", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        dept.ID = detail.ID;
                        dept.DepartmentName = txtDepartment.Text;
                        Department.UpdateDepartment(dept);
                        MessageBox.Show("Department updated!!!");
                        this.Close();
                    }
                }
                
            }
        }

        public bool isUpdate = false;
        public Department detail = new Department();

        private void FrmDepartment_Load(object sender, EventArgs e)
        {
            if (isUpdate)
                txtDepartment.Text = detail.DepartmentName;
        }
    }
}
