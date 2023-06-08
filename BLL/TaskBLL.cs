﻿using DAL.DAO;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TaskBLL
    {
        public static void AddTask(DAL.Task task)
        {
            TaskDAO.AddTask(task);
        }

        public static TaskDTO GetAll()
        {
            TaskDTO taskDTO = new TaskDTO();
            taskDTO.Employees = EmployeeDAO.GetEmployees();
            taskDTO.Departments = DepartmentDAO.GetDepartments();
            taskDTO.Positions = PositionDAO.GetPositions();
            taskDTO.TaskStates = TaskDAO.GettaskStates();
            taskDTO.Tasks = TaskDAO.GetTasks();
            return taskDTO;
        }

        public static void UpdateTask(DAL.Task task)
        {
            TaskDAO.UpdateTask(task);
        }
    }
}
