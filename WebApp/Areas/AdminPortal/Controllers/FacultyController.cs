using BusinessLayer.GlobalServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using WebApp_Model.AdminPortalModels.FacultyModels;

namespace WebApp.Areas.AdminPortal.Controllers
{
    [Area("AdminPortal")]
    public class FacultyController : Controller
    {
        private readonly IApplicationUrl _applicationUrl;
        public FacultyController(IApplicationUrl applicationUrl)
        {
            _applicationUrl = applicationUrl;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadFacultyData()
        {
            //var api = "http://localhost:4000/get-faculties";
            var baseUrl = await _applicationUrl.GetApplicationUrl();
            var api = baseUrl + "/get-faculties";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var token = Request.Cookies["token"];

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                    var response = await httpClient.GetAsync(api);

                    if (response.IsSuccessStatusCode)
                    {
                        var facultieListJson = await response.Content.ReadAsStringAsync();

                        FacultyResponse facultyResponse = JsonConvert.DeserializeObject<FacultyResponse>(facultieListJson);

                        IEnumerable<Faculty> faculties = facultyResponse.faculties;
                        var totalCount = faculties.Count();

                        return Json(new { draw = 1, recordsFiltered = totalCount, recordsTotal = totalCount, data = faculties });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Error in creating faculty." });
                    }
                }catch(Exception ex)
                {
                    return Json(new { success = false, message = "API is not working." });
                }

            }
            
        }

        [HttpGet]
        public async Task<IActionResult> FacultyModal()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateFaculty()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFaculty(Faculty faculty)
        {
            faculty.FacultyId = Guid.NewGuid();

            var facultyJsonString = JsonConvert.SerializeObject(faculty);
            //var api = "http://localhost:4000/create-faculty";
            var baseUrl = await _applicationUrl.GetApplicationUrl();
            var api = baseUrl + "/create-faculty";

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(facultyJsonString, Encoding.UTF8, "application/json");

                try
                {
                    var token = Request.Cookies["token"];

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                    var response = await httpClient.PostAsync(api, content);

                    if(response.IsSuccessStatusCode && response.StatusCode.Equals(HttpStatusCode.Created))
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Error in creating faculty." });
                    }
                }catch(Exception ex)
                {
                    return Json(new { success = false, message = "API is not working." });

                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditFaculty(string facultyId)
        {
            //var api = "http://localhost:4000/get-faculty/";
            var baseUrl = await _applicationUrl.GetApplicationUrl();
            var api = baseUrl + "/get-faculty/";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var token = Request.Cookies["token"];

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                    var response = await httpClient.GetAsync(api + facultyId);

                    if (response.IsSuccessStatusCode)
                    {
                        var facultyJson = await response.Content.ReadAsStringAsync();

                        FacultyResponse facultyResponse = JsonConvert.DeserializeObject<FacultyResponse>(facultyJson);

                        return View(facultyResponse.faculty);
                    }
                }
                catch(Exception ex)
                {

                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditFaculty(Faculty faculty)
        {
            var facultyJsonString = JsonConvert.SerializeObject(faculty);
            //var api = "http://localhost:4000/edit-faculty/";
            var baseUrl = await _applicationUrl.GetApplicationUrl();
            var api = baseUrl + "/edit-faculty/";

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(facultyJsonString, Encoding.UTF8, "application/json");

                try
                {
                    var token = Request.Cookies["token"];

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                    var response = await httpClient.PutAsync(api + faculty.FacultyId, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Error in updated faculty." });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "API is not working." });
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFaculty(string facultyId)
        {
            //var api = "http://localhost:4000/delete-faculty/";
            var baseUrl = await _applicationUrl.GetApplicationUrl();
            var api = baseUrl + "/delete-faculty/";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var token = Request.Cookies["token"];

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                    var response = await httpClient.DeleteAsync(api + facultyId);

                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { success = true }); ;
                    }
                    else
                    {
                        return Json(new { success = false, message = "Error in dateleting faculty." });
                    }
                }catch(Exception ex)
                {
                    return Json(new { success = false, message = "API is not working." });
                }
            }
        }
    }
}
