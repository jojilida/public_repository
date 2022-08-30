using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace LIB.Controllers
{
    //根据userid，返回其借阅信息：BOOK_NAME,ISBN,BOOK_ID,LOAN_TIME，STATE
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoanBooksController : Controller
    {
        [HttpPost]
        public string get_loan_information_by_userid(string userid)
        {
            string result = "";
            var datatable = DbHelperOra.Query("select * from my_reader_borrow where reader_id=" + userid);
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;
        }

        [HttpPost]
        public bool INSERTBOOKLOAN(String bookid, String loan_time, String loan_people_id)
        {

            string id = bookid;
            string sqlstr = "select LOAN_PEOPLE_ID from MY_LOAN_BOOKS where BOOK_ID=" + id;
            var judge1 = DbHelperOra.Exists(sqlstr);
            if (!judge1)
            {

                var strinsertinto = "insert into MY_LOAN_BOOKS (BOOK_ID,LOAN_TIME,LOAN_PEOPLE_ID) " +
                    "values (:bookid,to_date(:loan_time,'yyyy-mm-dd'),:userid)";
                List<OracleParameter> oracleParameters = new List<OracleParameter>();
                oracleParameters.Add(new OracleParameter(":bookid", bookid));
                oracleParameters.Add(new OracleParameter(":loan_time", loan_time));
                oracleParameters.Add(new OracleParameter(":userid", loan_people_id));

                DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
                var sqlstr1 = "update MY_BOOKS set STATE='已经借出' where BOOK_ID=" + bookid;
                DbHelperOra.ExecuteSql(sqlstr1);

                var sqlstr2 = "update MY_LOAN_BOOKS set RETURN_TIME=LOAN_TIME+30 where BOOK_ID=" + bookid;
                DbHelperOra.ExecuteSql(sqlstr2);
                return true;
            }

            return false;
        }

        [HttpGet]
        public string get_all_loan_information()
        {
            string result = "";
            var datatable = DbHelperOra.Query("select * from MY_LOAN_BOOKS");
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;
        }

    }
}
