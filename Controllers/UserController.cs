using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using CookTime.Models;

namespace CookTime.Controllers
{
    public class UserController : Controller
    {
        // GET: User
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

        [HttpGet]
        public ActionResult AddReceipt()
        {            
            return View();

        }
        [HttpPost]
        public ActionResult AddReceipt(Receipt receipt, HttpPostedFileBase upload, string OldImg="", string OldDate="", string OldName="")
        {
            if (OldDate != "")
            {

                string date = SqlDate(OldDate);

                SqlConnection sql = new SqlConnection(new Connection().ConnStr);
                using (sql)
                {
                    sql.Open();
                    command = new SqlCommand("DELETE FROM Receipt " +
                         $"where Date_create='{date}' and " +
                        $"[Name]=N'{OldName}' and " +
                        $"[User_id]={Session["name"]}", sql);
                    command.ExecuteNonQuery();
                }
            }
            
            if (receipt.Name == null)
                receipt.Name = "Блинчики со сметаной";
            receipt.Date_create = DateTime.Now;

            if (OldImg != "")
            {
                byte[] byteArray = Convert.FromBase64String(OldImg);
                receipt.Image = byteArray;
            }

            if (upload != null)
            {
                MemoryStream target = new MemoryStream();
                upload.InputStream.CopyTo(target);
                byte[] img = target.ToArray();
                receipt.Image = img;
            }
         
              receipt.User_id = Convert.ToInt32(Session["name"]);
            try
            {
                db.Receipts.Add(receipt);
            }
            catch
            {                
                db.SaveChanges();
            }

            db.SaveChanges();
              return View("ViewReceipt", receipt); 

        }
        [HttpGet]
        public ActionResult AllReceipts(string Type = "Все", string val = "")
        {
            string where = $"where user_id={Session["name"]} ";
            if (Type != "Все")
                where += $" AND Type=N'{Type}' ";
            if (val != "")
                where += $" AND Name LIke N'%{val}%' ";

            ViewBag.Type = Type;
            ViewBag.val = val;
            List<Receipt> receipts = new List<Receipt>();
            using (connection)
            {
                connection.Open();
                command = new SqlCommand($"select * from receipt {where} " +                
                    $" order by name", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Receipt receipt = new Receipt();
                    receipt.User_id = reader.GetInt32(0);
                    receipt.Name = reader.GetString(1);
                    if ((object)reader.GetValue(2) != DBNull.Value)
                        receipt.Time = (TimeSpan?)reader.GetValue(2);

                    if ((object)reader.GetValue(3) != DBNull.Value)
                        receipt.Callories = (int)reader.GetValue(3);
                    if ((object)reader.GetValue(4) != DBNull.Value)
                        receipt.Proteins = (decimal)reader.GetValue(4);
                    if ((object)reader.GetValue(5) != DBNull.Value)
                        receipt.Fats = (decimal)reader.GetValue(5);
                    if ((object)reader.GetValue(6) != DBNull.Value)
                        receipt.Carbohydrates = (decimal)reader.GetValue(6);
                    if ((object)reader.GetValue(7) != DBNull.Value)
                        receipt.Ingredients = (string)reader.GetValue(7);
                    if ((object)reader.GetValue(8) != DBNull.Value)
                        receipt.Steps = (string)reader.GetValue(8);
                    if ((object)reader.GetValue(9) != DBNull.Value)
                        receipt.Image = (byte[])reader.GetValue(9);
                    receipt.Date_create = (DateTime)reader.GetValue(10);
                    if ((object)reader.GetValue(11) != DBNull.Value)
                        receipt.Type = (string)reader.GetValue(11);

                    receipts.Add(receipt);
                }
            }
            ViewBag.Receipts = receipts;
            return View();

        }
        [HttpPost]
        public ActionResult AllReceipts(string action, string Type = "Все", string val = "")
        {
            return RedirectToAction("AllReceipts", "User", new { Type = Type, val = val });
        }

