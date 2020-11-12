using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using WEB_API_CORE.DL;
using WEB_API_CORE.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB_API_CORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class clientController : ControllerBase
    {

        // GET: api/<userController1>
        [Authorize]
        [HttpGet]
        public JsonResult Get()
        {
            var result = DBCarServiceDL_CLIENT.ClientOutAll();

            if (result.Count == 0)
            {
                return new JsonResult(new ResponsModel());
            }
            return new JsonResult(result);
        }

        // GET api/<userController1>/5
        //[Authorize]
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var result = DBCarServiceDL_CLIENT.ClientByID(id);

            if (result == null)
            {
                return new JsonResult(new ResponsModel() { status = false, description = "not found" });
            }
            return new JsonResult(result);
        }

        // GET api/<userController1>/5
        [Authorize]
        [HttpGet("search/phone/{phone}")]
        public JsonResult Get(string phone)
        {
            var result = DBCarServiceDL_CLIENT.ClientByPhone(phone);

            if (result.Count == 0)
            {
                return new JsonResult(new ResponsModel() { status = false, description = "not found" });
            }
            return new JsonResult(result);
        }

        [Authorize]
        [HttpPost("search/phone")]
        public JsonResult PostSearch(JObject date)
        {
            var result = DBCarServiceDL_CLIENT.ClientByPhone(date["phone"].ToString());

            if (result.Count == 0)
            {
                return new JsonResult(new ResponsModel() { status = false, description = "not found" });
            }
            return new JsonResult(result);
        }



        // POST api/<userController1>
        [Authorize]
        [HttpPost("new")]
        public JsonResult Post([FromBody] JObject value)
        {
            ClientModel candidat = new ClientModel()
            {
                name = value["name"].ToString(),
                birthDate = Convert.ToDateTime(value["b_date"].ToString()),
                email = value["email"].ToString(),
                lastName = value["last_name"].ToString(),
                phone = value["phone"].ToString()
            };

            int UID = DBCarServiceDL_CLIENT.ClientADD(candidat);

            CarModel candidatCar = new CarModel()
            {
                brand = value["brand"].ToString(),
                mileage = Convert.ToInt32(value["mileageCar"].ToString()),
                model = value["model"].ToString(),
                stateNumber = value["stateNumber"].ToString(),
                VIN = value["vinNumber"].ToString(),
                yearOfIssue = Convert.ToDateTime(value["yearOfIssue"].ToString()),
                clientID = UID

            };

            int CID = DBCarServiceDL_CAR.AddCar(candidatCar);

            var result = DBCarServiceDL_CLIENT.ClientByID(UID);
            if (result != null)
            {
                return new JsonResult(new ResponsModel() { status = true });

            }
            else
                return new JsonResult(new ResponsModel() { status = false, description = "adding failed" });
        }

        //// DELETE api/<userController1>/5
        [Authorize]
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var result = DBCarServiceDL_CLIENT.ClientDeleteByID(id);
            if (result)
            {
                return new JsonResult(new ResponsModel());
            }
            return new JsonResult(new ResponsModel() { status = false, description = "deleting failed" });
        }
    }
}
