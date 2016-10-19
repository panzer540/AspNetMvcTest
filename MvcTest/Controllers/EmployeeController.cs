using System.Collections.Generic;
using System.Web.Mvc;
using MvcTestEntity.Models;
using MvcTestEntity.ViewModels;
using System;
using MvcTestEntity.Filters;


namespace MvcApplication1.Controllers
{

    public class EmployeeController : Controller
    {
        /// <summary>
        /// 返回传递数据给视图Index
        /// </summary>
        /// <returns></returns>
        [Authorize][HeaderFooterFilter]     //[Route("Employee/List")]
        public ActionResult Index()
        {
            EmployeeListViewModel empListVM = new EmployeeListViewModel();//这是EmployeeListViewModel的实例化
            List<EmployeeViewModel> evmList = new List<EmployeeViewModel>();//这是EmployeeViewModel 的集合
            List<Employee> listemployee = new List<Employee>();//这是Employee的集合
            EmployeeBusinessLayer employeebusinesslayer = new EmployeeBusinessLayer();//实例化数据逻辑操作对象
            listemployee = employeebusinesslayer.GetEmployees( User.Identity.Name );//获取Employee数据，根据登陆的用户获取特定数据

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

            empListVM.Employees = evmList;

            return View("Index", empListVM);
        }


        /// <summary>
        /// 提交保存Save Employee
        /// </summary>
        /// <param name="e"></param>
        /// <param name="btnSubmit"></param>
        /// <returns></returns>
        [AdminFilter][HeaderFooterFilter]
        public ActionResult SaveEmployee(Employee e, string btnSubmit)
        {

            switch (btnSubmit)
            {
                case "Save"://点击的是保存按钮
                    if (ModelState.IsValid && e.Salary != null)
                    {
                        EmployeeBusinessLayer ebl = new EmployeeBusinessLayer();
                        ebl.SaveEmployee(e);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        CreateEmployeeViewModel cevm = new CreateEmployeeViewModel();
                        cevm.FirstName = e.FirstName;
                        cevm.LastName = e.LastName;
                        if (e.Salary.HasValue)
                        {
                            cevm.Salary = e.Salary.ToString();
                        }
                        else
                        {
                            cevm.Salary = ModelState["Salary"].Value.AttemptedValue;
                        }                        

                        return View("CreateEmployee", cevm);
                    }
                case "Cancel"://点击取消按钮，则取消并返回Index
                    return RedirectToAction("Index");
            }
            return new EmptyResult();
        }


        /// <summary>
        /// Add New Employee的链接
        /// </summary>
        /// <returns></returns>
        [AdminFilter][HeaderFooterFilter]
        public ActionResult AddNew()
        {
            CreateEmployeeViewModel createEMP = new CreateEmployeeViewModel();
            return View("CreateEmployee", createEMP);
        }


        /// <summary>
        /// 显示或者隐藏AddNew链接
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