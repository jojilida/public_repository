using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace LIB.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ForumController : Controller
    {
        [HttpPost]
        public bool enterForum(String userid,String content,String topic)
        {
            ////string sqlstr = "select * from MY_FORUM where USER_ID=" + userid;
            var data = DbHelperOra.Query("select * from  MY_FORUM");
            int id = data.Tables[0].Rows.Count + 1;
            ////var id = DbHelperOra.Query("select * from MY_SEAT_APPOINTMENT");
            ////string JsonString = string.Empty;
            ////JsonString = JsonConvert.SerializeObject(id.Tables[0]);
            ////return JsonString;
            var strinsertinto = "insert into MY_FORUM (CONTENTS_ID,FORUM_CONTENT,USER_ID,TOPIC,FORUM_BACK,COMMENTS_TIME) values (:contentid,:content,:userid,:topic,:forumbavk,2022-08-28)";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            oracleParameters.Add(new OracleParameter(":contentid", id.ToString()));
            //oracleParameters.Add(new OracleParameter(":rseatid", (1).ToString()));
            oracleParameters.Add(new OracleParameter(":content", content));
            oracleParameters.Add(new OracleParameter(":userid", userid));
            oracleParameters.Add(new OracleParameter(":topic", topic));
            oracleParameters.Add(new OracleParameter(":forumbavk", userid));
            DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
            return true;
        }
        [HttpGet]
        public string getForum()
        {
            string result = "";
            DataSet datatable = new DataSet();
            datatable = DbHelperOra.Query("select * from MY_FORUM");

            //foreach (DataRow item in datatable.Tables[0].Rows)
            //{
            //    Console.WriteLine(item["USER_NAME"].ToString() + "___" + item["USER_ID"].ToString());
            //    result += item["USER_NAME"].ToString() + "___" + item["USER_ID"].ToString() + ",";
            //}
            //string jsonString = "{";
            //foreach (DataRow item in datatable.Tables[0].Rows)
            //{
            //    jsonString += "\"" + item.RowName + "\":" + Json(table) + ",";
            //}
            //jsonString = jsonString.TrimEnd(',');

            //return jsonString + "}";
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;

        }

        [HttpPost]
        public ActionResult deleteForum(String commentid)
        {

            if (String.IsNullOrEmpty(commentid))
            {
                return Ok();
            }
            var strinsertinto = "delete from MY_FORUM where CONTENTS_ID=:id";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            oracleParameters.Add(new OracleParameter(":id", commentid));
            var isok = DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
            Console.WriteLine(isok);


            return Ok();
        }
    }
}
