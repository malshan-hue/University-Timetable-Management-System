using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimpleCrypto;
using System.Net;
using System.Text;
using WebApp.Models.UserModels;
using System.Text.Json;
using NuGet.Protocol.Plugins;
using System.Security.Cryptography.Xml;
using NuGet.Common;
using System.Net.Http.Headers;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        public UserController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            //var userJsonString = HttpContext.Session.GetString("User");
            var userJsonString = HttpContext.Request.Cookies["User"];

            if (userJsonString is not null)
            {
                var user = JsonConvert.DeserializeObject<UserModel>(Uri.UnescapeDataString(userJsonString));

                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("FirstName", user.FirstName.ToString());
                HttpContext.Session.SetString("LastName", user.LastName.ToString());
                HttpContext.Session.SetInt32("UserRole", (int)user.UserRoleEnum);
                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));

                var sessionOptions = new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(1)
                };
                HttpContext.Response.Cookies.Append("MySessionCookie", "value", sessionOptions);

                switch (user.UserRoleEnum)
                {
                    case UserRoleEnum.Admin:
                        return RedirectToAction("Index", "Dashboard", new { Area = "AdminPortal" });
                    case UserRoleEnum.Faculty:
                        return RedirectToAction("Index", "Dashboard", new { Area = "FacultyPortal" });
                    case UserRoleEnum.Student:
                        return RedirectToAction("Index", "Dashboard", new { Area = "StudentPortal" });
                    default:
                        return RedirectToAction("Index", "User");
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserModel userModel)
        {
            if(userModel.UserName != null && userModel.Password != null)
            {
                var userJsonString = JsonConvert.SerializeObject(userModel);
                var api = "http://localhost:4000/login";

                using (var httpClient = new HttpClient())
                {
                    var content = new StringContent(userJsonString, Encoding.UTF8, "application/json");

                    try
                    {
                        var response = await httpClient.PostAsync(api, content);

                        if (response.IsSuccessStatusCode && response.StatusCode.Equals(HttpStatusCode.OK))
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();

                            var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);

                            if(loginResponse != null)
                            {
                                ICryptoService cryptoService = new PBKDF2();

                                var userPasword = loginResponse.user.Password;
                                var submittedPassword = userModel.Password;
                                var userPasswordSalt = loginResponse.user.PasswordSalt;

                                if (cryptoService.Compare(userPasword, cryptoService.Compute(submittedPassword, userPasswordSalt)))
                                {
                                    HttpContext.Session.SetString("UserId", loginResponse.user.UserId.ToString());
                                    HttpContext.Session.SetString("FirstName", loginResponse.user.FirstName.ToString());
                                    HttpContext.Session.SetString("LastName", loginResponse.user.LastName.ToString());
                                    HttpContext.Session.SetInt32("UserRole", (int)loginResponse.user.UserRoleEnum);
                                    HttpContext.Session.SetString("UserName", loginResponse.user.UserName);
                                    HttpContext.Session.SetString("User", JsonConvert.SerializeObject(loginResponse.user));

                                    var sessionOptions = new CookieOptions
                                    {
                                        Expires = DateTime.UtcNow.AddDays(1)
                                    };
                                    HttpContext.Response.Cookies.Append("MySessionCookie", "value", sessionOptions);

                                    var userCookieOptions = new CookieOptions
                                    {
                                        Expires = DateTime.UtcNow.AddDays(7),
                                    };
                                    HttpContext.Response.Cookies.Append("User", Uri.EscapeDataString(JsonConvert.SerializeObject(loginResponse.user)), userCookieOptions);

                                    var token = loginResponse.token;
                                    HttpContext.Response.Cookies.Append("token", token, new CookieOptions
                                    {
                                        HttpOnly = true,
                                        SameSite = SameSiteMode.Strict,
                                        Expires = DateTime.Now.AddDays(1)
                                    });

                                    if (loginResponse.user.UserRoleEnum.Equals(UserRoleEnum.Admin))
                                    {
                                        return RedirectToAction("Index", "Dashboard", new { Area = "AdminPortal" });
                                    }

                                    if (loginResponse.user.UserRoleEnum.Equals(UserRoleEnum.Faculty))
                                    {
                                        return RedirectToAction("Index", "Dashboard", new { Area = "FacultyPortal" });
                                    }

                                    if (loginResponse.user.UserRoleEnum.Equals(UserRoleEnum.Student))
                                    {
                                        return RedirectToAction("Index", "Dashboard", new { Area = "StudentPortal" });
                                    }

                                }
                                else
                                {
                                    return RedirectToAction("Login");
                                }
                            }

                            return RedirectToAction("Login");
                        }
                        else
                        {
                            return RedirectToAction("Login");
                        }
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("Login");
                    }
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserModel userModel)
        {
            userModel.UserId = Guid.NewGuid();

            //generate official email
            var officialEmail = userModel.FirstName.ToLower() + "." + userModel.LastName.ToLower() + "@sliit.lk";
            userModel.OfficialEmail = officialEmail;
            userModel.UserName = officialEmail;

            //encrypt password
            var crypto = new PBKDF2();
            var encryptedPwd = crypto.Compute(userModel.Password);
            userModel.Password = encryptedPwd;
            userModel.PasswordSalt = crypto.Salt;

            var userJsonString = JsonConvert.SerializeObject(userModel);
            var api = "http://localhost:4000/signup";

            using(var httpClient = new HttpClient())
            {
                var content = new StringContent(userJsonString, Encoding.UTF8, "application/json");
                try
                {
                    var response = await httpClient.PostAsync(api, content);
                    //var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode && response.StatusCode.Equals(HttpStatusCode.Created))
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        return RedirectToAction("SignUp");
                    }
                }catch(Exception ex)
                {
                    return RedirectToAction("SignUp");
                }
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Delete("User");

            return RedirectToAction("Login");
        }

    }


}
