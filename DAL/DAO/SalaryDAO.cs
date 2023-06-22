using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class SalaryDAO : EmployeeContext
    {
        public static void AddSalary(Salary salary)
        {
            try
            {
                db.Salaries.InsertOnSubmit(salary);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<Month> GetMonths()
        {
            return db.Months.ToList();
        }

        public static List<SalaryDetailDTO> GetSalaries()
        {
            List<SalaryDetailDTO> salaryList = new List<SalaryDetailDTO>();
            var list = (from s in db.Salaries
                        join e in db.Employees on s.EmployeeID equals e.EmployeeID
                        join m in db.Months on s.MonthID equals m.ID
                        select new
                        {
                            UserNo = e.UserNo,
                            Firstname = e.Firstname,
                            Surname = e.Surname,
                            EmployeeID = e.EmployeeID,
                            Amount = s.Amount,
                            Year = s.Year,
                            MonthName = m.MonthName,
                            MonthID = s.MonthID,
                            SalaryID = s.ID,
                            DepartmentID = e.DepartmentID,
                            PositionID = e.PositionID
                        }).OrderBy(x => x.Year).ToList();
            foreach (var item  in list)
            {
                SalaryDetailDTO dto = new SalaryDetailDTO();
                dto.UserNo = item.UserNo;
                dto.Firstname = item.Firstname;
                dto.Surname = item.Surname;
                dto.EmployeeID = item.EmployeeID;
                dto.SalaryAmount = item.Amount;
                dto.SalaryYear = item.Year;
                dto.MonthName = item.MonthName;
                dto.MonthID = item.MonthID;
                dto.DepartmentID = item.DepartmentID;
                dto.SalaryID = item.SalaryID;
                dto.PositionID = item.PositionID;
                dto.OldSalary = item.Amount;
                salaryList.Add(dto);
            }
            return salaryList;
        }

        public static void UpdateSalary(Salary salary)
        {
            try
            {
                Salary s1 = db.Salaries.First(x => x.ID == salary.ID);
                s1.Amount = salary.Amount;
                s1.Year = salary.Year;
                s1.MonthID = salary.MonthID;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
