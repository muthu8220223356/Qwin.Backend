using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        [MaxLength(10)]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
