using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcTestEntity.Models
{
    public class UserDetails
    {
        [StringLength(7, MinimumLength = 2, ErrorMessage = "UserName length should be between 2 and 7")]
        public string UserName { get; set; }

        [StringLength(100,MinimumLength =5,ErrorMessage ="密码长度应为5-100位")]
        public string Password { get; set; }

    }
}