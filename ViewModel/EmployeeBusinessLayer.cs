using MvcTestEntity.Data_Access_Layer;
using MvcTestEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    /// <summary>
    /// Employee逻辑数据操作层
    /// </summary>
    public class EmployeeBusinessLayer
    {

        public List<Employee> GetEmployees(string username)
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            if (username == "admin")    //如果是管理员，则获取全部的Employee
            {
                return salesDal.Employees.ToList();
            }
            else
            {                       //否则只获取用户自己的数据，前提：用户名是FirstName
                List<Employee> listemEM = new List<Employee>();
                foreach (Employee e in salesDal.Employees)
                {

                    if (e.FirstName == username)
                    {
                        listemEM.Add(e);
                        break;
                    }
                }
                return listemEM;
            }

        }
        public Employee SaveEmployee(Employee e)
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            salesDal.Employees.Add(e);
            salesDal.SaveChanges();
            return e;
        }
        public UserStatus GetUserValidity(UserDetails u)
        {
            if (u.UserName == "admin" && u.Password == "admin")
            {
                return UserStatus.AuthenticatedAdmin;
            }
            else if (u.UserName == "Suk" && u.Password == "123456")
            {
                return UserStatus.AuthenticatedUser;
            }
            else
            {
                return UserStatus.NonAuthenticatedUser;
            }
        }
        public void UploadEmployees(List<Employee> employees)
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            salesDal.Employees.AddRange(employees);
            salesDal.SaveChanges();
        }


    }
}
