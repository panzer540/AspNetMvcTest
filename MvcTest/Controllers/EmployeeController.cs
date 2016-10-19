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
        [Authorize][HeaderFooterFilter]
        public ActionResult Index()
        {
            EmployeeListViewModel empListVM = new EmployeeListViewModel();

            List<EmployeeViewModel> evmList = new List<EmployeeViewModel>();
            List<Employee> lemp = new List<Employee>();

            EmployeeBusinessLayer ebl = new EmployeeBusinessLayer();
            lemp = ebl.GetEmployees();

            foreach (Employee emp in lemp)
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
            }

            empListVM.Employees = evmList;

            return View("Index", empListVM);
        }


        /// <summary>
        /// 保存网页表单提交的数据：保存Employee
        /// </summary>
        /// <param name="e"></param>
        /// <param name="btnSubmit"></param>
        /// <returns></returns>
        [AdminFilter][HeaderFooterFilter]
        public ActionResult SaveEmployee(Employee e, string btnSubmit)
        {

            switch (btnSubmit)
            {
                case "Save":
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
                case "Cancel":
                    return RedirectToAction("Index");
            }
            return new EmptyResult();
        }


        /// <summary>
        /// 跳转到添加页面
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