using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_3._1
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength =3)]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

    }
}
