using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace LIB.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoomController : Controller
    {
        [HttpPost]
        public bool INSERTROOMAPPOINTMENT(String userid, String reserve, String room_number)
        {
            Random rad = new Random();
            int id = rad.Next(10, 1000000);
            string sqlstr = "select R_ROOM_ID from MY_ROOM_APPOINTMENT where R_ROOM_ID=" + id;
            var judge1 = DbHelperOra.Exists(sqlstr);
            if (!judge1)
            {
                string rroomid = id.ToString();
                var strinsertinto = "insert into MY_ROOM_APPOINTMENT (R_ROOM_ID,RESERVE,USER_ID,ROOM_NUMBER) values (:rroomid,to_date(:reserve,'yyyy-mm-dd'),:userid,:room_number)";
                List<OracleParameter> oracleParameters = new List<OracleParameter>();
                oracleParameters.Add(new OracleParameter(":rroomid", rroomid));
                oracleParameters.Add(new OracleParameter(":reserve", reserve));
                oracleParameters.Add(new OracleParameter(":userid", userid));
                oracleParameters.Add(new OracleParameter(":room_number", room_number));
                DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
                return true;
            }

            return false;
        }

        [HttpPost]
        public bool INSERTROOM(String capicity, String room_number)
        {
           
            string sqlstr = "select ROOM_NUMBER from MY_SEMINAR_ROOM where ROOM_NUMBER=" + room_number;
            var judge1 = DbHelperOra.Exists(sqlstr);
            if (!judge1)
            {

                var strinsertinto = "insert into MY_SEMINAR_ROOM (CAPICITY,ROOM_NUMBER) values (:room_number,:capicity)";
                List<OracleParameter> oracleParameters = new List<OracleParameter>();
                oracleParameters.Add(new OracleParameter(":capicity", capicity));
                oracleParameters.Add(new OracleParameter(":room_number", room_number));
                DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
                return true;
            }
            return false;
           
        }

        //这个用来实现查询
        [HttpGet]
        public string getroom()
        {
            string result = "";
            var datatable = DbHelperOra.Query("select * from MY_SEMINAR_ROOM");
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;

        }

        //这个用来实现查询
        [HttpGet]
        public string getroomapp()
        {
            string result = "";
            var datatable = DbHelperOra.Query("select * from MY_ROOM_APPOINTMENT");
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;

        }


    }


}
