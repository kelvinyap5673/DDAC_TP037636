using MaerskCMS_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;

namespace MaerskCMS_v1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "World leader of container shipping company";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public List<MainPageViewModel> LoadShippingSchedulePOD()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("EXECUTE dbo.loadPOD", conn);
                myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    List<MainPageViewModel> newAgent = new List<MainPageViewModel>();
                    do
                    {
                        MainPageViewModel sm = new MainPageViewModel();
                        sm.POD = myReader["POD"].ToString();
                        newAgent.Add(sm);
                    } while (myReader.Read());
                    return newAgent;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return null;
        }
    }
}