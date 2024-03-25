using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp_Model.AdminPortalModels.FacultyModels;

namespace WebApp_Model.AdminPortalModels.CourseModels
{
    public class CourseResponse
    {
        public string message { get; set; }

        public bool success { get; set; }

        public string error { get; set; }
        public string errorcode { get; set; }

        public string errorname { get; set; }

        #region Navigational Properties
        public Course course { get; set; }
        public IEnumerable<Course> courses { get; set; }
        #endregion
    }
}
