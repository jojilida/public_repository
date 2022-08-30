using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace LIB.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NoticeController : Controller
    {
        [HttpGet]
        public string getnotice()
        {
            string result = "";
            DataSet datatable = new DataSet();
            datatable = DbHelperOra.Query("select * from MY_NOTICE");
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;

        }


    }
}
