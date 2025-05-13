using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSystem.Model;
using HotelSystem.DTO;

namespace HotelSystem.DAL
{
    public class EmployeeDAL
    {
        private readonly DBHotelSystem db;

        public EmployeeDAL()
        {
            db = new DBHotelSystem();
        }

        public List<Employee> GetAllEmployees()
        {
            try
            {
                return db.Employees.ToList();
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"Error getting all employees: {ex.Message}");
                throw;
            }
        }

        public Employee GetEmployeeByID(int employeeId)
        {
            try
            {
                return db.Employees.FirstOrDefault(e => e.employee_id == employeeId);
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"Error getting employee by ID: {ex.Message}");
                throw;
            }
        }

        public Employee GetEmployeeByCCCD(string cccd)
        {
            try
            {
                return db.Employees.FirstOrDefault(e => e.cccd == cccd);
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"Error getting employee by CCCD: {ex.Message}");
                throw;
            }
        }

        public List<Employee> SearchEmployees(string name, string phone, string cccd, string position)
        {
            try
            {
                var query = db.Employees.AsQueryable();

                // Apply filters if provided
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(emp => emp.name.ToLower().Contains(name.ToLower()));

                if (!string.IsNullOrWhiteSpace(phone))
                    query = query.Where(emp => emp.phone.Contains(phone));

                if (!string.IsNullOrWhiteSpace(cccd))
                    query = query.Where(emp => emp.cccd.Contains(cccd));

                if (!string.IsNullOrWhiteSpace(position))
                    query = query.Where(emp => emp.position.ToLower().Contains(position.ToLower()));

                return query.ToList();
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"Error searching employees: {ex.Message}");
                throw;
            }
        }

        public void AddEmployee(EmployeeDTO employeeDTO)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // Create user first
                    var newUser = new User
                    {
                        username = employeeDTO.Phone, // Using phone as username
                        password = employeeDTO.Password ?? "defaultpassword", // Default password if not provided
                        role = employeeDTO.Role ?? "staff", // Default role if not provided
                        status = employeeDTO.Status ?? "active", // Default status if not provided
                        date_register = employeeDTO.DateRegister ?? DateTime.Now
                    };

                    // Add user to database
                    db.Users.Add(newUser);
                    db.SaveChanges();

                    // Create employee with reference to the user
                    var newEmployee = new Employee
                    {
                        name = employeeDTO.Name,
                        phone = employeeDTO.Phone,
                        cccd = employeeDTO.CCCD,
                        gender = employeeDTO.Gender,
                        position = employeeDTO.Position,
                        salary = employeeDTO.Salary,
                        id = newUser.id // Link to the User record
                    };

                    // Add employee to database
                    db.Employees.Add(newEmployee);
                    db.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error adding employee: {ex.Message}");
                    throw;
                }
            }
        }

        public void UpdateEmployee(EmployeeDTO employeeDTO)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // Find the employee by CCCD
                    var employee = db.Employees.FirstOrDefault(emp => emp.cccd == employeeDTO.CCCD);

                    if (employee == null)
                    {
                        throw new Exception("Employee not found");
                    }

                    // Update employee data
                    employee.name = employeeDTO.Name;
                    employee.phone = employeeDTO.Phone;
                    employee.cccd = employeeDTO.CCCD;
                    employee.gender = employeeDTO.Gender;
                    employee.position = employeeDTO.Position;
                    employee.salary = employeeDTO.Salary;

                    // Also update the associated user's username (phone) if changed
                    var user = db.Users.Find(employee.id);
                    if (user != null)
                    {
                        user.username = employeeDTO.Phone;
                    }

                    // Save changes
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error updating employee: {ex.Message}");
                    throw;
                }
            }
        }

        public void DeleteEmployee(string cccd)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // Find the employee by CCCD
                    var employee = db.Employees.FirstOrDefault(emp => emp.cccd == cccd);

                    if (employee == null)
                    {
                        throw new Exception("Employee not found");
                    }

                    // Find the associated user
                    var userId = employee.id;
                    var user = db.Users.Find(userId);

                    // Remove employee first (due to foreign key constraints)
                    db.Employees.Remove(employee);
                    db.SaveChanges();

                    // Remove the user if found
                    if (user != null)
                    {
                        db.Users.Remove(user);
                        db.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error deleting employee: {ex.Message}");
                    throw;
                }
            }
        }
    }
}