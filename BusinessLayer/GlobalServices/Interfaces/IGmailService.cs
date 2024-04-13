using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.GlobalServices.Interfaces
{
    public interface IGmailService
    {
        Task<bool> SendEmail(string[] recipients, string subject, string body);
    }
}
