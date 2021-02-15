using CookTime.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CookTime.Controllers
{
    public class HomeController : Controller
    {
        CookBookContext db = new CookBookContext();
        SqlConnection connection = new SqlConnection(new Connection().ConnStr);
        SqlCommand command;
        SqlDataReader reader;

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Help()
        {
            return View();
        }
        public ActionResult Index()
        {
            Session["name"] = "";
            return View();
        }
        [HttpGet]
        public ActionResult SignIn(string mes="")
        {
            ViewBag.Mes = mes;
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(string email, string password)
        {
            int? id = null;
            using (connection)
            {
                connection.Open();
                command = new SqlCommand($"select * from users " +
                    $" where login='{email}' and password='{password}'", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                    id = (int?)reader.GetValue(0);
            }
            if (id == null)
                return RedirectToAction("SignIn", "Home", new { mes = "Проверьте ваши данные" });
            else
            {
                Session["name"] = id;
                return RedirectToAction("AllReceipts", "User");
            }
        }
        [HttpGet]
        public ActionResult LogIn(string mes="")
        {
            ViewBag.Mes = mes;
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(string email, string password, string password_rep)
        {
            int? id = null;
            using (connection)
            {
                connection.Open();
                command = new SqlCommand($"select count(login) from users " +
                    $"where login='{email}'", connection);
                if ((int)command.ExecuteScalar()>0)
                    return RedirectToAction("LogIn", "Home", new { mes = "Эта почта уже зарегестрирована" });

                if (password!=password_rep)
                    return RedirectToAction("LogIn", "Home", new { mes = "Пароли не совпадают" });

                command = new SqlCommand("INSERT INTO Users (Login, password) " +
                    $"VALUES ('{email}','{password}')", connection);
                command.ExecuteNonQuery(); command = new SqlCommand($"select * from users " +
                    $" where login='{email}' and password='{password}'", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                    id = (int?)reader.GetValue(0);
            }
         
                Session["name"] = id;
                return RedirectToAction("AllReceipts", "User");
            
        }
    }
}