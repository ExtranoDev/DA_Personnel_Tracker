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

        public static void DeleteDepartment(int iD)
        {
            try
            {
                Department dept = db.Departments.First(x => x.ID == iD);
                db.Departments.DeleteOnSubmit(dept);
                db.SubmitChanges();

                // Add C# codes to delete Employee & Position with deleted Department
                // ID
                // Code has been created with Trigger
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<Department> GetDepartments()
        {
            return db.Departments.ToList();
        }

        internal static void UpdateDepartment(Department dept)
        {
            try
            {
                Department dpt = db.Departments.First(x => x.ID == dept.ID);
                dpt.DepartmentName = dept.DepartmentName;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
