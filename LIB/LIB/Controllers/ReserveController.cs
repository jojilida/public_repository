using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace LIB.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReserveController : Controller
    {
        [HttpPost]
        public bool EnterReserve(String userid)
        {
            string sqlstr = "select * from MY_SEAT_APPOINTMENT where STATE=0 and USER_ID=" + userid;
            var data = DbHelperOra.Query("select * from MY_SEAT_APPOINTMENT");
            int id = data.Tables[0].Rows.Count + 1;
            ////var id = DbHelperOra.Query("select * from MY_SEAT_APPOINTMENT");
            ////string JsonString = string.Empty;
            ////JsonString = JsonConvert.SerializeObject(id.Tables[0]);
            ////return JsonString;
            var judge1 = DbHelperOra.Exists(sqlstr);
            if (!judge1)
            {
                var strinsertinto = "insert into MY_SEAT_APPOINTMENT (R_SEAT_ID,STATE,USER_ID,TIME,DAY) values (:rseatid,:state,:userid,:time,:day)";
                List<OracleParameter> oracleParameters = new List<OracleParameter>();
                oracleParameters.Add(new OracleParameter(":rseatid", id.ToString()));
                //oracleParameters.Add(new OracleParameter(":rseatid", (1).ToString()));
                int i = 0;
                oracleParameters.Add(new OracleParameter(":state", i));
                oracleParameters.Add(new OracleParameter(":userid", userid));
                string time, day;
                time = DateTime.Now.ToLongTimeString().ToString();
                day = DateTime.Now.ToShortDateString().ToString();
                oracleParameters.Add(new OracleParameter(":time", time));
                oracleParameters.Add(new OracleParameter(":day", day));
                DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
                return true;
            }
            return false;
        }

        [HttpGet]
        public int GetNumber()
        {
            var data = DbHelperOra.Query("select * from MY_SEAT_APPOINTMENT where STATE=0");
            int num = data.Tables[0].Rows.Count;
            return num;
        }

        [HttpPost]
        public bool EndReserve(String userid)
        {
            string sqlstr = "select * from MY_SEAT_APPOINTMENT where STATE=0 and USER_ID=" + userid;
            var judge1 = DbHelperOra.Exists(sqlstr);
            if (judge1)
            {
                var strinsertinto = "update MY_SEAT_APPOINTMENT set STATE=:state where USER_ID=:userid and STATE=0";
                List<OracleParameter> oracleParameters = new List<OracleParameter>();
                int i = 1;
                oracleParameters.Add(new OracleParameter(":state", i));
                oracleParameters.Add(new OracleParameter(":userid", userid));
                var isok = DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
                Console.WriteLine(isok);
                return true;
            }
            return false;
        }

            //    [HttpPost]
            //    public bool INSERTROOM(String capicity, String room_number)
            //    {

            //        string sqlstr = "select ROOM_NUMBER from MY_SEMINAR_ROOM where ROOM_NUMBER=" + room_number;
            //        var judge1 = DbHelperOra.Exists(sqlstr);
            //        if (!judge1)
            //        {

            //            var strinsertinto = "insert into MY_SEMINAR_ROOM (CAPICITY,ROOM_NUMBER) values (:room_number,:capicity)";
            //            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            //            oracleParameters.Add(new OracleParameter(":capicity", capicity));
            //            oracleParameters.Add(new OracleParameter(":room_number", room_number));
            //            DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
            //            return true;
            //        }
            //        return false;

            //    }

            //    //这个用来实现查询
            //    [HttpGet]
            //    public string getroom()
            //    {
            //        string result = "";
            //        var datatable = DbHelperOra.Query("select * from MY_SEMINAR_ROOM");
            //        string JsonString = string.Empty;
            //        JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            //        return JsonString;

            //    }

            //    //这个用来实现查询
            //    [HttpGet]
            //    public string getroomapp()
            //    {
            //        string result = "";
            //        var datatable = DbHelperOra.Query("select * from MY_ROOM_APPOINTMENT");
            //        string JsonString = string.Empty;
            //        JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            //        return JsonString;

            //    }


        }


}
