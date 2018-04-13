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
    public class AgentController : Controller
    {
        // GET: Agent
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

        public ActionResult Customers()
        {
            if (Session["UserID"] != null)
            {
                var data = LoadCustomerList();
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

        public List<CustomerListViewModel> LoadCustomerList()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("EXECUTE dbo.LoadCustomerList ", conn);
                myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    List<CustomerListViewModel> list = new List<CustomerListViewModel>();
                    do
                    {
                        CustomerListViewModel item = new CustomerListViewModel()
                        {
                            ID = Convert.ToInt32(myReader["ID"]),
                            Name = myReader["Name"].ToString(),
                            Address = myReader["Address"].ToString(),
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

        public ActionResult AddNewCustomer()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult AddNewCustomerAction(AddNewCustomerViewModel model)
        {
            string custName = model.Name;
            string custAddr = model.Address;
            string agentID = Session["UserID"].ToString();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                SqlCommand myCommand = new SqlCommand($"EXECUTE dbo.AddNewCustomerProc '{custName}', '{custAddr}', {agentID}", conn);
                conn.Open();
                myCommand.ExecuteNonQuery();
                conn.Close();
                ModelState.Clear();
                ModelState.AddModelError("", "New Customer Added. ");
            }
            catch(Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.ToString());
            }
            return RedirectToAction("AddNewCustomer");
        }

        public ActionResult EditCustomer(int id)
        {
            if (Session["UserID"] != null)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand myCommand = new SqlCommand($"EXECUTE dbo.LoadCustomerDetails {id}", conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = myCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        EditCustomerViewModel model = new EditCustomerViewModel
                        {
                            CustID = id,
                            Name = reader["Name"].ToString(),
                            Address = reader["Address"].ToString()
                        };
                        conn.Close();
                        return View(model);
                    }
                    else
                    {
                        conn.Close();
                        return RedirectToAction("Customers");
                    }
                }
                catch(Exception ex)
                {
                    conn.Close();
                    Console.WriteLine(ex.ToString());
                    ModelState.AddModelError("", "Error Occurred. Please Try Again. ");
                    return RedirectToAction("Customers");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditCustomerAction(EditCustomerViewModel model)
        {
            string custName = model.Name;
            string custAddr = model.Address;
            int custID = model.CustID;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand($"EXECUTE dbo.UpdateCustomer {custID}, '{custName}', '{custAddr}'", conn);
                myCommand.ExecuteNonQuery();
                conn.Close();
                ModelState.Clear();
                ModelState.AddModelError("", "Customer Details Updated. ");
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.ToString());
                ModelState.AddModelError("", "Error Occurred. Please Try Again. ");
            }
            return RedirectToAction("EditCustomer", new { id = custID });
        }

        public ActionResult DeleteCustomer(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand($"EXECUTE dbo.DeleteCustomer {id}", conn);
                myCommand.ExecuteNonQuery();
                conn.Close();
                ModelState.Clear();
                ModelState.AddModelError("", "Deleted a Customer. ");
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.ToString());
            }
            return RedirectToAction("Customers");
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

        public List<AgentScheduleListViewModel> LoadScheduleList()
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
                    List<AgentScheduleListViewModel> list = new List<AgentScheduleListViewModel>();
                    do
                    {
                        AgentScheduleListViewModel item = new AgentScheduleListViewModel()
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

        public ActionResult Booking()
        {
            if (Session["UserID"] != null)
            {
                var data = LoadAvailableScheduleList();
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

        public List<AgentChooseScheduleViewModel> LoadAvailableScheduleList()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("EXECUTE dbo.loadAvailableScheduleList ", conn);
                myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    List<AgentChooseScheduleViewModel> list = new List<AgentChooseScheduleViewModel>();
                    do
                    {
                        AgentChooseScheduleViewModel item = new AgentChooseScheduleViewModel()
                        {
                            ID = Convert.ToInt32(myReader["ScheduleID"]),
                            POD = myReader["POD"].ToString(),
                            POA = myReader["POA"].ToString(),
                            Departure = (Convert.ToDateTime(myReader["Departure"])).ToString("dd/MM/yyyy - hh:mm tt"),
                            Arrival = (Convert.ToDateTime(myReader["Arrival"])).ToString("dd/MM/yyyy - hh:mm tt"),
                            SpaceAvailable = Convert.ToInt32(myReader["SpaceAvailable"])
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

        public ActionResult ChooseSchedule(AgentChooseScheduleViewModel model)
        {

            return RedirectToAction("NewShipment", new { id = model.ID });
        }

        public ActionResult NewShipment(int id)
        {
            var m = new AgentNewShipmentViewModel();
            m.ScheduleID = id;
            List<SelectListItem> items = new List<SelectListItem>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try 
            {
                SqlCommand command = new SqlCommand($"EXECUTE dbo.LoadCustomerList", conn);
                conn.Open();
                SqlDataReader data = command.ExecuteReader();
                if (data.Read())
                {
                    do
                    {
                        items.Add(new SelectListItem { Text = $"{data["Name"].ToString()}, {data["Address"].ToString()}", Value = data["ID"].ToString() });
                    } while (data.Read());
                }
                else
                {
                    items.Add(new SelectListItem { Text = "Selection Not Available", Disabled = true });
                }
                conn.Close();
            }
            catch(Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.ToString());
            }
            m.CustomerList = items;
            SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand($"EXECUTE dbo.LoadScheduleDetails {id}", conn2);
                conn2.Open();
                SqlDataReader data = command.ExecuteReader();
                if (data.Read())
                {
                    m.POD = data["POD"].ToString();
                    m.POA = data["POA"].ToString();
                    m.Departure = (Convert.ToDateTime(data["Departure"])).ToString("dd/MM/yyyy - hh:mm tt");
                    m.Arrival = (Convert.ToDateTime(data["Arrival"])).ToString("dd/MM/yyyy - hh:mm tt");
                    m.SpaceAvailable = Convert.ToInt32(data["SpaceAvailable"]);
                }
                else
                {
                    items.Add(new SelectListItem { Text = "Selection Not Available", Disabled = true });
                }
                conn2.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.ToString());
            }
            return View(m);
        }

        [HttpPost]
        public ActionResult AddNewShipmentAction(AgentNewShipmentViewModel m)
        {
            string shipperID = Request["ddlShipper"];
            string consigneeID = Request["ddlConsignee"];
            if(shipperID != "" && consigneeID != "")
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand myCommand = new SqlCommand($"Execute NewShipment {m.ScheduleID}, '{m.Commodity}', '{m.ContainerType}', {m.PackagesAmount}, '{m.KindOfPackages}', {m.Weight.ToString()}, {m.Volume.ToString()}, '{m.Description}', {Session["UserID"].ToString()}, {shipperID}, {consigneeID}, '{m.ContractNo}', {m.Price.ToString()}", conn);
                try
                {
                    conn.Open();
                    myCommand.ExecuteNonQuery();
                    conn.Close();
                }
                catch(Exception ex)
                {
                    conn.Close();
                    Console.WriteLine(ex.ToString());
                    ModelState.AddModelError("", "Error Occurred, please try again. ");
                    return RedirectToAction("NewShipment", new { id = m.ScheduleID });
                }
            }
            else
            {
                ModelState.AddModelError("", "Shipper and Consignee must be selected. ");
                return RedirectToAction("NewShipment", new { id = m.ScheduleID });
            }
            return RedirectToAction("Index");
        }

        public ActionResult Tracking()
        {

            return View();
        }

        public ActionResult Shipments()
        {
            if (Session["UserID"] != null)
            {
                var data = LoadShipmentList();
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

        public List<AgentShipmentViewModel> LoadShipmentList()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand($"EXECUTE dbo.LoadShipmentList {Session["UserID"].ToString()}", conn);
                myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    List<AgentShipmentViewModel> list = new List<AgentShipmentViewModel>();
                    do
                    {
                        AgentShipmentViewModel item = new AgentShipmentViewModel()
                        {
                            ID = Convert.ToInt64(myReader["ShipmentID"]).ToString(),
                            POD = myReader["POD"].ToString(),
                            POA = myReader["POA"].ToString(),
                            Departure = (Convert.ToDateTime(myReader["Departure"])).ToString("dd/MM/yyyy - hh:mm tt"),
                            Arrival = (Convert.ToDateTime(myReader["Arrival"])).ToString("dd/MM/yyyy - hh:mm tt"),
                            Status = myReader["Status"].ToString()
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

        public ActionResult ShipmentDetails(string id)
        {
            AgentShipmentDetailsViewModel m = null;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                SqlDataReader reader = null;
                SqlCommand myCommand = new SqlCommand($"EXECUTE dbo.LoadShipmentDetail {id}", conn);
                reader = myCommand.ExecuteReader();
                if (reader.Read())
                {
                    m = new AgentShipmentDetailsViewModel
                    {
                        ID = reader["ShipmentID"].ToString(),
                        POD = reader["POD"].ToString(),
                        POA = reader["POA"].ToString(),
                        Departure = (Convert.ToDateTime(reader["Departure"])).ToString("dd/MM/yyyy - hh:mm tt"),
                        Arrival = (Convert.ToDateTime(reader["Arrival"])).ToString("dd/MM/yyyy - hh:mm tt"),
                        ContractNo = ((reader["ContractNo"] != null) ? reader["ContractNo"].ToString() : "N/A"),
                        Price = String.Format("{0:0.00}", Convert.ToDecimal(reader["Price"])),
                        Status = reader["Status"].ToString(),
                        ShipperName = reader["ShipperName"].ToString(),
                        ShipperAddress = reader["ShipperAddress"].ToString(),
                        ConsigneeName = reader["ConsigneeName"].ToString(),
                        ConsigneeAddress = reader["ConsigneeAddress"].ToString(),
                        Commodity = reader["Commodity"].ToString(),
                        ContainerType = reader["ContainerType"].ToString(),
                        PackagesAmount = Convert.ToInt32(reader["PackagesAmount"]).ToString(),
                        KindOfPackages = reader["KindOfPackages"].ToString(),
                        Weight = String.Format("{0:0.000}", Convert.ToDecimal(reader["Weight"])),
                        Volume = String.Format("{0:0.000}", Convert.ToDecimal(reader["Volume"])),
                        Description = reader["Description"].ToString()
                    };
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
            return View(m);
        }

        public ActionResult DeleteShipment(string id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand($"EXECUTE dbo.DeleteShipment {id}", conn);
                myCommand.ExecuteNonQuery();
                conn.Close();
                ModelState.Clear();
                ModelState.AddModelError("", "Deleted a Shipment. ");
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.ToString());
            }
            return RedirectToAction("Shipments");
        }
    }
}