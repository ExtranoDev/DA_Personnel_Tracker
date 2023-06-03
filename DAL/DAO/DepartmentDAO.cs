using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class DepartmentDAO : EmployeeContext
    {
        public static void AddDepartment(Department dept)
        {
            try
            {
                db.Departments.InsertOnSubmit(dept);
                db.SubmitChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Department> GetDepartments()
        {
            return db.Departments.ToList();
        }
    }
}
