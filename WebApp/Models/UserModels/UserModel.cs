using System.ComponentModel;

namespace WebApp.Models.UserModels
{
    public class UserModel
    {
        [DisplayName("User")]
        public Guid UserId { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        [DisplayName("Personal Email")]
        public string PersonalEmail { get; set; }

        [DisplayName("Officeal Email")]
        public string OfficialEmail { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("Mobile Number")]
        public string MobileNumber { get; set; }

        [DisplayName("User Role Enum")]
        [DefaultValue(UserRoleEnum.Admin)]
        public UserRoleEnum UserRoleEnum { get; set; }

        [DisplayName("User Role")]
        public string UserRoleEnumDisplayname
        {
            get { return Enum.GetName(typeof(UserRoleEnum), this.UserRoleEnum); }
        }

        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }

    }
}
