﻿using BLL;
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
    public partial class FrmEmployeeList : Form
    {
        public FrmEmployeeList()
        {
            InitializeComponent();
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmEmployee frm = new FrmEmployee();
            this.Hide();
            frm.ShowDialog(); 
            this.Visible = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FrmEmployee frm = new FrmEmployee();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        EmployeeDTO dto = new EmployeeDTO();
        private bool comboFull = false;
        private void FrmEmployeeList_Load(object sender, EventArgs e)
        {
            dto = EmployeeBLL.GetAll();
            dataGridView1.DataSource = dto.Employees;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "User No";
            dataGridView1.Columns[2].HeaderText = "First Name";
            dataGridView1.Columns[3].HeaderText = "Surname";
            dataGridView1.Columns[4].HeaderText = "Department";
            dataGridView1.Columns[5].HeaderText = "Position";
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].HeaderText = "Salary";
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;

            comboFull = false;
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

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
                cmbPosition.DataSource = dto.Positions.Where(x => x.DepartmentID == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<EmployeeDetailDTO> list = dto.Employees;
            if(txtUserNo.Text.Trim() != "")
                list = list.Where(x => x.UserNo == Convert.ToInt32(txtUserNo.Text)).ToList();
            if (txtFirstname.Text.Trim() != "")
                list = list.Where(x => x.Firstname.Contains(txtFirstname.Text)).ToList();
            if (txtSurname.Text.Trim() != "")
                list = list.Where(x => x.Surname.Contains(txtSurname.Text)).ToList();
            if (cmbDepartment.SelectedIndex != -1)
                list = list.Where(x => x.DepartmentID == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            if (cmbPosition.SelectedIndex != -1)
                list = list.Where(x => x.PositionID == Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            dataGridView1.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserNo.Clear();
            txtFirstname.Clear();
            txtSurname.Clear();
            comboFull = false;
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.DataSource = dto.Positions;
            comboFull = true;
            dataGridView1.DataSource = dto.Employees;
        }
    }
}