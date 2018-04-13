using MaerskCMS_v1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaerskCMS_v1.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ManageAgent()
        {
            if (Session["UserID"] != null)
            {
                var data = LoadAgentRegistration();
                if(data != null)
                {
                    return View(data);
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AdminApproval(int? ID)
        {
            if (ID != null)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                try
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand("EXECUTE dbo.adminApproval " + ID + ", " + Session["UserID"], conn);
                    myCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    conn.Close();
                }
                return RedirectToAction("ManageAgent");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AdminDeleteAgent(int? ID)
        {
            if (ID != null)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                try
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand("EXECUTE dbo.adminDeleteAgent " + ID, conn);
                    myCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    conn.Close();
                }
                return RedirectToAction("ManageAgent");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        

        public List<AgentModel> LoadAgentRegistration()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("EXECUTE dbo.adminViewRegistration ", conn);
                myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    List<AgentModel> newAgent = new List<AgentModel>();
                    do
                    {
                        AgentModel am = new AgentModel();
                        am.ID = Convert.ToInt32(myReader["ID"]);
                        am.Username = myReader["Username"].ToString();
                        am.FirstName = myReader["FirstName"].ToString();
                        am.LastName = myReader["LastName"].ToString();
                        am.Email = myReader["Email"].ToString();
                        am.Registration = Convert.ToDateTime(myReader["RegSubmitDateTime"]);
                        am.Status = Convert.ToBoolean(myReader["ApprovalStatus"]);
                        newAgent.Add(am);
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

        public ActionResult ShippingSchedule()
        {
            if (Session["UserID"] != null)
            {
                var data = LoadScheduleList();
                if (data != null)
                {
                    return View(data);
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public List<AdminScheduleListViewModel> LoadScheduleList()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("EXECUTE dbo.loadScheduleList ", conn);
                myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    List<AdminScheduleListViewModel> list = new List<AdminScheduleListViewModel>();
                    do
                    {
                        AdminScheduleListViewModel item = new AdminScheduleListViewModel()
                        {

                            ID = Convert.ToInt32(myReader["ScheduleID"]),
                            VesselName = myReader["VesselName"].ToString(),
                            POD = myReader["POD"].ToString(),
                            POA = myReader["POA"].ToString(),
                            Departure = (Convert.ToDateTime(myReader["Departure"])).ToString("dd/MM/yyyy - hh:mm tt"),
                            Arrival = (Convert.ToDateTime(myReader["Arrival"])).ToString("dd/MM/yyyy - hh:mm tt")
                        };
                        list.Add(item);
                    } while (myReader.Read());
                    return list;
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

        public ActionResult AddNewSchedule()
        {
            if (Session["UserID"] != null)
            {
                var m = new AddNewScheduleViewModel();
                List<SelectListItem> items = new List<SelectListItem>();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                try
                {
                    conn.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand("EXECUTE dbo.loadVesselList ", conn);
                    myReader = myCommand.ExecuteReader();
                    if (myReader.Read())
                    {
                        do
                        {
                            items.Add(new SelectListItem { Text = myReader["ID"].ToString() + " - " + myReader["Name"].ToString() + " - Size: " + (Convert.ToInt32(myReader["Size"])).ToString(), Value = myReader["ID"].ToString() });
                        } while (myReader.Read());
                    }
                    else
                    {
                        items.Add(new SelectListItem { Text = "Selection Not Available", Disabled = true });
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    Console.WriteLine(ex.ToString());
                }
                m.Vessel = items;
                return View(m);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DeleteSchedule(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand($"EXECUTE dbo.AdminDeleteSchedule {id}", conn);
                myCommand.ExecuteNonQuery();
                conn.Close();
                ModelState.Clear();
                ModelState.AddModelError("", "Deleted a Shipping Schedule. ");
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.ToString());
            }
            return RedirectToAction("ShippingSchedule", "Admin");
        }

        [HttpPost]
        public ActionResult NewScheduleAction(AddNewScheduleViewModel model)
        {
            string vesselID = Request["ddlVessel"];
            string departure = model.Departure.ToString("yyyy-MM-dd hh:mm:ss");
            string arrival = model.Arrival.ToString("yyyy-MM-dd hh:mm:ss");
            if (vesselID != "")
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                try
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand($"EXECUTE dbo.AddNewSchedule {Session["UserID"].ToString()}, {vesselID}, '{model.POD}', '{model.POA}', '{departure}', '{arrival}'", conn);
                    myCommand.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    Console.WriteLine(ex.ToString());
                }
            }
            return RedirectToAction("AddNewSchedule");
        }

        //Manage Vessel Main Page
        public ActionResult ManageVessel()
        {
            if (Session["UserID"] != null)
            {
                var data = LoadVesselList();
                if (data != null)
                {
                    return View(data);
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public List<ManageVesselViewModel> LoadVesselList()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("EXECUTE dbo.loadVesselList ", conn);
                myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    List<ManageVesselViewModel> list = new List<ManageVesselViewModel>();
                    do
                    {
                        ManageVesselViewModel item = new ManageVesselViewModel()
                        {
                            ID = Convert.ToInt32(myReader["ID"]),
                            VesselName = myReader["Name"].ToString(),
                            VesselDescription = myReader["Description"].ToString(),
                            VesselSize = Convert.ToInt32(myReader["Size"]),
                            AddedDT = (Convert.ToDateTime(myReader["Added"])).ToString("dd/MM/yyyy - hh:mm:ss tt")
                        };
                        list.Add(item);
                    } while (myReader.Read());
                    return list;
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

        public ActionResult AddNewVessel()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddNewVesselAction(AddNewVesselViewModel model)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                try
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand($"EXECUTE dbo.AddNewVessel '{model.VesselName}', '{model.VesselDescription}', '{model.VesselSize}'", conn);
                    myCommand.ExecuteNonQuery();
                    conn.Close();
                    ModelState.Clear();
                    ModelState.AddModelError("", "New Vessel Added. ");
                    return View("AddNewVessel");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error has occured when adding new vessel. ");
                    Console.WriteLine(ex.ToString());
                }
            }
            return View("AddNewVessel");
        }

        
        public ActionResult DeleteVessel(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand($"EXECUTE dbo.deleteVesselProc {id}", conn);
                myCommand.ExecuteNonQuery();
                conn.Close();
                ModelState.Clear();
                ModelState.AddModelError("", "Deleted a Vessel. ");
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.ToString());
            }
            return RedirectToAction("ManageVessel", "Admin");
        }

        public ActionResult ManageShipment()
        {

            return View();
        }
    }
}