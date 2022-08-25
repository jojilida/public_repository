using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace LIB.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookInfoController : Controller
    {
        [HttpPost]
        public bool INSERTBOOKINFO(String bookname, String author, String translater, String repre, String publisher, String isbn, String booknumber, String booktext, String authorabout)
        {


            var strinsertinto = "insert into MY_BOOKINFO (BOOK_NAME,BOOK_AUTHOR,BOOK_TRANSLATER,BOOK_REPRE,BOOK_PUBLISHER,ISBN,BOOK_COLLECTION_NUMBER,BOOK_TEXT,BOOK_AUTHORABOUT) " +
                                "values (:bookname,:author,:translater,:repre,:publisher,:isbn,:booknumber,:booktext,:authorabout)";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            oracleParameters.Add(new OracleParameter(":bookname", bookname));
            oracleParameters.Add(new OracleParameter(":author", author));
            oracleParameters.Add(new OracleParameter(":translater", translater));
            oracleParameters.Add(new OracleParameter(":repre", repre));
            oracleParameters.Add(new OracleParameter(":publisher", publisher));
            oracleParameters.Add(new OracleParameter(":isbne", isbn));
            oracleParameters.Add(new OracleParameter(":booknumber", booknumber));
            oracleParameters.Add(new OracleParameter(":booktext", booktext));
            oracleParameters.Add(new OracleParameter(":authorabout", authorabout));

            DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
            return true;

        }

        [HttpPost]
        public string GETBOOKINFObyNAME(String bookname)
        {
            string result = "";
            string sqlstr = "select * from MY_BOOKINFO where BOOK_NAME=:bookname";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            oracleParameters.Add(new OracleParameter(":bookname", bookname));
            var datatable = DbHelperOra.Query(sqlstr, oracleParameters.ToArray());
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;

        }

        [HttpPost]
        public string GETBOOKINFObyISBN(String isbn)
        {
            string result = "";
            string sqlstr = "select * from MY_BOOKINFO where ISBN=:isbn";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            oracleParameters.Add(new OracleParameter(":isbn", isbn));
            var datatable = DbHelperOra.Query(sqlstr, oracleParameters.ToArray());
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;

        }

        [HttpPost]
        public string GETBOOKRERATEbyNAME(String bookname)
        {
            string result = "";
            string sqlstr = "select RATE from MY_BOOKINFO where BOOK_NAME=:bookname";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            oracleParameters.Add(new OracleParameter(":bookname", bookname));
            var datatable = DbHelperOra.Query(sqlstr, oracleParameters.ToArray());
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;

        }
    }
}
