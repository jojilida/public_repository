using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace LIB.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AdminController : Controller
    {
        [HttpPost]
        public bool AddNotice(string title, string text)
        {
            var data = DbHelperOra.Query("select * from MY_NOTICE");
            int id = data.Tables[0].Rows.Count + 1;
            var strinsertinto = "insert into MY_NOTICE(ID,TITLE,RELEASE_DATE,TEXT) values (:id,:title,:day,:text)";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            oracleParameters.Add(new OracleParameter(":id", id.ToString()));
            oracleParameters.Add(new OracleParameter(":title", title));
            string day = DateTime.Now.ToString("yyyy-MM-dd");
            oracleParameters.Add(new OracleParameter(":day", day));
            oracleParameters.Add(new OracleParameter(":text", text));
            DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
            return true;
        }

        [HttpPost]
        public bool EditNotice(string id, string title, string text)
        {
            var strinsertinto = "update MY_NOTICE set TITLE=\'"+title+"\',TEXT=\'"+text+"\' where ID like \'" + id + "\'";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            var isok = DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
            Console.WriteLine(isok);
            return true;
        }

        [HttpPost]
        public bool DeleteNotice(string id)
        {
            var strinsertinto = "delete from MY_NOTICE where ID like \'" + id + "\'";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            var isok = DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
            Console.WriteLine(isok);
            return true;
        }

        [HttpGet]
        public string GetDamagedBook()
        {
            DataSet datatable = new DataSet();
            datatable = DbHelperOra.Query("select * from MY_DAMAGED_BOOK");
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;
        }

        [HttpGet]
        public string GetBooks()
        {
            DataSet datatable = new DataSet();
            datatable = DbHelperOra.Query("select * from MY_BOOKS");
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;
        }
    }
}
