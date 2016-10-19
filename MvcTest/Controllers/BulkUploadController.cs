using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTestEntity.ViewModels;
using MvcTestEntity.Filters;
using MvcTestEntity.Models;
using System.IO;
using System.Threading.Tasks;

namespace MvcApplication1.Controllers
{
    public class BulkUploadController : AsyncController
    {
        // GET: BulkUpload
        [AdminFilter][HeaderFooterFilter]
        public ActionResult Index()
        {
            return View( new FileUploadViewModel() );
        }




        [AdminFilter][HandleError]
        public async Task<ActionResult> Upload( FileUploadViewModel filevm )
        {
            List<Employee> employees = await Task.Factory.StartNew<List<Employee>>( ()=> GetEmployees(filevm))    ;
            EmployeeBusinessLayer ebl = new EmployeeBusinessLayer();
            ebl.UploadEmployees(employees);

            return RedirectToAction("Index","Employee");
        }

        private List<Employee> GetEmployees( FileUploadViewModel filevm )
        {
            List<Employee> employees = new List<Employee>();
            StreamReader csvreader = new StreamReader( filevm.FileUpload.InputStream );
            csvreader.ReadLine();//去掉表头
            while( !csvreader.EndOfStream )
            {
                var line = csvreader.ReadLine();
                var value = line.Split('|');
                Employee e = new Employee();
                e.FirstName = value[0];
                e.LastName = value[1];
                e.Salary = int.Parse(value[2]);
                employees.Add(e);
            }
            return employees;

        }



       
    }
}