using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp_Model.AdminPortalModels.FacultyModels
{
    public class Faculty
    {
        [DisplayName("Faculty")]
        public Guid FacultyId { get; set; }

        [DisplayName("Faculty Name")]
        public string FacultyName { get; set; }
        
        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
    }
}
