using JsonPatch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using TestCodingASPNET_QE_API.Filters;
using TestCodingASPNET_QE_BAL;
using TestCodingASPNET_QE_DAL;

namespace TestCodingASPNET_QE_API.Controllers
{
    public class HomeController : ApiController
    {
        UserMasterService _userMasterService = new UserMasterService();

        /// <summary>
        /// Login for accessing APIs resources
        /// </summary>
        /// <param name="loginVM">it uses LoginVM model which contains two properties Email and Password</param>
        /// <returns>on successful status code 200 and User object with JWT Token</returns>
        /// <returns>on login failure status code 400, with string message of invalid credentials</returns>
        /// <returns>on failure in other issues, status code 400 with empty string message</returns>

        [ValidateModel]
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Post(LoginVM loginVM)
        {
            try
            {
                if (loginVM != null)
                {
                    List<UserMaster> userProfile = _userMasterService.Login(loginVM.Email, loginVM.Password).ToList();
                    if (userProfile.Count > 0)
                    {
                        userProfile[0].UserPassword = "";
                        userProfile[0].Token = JwtManager.GenerateToken(loginVM.Email);
                        return Request.CreateResponse(HttpStatusCode.OK, userProfile[0]);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Invalid credentials");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "missing credentials");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        /// <summary>
        /// User Registration
        /// </summary>
        /// <param name="RegisterVM">it uses RegisterVM model which contains user information</param>
        /// <returns>on register successful status code 200 and return string message</returns>
        /// <returns>on register failure status code 400, with string message of invalid user details</returns>
        /// <returns>on failure in other issues, status code 400 with empty string message</returns>
        [ValidateModel]
        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public HttpResponseMessage register(RegisterVM registerVM)
        {
            try
            {
                if (!_userMasterService.CheckDuplicateEmail(registerVM.EmailID))
                {
                    int result = _userMasterService.UserRegistration(registerVM);
                    if (result > 0)
                        return Request.CreateResponse(HttpStatusCode.Created, "registration successful!");
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, "something went wrong");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "email address already exist");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Get User data
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <returns>on get user profile successful status code 200 and return user's profile object (UserMaster) model</returns>
        /// <returns>on get user profile failure status code 400</returns>
        /// <returns>on failure in other issues, status code 400 with empty string message</returns>
        [Authorize]
        [Route("getprofile")]
        [HttpGet]
        public HttpResponseMessage getprofile(int UserId)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    UserMaster user = _userMasterService.GetUserById(UserId);
                    if (user.UserMasterId > 0)
                    {
                        user.UserPassword = "";
                        return Request.CreateResponse(HttpStatusCode.OK, user);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Invalid user ID");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "something went wrong");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
        /// <summary>
        /// User Patch Method, allow us to update partial object
        /// </summary>
        /// <param name="Id">UserId</param>
        /// <param name="userPath">takes JSON Patch document</param>
        /// <returns>on successful status code 200 and return success message</returns>
        /// <returns>if UserId not found status code 200 with error message</returns>
        /// <returns>on failure in unauthorization with error string message</returns>
        [Authorize]
        [Route("updateUser/{id}")]
        [HttpPatch]
        public HttpResponseMessage Patch(int Id, [FromBody] JsonPatchDocument<UserMaster> userPath)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null && Id > 0)
                {
                    int result = _userMasterService.UpdateUser(Id, userPath);
                    if (result > 0)
                        return Request.CreateResponse(HttpStatusCode.Created, "update details successful!");
                    else if (result == -2)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Invalid user ID");
                    }
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, "something went wrong");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "authorization failed");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="Id">UserId</param>
        /// <returns>on successful status code 200 and return success message</returns>
        /// <returns>if UserId not found status code 200 with error message</returns>
        /// <returns>on failure in unauthorization with error string message</returns>
        [Authorize]
        [Route("deleteUser/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int Id)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null && Id > 0)
                {
                    int result = _userMasterService.DeleteUser(Id);
                    if (result > 0)
                        return Request.CreateResponse(HttpStatusCode.Created, "delete details successful!");
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, "something went wrong");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "authorization failed");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.InnerException.Message);
            }
        }
    }
}
