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
        public bool INSERTBOOKneedINFO(String bookname, String author, String translater, String repre, String publisher, String isbn, String booknumber, String booktext, String authorabout)
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

        
    }
}
