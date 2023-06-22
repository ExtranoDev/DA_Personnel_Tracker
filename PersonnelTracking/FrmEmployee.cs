using BLL;
using DAL;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonnelTracking
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        EmployeeDTO dto = new EmployeeDTO();
        public EmployeeDetailDTO detail = new EmployeeDetailDTO();
        public bool isUpdate  = false;
        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            dto = EmployeeBLL.GetAll();
            cmbDepartment.DataSource = dto.Departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";

            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            comboFull = true;
        }

        bool comboFull = false;
        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                int departmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                cmbPosition.DataSource = dto.Positions.Where(x => x.DepartmentID == departmentID).ToList();
                cmbPosition.SelectedIndex = -1;
            }
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {   
        }

        string filename = "";
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                txtImagePath.Text = openFileDialog1.FileName;
                string unique = Guid.NewGuid().ToString();
                filename += unique + openFileDialog1.SafeFileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("User Number cannot be Empty"); 
            else if (!EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text)))
                MessageBox.Show("User no already in use!!!");
            else if (txtPassword.Text.Trim() == "")
                MessageBox.Show("Password cannot be Empty");
            else if (txtFirstname.Text.Trim() == "")
                MessageBox.Show("FirstName is Empty");
            else if (txtSurname.Text.Trim() == "")
                MessageBox.Show("Surname is Empty");
            else if (cmbDepartment.SelectedIndex == -1)
                MessageBox.Show("Choose a department");
            else if (cmbPosition.SelectedIndex == -1)
                MessageBox.Show("Choose a Position");
            else if (txtSalary.Text.Trim() == "")
                MessageBox.Show("Salary is Empty");
            else
            {
                Employee employee = new Employee();
                employee.UserNo = Convert.ToInt32(txtUserNo.Text);
                employee.Password = txtPassword.Text;
                employee.isAdmin = chkAdmin.Checked;
                employee.Firstname = txtFirstname.Text;
                employee.Surname = txtSurname.Text;
                employee.Salary = Convert.ToInt32(txtSalary.Text);
                employee.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                employee.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                employee.Address = txtAddress.Text;
                employee.BirthDate = dtBirthday.Value;
                employee.ImagePath = filename;

                EmployeeBLL.AddEmployee(employee);
                File.Copy(txtImagePath.Text, @"images\\" + filename);
                MessageBox.Show("Employee was successfully added");
                
                // Clearing input fields after form submission
                txtUserNo.Clear();
                txtPassword.Clear();
                txtSalary.Clear();
                txtAddress.Clear();
                chkAdmin.Checked = false;
                txtFirstname.Clear();
                txtSurname.Clear();
                txtAddress.Clear();
                txtImagePath.Clear();
                pictureBox1.Image = null;
                
                comboFull = false;
                cmbPosition.SelectedIndex = -1;
                cmbDepartment.SelectedIndex = -1;
                cmbPosition.DataSource = dto.Positions;
                comboFull = true;

                dtBirthday.Value = DateTime.Today;
            }
        }

        bool isUnique = false;
        private void bthCheck_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("User Number cannot be Empty");
            else
            {
                isUnique = EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text));
                if (!isUnique)
                    MessageBox.Show("User no already in use!!!");
                else
                    MessageBox.Show("User no is available and can be used");
            }
        }
    }
}
