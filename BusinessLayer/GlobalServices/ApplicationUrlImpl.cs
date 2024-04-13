using BusinessLayer.GlobalServices.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.GlobalServices
{
    public class ApplicationUrlImpl : IApplicationUrl
    {
        private readonly IConfiguration _configuration;

        public ApplicationUrlImpl(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetApplicationUrl()
        {
            return _configuration["BaseUrl"];
        }
    }
}
