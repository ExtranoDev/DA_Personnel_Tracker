using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAO;

namespace BLL
{
    public class DepartmentBLL
    {
        public static void AddDepartment(Department dept)
        {
            DepartmentDAO.AddDepartment(dept);
        }

        public static void DeleteDepartment(int iD)
        {
            DepartmentDAO.DeleteDepartment(iD);
        }

        public static List<Department> GetDepartments()
        {
            return DepartmentDAO.GetDepartments();
        }
    }
}
