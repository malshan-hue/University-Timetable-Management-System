using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp_Model.AdminPortalModels.FacultyModels;

namespace WebApp_Model.AdminPortalModels.CourseModels
{
    public class Course
    {
        [DisplayName("Course")]
        public Guid CourseId { get; set; }
        public Guid FacultyId { get; set; }

        [DisplayName("Course Code")]
        public string CourseCode { get; set; }

        [DisplayName("Course Name")]
        public string CourseName { get; set; }

        [DisplayName("Course Description")]
        public string CourseDescription { get; set; }

        [DisplayName("Course Credit")]
        public int CourseCredit { get; set; }

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }

        #region Navigational Properties
        public Faculty Faculty { get; set; }
        #endregion
    }
}
