using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp_Model.AdminPortalModels.FacultyModels
{
    public class FacultyResponse
    {
        public string message { get; set; }

        public bool success { get; set; }

        public string errorcode { get; set; }

        public string errorname { get; set; }

        #region Navigational Properties
        public Faculty faculty { get; set; }
        public IEnumerable<Faculty> faculties { get; set; }
        #endregion
    }
}
