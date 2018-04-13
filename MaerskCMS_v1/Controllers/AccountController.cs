using MaerskCMS_v1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaerskCMS_v1.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegisterSuccessful()
        {

            return View();
        }

        public ActionResult AgentLogin()
        {

            return View();
        }

        public ActionResult AdminLogin()
        {

            return View();
        }

        public ActionResult Logout()
        {
            Session["Username"] = null;
            Session["Name"] = null;
            Session["UserID"] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult AgentLoginAction(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                int result = AgentValidateLogin(model);
                switch (result)
                {
                    case 1:
                        System.Diagnostics.Debug.WriteLine("Error Login. User is not found. Status Code: " + 1);
                        ModelState.AddModelError("", "Username or Password is Incorrect. Please try again. ");
                        break;
                    case 2:
                        System.Diagnostics.Debug.WriteLine("Error Login. User is not approved. Status Code: " + 2);
                        ModelState.AddModelError("", "Your registration approval is still pending. Please contact us for more information. ");
                        break;
                    case 3:
                        System.Diagnostics.Debug.WriteLine("Error Connecting the Database. Status Code: " + 3);
                        ModelState.AddModelError("", "An error has occured when connecting to the database. Please try again. ");
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine("Success Login Process. ID Number: " + result);
                        return RedirectToAction("Index", "Agent");
                }
            }
            return View("AgentLogin");
        }

        public int AgentValidateLogin(LoginModel model)
        {
            int result = 0;
            string username = model.Username;
            string password = model.Password;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("EXECUTE dbo.agentLogin " + username + ", " + password, conn);
                myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    if ((bool)myReader["ApprovalStatus"])
                    {
                        result = (int)myReader["ID"];
                        Session["UserID"] = myReader["ID"];
                        Session["Username"] = myReader["Username"];
                        Session["Name"] = myReader["FirstName"] + " " + myReader["LastName"];
                        conn.Close();
                        conn.Open();
                        new SqlCommand("EXECUTE dbo.agentLastSession " + result, conn).ExecuteNonQuery();
                    }
                    else
                        result = 2;
                }
                else
                    result = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = 3;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        [HttpPost]
        public ActionResult AdminLoginAction(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                int result = AdminValidateLogin(model);
                switch (result)
                {
                    case 1:
                        System.Diagnostics.Debug.WriteLine("Error Login. User is not found. Status Code: " + 1);
                        ModelState.AddModelError("", "Username or Password is Incorrect. Please try again. ");
                        break;
                    case 2:
                        System.Diagnostics.Debug.WriteLine("Error Connecting the Database. Status Code: " + 2);
                        ModelState.AddModelError("", "An error has occured when connecting to the database. Please try again. ");
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine("Success Login Process. ID Number: " + result);
                        return RedirectToAction("Index", "Admin");
                }
            }
            return View("AdminLogin");
        }

        public int AdminValidateLogin(LoginModel model)
        {
            int result = 0;
            string username = model.Username;
            string password = model.Password;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("EXECUTE dbo.adminLogin " + username + ", " + password, conn);
                myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    result = (int)myReader["ID"];
                    Session["UserID"] = myReader["ID"];
                    Session["Username"] = myReader["Username"];
                }
                else
                    result = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = 2;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }


        public ActionResult AgentSignUp()
        {

            return View();
        }

        public JsonResult CheckAvailableUser(string userdata)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("EXECUTE dbo.checkExistingUsername '" + userdata + "'", conn);
                try
                {
                    conn.Open();
                    myReader = myCommand.ExecuteReader();
                    if (myReader.Read())
                    {
                        if (myReader["Username"].ToString() == userdata)
                        {
                            return Json(1);
                        }
                    }
                    return Json(0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    conn.Close();
                }
                return Json(2);
            }
        }

        [HttpPost]
        public ActionResult SignUpAction(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                int result = RegisterAgent(model);
                if (result != 0)
                {
                    ViewBag.Message = "You have successfully submitted the registration form. Please wait while our personnel will review your submission. We'll notify you upon approval. ";
                    return View("RegisterSuccessful");
                }
                else
                {
                    ModelState.AddModelError("", "Error has occured when submitting your registration details. Please contact us to resolve this issue. ");
                }
            }
            return View("AgentSignUp");
        }

        public int RegisterAgent(RegisterModel model)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand("EXECUTE dbo.agentRegister '"
                    + model.FirstName + "', '"
                    + model.LastName + "', '"
                    + model.Username + "', '"
                    + model.Password + "', '"
                    + model.EmailAddress + "'"
                    , conn);
                myCommand.ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return 0;
        }

    }
}