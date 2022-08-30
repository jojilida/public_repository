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
        [HttpGet]
        public string GetLoanBooks(String userid)
        {
            string result = "";
            DataSet datatable = new DataSet();
            datatable = DbHelperOra.Query("select * from MY_LOAN_BOOKS where LOAN_PEOPLE_ID=" + userid);

            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;
        }
        [HttpGet]
        public bool ChangePassword(String userid ,String password)
        {
            string sqlstr = "select * from MY_USER where and USER_ID=" + userid;
            var data = DbHelperOra.Query("select * from MY_USER");
            var judge1 = DbHelperOra.Exists(sqlstr);
            if (!judge1) return false;
            var strchange = "update MY_USER set PASSWORD=:password where USER_ID=:userid";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            oracleParameters.Add(new OracleParameter(":password", password));
            oracleParameters.Add(new OracleParameter(":userid", userid));
            DbHelperOra.ExecuteSql(strchange, oracleParameters.ToArray());
            return true;
        }
    }
}
