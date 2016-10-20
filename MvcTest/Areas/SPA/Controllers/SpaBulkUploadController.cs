using MvcTestEntity.Models;
using MvcTestEntity.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using MvcTestEntity.Filters;

namespace MvcTest.Areas.SPA.Controllers
{
    public class SpaBulkUploadController : AsyncController
    {
        // GET: SPA/SpaBulkUpload
        public ActionResult UploadEmployees()
        {
            return PartialView("UploadEmployees");
        }

        [AdminFilter]
        public async Task<ActionResult> Upload( FileUploadViewModel filevm )
        {
            List<Employee> listemp = await Task.Factory.StartNew<List<Employee>>( ()=>GetEmployees(filevm) );
            EmployeeBusinessLayer employeeBL = new EmployeeBusinessLayer();
            employeeBL.UploadEmployees(listemp);

            EmployeeListViewModel employeeListvm = new EmployeeListViewModel();
            List<EmployeeViewModel> listevm = new List<EmployeeViewModel>();
            foreach( Employee e in listemp)
            {
                EmployeeViewModel evm = new EmployeeViewModel();
                evm.EmployeeName = e.FirstName + " " + e.LastName;
                evm.Salary = e.Salary.ToString();
                evm.Salary = int.Parse(evm.Salary).ToString("c");
                if( e.Salary>15000)
                {
                    evm.SalaryColor = "yellow";
                }
                else
                {
                    evm.SalaryColor = "green";
                }
                listevm.Add(evm);
            }
            employeeListvm.Employees = listevm;

            return Json(employeeListvm);


        }

        private List<Employee> GetEmployees(FileUploadViewModel filevm)
        {
            List<Employee> employees = new List<Employee>();
            StreamReader csvreader = new StreamReader(filevm.FileUpload.InputStream);         
            csvreader.ReadLine();//去掉表头
            while (!csvreader.EndOfStream)
            {
                var line = csvreader.ReadLine();
                var value = line.Split('|');
                Employee e = new Employee();
                e.FirstName = value[0];
                e.LastName = value[1];
                e.Salary = int.Parse(value[2]);  //如果文件内容格式有问题将产生异常
                employees.Add(e);
            }
            return employees;           
            
        }

    }
}