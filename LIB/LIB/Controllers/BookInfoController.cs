using Microsoft.AspNetCore.Mvc;
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
            foreach (DataRow item in datatable.Tables[0].Rows)
            {
                Console.WriteLine(item["BOOK_NAME"].ToString() + " " + item["BOOK_AUTHOR"].ToString() 
                          + " " + item["BOOK_TRANSLATER"].ToString() + " " + item["BOOK_REPRE"].ToString() 
                          + " " + item["BOOK_PUBLISHER"].ToString() + " " + item["ISBN"].ToString() + " " 
                                + item["BOOK_COLLECTION_NUMBER"].ToString() + " " + item["BOOK_TEXT"].ToString() + " " 
                                + item["BOOK_AUTHORABOUT"].ToString());
                result += item["BOOK_NAME"].ToString() + " " + item["BOOK_AUTHOR"].ToString()
                          + " " + item["BOOK_TRANSLATER"].ToString() + " " + item["BOOK_REPRE"].ToString()
                          + " " + item["BOOK_PUBLISHER"].ToString() + " " + item["ISBN"].ToString() + " "
                                + item["BOOK_COLLECTION_NUMBER"].ToString() + " " + item["BOOK_TEXT"].ToString() + " "
                                + item["BOOK_AUTHORABOUT"].ToString() + ",\n";
            }
            return result;

        }

        [HttpPost]
        public string GETBOOKINFObyISBN(String isbn)
        {
            string result = "";
            string sqlstr = "select * from MY_BOOKINFO where ISBN=:isbn";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            oracleParameters.Add(new OracleParameter(":isbn", isbn));
            var datatable = DbHelperOra.Query(sqlstr, oracleParameters.ToArray());
            foreach (DataRow item in datatable.Tables[0].Rows)
            {
                Console.WriteLine(item["BOOK_NAME"].ToString() + " " + item["BOOK_AUTHOR"].ToString()
                          + " " + item["BOOK_TRANSLATER"].ToString() + " " + item["BOOK_REPRE"].ToString()
                          + " " + item["BOOK_PUBLISHER"].ToString() + " " + item["ISBN"].ToString() + " "
                                + item["BOOK_COLLECTION_NUMBER"].ToString() + " " + item["BOOK_TEXT"].ToString() + " "
                                + item["BOOK_AUTHORABOUT"].ToString());
                result += item["BOOK_NAME"].ToString() + " " + item["BOOK_AUTHOR"].ToString()
                          + " " + item["BOOK_TRANSLATER"].ToString() + " " + item["BOOK_REPRE"].ToString()
                          + " " + item["BOOK_PUBLISHER"].ToString() + " " + item["ISBN"].ToString() + " "
                                + item["BOOK_COLLECTION_NUMBER"].ToString() + " " + item["BOOK_TEXT"].ToString() + " "
                                + item["BOOK_AUTHORABOUT"].ToString() + ",\n";
            }
            return result;

        }
    }
}
