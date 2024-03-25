using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebApp_Model.AdminPortalModels.TimeTableModels
{
    public enum LocationEnum : int
    {
        [Display(Name = "B401")]
        B401 = 0,

        [Display(Name = "B402")]
        B402 = 1,

        [Display(Name = "B411")]
        A411 = 2,

        [Display(Name = "B301")]
        B301 = 3
    }
}
