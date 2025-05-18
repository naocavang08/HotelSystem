using HotelSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.DAL
{
    public class DAL_Staff
    {
        private DBHotelSystem db = new DBHotelSystem();

        // Lấy danh sách nhân viên
        public List<Employee> GetAllEmployees()
        {
            return db.Employees.ToList();
        }

        // Lấy thông tin nhân viên theo CCCD
        public Employee GetEmployeeByCCCD(string cccd)
        {
            return db.Employees.FirstOrDefault(e => e.cccd == cccd);
        }

        // Kiểm tra số điện thoại hoặc CCCD đã tồn tại chưa
        public bool IsEmployeeExists(string phone, string cccd, int? employeeId = null)
        {
            if (employeeId.HasValue)
            {
                return db.Employees.Any(e => (e.phone == phone || e.cccd == cccd) && e.employee_id != employeeId);
            }
            return db.Employees.Any(e => e.phone == phone || e.cccd == cccd);
        }

        // Thêm nhân viên mới
        public Employee AddEmployee(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
            return employee;
        }

        // Cập nhật thông tin nhân viên
        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                var existingEmployee = db.Employees.Find(employee.employee_id);
                if (existingEmployee != null)
                {
                    existingEmployee.name = employee.name;
                    existingEmployee.phone = employee.phone;
                    existingEmployee.cccd = employee.cccd;
                    existingEmployee.gender = employee.gender;
                    existingEmployee.position = employee.position;
                    existingEmployee.salary = employee.salary;

                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Xóa nhân viên
        public bool DeleteEmployee(int employeeId)
        {
            try
            {
                var employee = db.Employees.Find(employeeId);
                if (employee != null)
                {
                    db.Employees.Remove(employee);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Lấy lịch làm việc của nhân viên
        public List<WorkSchedule> GetWorkSchedulesByEmployeeId(int employeeId)
        {
            return db.WorkSchedules.Where(ws => ws.employee_id == employeeId).ToList();
        }

        // Thêm lịch làm việc mới
        public WorkSchedule AddWorkSchedule(WorkSchedule workSchedule)
        {
            db.WorkSchedules.Add(workSchedule);
            db.SaveChanges();
            return workSchedule;
        }

        // Cập nhật lịch làm việc
        public bool UpdateWorkSchedule(WorkSchedule workSchedule)
        {
            try
            {
                var existingWorkSchedule = db.WorkSchedules.Find(workSchedule.schedule_id);
                if (existingWorkSchedule != null)
                {
                    existingWorkSchedule.shift_date = workSchedule.shift_date;
                    existingWorkSchedule.shift_time = workSchedule.shift_time;

                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Xóa lịch làm việc
        public bool DeleteWorkSchedule(int scheduleId)
        {
            try
            {
                var workSchedule = db.WorkSchedules.Find(scheduleId);
                if (workSchedule != null)
                {
                    db.WorkSchedules.Remove(workSchedule);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Tìm kiếm nhân viên theo nhiều tiêu chí
        public List<Employee> SearchEmployees(string name = null, string phone = null, string cccd = null,
            string position = null, string shiftDate = null, string shiftTime = null)
        {
            var query = db.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(e => e.name.ToLower().Contains(name.ToLower()));

            if (!string.IsNullOrWhiteSpace(phone))
                query = query.Where(e => e.phone.Contains(phone));

            if (!string.IsNullOrWhiteSpace(cccd))
                query = query.Where(e => e.cccd.Contains(cccd));

            if (!string.IsNullOrWhiteSpace(position))
                query = query.Where(e => e.position.ToLower().Contains(position.ToLower()));

            var result = query.ToList();

            if (!string.IsNullOrWhiteSpace(shiftDate) || !string.IsNullOrWhiteSpace(shiftTime))
            {
                result = result.Where(e =>
                    db.WorkSchedules.Any(ws =>
                        ws.employee_id == e.employee_id &&
                        (string.IsNullOrWhiteSpace(shiftDate) || ws.shift_date.Contains(shiftDate)) &&
                        (string.IsNullOrWhiteSpace(shiftTime) || ws.shift_time.Contains(shiftTime))
                    )
                ).ToList();
            }

            return result;
        }
    }
}