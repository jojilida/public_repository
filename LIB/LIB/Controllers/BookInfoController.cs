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
        public bool INSERTBOOKINFO(String bookname, String author, String translater, String repre, String publisher, String isbn, String booknumber, String booktext, String authorabout, String book_img, String author_img, String place)
        {
            var strinsertinto = "insert into MY_BOOKINFO (BOOK_NAME,BOOK_AUTHOR,BOOK_TRANSLATER,BOOK_REPRE,BOOK_PUBLISHER,ISBN,BOOK_COLLECTION_NUMBER,BOOK_TEXT,BOOK_AUTHORABOUT,BOOK_IMG,AUTHOR_IMG,RATE_PEOPLE_NUMBER,RATE,PLACE,UPDATE_DATE) " +
                                "values (:bookname,:author,:translater,:repre,:publisher,:isbn,:booknumber,:booktext,:authorabout,:book_img,:author_img,'0',0,:place,to_date(:update_date,'yyyy-mm-dd'))";
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
            oracleParameters.Add(new OracleParameter(":book_img", book_img));
            oracleParameters.Add(new OracleParameter(":author_img", author_img));
            oracleParameters.Add(new OracleParameter(":place", place));
            oracleParameters.Add(new OracleParameter(":update_date", DateTime.Now.ToString("yyyy-MM-dd")));
            DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());

            var data = DbHelperOra.Query("select * from MY_BOOKS");
            int book_id = data.Tables[0].Rows.Count+1;

            var strinsertinto2 = "insert into MY_BOOKS(BOOK_NAME,ISBN,BOOK_ID,BORROWED_TIMES,STATE,BOOK_DAMAGE) values (:book_name,:isbn,:book_id,0,'未预约','0')";
            List<OracleParameter> oracleParameters2 = new List<OracleParameter>();
            oracleParameters2.Add(new OracleParameter(":book_name", bookname));
            oracleParameters2.Add(new OracleParameter(":isbn", isbn));
            oracleParameters2.Add(new OracleParameter(":book_id", book_id));
            DbHelperOra.ExecuteSql(strinsertinto2, oracleParameters2.ToArray());

            return true;
        }

        [HttpGet]
        public string GetBookInfo(String isbn)
        {
            DataSet datatable = new DataSet();
            datatable = DbHelperOra.Query("select * from MY_BOOKINFO where ISBN='" + isbn+"'");
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;
        }



        [HttpPost]
        public bool UpdateBookInfo(String author, String translater, String repre, String publisher, String isbn, String booknumber, String booktext, String authorabout, String book_img, String author_img, String place)
        {
            var strinsertinto = "update MY_BOOKINFO set BOOK_AUTHOR='" + author + "',BOOK_TRANSLATER='" + translater 
                + "',BOOK_REPRE='" + repre + "',BOOK_PUBLISHER='" + publisher 
                + "',BOOK_COLLECTION_NUMBER='" + booknumber + "',BOOK_TEXT='" 
                + booktext + "',BOOK_AUTHORABOUT='" + authorabout + "',BOOK_IMG='" 
                + book_img + "',AUTHOR_IMG='" + author_img + "',PLACE='" + place 
                + "',UPDATE_DATE='" + DateTime.Now.ToString("yyyy-MM-dd") 
                + "' where ISBN='"+isbn+"'";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
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
        public string UPDATEBOOKRERATEbyNAME(String bookname, String rate)
        {

            string ratenow = "";
            string rate_people_number = "";

            string sqlstr = "select * from MY_BOOKINFO where BOOK_NAME=:bookname";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            oracleParameters.Add(new OracleParameter(":bookname", bookname));
            var datatable = DbHelperOra.Query(sqlstr, oracleParameters.ToArray());
            foreach (DataRow item in datatable.Tables[0].Rows)
            {
                ratenow += item["RATE"].ToString();
                break;
            }
        

            string sqlstr2 = "select RATE_PEOPLE_NUMBER from MY_BOOKINFO where BOOK_NAME=:bookname";
            List<OracleParameter> oracleParameters2 = new List<OracleParameter>();
            oracleParameters2.Add(new OracleParameter(":bookname", bookname));
            var datatable2 = DbHelperOra.Query(sqlstr2, oracleParameters2.ToArray());
            foreach (DataRow item in datatable2.Tables[0].Rows)
            {
                rate_people_number += item["RATE_PEOPLE_NUMBER"].ToString();
                break;
            }
            int rate_people_numberafter = int.Parse(rate_people_number) + 1;
            Console.WriteLine(rate_people_numberafter);

            string peoplenumber = rate_people_numberafter.ToString();

            var sqlstr3 = "update MY_BOOKINFO set RATE_PEOPLE_NUMBER=" + peoplenumber + "where BOOK_NAME=:bookname";
            List<OracleParameter> oracleParameters3 = new List<OracleParameter>();
            oracleParameters3.Add(new OracleParameter(":bookname", bookname));
            var isok1 = DbHelperOra.ExecuteSql(sqlstr3, oracleParameters3.ToArray());
            Console.WriteLine(isok1);


            double rateafter = double.Parse(rate) + double.Parse(ratenow) * int.Parse(rate_people_number);
            Console.WriteLine(rateafter);
            double ratenew = rateafter / rate_people_numberafter;
            string newrate = ratenew.ToString();

            var sqlstr4 = "update MY_BOOKINFO set RATE=" + newrate + "where BOOK_NAME=:bookname";
            List<OracleParameter> oracleParameters4 = new List<OracleParameter>();
            oracleParameters4.Add(new OracleParameter(":bookname", bookname));
            var isok2 = DbHelperOra.ExecuteSql(sqlstr4, oracleParameters4.ToArray());

            var datatable4 = DbHelperOra.Query(sqlstr, oracleParameters.ToArray());
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable4.Tables[0]);
            return JsonString;
        }

        [HttpPost]
        public bool UPDATEBOOKRERATEbyNAMEandNOT(String bookname, String rate)
        {

            var sqlstr = "update MY_BOOKINFO set RATE=" + rate + "where BOOK_NAME=:bookname";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            oracleParameters.Add(new OracleParameter(":bookname", bookname));
            var isok1 = DbHelperOra.ExecuteSql(sqlstr, oracleParameters.ToArray());
            Console.WriteLine(isok1);
            return true;
        }

        [HttpPost]
        public bool UPDATEBOOKPEOPLEbyNAMEandNOT(String bookname, String peoplenumber)
        {

            var sqlstr = "update MY_BOOKINFO set RATE_PEOPLE_NUMBER=" + peoplenumber + "where BOOK_NAME=:bookname";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            oracleParameters.Add(new OracleParameter(":bookname", bookname));
            var isok1 = DbHelperOra.ExecuteSql(sqlstr, oracleParameters.ToArray());
            Console.WriteLine(isok1);
            return true;
        }

        [HttpPost]
        public string GETALLBOOKINFO()
        {
            
            string sqlstr = "select * from my_booktable";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            
            var datatable = DbHelperOra.Query(sqlstr);
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;

        }
    }
}
