namespace MvcDay_33.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("user")]
    public partial class user
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "username")]
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }

        [StringLength(50)]
        //   [RegularExpression("",ErrorMessage="invalid email")]

        //[Remote("check", "userdata", ErrorMessage = "email aleardy exist !!")]

        public string Email { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "*")]
        public string Password { get; set; }


        [NotMapped]
        [Required(ErrorMessage = "*")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "password not match !!")]
        public string confirm_password { get; set; }



        [Range(20, 60, ErrorMessage = "invalid age")]
        public int? age { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(150)]
        public string Photo { get; set; }

        public int? deptId { get; set; }

        public virtual department department { get; set; }
    }
}
