using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace LIB.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookController : Controller
    {

        [HttpPost]
        public bool INSERTBOOK(String rate, String bookname, String isbn, String book_damage)
        {    
            Random rad = new Random();
            int id = rad.Next(10, 1000000);
            string sqlstr = "select BOOK_ID from MY_BOOKS where BOOK_ID=" + id;
            var judge1 = DbHelperOra.Exists(sqlstr);
            if (!judge1)
            {
                string bookid = id.ToString();
                //string bookname = "毛选";
                //string isbn = "12345678";
                //string rate = "1234567";
                string borrowed_times = "1234567";
                string state = "1234567";
                //string book_damage = "1234567";
                var strinsertinto = "insert into MY_BOOKS (BOOK_NAME,ISBN,BOOK_ID,RATE,BORROWED_TIMES,STATE,BOOK_DAMAGE) values (:bookname,:isbn,:id,:rate,:borrowed_times,:state,:book_damage)";
                List<OracleParameter> oracleParameters = new List<OracleParameter>();
                
                oracleParameters.Add(new OracleParameter(":bookname", bookname));
                oracleParameters.Add(new OracleParameter(":isbn", isbn));
                oracleParameters.Add(new OracleParameter(":id", bookid));
                oracleParameters.Add(new OracleParameter(":rate", rate));
                oracleParameters.Add(new OracleParameter(":borrowed_times", borrowed_times));
                oracleParameters.Add(new OracleParameter(":state", state));
                oracleParameters.Add(new OracleParameter(":book_damage", book_damage));
                DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
                return true;
            }

            return false;
        }
        [HttpPost]
        public string updaterate(String rate,String bookname)
        {
            if (String.IsNullOrEmpty(rate))
            {
                return "false";
            }
            var strinsertinto = "update MY_BOOKS set RATE=:rate where BOOK_NAME=:bookname";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            oracleParameters.Add(new OracleParameter(":rate", rate));
            oracleParameters.Add(new OracleParameter(":bookname", bookname));
            var isok = DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
            Console.WriteLine(isok);

            return rate;
        }

        [HttpPost]
        public string query()
        {
            var datatable = DbHelperOra.Query("select * from MY_BOOKS");
            foreach (DataRow item in datatable.Tables[0].Rows)
            {
                Console.WriteLine(item["BOOK_NAME"].ToString() + "___" + item["ISBN"].ToString() + "___" + item["BOOK_ID"].ToString());
            }
            return datatable.Tables[0].Rows.ToString();
        }

        [HttpPost]
        public string querybook(String isbn)
        { 
            string result = "";
            string sqlstr = "select * from MY_BOOKS";
            var datatable = DbHelperOra.Query(sqlstr);
            foreach (DataRow item in datatable.Tables[0].Rows)
            {
                Console.WriteLine(item["BOOK_NAME"].ToString() + "___" + item["ISBN"].ToString() + "___" + item["BOOK_ID"].ToString());
                result += item["BOOK_NAME"].ToString() + "___" + item["ISBN"].ToString() + "___" + item["BOOK_ID"].ToString() + ",";
            }
            return result;
        }
    }
}
