using BusinessLayer.GlobalServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using WebApp_Model.AdminPortalModels;
using WebApp_Model.AdminPortalModels.CourseModels;
using WebApp_Model.AdminPortalModels.FacultyModels;

namespace WebApp.Areas.AdminPortal.Controllers
{
    [Area("AdminPortal")]
    public class CourseController : Controller
    {
        private readonly IApplicationUrl _applicationUrl;

        public CourseController(IApplicationUrl applicationUrl)
        {
            _applicationUrl = applicationUrl;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadCourseData()
        {
            //var api = "http://localhost:4000/get-courses";
            var baseUrl = await _applicationUrl.GetApplicationUrl();
            var api = baseUrl + "/get-courses";

            IEnumerable<Course> courses = new List<Course>();
            var totalCount = 0;

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var token = Request.Cookies["token"];

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                    var response = await httpClient.GetAsync(api);
                    var courseListJson = await response.Content.ReadAsStringAsync();
                    CourseResponse courseResponse = JsonConvert.DeserializeObject<CourseResponse>(courseListJson);

                    if (response.IsSuccessStatusCode)
                    {
                        courses = courseResponse.courses;
                        totalCount = courses.Count();

                        return Json(new { draw = 1, recordsFiltered = totalCount, recordsTotal = totalCount, data = courses }) ;
                    }
                    else
                    {
                        return Json(new { draw = 1, recordsFiltered = totalCount, recordsTotal = totalCount, data = courses });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { draw = 1, recordsFiltered = totalCount, recordsTotal = totalCount, data = courses });
                }

            }

        }

        [HttpGet]
        public async Task<IActionResult> CourseModal()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateCourse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            course.CourseId = Guid.NewGuid();

            var courseJsonString = JsonConvert.SerializeObject(course);
            //var api = "http://localhost:4000/create-course";
            var baseUrl = await _applicationUrl.GetApplicationUrl();
            var api = baseUrl + "/create-course";

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(courseJsonString, Encoding.UTF8, "application/json");

                try
                {
                    var token = Request.Cookies["token"];

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                    var response = await httpClient.PostAsync(api, content);

                    if (response.IsSuccessStatusCode && response.StatusCode.Equals(HttpStatusCode.Created))
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
        public async Task<IActionResult> EditCourse(string courseId)
        {
            //var api = "http://localhost:4000/get-course/";
            var baseUrl = await _applicationUrl.GetApplicationUrl();
            var api = baseUrl + "/get-course/";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var token = Request.Cookies["token"];

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                    var response = await httpClient.GetAsync(api + courseId);

                    if (response.IsSuccessStatusCode)
                    {
                        var courseJsonString = await response.Content.ReadAsStringAsync();

                        CourseResponse courseResponse = JsonConvert.DeserializeObject<CourseResponse>(courseJsonString);

                        return View(courseResponse.course);
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditCourse(Course course)
        {
            var courseJsonString = JsonConvert.SerializeObject(course);
            //var api = "http://localhost:4000/edit-course/";
            var baseUrl = await _applicationUrl.GetApplicationUrl();
            var api = baseUrl + "/edit-course/";

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(courseJsonString, Encoding.UTF8, "application/json");

                try
                {
                    var token = Request.Cookies["token"];

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                    var response = await httpClient.PutAsync(api + course.CourseId, content);

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
        public async Task<IActionResult> DeleteCourse(string courseId)
        {
            //var api = "http://localhost:4000/delete-course/";
            var baseUrl = await _applicationUrl.GetApplicationUrl();
            var api = baseUrl + "/delete-course/";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var token = Request.Cookies["token"];

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                    var response = await httpClient.DeleteAsync(api + courseId);

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
                        return Json(new {success = false});
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false });
                }

            }
        }
    }
}
