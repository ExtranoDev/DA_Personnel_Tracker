using BLL;
using DAL.DTO;
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
    public partial class FrmPermissionList : Form
    {
        public FrmPermissionList()
        {
            InitializeComponent();
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void txtDayAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmPermission frm = new FrmPermission();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            FillALLData();
            CleanFilters();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.PermissionID == 0)
                MessageBox.Show("Please select a permission from the table");
            else if (detail.State == PermissionStates.Approved || detail.State == PermissionStates.Disapproved)
                MessageBox.Show("You cannot update any approved or disapproved permission");
            else
            {
                FrmPermission frm = new FrmPermission();
                frm.isUpdate = true;
                frm.detail = detail;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                FillALLData();
                CleanFilters();
            }
        }

        PermissionDTO dto = new PermissionDTO();
        private bool comboFull = false;

        void FillALLData()
        {
            dto = PermissionBLL.GetAll();
            if (!UserStatic.isAdmin)
                dto.Permissions = dto.Permissions.Where(x => x.EmployeeID == UserStatic.EmployeeID).ToList();
            dataGridView1.DataSource = dto.Permissions;

            comboFull = false;
            cmbDepartment.DataSource = dto.Departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            if (dto.Departments.Count > 0)
                comboFull = true;

            cmbPosition.DataSource = dto.Positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";

            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;

            cmbState.DataSource = dto.States;
            cmbState.DisplayMember = "StateName";
            cmbState.ValueMember = "ID";
            cmbState.SelectedIndex = -1;
        }
        private void FrmPermissionList_Load(object sender, EventArgs e)
        {        
            FillALLData();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "User No";
            dataGridView1.Columns[2].HeaderText = "First Name";
            dataGridView1.Columns[3].HeaderText = "Surname";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].HeaderText = "Start Date";
            dataGridView1.Columns[9].HeaderText = "End Date";
            dataGridView1.Columns[10].HeaderText = "Day Amount";
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[11].HeaderText = "State";
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;

            if (!UserStatic.isAdmin)
            {
                panel3.Visible = false;
                btnApproved.Hide();
                btnDisapprove.Hide();
                btnDelete.Hide();
                btnClose.Location = new Point(441, 17);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<PermissionDetailDTO> list = dto.Permissions;
            if (txtUserNo.Text.Trim() != "")
                list = list.Where(x => x.UserNo == Convert.ToInt32(txtUserNo.Text)).ToList();
            if (txtFirstname.Text.Trim() != "")
                list = list.Where(x => x.Firstname.Contains(txtFirstname.Text)).ToList();
            if (txtSurname.Text.Trim() != "")
                list = list.Where(x => x.Surname.Contains(txtSurname.Text)).ToList();
            if (cmbDepartment.SelectedIndex != -1)
                list = list.Where(x => x.DepartmentID == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            if (cmbPosition.SelectedIndex != -1)
                list = list.Where(x => x.PositionID == Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            if (rbStartDate.Checked)
                list = list.Where(x => x.StartDate > Convert.ToDateTime(dtStart.Value) && x.StartDate < Convert.ToDateTime(dtEnd.Value)).ToList();
            else if (rbEndDate.Checked)
                list = list.Where(x => x.EndDate > Convert.ToDateTime(dtStart.Value) && x.EndDate < Convert.ToDateTime(dtEnd.Value)).ToList();
            if (cmbState.SelectedIndex != -1)
                list = list.Where(x => x.State == Convert.ToInt32(cmbState.SelectedValue)).ToList();
            if (txtDayAmount.Text.Trim() != "")
                list = list.Where(x => x.PermissionDayAmount == Convert.ToInt32(txtDayAmount.Text)).ToList();
            dataGridView1.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CleanFilters();
        }

        private void CleanFilters()
        {
            txtUserNo.Clear();
            txtFirstname.Clear();
            txtSurname.Clear();
            comboFull = false;
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.DataSource = dto.Positions;
            comboFull = true;
            rbEndDate.Checked = false;
            rbStartDate.Checked = false;
            cmbState.SelectedIndex = -1;
            txtDayAmount.Clear();
            dataGridView1.DataSource = dto.Permissions;
        }

        PermissionDetailDTO detail = new PermissionDetailDTO();
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.PermissionID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[14].Value);
            detail.StartDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
            detail.EndDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
            detail.UserNo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            detail.PermissionDayAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
            detail.Explanation = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
        }

        private void btnApproved_Click(object sender, EventArgs e)
        {
            PermissionBLL.UpdatePermission(detail.PermissionID, PermissionStates.Approved);
            MessageBox.Show("Permission Approved");
            FillALLData();
            CleanFilters();
        }

        private void btnDisapprove_Click(object sender, EventArgs e)
        {
            PermissionBLL.UpdatePermission(detail.PermissionID, PermissionStates.Disapproved);
            MessageBox.Show("Permission Deleted");
            FillALLData();
            CleanFilters();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this Permission?", "warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (detail.State == PermissionStates.Approved || detail.State == PermissionStates.Disapproved)
                    MessageBox.Show("You cannot delete an Approved or Disapproved permission");
                else
                {
                    PermissionBLL.DeletePermission(detail.PermissionID);
                    MessageBox.Show("Permission deleted");
                    FillALLData();
                    CleanFilters();
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel.ExcelExport(dataGridView1);
        }
    }
}
