﻿using System;
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
    public partial class FrmPosition : Form
    {
        public FrmPosition()
        {
            InitializeComponent();
        }
        List<Department> departmentList = new List<Department>();
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmPosition_Load(object sender, EventArgs e)
        {
            departmentList = DepartmentBLL.GetDepartments();
            cmbDepartment.DataSource = departmentList;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember= "ID";
            cmbDepartment.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPosition.Text.Trim() == "")
                MessageBox.Show("Please fill the position now");
            else if (cmbDepartment.SelectedIndex == -1)
                MessageBox.Show("Please select a Department");
            else
            {
                Position position = new Position();
                position.PositionName = txtPosition.Text;
                position.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                PositionBLL.AddPosition(position);
                MessageBox.Show("Position added successfully");
                txtPosition.Clear();
                cmbDepartment.SelectedIndex = -1;
            }
        }
    }
}