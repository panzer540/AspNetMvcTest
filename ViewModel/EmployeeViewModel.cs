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
    
}