using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcTestEntity.Models;
using MvcTestEntity.Data_Access_Layer;

namespace MvcTestEntity.ViewModels
{

    /// <summary>
    /// Employee的ViewModel
    /// </summary>
    public class EmployeeViewModel
    {
        public string EmployeeName { get; set; }
        public string Salary { get; set; }
        public string SalaryColor { get; set; }
    }


    /// <summary>
    /// 在数据验证中返回数据 在CreatedEmployee网页中的Model数据有用
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
    /// Employee逻辑业务层
    /// </summary>
    public class EmployeeBusinessLayer
    {
        public List<Employee> GetEmployees()
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            return salesDal.Employees.ToList();
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