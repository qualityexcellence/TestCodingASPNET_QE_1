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
    public class CarController : ApiController
    {
        CarMasterService _CarMasterService = new CarMasterService();

        /// <summary>
        /// Add New Car
        /// </summary>
        /// <param name="CarVM">it uses CarVM model which contains user information</param>
        /// <returns>on register successful status code 200 and return string message</returns>
        /// <returns>on register failure status code 400, with string message of invalid user details</returns>
        /// <returns>on failure in other issues, status code 400 with empty string message</returns>
        [ValidateModel]
        [Authorize]
        [Route("newcar")]
        [HttpPost]
        public HttpResponseMessage newcar(CarVM carVM)
        {
            try
            {
                if (!_CarMasterService.CheckDuplicateCarName(carVM.Name))
                {
                    int result = _CarMasterService.CarRegistration(carVM);
                    if (result > 0)
                        return Request.CreateResponse(HttpStatusCode.Created, "new car added successful!");
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, "something went wrong");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "car name already exist");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Get Car data
        /// </summary>
        /// <param name="MadeYear">MadeYear</param>
        /// <returns>on get car information successful status code 200 and return car's data object (CarMaster) model</returns>
        /// <returns>on get car information failure status code 400</returns>
        /// <returns>on failure in other issues, status code 400 with empty string message</returns>
        [Authorize]
        [Route("getcarbyyear")]
        [HttpGet]
        public HttpResponseMessage getcarbyyear(int MadeYear)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    List<CarMaster> cars = _CarMasterService.GetCarByYearMade(MadeYear);
                    if (cars.Count > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, cars);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "No Cars Made on provided year");
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
        /// Car Patch Method, allow us to update partial information related to car
        /// </summary>
        /// <param name="Id">CarId</param>
        /// <param name="carPath">takes JSON Patch document</param>
        /// <returns>on successful status code 200 and return success message</returns>
        /// <returns>if CarId not found status code 200 with error message</returns>
        /// <returns>on failure in unauthorization with error string message</returns>
        [Authorize]
        [Route("updateCar/{id}")]
        [HttpPatch]
        public HttpResponseMessage Patch(int Id, [FromBody] JsonPatchDocument<CarMaster> carPath)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null && Id > 0)
                {
                    int result = _CarMasterService.UpdateCar(Id, carPath);
                    if (result > 0)
                        return Request.CreateResponse(HttpStatusCode.Created, "car details updated successful!");
                    else if (result == -2)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Invalid car ID");
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
        /// Delete Car
        /// </summary>
        /// <param name="Id">CarId</param>
        /// <returns>on successful status code 200 and return success message</returns>
        /// <returns>if CarId not found status code 200 with error message</returns>
        /// <returns>on failure in unauthorization with error string message</returns>
        [Authorize]
        [Route("deleteCar/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int Id)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null && Id > 0)
                {
                    int result = _CarMasterService.DeleteCar(Id);
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
