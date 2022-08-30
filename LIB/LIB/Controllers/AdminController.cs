using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace LIB.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AdminController : Controller
    {
        [HttpPost]
        public bool AddNotice(string title, string text)
        {
            var data = DbHelperOra.Query("select * from MY_NOTICE");
            int id = data.Tables[0].Rows.Count + 1;
            var strinsertinto = "insert into MY_NOTICE(ID,TITLE,RELEASE_DATE,TEXT) values (:id,:title,:day,:text)";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            oracleParameters.Add(new OracleParameter(":id", id.ToString()));
            oracleParameters.Add(new OracleParameter(":title", title));
            string day = DateTime.Now.ToString("yyyy-MM-dd");
            oracleParameters.Add(new OracleParameter(":day", day));
            oracleParameters.Add(new OracleParameter(":text", text));
            DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
            return true;
        }

        [HttpPost]
        public bool EditNotice(string id, string title, string text)
        {
            var strinsertinto = "update MY_NOTICE set TITLE=\'" + title + "\',TEXT=\'" + text + "\' where ID like \'" + id + "\'";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            var isok = DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
            Console.WriteLine(isok);
            return true;
        }

        [HttpPost]
        public bool DeleteNotice(string id)
        {
            var strinsertinto = "delete from MY_NOTICE where ID like \'" + id + "\'";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            var isok = DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
            Console.WriteLine(isok);
            return true;
        }

        [HttpGet]
        public string GetDamagedBook()
        {
            DataSet datatable = new DataSet();
            datatable = DbHelperOra.Query("select * from MY_DAMAGED_BOOK where DAMAGE_STATE like '1'");
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;
        }

        [HttpGet]
        public string GetBooks()
        {
            DataSet datatable = new DataSet();
            datatable = DbHelperOra.Query("select * from MY_BOOKS");
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;
        }

        [HttpPost]
        public bool RepairBook(string book_id, string damage_time)
        {
            string sqlstr = "select * from MY_DAMAGED_BOOK where BOOK_ID like \'" + book_id + "\' and DAMAGE_TIME like \'" + damage_time + "\' and DAMAGE_STATE=1";
            var data = DbHelperOra.Query(sqlstr);
            int judge1 = data.Tables[0].Rows.Count;
            if (judge1 > 0)
            {
                var strinsertinto = "update MY_DAMAGED_BOOK set DAMAGE_STATE=0,REPAIR_TIME=\'" + DateTime.Now.ToString("yyyy-MM-dd") + "\' where BOOK_ID like \'" + book_id + "\' and DAMAGE_TIME like \'" + damage_time + "\' and DAMAGE_STATE=1";
                List<OracleParameter> oracleParameters = new List<OracleParameter>();
                var isok = DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
                Console.WriteLine(isok);

                var strinsertinto2 = "update MY_BOOKS set STATE='未借阅',BOOK_DAMAGE=0 where BOOK_ID like \'" + book_id + "\'";
                List<OracleParameter> oracleParameters2 = new List<OracleParameter>();
                var isok2 = DbHelperOra.ExecuteSql(strinsertinto2, oracleParameters2.ToArray());
                Console.WriteLine(isok2);
                return true;
            }
            return false;
        }

        [HttpPost]
        public bool AddDamagedBook(string book_id, string user_id = "")
        {
            string sqlstr = "select * from MY_DAMAGED_BOOK where BOOK_ID like \'" + book_id + "\' and  DAMAGE_STATE=1";
            var data = DbHelperOra.Query(sqlstr);
            int judge1 = data.Tables[0].Rows.Count;
            if (judge1 == 0)
            {
                var strinsertinto = "insert into MY_DAMAGED_BOOK(BOOK_NAME,ISBN,BOOK_ID,DAMAGE_STATE,DAMAGE_TIME,USER_ID) values (:book_name,:isbn,:book_id,1,\'" + DateTime.Now.ToString("yyyy-MM-dd") + "\',:user_id)";
                List<OracleParameter> oracleParameters = new List<OracleParameter>();
                var data2 = DbHelperOra.Query("select BOOK_NAME,ISBN from MY_BOOKS where BOOK_ID like \'" + book_id + "\'");
                foreach (DataRow item in data2.Tables[0].Rows)
                {
                    oracleParameters.Add(new OracleParameter(":book_name", item["BOOK_NAME"]));
                    oracleParameters.Add(new OracleParameter(":isbn", item["ISBN"]));
                    oracleParameters.Add(new OracleParameter(":book_id", book_id));
                    oracleParameters.Add(new OracleParameter(":user_id", user_id));
                }
                DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());

                var strinsertinto2 = "update MY_BOOKS set STATE='无法借阅',BOOK_DAMAGE=1 where BOOK_ID like \'" + book_id + "\'";
                List<OracleParameter> oracleParameters2 = new List<OracleParameter>();
                var isok2 = DbHelperOra.ExecuteSql(strinsertinto2, oracleParameters2.ToArray());
                Console.WriteLine(isok2);
                return true;
            }
            return false;
        }
    }
}
