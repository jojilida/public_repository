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
            //string sqlstr = "select BOOK_NAME from MY_BOOKS where ISBN=" + isbn;
            //var datatable = DbHelperOra.Query(sqlstr);
            //DataRow item = datatable.Tables[0].Rows;
            //int judege1 = (bookname == item["BOOK_NAME"].ToString());
            //if (!judge1)
            //{
                var strinsertinto = "insert into MY_BOOK_NEEDED (BOOK_NAME,ISBN,NEED_NUMS,NEED_ID) values (:bookname,:isbn,:num,:id)";
                List<OracleParameter> oracleParameters = new List<OracleParameter>();
                oracleParameters.Add(new OracleParameter(":bookname", bookname));
                oracleParameters.Add(new OracleParameter(":isbn", isbn));
                oracleParameters.Add(new OracleParameter(":num", num));
                oracleParameters.Add(new OracleParameter(":id", ID));
                DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
                return true;
            //}
            return false;
        }

        [HttpGet]
        public string get_needed_list()
        {
            string result = "";
            var datatable = DbHelperOra.Query("select * from MY_BOOK_NEEDED");
            foreach (DataRow item in datatable.Tables[0].Rows)
            {
                result += item["BOOK_NAME"].ToString() + "," + item["ISBN"].ToString() + "," + item["NEED_NUMS"].ToString() + "," + item["NEED_ID"].ToString() + ";\n";
            }
            return result;
        }
    }

}
