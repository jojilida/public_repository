using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace LIB.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ManageController : Controller
    {
        [HttpGet]
        public string ManageReturn(String userid)
        {
            string result = "";
            DataSet datatable = new DataSet();
            datatable = DbHelperOra.Query("select * from MY_SEAT_APPOINTMENT natural join MY_FORUM where USER_ID=" + userid);

            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;
        }
        [HttpGet]
        public string GetUserInf(String userid)
        {
            string result = "";
            DataSet datatable = new DataSet();
            datatable = DbHelperOra.Query("select * from MY_USER where USER_ID=" + userid);

            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;
        }
        [HttpPost]
        public string GetLoanBooks(String userid)
        {
            string result = "";
            DataSet datatable = new DataSet();
            datatable = DbHelperOra.Query("select * from MY_LOAN_BOOKS natural join MY_BOOKINFO where LOAN_PEOPLE_ID=" + userid);

            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;
        }
        [HttpPost]
        public string GetDamagedBooks(String userid)
        {
            string result = "";
            var datatable = DbHelperOra.Query("select * from MY_DAMAGED_BOOKS natural join MY_BOOKINFO where USER_ID='" + userid);
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;
        }
        [HttpPost]
        public int GetLoanBooksNum(String userid)
        {
            var data = DbHelperOra.Query("select * from MY_LOAN_BOOKS where LOAN_PEOPLE_ID=" + userid);
            int num = data.Tables[0].Rows.Count;
            return num;
        }
        [HttpPost]
        public int GetRoomsNum(String userid)
        {
            var data = DbHelperOra.Query("select * from MY_ROOM_APPOINTMENT where USER_ID=" + userid);
            int num = data.Tables[0].Rows.Count;
            return num;
        }
    }
}
