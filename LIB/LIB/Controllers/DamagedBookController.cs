using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LIB.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DamagedBookController : Controller
    {
        [HttpPost]
        public string Get(string userid)
        {
            string result = "";
            var datatable = DbHelperOra.Query("select * from MY_LOAN_BOOKS where LOAN_PEOPLE=" + userid);
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;
        }
    }
}
