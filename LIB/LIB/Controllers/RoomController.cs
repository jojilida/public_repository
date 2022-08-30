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
        [HttpGet]
        public string GetMyReservation(String userid)
        {
            string result = "";
            DataSet datatable = new DataSet();
            //datatable = DbHelperOra.Query("select ROOM_TYPE,ROOM_NUMBER,RESERVE_TIME,RESERVE_ID from MY_SEAT_APPOINTMENT where STATE=0 and USER_ID=" + userid);
            datatable = DbHelperOra.Query("select ROOM_TYPE,ROOM_NUMBER,RESERVE_DATE,RESERVE_TIME,RESERVE_ID from MY_ROOM_APPOINTMENT where USER_ID=" + userid);
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;
        }

        [HttpGet]
        public string GetRoom(string type, string date)
        {
            string sqlstr = "";
            DataSet datatable = new DataSet();
            //sqlstr = "select ROOM_TYPE,ROOM_NUMBER,RESERVE_TIME,ROOM_MODE,RESERVE_ID from MY_ROOM_APPOINTMENT";
            sqlstr = "select ROOM_TYPE,ROOM_NUMBER,RESERVE_TIME,ROOM_MODE,RESERVE_ID from MY_ROOM_APPOINTMENT where ROOM_TYPE like \'" + type + "\' and RESERVE_DATE like \'" + date + '\'';
            //datatable = DbHelperOra.Query("select * from MY_SEAT_APPOINTMENT where STATE=0 and USER_ID=" + userid);
            datatable = DbHelperOra.Query(sqlstr);
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;
        }

        [HttpPost]
        public bool ReserveRoom(string userid, string reserve_id)
        {
            string sqlstr = "select * from MY_ROOM_APPOINTMENT where RESERVE_ID like \'" + reserve_id + "\' and ROOM_MODE like \'未预约\'";
            //string sqlstr = "select * from MY_ROOM_APPOINTMENT where RESERVE_ID like \'" + reserve_id + "\'";
            var judge1 = DbHelperOra.Exists(sqlstr);
            if (judge1)
            {
                var strinsertinto = "update MY_ROOM_APPOINTMENT set USER_ID=:userid,ROOM_MODE='已预约' where RESERVE_ID like \'" + reserve_id + "\'";
                List<OracleParameter> oracleParameters = new List<OracleParameter>();
                oracleParameters.Add(new OracleParameter(":userid", userid));
                var isok = DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
                Console.WriteLine(isok);
                return true;
            }
            return false;
        }

        [HttpPost]
        public bool CancelRoomReserve(string reserve_id)
        {
            string sqlstr = "select * from MY_ROOM_APPOINTMENT where RESERVE_ID like \'" + reserve_id + "\' and ROOM_MODE like \'已预约\'";
            var judge1 = DbHelperOra.Exists(sqlstr);
            if (judge1)
            {
                var strinsertinto = "update MY_ROOM_APPOINTMENT set USER_ID=NULL,ROOM_MODE='未预约' where RESERVE_ID like \'" + reserve_id + "\'";
                List<OracleParameter> oracleParameters = new List<OracleParameter>();
                var isok = DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
                Console.WriteLine(isok);
                return true;
            }
            return false;
        }

        //[HttpPost]
        //public bool new_reservation_list(string date)
        //{
        //    var str = "delete from MY_ROOM_APPOINTMENT where RESERVE_DATE=\'" + date + "\'";
        //    List<OracleParameter> oracleParameters = new List<OracleParameter>();
        //    var isok = DbHelperOra.ExecuteSql(str, oracleParameters.ToArray());
        //    Console.WriteLine(isok);
        //    var data = DbHelperOra.Query("select * from MY_ROOM_APPOINTMENT");
        //    int reserve_id = data.Tables[0].Rows.Count;
        //    data = DbHelperOra.Query("select * from MY_ROOM_APPOINTMENT where RESERVE_DATE like '08-26'");
        //    foreach (DataRow item in data.Tables[0].Rows)
        //    {
        //        string strinsertinto2 = "insert into MY_ROOM_APPOINTMENT(RESERVE_ID,RESERVE_DATE,ROOM_NUMBER,ROOM_TYPE,RESERVE_TIME) values (:reserve_id,:reserve_date,:room_num,:room_type,:reserve_time)";
        //        //string strinsertinto2 = "insert into MY_ROOM_APPOINTMENT(RESERVE_ID,RESERVE_DATE,ROOM_NUMBER,ROOM_TYPE,RESERVE_TIME,ROOM_MODE) values (:reserve_id,:reserve_date,:room_num,:room_type,:reserve_time,:mode)";
        //        List<OracleParameter> oracleParameters2 = new List<OracleParameter>();
        //        reserve_id++;
        //        oracleParameters2.Add(new OracleParameter(":reserve_id", reserve_id));
        //        oracleParameters2.Add(new OracleParameter(":reserve_date", date));
        //        oracleParameters2.Add(new OracleParameter(":room_num", item["ROOM_NUMBER"].ToString()));
        //        oracleParameters2.Add(new OracleParameter(":room_type", item["ROOM_TYPE"].ToString()));
        //        oracleParameters2.Add(new OracleParameter(":reserve_time", item["RESERVE_TIME"].ToString()));
        //        //oracleParameters2.Add(new OracleParameter(":mode", item["ROOM_MODE"].ToString()));
        //        var isok2 = DbHelperOra.ExecuteSql(strinsertinto2, oracleParameters2.ToArray());
        //        Console.WriteLine(isok2);
        //        //Console.WriteLine(item["USER_NAME"].ToString() + "___" + item["USER_ID"].ToString());
        //        //result += item["USER_NAME"].ToString() + "___" + item["USER_ID"].ToString() + ",";
        //    }
        //    return true;
        //}


        //[HttpPost]
        //public bool INSERTROOMAPPOINTMENT(String userid, String reserve, String room_number)
        //{
        //    Random rad = new Random();
        //    int id = rad.Next(10, 1000000);
        //    string sqlstr = "select R_ROOM_ID from MY_ROOM_APPOINTMENT where R_ROOM_ID=" + id;
        //    var judge1 = DbHelperOra.Exists(sqlstr);
        //    if (!judge1)
        //    {
        //        string rroomid = id.ToString();
        //        var strinsertinto = "insert into MY_ROOM_APPOINTMENT (R_ROOM_ID,RESERVE,USER_ID,ROOM_NUMBER) values (:rroomid,to_date(:reserve,'yyyy-mm-dd'),:userid,:room_number)";
        //        List<OracleParameter> oracleParameters = new List<OracleParameter>();
        //        oracleParameters.Add(new OracleParameter(":rroomid", rroomid));
        //        oracleParameters.Add(new OracleParameter(":reserve", reserve));
        //        oracleParameters.Add(new OracleParameter(":userid", userid));
        //        oracleParameters.Add(new OracleParameter(":room_number", room_number));
        //        DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
        //        return true;
        //    }

        //    return false;
        //}

        //[HttpPost]
        //public bool INSERTROOM(String capicity, String room_number)
        //{

        //    string sqlstr = "select ROOM_NUMBER from MY_SEMINAR_ROOM where ROOM_NUMBER=" + room_number;
        //    var judge1 = DbHelperOra.Exists(sqlstr);
        //    if (!judge1)
        //    {

        //        var strinsertinto = "insert into MY_SEMINAR_ROOM (CAPICITY,ROOM_NUMBER) values (:room_number,:capicity)";
        //        List<OracleParameter> oracleParameters = new List<OracleParameter>();
        //        oracleParameters.Add(new OracleParameter(":capicity", capicity));
        //        oracleParameters.Add(new OracleParameter(":room_number", room_number));
        //        DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
        //        return true;
        //    }
        //    return false;

        //}

        ////这个用来实现查询
        //[HttpGet]
        //public string getroom()
        //{
        //    string result = "";
        //    var datatable = DbHelperOra.Query("select * from MY_SEMINAR_ROOM");
        //    string JsonString = string.Empty;
        //    JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
        //    return JsonString;

        //}

        ////这个用来实现查询
        //[HttpGet]
        //public string getroomapp()
        //{
        //    string result = "";
        //    var datatable = DbHelperOra.Query("select * from MY_ROOM_APPOINTMENT");
        //    string JsonString = string.Empty;
        //    JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
        //    return JsonString;

        //}


    }


}