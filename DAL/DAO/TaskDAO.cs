using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class TaskDAO : EmployeeContext
    {
        public static void AddTask(Task task)
        {
            try
            {
                db.Tasks.InsertOnSubmit(task);
                db.SubmitChanges();
            }
            catch (Exception ex)
            { 
                throw ex;
            }
        }

        public static void DeleteTask(int taskID)
        {
            Task ts = db.Tasks.First(x =>  x.ID == taskID);
            db.Tasks.DeleteOnSubmit(ts);
            db.SubmitChanges();
        }

        public static List<TaskDetailDTO> GetTasks()
        {
            List<TaskDetailDTO> taskList = new List<TaskDetailDTO>();
            var list = (from t in db.Tasks
                        join s in db.TaskStates on t.TaskState equals s.ID
                        join e in db.Employees on t.EmployeeID equals e.EmployeeID
                        join d in db.Departments on e.DepartmentID equals d.ID
                        join p in  db.Positions on e.PositionID equals p.ID
                        select new
                        {
                            taskID = t.ID,
                            title = t.TaskTitle,
                            content = t.TaskContent,
                            startDate = t.TaskStartDate,
                            deliveryDate = t.TaskDeliveryDate,
                            taskStateName = s.StatusName,
                            taskStateID = t.TaskState,
                            UserNo = e.UserNo,
                            Firstname = e.Firstname,
                            EmployeeID = t.EmployeeID,
                            Surname = e.Surname,
                            positionName = p.PositionName,
                            departmentName = d.DepartmentName,
                            positionID = e.PositionID,
                            departmentID = e.DepartmentID
                        }).OrderBy(x => x.startDate).ToList();
            foreach ( var item in list )
            {
                TaskDetailDTO dto = new TaskDetailDTO();
                dto.TaskID = item.taskID;
                dto.Title = item.title;
                dto.Content = item.content;
                dto.TaskStartDate = item.startDate;
                dto.TaskDeliveryDate = item.deliveryDate;
                dto.TaskStateName = item.taskStateName;
                dto.TaskStateID = item.taskStateID;
                dto.UserNo = item.UserNo;
                dto.Firstname = item.Firstname;
                dto.Surname = item.Surname;
                dto.DepartmentName = item.departmentName;
                dto.PositionName = item.positionName;
                dto.PositionID = item.positionID;
                dto.EmployeeID = item.EmployeeID;
                taskList.Add(dto);
            }
            return taskList;
        }

        public static List<TaskState> GettaskStates()
        {
            return db.TaskStates.ToList();
        }

        public static void UpdateTask(Task task)
        {
            try
            {
                Task ts = db.Tasks.First(x => x.ID == task.ID);
                ts.TaskTitle = task.TaskTitle;
                ts.TaskState = task.TaskState;
                ts.EmployeeID = task.EmployeeID;
                ts.TaskContent = task.TaskContent;

                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
