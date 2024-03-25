using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp_Model.AdminPortalModels.TimeTableModels
{
    public class ClassSessionResponse
    {
        public string message { get; set; }

        public bool success { get; set; }

        public string error { get; set; }
        public string errorcode { get; set; }

        public string errorname { get; set; }

        #region Navigational Properties
        public ClassSession classSession { get; set; }
        public IEnumerable<ClassSession> classSessions { get; set; }
        #endregion
    }
}
