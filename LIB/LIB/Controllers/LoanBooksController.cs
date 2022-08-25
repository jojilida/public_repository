using Microsoft.AspNetCore.Mvc;
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
        public string get_loan_information(string userid)
        {
            string result = "";
            var datatable = DbHelperOra.Query("select * from MY_LOAN_BOOKS where LOAN_PEOPLE="+userid);
            foreach (DataRow item in datatable.Tables[0].Rows)
            {
                result += item["BOOK_NAME"].ToString() + "," + item["ISBN"].ToString() + "," + item["BOOK_ID"].ToString() + "," + item["LOAN_TIME"].ToString();
                //var data2 = DbHelperOra.Query("select * from MY_BOOK_BACK where BACK_PEOPLE=" + userid+" and ISBN="+ item["ISBN"].ToString());
                var data2 = DbHelperOra.Query("select STATE from MY_BOOKS where BOOK_ID=" + item["BOOK_ID"].ToString());
                if (data2 != null)
                {
                    result += ",yes";
                }
                else
                {
                    result += ",no";
                }
                result+="/n";
            }
            return result;
        }
    }
}
