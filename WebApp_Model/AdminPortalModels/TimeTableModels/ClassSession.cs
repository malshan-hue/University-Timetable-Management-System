using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp_Model.AdminPortalModels.CourseModels;
using WebApp_Model.AdminPortalModels.FacultyModels;

namespace WebApp_Model.AdminPortalModels.TimeTableModels
{
    public class ClassSession
    {
        public Guid ClassSessionId { get; set; }

        public Guid FacultyId { get; set; }

        public Guid CourseId { get; set; }

        [DisplayName("Time")]
        public DateTime SessionDateTime { get; set; }

        [DisplayName("Location")]
        public LocationEnum LocationEnum { get; set; }

        [DisplayName("Location")]
        public string LocationEnumDisplayname
        {
            get { return Enum.GetName(typeof(LocationEnum), this.LocationEnum); }
        }

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }

        #region Navigational Properties
        public Faculty Faculty { get; set; }
        public Course Course { get; set; }
        #endregion
    }
}
