using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace LIB.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookNeededController : Controller
    {
        [HttpPost]
        public bool insert_book_needed(String bookname, String isbn, int num, int ID)
        {
            string sqlstr = "select * from MY_BOOKS where ISBN=" + isbn;
            var judge1 = DbHelperOra.Exists(sqlstr);
            if (!judge1)
            {
                var strinsertinto = "insert into MY_BOOK_NEEDED (BOOK_NAME,ISBN,NEED_NUMS,NEED_ID) values (:bookname,:isbn,:num,:id)";
                List<OracleParameter> oracleParameters = new List<OracleParameter>();
                oracleParameters.Add(new OracleParameter(":bookname", bookname));
                oracleParameters.Add(new OracleParameter(":isbn", isbn));
                oracleParameters.Add(new OracleParameter(":num", num));
                oracleParameters.Add(new OracleParameter(":id", ID));
                DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
                return true;
            }

            return false;
        }
    }
}
