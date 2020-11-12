using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WEB_API_CORE.DL;
using WEB_API_CORE.Hash;
using WEB_API_CORE.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB_API_CORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class adminController : ControllerBase
    {
        // GET: api/<adminController1>
        //[Authorize]
        //[HttpGet]
        //public JsonResult Get()
        //{
        //    var result = DBCarServiceDL_ADMIN.allAdmin();

        //    return new JsonResult(result);
        //}

        // GET api/<adminController1>/5
        //[Authorize]
        //[HttpGet("{id}")]
        //public JsonResult Get(int id)
        //{
        //    var result = DBCarServiceDL_ADMIN.searchByID();

        //    return new JsonResult(result);
        //}

        // POST api/<adminController1>
        //[Authorize]
        [HttpPost]
        public JsonResult Post([FromBody] JObject value)
        {
            AdminModel candidate = new AdminModel()
            {
                login = value["login"].ToString(),
                password = GetCode.Hash(value["password"].ToString()),
                role = value["role"].ToString()

            };

            var result = DBCarServiceDL_ADMIN.SearchAdmin(candidate.login);

            if (result == null)
            {
                var temp = DBCarServiceDL_ADMIN.AdminADD(candidate);
                if (temp > 0)
                {
                    return new JsonResult(new ResponsModel() { status = true });
                }
                else return new JsonResult(new ResponsModel() { status = false, description = "registration failed" });
            }
            else return new JsonResult(new ResponsModel() { status = false, description = "already exists" });

        }



        //// DELETE api/<adminController1>/5
        //[Authorize]
        //[HttpDelete("{id}")]
        //public JsonResult Delete(int id)
        //{
        //    var result= DBCarServiceDL_ADMIN.deleteByID(id);

        //    return new JsonResult(new ResponsModel() {  status = result });
        //}
    }
}
