using HotelSystem.DAL;
using HotelSystem.DTO;
using HotelSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.BLL
{
    public class BLL_Staff
    {
        private DAL_Staff dalStaff = new DAL_Staff();

        // Lấy danh sách tất cả nhân viên
        public List<DTO_Staff> GetAllStaff()
        {
            var employees = dalStaff.GetAllEmployees();
            var result = new List<DTO_Staff>();

            using (var db = new DBHotelSystem())
            {
                foreach (var employee in employees)
                {
                    var workSchedule = db.WorkSchedules
                        .Where(ws => ws.employee_id == employee.employee_id)
                        .OrderByDescending(ws => ws.schedule_id)
                        .FirstOrDefault();

                    var user = db.Users.FirstOrDefault(u => u.id == employee.id);

                    result.Add(new DTO_Staff
                    {
                        EmployeeId = employee.employee_id,
                        Name = employee.name,
                        Phone = employee.phone,
                        CCCD = employee.cccd,
                        Gender = employee.gender ?? false,
                        Position = employee.position,
                        Salary = employee.salary,
                        UserId = employee.id,
                        Username = user?.username,
                        Password = user?.password,
                        Role = user?.role,
                        ShiftDate = workSchedule?.shift_date,
                        ShiftTime = workSchedule?.shift_time
                    });
                }
            }

            return result;
        }

        // Thêm nhân viên mới
        public bool AddStaff(DTO_Staff staffDto)
        {
            try
            {
                using (var db = new DBHotelSystem())
                {
                    // Kiểm tra tồn tại
                    bool isExists = dalStaff.IsEmployeeExists(staffDto.Phone, staffDto.CCCD);
                    bool isUsernameExists = db.Users.Any(u => u.username == staffDto.Username);

                    if (isExists || isUsernameExists)
                        return false;

                    // Tạo user mới
                    var newUser = new User
                    {
                        username = staffDto.Username,
                        password = staffDto.Password,
                        role = string.IsNullOrEmpty(staffDto.Role) ? "Staff" : staffDto.Role,
                        status = "active",
                        date_register = DateTime.Now
                    };

                    db.Users.Add(newUser);
                    db.SaveChanges();

                    // Tạo nhân viên mới
                    var newEmployee = new Employee
                    {
                        name = staffDto.Name,
                        phone = staffDto.Phone,
                        cccd = staffDto.CCCD,
                        gender = staffDto.Gender,
                        position = staffDto.Position,
                        salary = staffDto.Salary,
                        id = newUser.id
                    };

                    var employee = dalStaff.AddEmployee(newEmployee);

                    // Thêm lịch làm việc nếu có
                    if (!string.IsNullOrWhiteSpace(staffDto.ShiftDate) && !string.IsNullOrWhiteSpace(staffDto.ShiftTime))
                    {
                        var workSchedule = new WorkSchedule
                        {
                            employee_id = employee.employee_id,
                            shift_date = staffDto.ShiftDate,
                            shift_time = staffDto.ShiftTime
                        };

                        dalStaff.AddWorkSchedule(workSchedule);
                    }

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // Cập nhật thông tin nhân viên
        public bool UpdateStaff(DTO_Staff staffDto)
        {
            try
            {
                using (var db = new DBHotelSystem())
                {
                    // Kiểm tra tồn tại
                    var employee = db.Employees.Find(staffDto.EmployeeId);
                    if (employee == null)
                        return false;

                    bool isExists = dalStaff.IsEmployeeExists(staffDto.Phone, staffDto.CCCD, staffDto.EmployeeId);
                    var user = db.Users.Find(employee.id);
                    bool isUsernameExists = db.Users.Any(u => u.username == staffDto.Username && u.id != employee.id);

                    if (isExists || isUsernameExists)
                        return false;

                    // Cập nhật thông tin nhân viên
                    employee.name = staffDto.Name;
                    employee.phone = staffDto.Phone;
                    employee.cccd = staffDto.CCCD;
                    employee.gender = staffDto.Gender;
                    employee.position = staffDto.Position;
                    employee.salary = staffDto.Salary;

                    // Cập nhật thông tin user
                    if (user != null)
                    {
                        user.username = staffDto.Username;
                        user.password = staffDto.Password;
                        user.role = string.IsNullOrEmpty(staffDto.Role) ? "Staff" : staffDto.Role;
                    }

                    // Cập nhật lịch làm việc
                    if (!string.IsNullOrWhiteSpace(staffDto.ShiftDate) && !string.IsNullOrWhiteSpace(staffDto.ShiftTime))
                    {
                        var workSchedule = db.WorkSchedules
                            .Where(ws => ws.employee_id == staffDto.EmployeeId)
                            .OrderByDescending(ws => ws.schedule_id)
                            .FirstOrDefault();

                        if (workSchedule != null)
                        {
                            workSchedule.shift_date = staffDto.ShiftDate;
                            workSchedule.shift_time = staffDto.ShiftTime;
                        }
                        else
                        {
                            var newWorkSchedule = new WorkSchedule
                            {
                                employee_id = staffDto.EmployeeId,
                                shift_date = staffDto.ShiftDate,
                                shift_time = staffDto.ShiftTime
                            };
                            db.WorkSchedules.Add(newWorkSchedule);
                        }
                    }

                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // Xóa nhân viên
        public bool DeleteStaff(int employeeId)
        {
            try
            {
                using (var db = new DBHotelSystem())
                {
                    var employee = db.Employees.Find(employeeId);
                    if (employee == null)
                        return false;

                    // Xóa lịch làm việc
                    var workSchedules = db.WorkSchedules
                        .Where(ws => ws.employee_id == employeeId)
                        .ToList();

                    foreach (var schedule in workSchedules)
                    {
                        db.WorkSchedules.Remove(schedule);
                    }

                    // Xóa nhân viên
                    db.Employees.Remove(employee);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // Tìm kiếm nhân viên
        public List<DTO_Staff> SearchStaff(string name = null, string phone = null, string cccd = null,
            string position = null, string shiftDate = null, string shiftTime = null, string username = null, string role = null)
        {
            var employees = dalStaff.SearchEmployees(name, phone, cccd, position, shiftDate, shiftTime);
            var result = new List<DTO_Staff>();

            using (var db = new DBHotelSystem())
            {
                foreach (var employee in employees)
                {
                    var user = db.Users.FirstOrDefault(u => u.id == employee.id);

                    // Lọc theo username nếu có
                    if (!string.IsNullOrWhiteSpace(username) && (user == null || !user.username.Contains(username)))
                        continue;

                    // Lọc theo role nếu có
                    if (!string.IsNullOrWhiteSpace(role) && (user == null || user.role != role))
                        continue;

                    var workSchedule = db.WorkSchedules
                        .Where(ws => ws.employee_id == employee.employee_id)
                        .OrderByDescending(ws => ws.schedule_id)
                        .FirstOrDefault();

                    result.Add(new DTO_Staff
                    {
                        EmployeeId = employee.employee_id,
                        Name = employee.name,
                        Phone = employee.phone,
                        CCCD = employee.cccd,
                        Gender = employee.gender ?? false,
                        Position = employee.position,
                        Salary = employee.salary,
                        UserId = employee.id,
                        Username = user?.username,
                        Password = user?.password,
                        Role = user?.role,
                        ShiftDate = workSchedule?.shift_date,
                        ShiftTime = workSchedule?.shift_time
                    });
                }
            }

            return result;
        }

        // Lấy thông tin nhân viên theo CCCD
        public DTO_Staff GetStaffByCCCD(string cccd)
        {
            var employee = dalStaff.GetEmployeeByCCCD(cccd);
            if (employee == null)
                return null;

            using (var db = new DBHotelSystem())
            {
                var user = db.Users.FirstOrDefault(u => u.id == employee.id);
                var workSchedule = db.WorkSchedules
                    .Where(ws => ws.employee_id == employee.employee_id)
                    .OrderByDescending(ws => ws.schedule_id)
                    .FirstOrDefault();

                return new DTO_Staff
                {
                    EmployeeId = employee.employee_id,
                    Name = employee.name,
                    Phone = employee.phone,
                    CCCD = employee.cccd,
                    Gender = employee.gender ?? false,
                    Position = employee.position,
                    Salary = employee.salary,
                    UserId = employee.id,
                    Username = user?.username,
                    Password = user?.password,
                    Role = user?.role,
                    ShiftDate = workSchedule?.shift_date,
                    ShiftTime = workSchedule?.shift_time
                };
            }
        }
    }
}
