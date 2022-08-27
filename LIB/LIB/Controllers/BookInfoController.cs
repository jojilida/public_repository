using LIB.Models;
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

        [HttpPost]
        public string UPDATEBOOKRERATEbyNAME(String bookname,String rate)
        {

            string ratenow = "";
            string rate_people_number = "";

            string sqlstr = "select RATE from MY_BOOKINFO where BOOK_NAME=:bookname";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            oracleParameters.Add(new OracleParameter(":bookname", bookname));
            var datatable = DbHelperOra.Query(sqlstr, oracleParameters.ToArray());
            foreach (DataRow item in datatable.Tables[0].Rows)
            {
                ratenow += item["RATE"].ToString();
                Console.WriteLine(ratenow);
                break;
            }
            double rateafter = double.Parse(ratenow) + double.Parse(rate);
            Console.WriteLine(rateafter);

            string sqlstr2 = "select RATE_PEOPLE_NUMBER from MY_BOOKINFO where BOOK_NAME=:bookname";
            List<OracleParameter> oracleParameters2 = new List<OracleParameter>();
            oracleParameters2.Add(new OracleParameter(":bookname", bookname));
            var datatable2 = DbHelperOra.Query(sqlstr2, oracleParameters.ToArray());
            foreach (DataRow item in datatable2.Tables[0].Rows)
            {
                rate_people_number += item["RATE_PEOPLE_NUMBER"].ToString();
                break;
            }
            int rate_people_numberafter = int.Parse(rate_people_number) + 1;
            Console.WriteLine(rate_people_numberafter);

            string peoplenumber = rate_people_numberafter.ToString();
            Console.WriteLine(peoplenumber);

            var sqlstr3 = "update MY_BOOKINFO set RATE_PEOPLE_NUMBER=:number where BOOK_NAME=:bookname";
            List<OracleParameter> oracleParameters3 = new List<OracleParameter>();
            oracleParameters3.Add(new OracleParameter(":bookname", bookname));
            oracleParameters3.Add(new OracleParameter(":number", peoplenumber));
            var isok = DbHelperOra.ExecuteSql(sqlstr3, oracleParameters.ToArray());

            double ratenew = rateafter / rate_people_numberafter;
            Console.WriteLine(ratenew);
            string newrate = ratenew.ToString();

            var sqlstr4 = "update MY_BOOKINFO set RATE=:rate where BOOK_NAME=:bookname";
            List<OracleParameter> oracleParameters4 = new List<OracleParameter>();
            oracleParameters4.Add(new OracleParameter(":bookname", bookname));
            oracleParameters4.Add(new OracleParameter(":rate", newrate));
            var isok1 = DbHelperOra.ExecuteSql(sqlstr4, oracleParameters.ToArray());

            var datatable4 = DbHelperOra.Query(sqlstr, oracleParameters.ToArray());
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable4.Tables[0]);
            return JsonString;
        }
    }
}
