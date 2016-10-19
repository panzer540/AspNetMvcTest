using MvcTestEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using ViewModel.SPA;

namespace MvcTest.Areas.SPA.Controllers
{
    public class EmployeeListController : Controller
    {
        // GET: SPA/EmployeeList
        /// <summary>
        /// 显示列表：EmployeeList
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            EmployeeListViewModel empListVM = new EmployeeListViewModel();//这是EmployeeListViewModel的实例化
            List<EmployeeViewModel> evmList = new List<EmployeeViewModel>();//这是EmployeeViewModel 的集合
            List<Employee> listemployee = new List<Employee>();//这是Employee的集合
            EmployeeBusinessLayer employeebusinesslayer = new EmployeeBusinessLayer();//实例化数据逻辑操作对象
            listemployee = employeebusinesslayer.GetEmployees(User.Identity.Name);//获取Employee数据，根据登陆的用户获取特定数据

            foreach (Employee emp in listemployee)
            {
                EmployeeViewModel evm = new EmployeeViewModel();
                evm.EmployeeName = emp.FirstName + " " + emp.LastName;
                evm.Salary = emp.Salary.ToString();//int? 可空类型不能直接ToString("c")  
                evm.Salary = int.Parse(evm.Salary).ToString("c");

                if (emp.Salary > 15000)
                {
                    evm.SalaryColor = "yellow";
                }
                else
                {
                    evm.SalaryColor = "green";
                }
                evmList.Add(evm);
                //empListVM.Employees.Add(evm);
            }

            empListVM.listEVM = evmList;

            return View("EmployeeList", empListVM);
        }


        /// <summary>
        /// 如果是管理员则显示AddNew,否则不显示
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAddNewLink()
        {
            if( Convert.ToBoolean( Session["IsAdmin"] ) )
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }            
        }






    }
}