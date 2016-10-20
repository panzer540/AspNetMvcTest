using MvcTestEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using ViewModel.SPA;
using OldViewModel = MvcTestEntity.ViewModels;
using MvcTestEntity.Filters;

namespace MvcTest.Areas.SPA.Controllers
{
    public class MainController : Controller
    {
        // GET: SPA/Main
        [Authorize]
        public ActionResult Index()
        {
            MainViewModel v = new MainViewModel();
            v.UserName = User.Identity.Name;
            v.FooterData = new OldViewModel.FooterViewModel();
            v.FooterData.CompanyName = "Stack Technology Company";
            v.FooterData.Year = DateTime.Now.Year.ToString();
            return View("Index",v);
        }


        // GET: SPA/EmployeeList
        /// <summary>
        /// 显示列表：EmployeeList
        /// </summary>
        /// <returns></returns>
        public ActionResult EmployeeList()
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
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }


        public ActionResult AddNew()
        {
            OldViewModel.CreateEmployeeViewModel cevm = new OldViewModel.CreateEmployeeViewModel();
            return PartialView("CreateEmployee",cevm);
        }


        [AdminFilter]
        public ActionResult SaveEmployee(Employee emp)
        {
            EmployeeBusinessLayer employeeBL = new EmployeeBusinessLayer();
            employeeBL.SaveEmployee(emp);

            EmployeeViewModel employeeVM = new EmployeeViewModel();
            employeeVM.EmployeeName = emp.FirstName + " " + emp.LastName;
            employeeVM.Salary = emp.Salary.ToString();
            employeeVM.Salary = int.Parse(employeeVM.Salary).ToString("c");
            if( emp.Salary>15000 )
            {
                employeeVM.SalaryColor = "yellow";
            }
            else
            {
                employeeVM.SalaryColor = "green";
            }
            return Json(employeeVM);
        }


    }
}