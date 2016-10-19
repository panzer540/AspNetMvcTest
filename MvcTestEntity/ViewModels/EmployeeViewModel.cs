using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcTestEntity.Models;
using MvcTestEntity.Data_Access_Layer;

namespace MvcTestEntity.ViewModels
{

    /// <summary>
    /// EmployeeViewModel
    /// </summary>
    public class EmployeeViewModel
    {
        public string EmployeeName { get; set; }
        public string Salary { get; set; }
        public string SalaryColor { get; set; }
    }


    /// <summary>
    /// CreateEmployeeViewModel在进行服务器端验证失败时，将用户提交的数据返回给用户，
    /// 保证用户输入的数据因为验证失败而清空
    /// </summary>
    public class CreateEmployeeViewModel:BaseViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salary { get; set; }

    }


    /// <summary>
    /// EmployeeViewModel集合
    /// </summary>
    public class EmployeeListViewModel:BaseViewModel
    {
        public List<EmployeeViewModel> Employees { get; set; }

    }


    /// <summary>
    /// Employee逻辑数据操作层
    /// </summary>
    public class EmployeeBusinessLayer
    {
        public List<Employee> GetEmployees( string username )
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
        public UserStatus GetUserValidity( UserDetails u )
        {
            if( u.UserName == "admin" && u.Password == "admin" )
            {
                return UserStatus.AuthenticatedAdmin;               
            }
            else if( u.UserName == "Suk" && u.Password == "123456" )
            {
                return UserStatus.AuthenticatedUser;
            }
            else
            {
                return UserStatus.NonAuthenticatedUser;
            }
        }
        public void UploadEmployees( List<Employee> employees )
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            salesDal.Employees.AddRange(employees);
            salesDal.SaveChanges();
        }



    }


    

}