        [HttpGet]
        public ActionResult UpdateReceipt(string Date_create, string Name = "")
        {
            Receipt receipt = GetReceipt(Date_create, Name);
            if (receipt.Image != null)
                ViewBag.Image = Convert.ToBase64String(receipt.Image);
            else
                ViewBag.Image = "";
            ViewBag.Date_create = receipt.Date_create.ToString();
            ViewBag.Name = receipt.Name;
            ViewBag.Time = receipt.Time;
            ViewBag.Type = receipt.Type;
            ViewBag.Callories = receipt.Callories;
            ViewBag.Proteins = receipt.Proteins;
            ViewBag.Fats = receipt.Fats;
            ViewBag.Carbohydrates = receipt.Carbohydrates;
            ViewBag.Ingredients = receipt.Ingredients;
            ViewBag.Steps = receipt.Steps;

            string date = SqlDate(Date_create);

            SqlConnection sql = new SqlConnection(new Connection().ConnStr);
            using (sql)
            {
                sql.Open();
                command = new SqlCommand("DELETE FROM Receipt " +
                     $"where Date_create='{date}' and " +
                    $"[Name]=N'{Name}' and " +
                    $"[User_id]={Session["name"]}", sql);
                command.ExecuteNonQuery(); 
            }

            return View();
        }

        public ActionResult Delete(string Date_create, string Name = "")
        {
            string date = SqlDate(Date_create);
            using (connection)
            {
                connection.Open();
                command = new SqlCommand("DELETE FROM Receipt " +
                     $"where Date_create='{date}' and " +
                    $"[Name]=N'{Name}' and " +
                    $"[User_id]={Session["name"]}", connection);
                command.ExecuteNonQuery();
            }
            return RedirectToAction("AllReceipts");
        }
        [HttpPost]
        public ActionResult UpdateReceipt(Receipt receipt, HttpPostedFileBase upload_img)
        {
            if (receipt.Name == null)
                receipt.Name = "Блинчики со сметаной";
            Session["name"] = 1;
            receipt.Date_create = DateTime.Now;
            if (upload_img != null)
            {
                receipt.Image = new byte[upload_img.ContentLength];
                upload_img.InputStream.Read(receipt.Image, 0, upload_img.ContentLength);
            }
            receipt.User_id = Convert.ToInt32(Session["name"]);          
            db.SaveChanges();
            return View("ViewReceipt", receipt);

        }
        public ActionResult Update(string Date_create, string Name="")
        {
            return RedirectToAction("UpdateReceipt", new
            {
                Date_create = Date_create,
                Name = Name
            });

        }
        public ActionResult Cook(string Date_create, string Name = "")
        {
            Receipt receipt = GetReceipt(Date_create, Name);
            return View("ViewReceipt", receipt);

        }


        public ActionResult ViewReceipt(Receipt receipt)
        {
            return View("ViewReceipt", receipt);
        }

        public ActionResult Exit()
        {
            return RedirectToAction("Index", "Home");
        }
       private string SqlDate (string Date_create)
        {
            Date_create = Date_create.Replace(".", "-");
            string date = Date_create.Substring(Date_create.LastIndexOf("-") + 1, 4) + "-";
            date += Date_create.Substring(Date_create.IndexOf("-") + 1, 2) + "-";
            date += Date_create.Substring(0, 2);
            date += Date_create.Substring(Date_create.IndexOf(" "));
            return date;
        }

        private Receipt GetReceipt(string Date_create, string Name = "")
        {
            string date = SqlDate(Date_create);
            Receipt receipt = new Receipt();
            using (connection)
            {
                connection.Open();
                command = new SqlCommand($"select * from receipt " +
                    $" where Date_create='{date}' and " +
                    $" [Name]=N'{Name}' and " +
                    $" [User_id]={Session["name"]}", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    receipt.User_id = reader.GetInt32(0);
                    receipt.Name = reader.GetString(1);
                    if ((object)reader.GetValue(2) != DBNull.Value)
                        receipt.Time = (TimeSpan?)reader.GetValue(2);

                    if ((object)reader.GetValue(3) != DBNull.Value)
                        receipt.Callories = (int)reader.GetValue(3);
                    if ((object)reader.GetValue(4) != DBNull.Value)
                        receipt.Proteins = (decimal)reader.GetValue(4);
                    if ((object)reader.GetValue(5) != DBNull.Value)
                        receipt.Fats = (decimal)reader.GetValue(5);
                    if ((object)reader.GetValue(6) != DBNull.Value)
                        receipt.Carbohydrates = (decimal)reader.GetValue(6);
                    if ((object)reader.GetValue(7) != DBNull.Value)
                        receipt.Ingredients = (string)reader.GetValue(7);
                    if ((object)reader.GetValue(8) != DBNull.Value)
                        receipt.Steps = (string)reader.GetValue(8);
                    if ((object)reader.GetValue(9) != DBNull.Value)
                        receipt.Image = (byte[])reader.GetValue(9);
                    receipt.Date_create = (DateTime)reader.GetValue(10);
                    if ((object)reader.GetValue(11) != DBNull.Value)
                        receipt.Type = (string)reader.GetValue(11);
                }

            }
            return receipt;

        }

    }
}