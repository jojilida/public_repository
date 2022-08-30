using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace LIB.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookBackController : Controller
    {
        [HttpPost]
        public bool INSERTBOOKBACK(String bookid, String back_time, String back_people_id)
        {

      

                var strinsertinto = "insert into MY_BOOK_BACK (BOOK_ID,BACK_TIME,BACK_PEOPLE_ID) " +
                    "values (:bookid,to_date(:back_time,'yyyy-mm-dd'),:userid)";
                List<OracleParameter> oracleParameters = new List<OracleParameter>();
                oracleParameters.Add(new OracleParameter(":bookid", bookid));
                oracleParameters.Add(new OracleParameter(":back_time", back_time));
                oracleParameters.Add(new OracleParameter(":userid", back_people_id));

                DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());

                var sqlstr1 = "update MY_BOOKS set STATE='已归还' where BOOK_ID=" + bookid;
                DbHelperOra.ExecuteSql(sqlstr1);
                return true;
           
        }
    }
}
