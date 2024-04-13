using BusinessLayer.GlobalServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using WebApp_Model.AdminPortalModels.CourseModels;
using WebApp_Model.AdminPortalModels.FacultyModels;
using WebApp_Model.AdminPortalModels.TimeTableModels;

namespace WebApp.Areas.AdminPortal.Controllers
{
    [Area("AdminPortal")]
    public class ClassSessionController : Controller
    {
        private readonly IApplicationUrl _applicationUrl;

        public ClassSessionController(IApplicationUrl applicationUrl)
        {
            _applicationUrl = applicationUrl;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadClassSessionData()
        {
            //var api = "http://localhost:4000/get-classsessions";
            var baseUrl = await _applicationUrl.GetApplicationUrl();
            var api = baseUrl + "/get-classsessions";

            IEnumerable<ClassSession> classSessions = new List<ClassSession>();
            var totalCount = 0;

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var token = Request.Cookies["token"];

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                    var response = await httpClient.GetAsync(api);
                    var classSessionsListJson = await response.Content.ReadAsStringAsync();
                    ClassSessionResponse classSessionResponse = JsonConvert.DeserializeObject<ClassSessionResponse>(classSessionsListJson);

                    if (response.IsSuccessStatusCode)
                    {
                        classSessions = classSessionResponse.classSessions;
                        totalCount = classSessions.Count();

                        return Json(new { draw = 1, recordsFiltered = totalCount, recordsTotal = totalCount, data = classSessions });
                    }
                    else
                    {
                        return Json(new { draw = 1, recordsFiltered = totalCount, recordsTotal = totalCount, data = classSessions });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { draw = 1, recordsFiltered = totalCount, recordsTotal = totalCount, data = classSessions });
                }

            }

        }
        [HttpGet]
        public async Task<IActionResult> ClassSessionModal()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateClassSession()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateClassSession(ClassSession classSession)
        {
            classSession.ClassSessionId = Guid.NewGuid();

            var classSessionJsonString = JsonConvert.SerializeObject(classSession);
            //var api = "http://localhost:4000/create-classsession";
            var baseUrl = await _applicationUrl.GetApplicationUrl();
            var api = baseUrl + "/create-classsession";

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(classSessionJsonString, Encoding.UTF8, "application/json");

                try
                {
                    var token = Request.Cookies["token"];

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                    var response = await httpClient.PostAsync(api, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Error in creating course." });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "API is not working." });

                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditClassSession(string classSessionId)
        {
            //var api = "http://localhost:4000/get-classsession/";
            var baseUrl = await _applicationUrl.GetApplicationUrl();
            var api = baseUrl + "/get-classsession/";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var token = Request.Cookies["token"];

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                    var response = await httpClient.GetAsync(api + classSessionId);

                    if (response.IsSuccessStatusCode)
                    {
                        var classSessionJsonString = await response.Content.ReadAsStringAsync();

                        ClassSessionResponse classSessionResponse = JsonConvert.DeserializeObject<ClassSessionResponse>(classSessionJsonString);

                        var sessionDateTime = classSessionResponse.classSession.SessionDateTime.ToString("yyyy-MM-ddTHH:mm");

                        ViewBag.SessionDateTimeValue = sessionDateTime;
                        ViewBag.LocationEnumValue = (int)classSessionResponse.classSession.LocationEnum;
                        return View(classSessionResponse.classSession);
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditClassSession(ClassSession classSession)
        {
            var classSessionJsonString = JsonConvert.SerializeObject(classSession);
            //var api = "http://localhost:4000/edit-classsession/";
            var baseUrl = await _applicationUrl.GetApplicationUrl();
            var api = baseUrl + "/edit-classsession/";

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(classSessionJsonString, Encoding.UTF8, "application/json");

                try
                {
                    var token = Request.Cookies["token"];

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                    var response = await httpClient.PutAsync(api + classSession.ClassSessionId, content);

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
        public async Task<IActionResult> DeleteClassSession(string classSessionId)
        {
            //var api = "http://localhost:4000/delete-classsession/";
            var baseUrl = await _applicationUrl.GetApplicationUrl();
            var api = baseUrl + "/delete-classsession/";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var token = Request.Cookies["token"];

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                    var response = await httpClient.DeleteAsync(api + classSessionId);

                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { success = true }); ;
                    }
                    else
                    {
                        return Json(new { success = false, message = "Error in dateleting faculty." });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "API is not working." });
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFacultyListJson()
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

                        return Json(faculties);

                    }
                    else
                    {
                        return Json(new { success = false });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false });
                }

            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCourseListJson()
        {
            //var api = "http://localhost:4000/get-courses";
            var baseUrl = await _applicationUrl.GetApplicationUrl();
            var api = baseUrl + "/get-courses";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var token = Request.Cookies["token"];

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                    var response = await httpClient.GetAsync(api);

                    if (response.IsSuccessStatusCode)
                    {
                        var coursesListJson = await response.Content.ReadAsStringAsync();

                        CourseResponse courseResponse = JsonConvert.DeserializeObject<CourseResponse>(coursesListJson);

                        IEnumerable<Course> courses = courseResponse.courses;
                        var totalCount = courses.Count();

                        return Json(courses);

                    }
                    else
                    {
                        return Json(new { success = false });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false });
                }

            }
        }

        [HttpGet]
        public IActionResult GetLocationListJson()
        {
            try
            {
                var locationEnums = Enum.GetValues(typeof(LocationEnum)).Cast<LocationEnum>();

                var locationList = locationEnums.Select(location => new
                {
                    value = (int)location,
                    text = GetEnumDisplayName(location)
                }).ToList();

                return Json(locationList);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }

        private string GetEnumDisplayName(Enum value)
        {
            var displayAttribute = value.GetType().GetMember(value.ToString())
                                     .FirstOrDefault()?.GetCustomAttributes(typeof(DisplayAttribute), false)
                                     .Cast<DisplayAttribute>().FirstOrDefault();
            return displayAttribute?.Name ?? value.ToString();
        }

    }
}
