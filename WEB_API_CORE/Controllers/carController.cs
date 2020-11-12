using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json.Linq;
using WEB_API_CORE.DL;
using WEB_API_CORE.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB_API_CORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class carController : ControllerBase
    {
        //// GET api/<carController>/5
        //[Authorize]
        //[HttpGet("car/client/{id}")]
        //public JsonResult Get(int id)
        //{

        //    var result = DBCarServiceDL_CAR.searchCarByClientID(id);

        //    return new JsonResult(result);
        //}

        [Authorize]
        [HttpGet("car/state_number/{number}")]
        public JsonResult Get(string number)
        {
            var result = DBCarServiceDL_CAR.CarByNumber(number);

            if (result == null)
            {
                return new JsonResult(new ResponsModel() { status = false, description = "not found" });
            }
            return new JsonResult(result);
        }

        // POST api/<carController>
        [Authorize]
        [HttpPost]
        public JsonResult Post([FromBody] JObject value)
        {
            CarModel candidat = new CarModel()
            {
                brand = value[""].ToString(),
                mileage = Convert.ToInt32(value[""].ToString()),
                model = value[""].ToString(),
                stateNumber = value[""].ToString(),
                VIN = value[""].ToString(),
                yearOfIssue = Convert.ToDateTime(value[""].ToString()),
                clientID = Convert.ToInt32(value[""].ToString())
            };

            var search = DBCarServiceDL_CLIENT.ClientByID(candidat.clientID);

            if (search == null)
            {
                return new JsonResult(new ResponsModel() { status = false, description = "client not found" });
            }
            else
            {
                var resul = DBCarServiceDL_CAR.AddCar(candidat);
                if (resul > 0)
                {
                    return new JsonResult(new ResponsModel() { status = true });
                }
                else
                    return new JsonResult(new ResponsModel() { status = false, description = "adding failed" });
            }


        }


        // DELETE api/<carController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var result = DBCarServiceDL_CAR.CarDeleteByID(id);

            if (result)
            {
                return new JsonResult(new ResponsModel() { status = true });
            }
            else return new JsonResult(new ResponsModel() { status = false, description = "deleting failed" });
        }
    }
}
