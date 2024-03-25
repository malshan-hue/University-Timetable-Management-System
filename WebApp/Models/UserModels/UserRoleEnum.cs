using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.UserModels
{
    public enum UserRoleEnum : int
    {
        [Display(Name = "Admin")]
        Admin = 0,

        [Display(Name = "Faculty")]
        Faculty = 1,

        [Display(Name = "Student")]
        Student = 3
    }
}
