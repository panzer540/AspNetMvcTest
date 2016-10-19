using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcTestEntity.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [FirstNameValidation]
        public string FirstName { get; set; }

        [StringLength(10, ErrorMessage = "The last name shuold not be greater than 10")]
        public string LastName { get; set; }

        [SalaryValidation]
        public int? Salary { get; set; }

    }

    /// <summary>
    /// 自定义FirstName的验证限制
    /// </summary>
    public class FirstNameValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Please input first name");
            }
            else if (value.ToString().Contains("@"))
            {
                return new ValidationResult("The first name shuold not contain @");
            }
            return ValidationResult.Success;
        }
    }


    /// <summary>
    /// 自定义的Salary的验证限制
    /// </summary>
    public class SalaryValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Please input salary");
            }
            return ValidationResult.Success;
        }
    }


}