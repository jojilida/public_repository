using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;

namespace LIB.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller

    {
        [HttpPost]
        public ActionResult postest(String username, String password)
        {
            if (String.IsNullOrEmpty(username))
            {
                return Ok();
            }
            if (String.IsNullOrEmpty(password))
            {
                return Ok();
            }
            //Random rad = new Random();
            //int id = rad.Next(10, 1000000);
            //string userid = id.ToString();
            //string sex = null;
            //string age = null;
            //string nickname = "zqh";
            //string mail = null;
            //var strinsertinto = "insert into MY_USER (USER_ID,USER_NAME,PASSWORD,AGE,SEX,NICK_NAME,USER_MAIL) values (:id,:name,:password,:age,:sex,:nickname,:mail)";
            //List<OracleParameter> oracleParameters = new List<OracleParameter>();
            //oracleParameters.Add(new OracleParameter(":id", userid));
            //oracleParameters.Add(new OracleParameter(":name", username));
            //oracleParameters.Add(new OracleParameter(":password", password));
            //oracleParameters.Add(new OracleParameter(":age", age));
            //oracleParameters.Add(new OracleParameter(":sex", sex));
            //oracleParameters.Add(new OracleParameter(":nickname", nickname));
            //oracleParameters.Add(new OracleParameter(":mail", mail));
            //DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());

            return Ok();
        }

        [HttpPost]
        public bool signin(String userid, String password)
        {
            if (String.IsNullOrEmpty(userid))
            {
                return false;
            }
            if (String.IsNullOrEmpty(password))
            {
                return false;
            }
            string id = userid;
            string sqlstr = "select USER_ID from MY_USER where USER_ID=" + id + "and PASSWORD=" + password;
            var judge1 = DbHelperOra.Exists(sqlstr);
            return judge1;
        }

        [HttpPost]
        public String register(String username, String password, String mail)
        {
            if (String.IsNullOrEmpty(username))
            {
                return "false";
            }
            if (String.IsNullOrEmpty(password))
            {
                return "false";
            }
            if (String.IsNullOrEmpty(mail))
            {
                return "false";
            }
            Random rad = new Random();
            int id = rad.Next(10, 1000000);
            string userid = id.ToString();
            string sex = null;
            string age = null;
            string nickname = "游客";
            var strinsertinto = "insert into MY_USER (USER_ID,USER_NAME,PASSWORD,AGE,SEX,NICK_NAME,USER_MAIL) values (:id,:name,:password,:age,:sex,:nickname,:mail)";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            oracleParameters.Add(new OracleParameter(":id", userid));
            oracleParameters.Add(new OracleParameter(":name", username));
            oracleParameters.Add(new OracleParameter(":password", password));
            oracleParameters.Add(new OracleParameter(":age", age));
            oracleParameters.Add(new OracleParameter(":sex", sex));
            oracleParameters.Add(new OracleParameter(":nickname", nickname));
            oracleParameters.Add(new OracleParameter(":mail", mail));
            DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
            return userid;
        }
        [HttpPost]
        public ActionResult Loginwithusernameandpassword(String username, String password)
        {
            if (String.IsNullOrEmpty(username))
            {
                return Ok();
            }
            if (String.IsNullOrEmpty(password))
            {
                return Ok();
            }
            Random rad = new Random();
            int id = rad.Next(10, 1000000);
            string userid = id.ToString();
            string sex = null;
            string age = null;
            string nickname = "游客";
            string mail = null;
            var strinsertinto = "insert into MY_USER (USER_ID,USER_NAME,PASSWORD,AGE,SEX,NICK_NAME,USER_MAIL) values (:id,:name,:password,:age,:sex,:nickname,:mail)";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            oracleParameters.Add(new OracleParameter(":id", userid));
            oracleParameters.Add(new OracleParameter(":name", username));
            oracleParameters.Add(new OracleParameter(":password", password));
            oracleParameters.Add(new OracleParameter(":age", age));
            oracleParameters.Add(new OracleParameter(":sex", sex));
            oracleParameters.Add(new OracleParameter(":nickname", nickname));
            oracleParameters.Add(new OracleParameter(":mail", mail));
            DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());

            return Ok();
        }


        //这个用来实现登录
        [HttpPost]
        public ActionResult Login(String userid, String username, String password, String age, String sex, String nickname, String mail)
        {
            if (String.IsNullOrEmpty(username))
            {
                return Ok();
            }
            if (String.IsNullOrEmpty(userid))
            {
                return Ok();
            }
            if (String.IsNullOrEmpty(password))
            {
                return Ok();
            }
            var strinsertinto = "insert into MY_USER (USER_ID,USER_NAME,PASSWORD,AGE,SEX,NICK_NAME,USER_MAIL) values (:id,:name,:password,:age,:sex,:nickname,:mail)";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            oracleParameters.Add(new OracleParameter(":id", userid));
            oracleParameters.Add(new OracleParameter(":name", username));
            oracleParameters.Add(new OracleParameter(":password", password));
            oracleParameters.Add(new OracleParameter(":age", age));
            oracleParameters.Add(new OracleParameter(":sex", sex));
            oracleParameters.Add(new OracleParameter(":nickname", nickname));
            oracleParameters.Add(new OracleParameter(":mail", mail));
            DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());

            return Ok();
        }

        //这个用来实现修改名字
        [HttpPost]
        public ActionResult updatename(String userid, String username, String password)
        {
            if (String.IsNullOrEmpty(username))
            {
                return Ok();
            }
            if (String.IsNullOrEmpty(userid))
            {
                return Ok();
            }
            if (String.IsNullOrEmpty(password))
            {
                return Ok();
            }
            var strinsertinto = "update MY_USER set USER_NAME=:name where USER_ID=:id and PASSWORD=:password";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();


            oracleParameters.Add(new OracleParameter(":name", username));
            oracleParameters.Add(new OracleParameter(":id", userid));
            oracleParameters.Add(new OracleParameter(":password", password));
            var isok = DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
            Console.WriteLine(isok);

            return Ok();
        }

        [HttpPost]
        public bool updatenickname(String userid, String nickname, String password)
        {
            if (String.IsNullOrEmpty(nickname))
            {
                return false;
            }
            if (String.IsNullOrEmpty(userid))
            {
                return false;
            }
            if (String.IsNullOrEmpty(password))
            {
                return false;
            }
            //string id = userid;
            //string sqlstr = "select USER_ID from MY_USER where USER_ID=" + id + "and PASSWORD=" + password;
            //var judge1 = DbHelperOra.Exists(sqlstr);
            //if (judge1)
            //{
            var strinsertinto = "update MY_USER set NICK_NAME=:nickname where USER_ID=:id and PASSWORD=:password";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();


            oracleParameters.Add(new OracleParameter(":nickname", nickname));
            oracleParameters.Add(new OracleParameter(":id", userid));
            oracleParameters.Add(new OracleParameter(":password", password));
            var isok = DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
            Console.WriteLine(password);

            return true;
            //}
            //else
            //    return judge1;
        }
        //这个用来实现修改密码
        [HttpPost]
        public ActionResult updatepassword(String userid, String username, String password, String passwordupdated)
        {
            if (String.IsNullOrEmpty(username))
            {
                return Ok();
            }
            if (String.IsNullOrEmpty(userid))
            {
                return Ok();
            }
            if (String.IsNullOrEmpty(password))
            {
                return Ok();
            }
            if (String.IsNullOrEmpty(passwordupdated))
            {
                return Ok();
            }
            var strinsertinto = "update MY_USER set PASSWORD=:passwordupdated where USER_ID=:id and PASSWORD=:password";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();


            oracleParameters.Add(new OracleParameter(":name", username));
            oracleParameters.Add(new OracleParameter(":id", userid));
            oracleParameters.Add(new OracleParameter(":password", password));
            oracleParameters.Add(new OracleParameter(":passwordupdated", passwordupdated));
            var isok = DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
            Console.WriteLine(isok);

            return Ok();
        }
        //这个用来实现删除
        [HttpPost]
        public ActionResult delete(String userid)
        {

            if (String.IsNullOrEmpty(userid))
            {
                return Ok();
            }
            var strinsertinto = "delete from MY_USER where USER_ID=:id";
            List<OracleParameter> oracleParameters = new List<OracleParameter>();
            oracleParameters.Add(new OracleParameter(":id", userid));
            var isok = DbHelperOra.ExecuteSql(strinsertinto, oracleParameters.ToArray());
            Console.WriteLine(isok);


            return Ok();
        }


        //这个用来实现查询
        [HttpPost]
        public ActionResult query()
        {
            var datatable = DbHelperOra.Query("select * from MY_USER");
            foreach (DataRow item in datatable.Tables[0].Rows)
            {
                Console.WriteLine(item["USER_NAME"].ToString() + "___" + item["USER_ID"].ToString() + "___" + item["PASSWORD"].ToString());
            }
            return Ok();
        }

        //这个用来实现查询
        [HttpPost]
        public string querylib()
        {
            string result="";
            var datatable = DbHelperOra.Query("select * from MY_USER");
            foreach (DataRow item in datatable.Tables[0].Rows)
            {
                Console.WriteLine(item["USER_NAME"].ToString() + "___" + item["USER_ID"].ToString());
                result += item["USER_NAME"].ToString() + "___" + item["USER_ID"].ToString()+",";
            }
            return result;
        }

        //这个用来实现查询
        [HttpGet]
        public string getlib()
        {
            string result = "";
            DataSet datatable = new DataSet();
            datatable = DbHelperOra.Query("select * from MY_USER");

            //foreach (DataRow item in datatable.Tables[0].Rows)
            //{
            //    Console.WriteLine(item["USER_NAME"].ToString() + "___" + item["USER_ID"].ToString());
            //    result += item["USER_NAME"].ToString() + "___" + item["USER_ID"].ToString() + ",";
            //}
            //string jsonString = "{";
            //foreach (DataRow item in datatable.Tables[0].Rows)
            //{
            //    jsonString += "\"" + item.RowName + "\":" + Json(table) + ",";
            //}
            //jsonString = jsonString.TrimEnd(',');

            //return jsonString + "}";
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(datatable.Tables[0]);
            return JsonString;

        }



    }
}
