using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinanceManager.Model.Models;
using FinanceManager.Repository;
using FinanceManager.Model.Abstract;
using RabantFinanceManager.Models;
using Nancy.Json;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using FinanceManager.Model.DataTransferModel;
using System.Security.Cryptography;
using System.Text;
using RabantFinanceManager.Security;
using Microsoft.AspNetCore.DataProtection;
//using System.Web.Script.Serialization;


namespace RabantFinanceManager.Controllers
{
    [Authorize]
    public class TripDetailsController : Controller
    {
        static JavaScriptSerializer sz = new JavaScriptSerializer();
        //private readonly FinanceManagerDbContext _context;
        private ITripDetailsRepository _repository;
        private IRecipientsRepository _recRepository;
        private ISendersRepository _sendRepository;
        private IShippersRepository _shipperRepository;
        private FinanceManagerDbContext _db;
        private readonly IDataProtector protector;
        public TripDetailsController(ITripDetailsRepository repository, IRecipientsRepository recRepository,
                                        ISendersRepository sendRepository, FinanceManagerDbContext db,
                                        IShippersRepository shipperRepository,
                                        IDataProtectionProvider dataProtectionProvider,
                                        CustomerLinkEncryption customerLinkEncryption)
        {
            _repository = repository;
            _recRepository = recRepository;
            _sendRepository = sendRepository;
            _shipperRepository = shipperRepository;
            _db = db;
            protector = dataProtectionProvider.CreateProtector(customerLinkEncryption.CustomerLinkEncryptionString);
        }
        public IActionResult MainTripsPanel()
        {
            ViewBag.ShowTopBar = true;
            return View();
        }
        public string GetSenderFullName(string data)
        {
            int id = int.Parse(data);
            var senderFullname = _db.Senders.Where(i => i.SenderId == id).Select(x => x.FirstName + " " + x.LastName).FirstOrDefault();
            return senderFullname;
        }
        public IEnumerable<SelectListItem> GetRecipientFullName(string data)
        {
            var allrecipients = _recRepository.GetAllRecipients();
            var recipientsfullname = allrecipients.Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.RecipientId.ToString()
            });
            return recipientsfullname;
        }
        [HttpGet]
        //public JsonResult getSender(string postcode, string telephone)//string data
        //public JsonResult getSender(string data)
        public JsonResult getSender(Param data)
        {
            //var collectData = data;// postcode, string telephone
            string message = "";
            //string refinedData = data.Substring(1, data.Length - 2);
            //string postcode = data.Split('~')[0];
            //string postcodeNoSpace = postcode.Replace(" ", "").ToUpper();
            //string telephone = data.Split('~')[1];
            //string postcodeNoSpace = postcode;
            //string tel = telephone;

            //var mySender = _db.Senders.Where(u => u.PostCode.ToUpper().Replace(" ", "") == postcodeNoSpace && u.Telephone == telephone).Select(x => x.SenderId + "~" + x.FirstName + " " + x.LastName).FirstOrDefault();
            var mySender = _db.Senders.Where(u => u.PostCode.ToUpper().Replace(" ", "") == data.Postcode && u.Telephone == data.Telephone).Select(x => new Senders
            {
                Title = x.Title,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Gender = x.Gender,
                AddressLineOne = x.AddressLineOne,
                AddressLineTwo = x.AddressLineTwo,
                PostTown = x.PostTown,
                PostCode = x.PostCode,
                Country = x.Country,
                Telephone = x.Telephone,
                EmailAddress = x.EmailAddress,
                SenderId = x.SenderId,
                ShippersId=x.ShippersId
            }).FirstOrDefault();
            try
            {
                if (mySender != null)
                {
                    message = sz.Serialize(new
                    {
                        message = "Found Sender",
                        senderrecord = mySender,
                        results = mySender.SenderId + "~" + mySender.FirstName + " " + mySender.LastName
                    });
                    return Json(message);//, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    message = "No Record Found!";
                }

            }
            catch (Exception)
            {


            }

            return Json(message);//, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        //public JsonResult getSender(string postcode, string telephone)//string data
        public JsonResult customerDetails(string Telephone, string Postcode)
        {
            string message = "";
            string postcodeNoSpace = Postcode.Replace(" ", "").ToUpper();
            //var mySender = _db.Senders.Where(u => u.PostCode.ToUpper().Replace(" ", "") == postcodeNoSpace && u.Telephone == Telephone).Select(x => x.SenderId + "~" + x.FirstName + " " + x.LastName).FirstOrDefault();
            var mySender = _db.Senders.Where(u => u.PostCode.ToUpper().Replace(" ", "") == postcodeNoSpace && u.Telephone == Telephone).Select(x => new Senders
            {
                SenderId = x.SenderId,
                Title = x.Title,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Gender = x.Gender,
                AddressLineOne = x.AddressLineOne,
                AddressLineTwo = x.AddressLineTwo,
                PostTown = x.PostTown,
                PostCode = x.PostCode,
                Country = x.Country,
                Telephone = x.Telephone,
                EmailAddress = x.EmailAddress,
            }).FirstOrDefault();

            try
            {
                if (mySender != null)
                {
                    message = sz.Serialize(new
                    {
                        message = "Found Sender",
                        senderrecord = mySender,
                        results = mySender.SenderId + "~" + mySender.FirstName + " " + mySender.LastName
                    });
                    return Json(message);
                }
                else
                {
                    message = "No Record Found!";
                }
            }
            catch (Exception)
            {

            }
            return Json(message);
        }

        public JsonResult UpdateSenderRecord(Senders obj)
        {
            string message = "";
            var senderToUpdate = _sendRepository.UpdateSenders(obj);
            message = "Record updated!";

            return Json(message);
        }
        [HttpGet]
        public JsonResult LoadRecipientsToGrid(string Id)
        {
            string data = Id;
            string message = "failed getting Recipient";
            Recipients rc = new Recipients();
            if (data != null)
            //if (Id != "")
            {
                string refinedData = data.Substring(1, data.Length - 2);
                var id = int.Parse(data);
                var recipients = _db.Recipients.Include(r => r.Sender).Where(u => u.SenderId == id).Select(x => new RecipientsModel
                {
                    Title = x.Title,
                    SenderID = x.SenderId,
                    MySender = x.Sender.FirstName + " " + x.Sender.LastName,
                    RecipientID = x.RecipientId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    FullName = x.FirstName + " " + x.LastName,
                    AddressLineOne = x.AddressLineOne,
                    AddressLineTwo = x.AddressLineTwo,
                    PostTown = x.PostTown,
                    PostCode = x.PostCode,
                    County = x.County,
                    Country = x.Country,
                    Telephone = x.Telephone,
                    EmailAddress = x.EmailAddress,
                    RecipientDetails = x.FirstName + " " + x.LastName + " " + x.Telephone,

                });
                var recipientData = recipients.Select(x => new SelectListItem
                {
                    Text = x.RecipientDetails,
                    Value = x.RecipientID.ToString()
                });
                message = sz.Serialize(new
                {
                    message = "Sender Added Successfully",
                    Results = recipientData
                });


                return Json(message);//, JsonRequestBehavior.AllowGet);
            }
            return Json("");
        }
        // GET: Trips
        public TripEditModel getSelectedSenderAndRecipient(int tripId)
        {
            var trip = _db.TripDetails
                .Where(i => i.TripId == tripId)
               .Include(s => s.Sender)
               .Include(s => s.Recipient)
               .Select(x => x)
               .FirstOrDefault();
            TripEditModel tem = new TripEditModel();
            tem.SelectedRecipientFullName = trip.Recipient.FirstName + " " + trip.Recipient.LastName;
            tem.SelectedSenderFullName = trip.Sender.FirstName + " " + trip.Sender.LastName;
            return tem;
        }
        public IActionResult Index()
        {
            TripEditModel tem = new TripEditModel();
            tem = _repository.GetAllTrips();
            return View(tem);
        }
        public IActionResult AdminItem()
        {
            //var data = _repository.GetAllTrips();
            var data = _repository.GetOrderList();//.AllTrips.Where(i=>i.Total !=0);
            return View(data);
        }
        public IActionResult ChargeOrders()
        {
            var data = _repository.GetAllUnChargedTrips();
            //var data = _repository.GetOrderList();
            return View(data);
        }
        [HttpGet]
        public JsonResult GetItemsToInvoice(string data)
        {
            List<AdminModel> admin = new List<AdminModel>();
            string refno = data.Substring(1, data.Length - 2);
            var adm = _repository.GetTripsByRefNo(refno);
            var myResults = adm.Select(x => new AdminModel
            {
                BatchId = x.BatchId,
                ActualRef = x.ActualRef,
                SenderID = x.SenderId,
                RecipientID = x.RecipientId,
                Name = x.Name,
                Quantity = x.Quantity
            });
            admin = myResults.ToList();
            //adm.TripList = _repository.GetTripsByRefNo(refno);
            var ItemNames = GetItemNames();
            return Json(myResults);
        }

        // GET: Senders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _db.TripDetails
                .Include(s => s.Sender)
                .Include(s => s.Recipient)
                .FirstOrDefaultAsync(m => m.TripId == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // GET: TripDetails/Create
        public IActionResult Create()
        {
            //var initialItems = _repository.GetTripInitialList();
            var initialItems = _repository.GetInitialOrderParam();
            return View(initialItems);
        }

        // POST: TripDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TripCreateModel trip)
        {
            if (ModelState.IsValid)
            {
                _repository.AddTrip(trip);//.Add(senders);
                return RedirectToAction("Index", trip);
            }
            return View(trip);
        }

        [HttpGet]
        public JsonResult getNewRefNumber()
        {
            string msg = "";
            string message = "failed getting account filters";
            var RefNumber = _repository.GetNewRef();
            var itemList = _repository.GetAllItems();
            if (RefNumber != 0)
            {
                try
                {
                    message = sz.Serialize(new
                    {
                        message = "Successful",
                        RefNum = RefNumber,
                        itemlist = itemList
                    });
                }
                catch (Exception)
                {
                    message = "Could not create Reference Number";
                }
            }
            return Json(message);
        }
        public JsonResult addModalNewSender2(string data)
        {
            return Json("");
        }

        public List<TripDetailsModel> searchTrip(string id)
        {
            var data = _db.TripDetails.Include(t => t.Batch).Include(t => t.Item).Include(t => t.Recipient).Include(t => t.Sender).Select(x => new TripDetailsModel
            {
                TripID = x.TripId,
                ActualRef = x.ActualRef,
                RecipientID = x.RecipientId,
                SenderID = x.SenderId,
                SendTown = x.Sender.PostTown,
                ItemName = x.Name,
                TheSender = x.Sender.FirstName + " " + x.Sender.LastName,
                TheRecipient = x.Recipient.FirstName + " " + x.Recipient.LastName,
                BatchNo = x.Batch.BatchId,
                Total = x.Total,
                Comment = x.Comment,
                Quantity = x.Quantity,
                Status = x.Status,
            }).ToList();
            return data.Where(u => u.ActualRef == id).ToList();
        }
        [HttpGet]
        public ActionResult getOrderJS(string myref, int batch)
        {
            string message = "failed getting items";
            List<TripDetailsModel> myData = new List<TripDetailsModel>();
            try
            {
                //ViewBag.BatchID = new SelectList(_db.Batches, "BatchID", "BatchID");
                int id = batch;//////int.Parse(batch);// refinedData.Split('~')[0]);
                myData = searchTrip(myref.ToUpper());
                message = sz.Serialize(new
                {
                    message = "Loaded successful",
                    result = myData.Select(i => new
                    {
                        TheSender = i.TheSender,
                        TheRecipient = i.TheRecipient,
                        Town = i.SendTown,
                        ActualRef = i.ActualRef,
                        Total = i.Total,
                        ItemName = i.ItemName,
                        Comment = i.Comment,
                        Quantity = i.Quantity,
                        TripID = i.TripID
                    })
                });
            }
            catch (Exception)
            {
                throw;
            }
            return Json(message);//, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DeleteOrderJS(string myref, int batch)
        {
            string message = "failed getting items";
            message = _repository.DeleteOrderByRef(myref);
            return Json(message);
        }
        [HttpGet]
        public JsonResult currentTrip()
        {
            //string msg = "";
            string message = "failed getting account filters";
            var data = _repository.GetAllUnChargedTrips();
            IEnumerable<TripDetails> myTrip = null;
            myTrip = data.AllTrips.ToList();
            //////message = sz.Serialize(new
            //////{
            //////    msg="Successful",
            //////    unpaidTrips = data.AllTrips
            //////});
            //msg = "Sender Added Successfully";
            var searchData = _db.TripDetails.Where(u => u.Total == null).Select(x => new TripDetailsModel
            {
                ActualRef = x.ActualRef,
                TheSender = x.Sender.FirstName + " " + x.Sender.LastName,
                sendAddress = x.Sender.AddressLineOne + " " + x.Sender.PostCode,
                SendTel = x.Sender.Telephone,
                SendTown = x.Sender.PostTown,
                CreatedDate = x.OrderDate,
                BatchID = x.Batch.BatchId

            }).Distinct().ToList();
            return Json(searchData);//, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult addModalNewSender(string ShiperID,
                                            string SenderTitle,
                                            string SenderFirstName,
                                            string SenderLastName,
                                            string SenderGender,
                                            string SenderAdd1,
                                            string SenderAdd2,
                                            string SenderTown,
                                            string SenderPostcode,
                                            string SenderCountry,
                                            string SenderTel,
                                            string SenderEmail)
        {
            string msg = "";
            string message = "failed getting account filters";
            var checkSenderExistence = _db.Senders.Where(u => u.PostCode.Trim().ToUpper() == SenderPostcode.ToUpper() && u.Telephone == SenderTel).Select(x => x).FirstOrDefault();


            if (checkSenderExistence == null)
            {
                try
                {
                    int shippersID = 4000;
                    Senders sd = new Senders();
                    sd.ShippersId = shippersID;
                    sd.Title = SenderTitle;
                    sd.FirstName = SenderFirstName.Trim();
                    sd.LastName = SenderLastName.Trim();
                    sd.AddressLineOne = SenderAdd1;
                    sd.AddressLineTwo = SenderAdd2;
                    sd.PostTown = SenderTown;
                    sd.PostCode = SenderPostcode.ToUpper();
                    sd.Country = SenderCountry;
                    sd.Telephone = SenderTel;
                    sd.EmailAddress = SenderEmail;
                    _db.Senders.Add(sd);
                    _db.SaveChanges();

                    message = sz.Serialize(new
                    {
                        message = "Sender Added Successfully",
                        Results = _db.Senders.OrderByDescending(x => x.SenderId).Select(x => new
                        {
                            senderID = x.SenderId,
                            fullname = x.FirstName + " " + x.LastName

                        }).FirstOrDefault()
                    });
                    msg = "Sender Added Successfully";
                }
                catch (Exception)
                {

                    message = "Could not Add sender";
                }
            }



            var data = _db.Senders.ToList();
            return Json(message);//, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult addModalNewRecipient(string SenderId,
                                               string RecipientTitle,
                                               string RecipientFirstName,
                                               string RecipientLastName,
                                               string RecipientGender,
                                               string RecipientAdd1,
                                               string RecipientTown,
                                               string RecipientCountry,
                                               string RecipientTel,
                                               string RecipientEmail,
                                               string textboxNewMemberRecFullName)

        {
            //string msg = "";
            string message = "failed Adding Recipient";
            ////string recipSender = textboxNewMemberRecFullName.Replace(" ", "").Trim().ToUpper();
            ////int nameLen = textboxNewMemberRecFullName.Trim().Split(' ').Length;
            ////string[] nameArray = textboxNewMemberRecFullName.Trim().Split(' ');
            ////string nameString = TrimSpacesBetweenString(textboxNewMemberRecFullName.Trim());
            ////string sFirstName = nameString.Split(' ')[0];
            ////string sLastName = nameString.Split(' ')[1];
            ////int senId = _db.Senders.Where(u => u.FirstName.Replace(" ", "").Trim().ToUpper() + u.LastName.Replace(" ", "").Trim().ToUpper() == recipSender).Select(x => x.SenderId).FirstOrDefault();
            var checkExistence = _db.Recipients.Where(i => i.FirstName == RecipientFirstName && i.Telephone == RecipientTel && i.LastName == RecipientLastName).Select(x => x).FirstOrDefault();
            if (checkExistence == null)
            {
                try
                {
                    Recipients rp = new Recipients();
                    rp.SenderId = int.Parse(SenderId);
                    rp.Title = RecipientTitle;
                    rp.FirstName = RecipientFirstName;
                    rp.LastName = RecipientLastName;
                    rp.AddressLineOne = RecipientAdd1;
                    rp.PostTown = RecipientTown;
                    rp.Country = RecipientCountry;
                    rp.Telephone = RecipientTel;
                    _db.Recipients.Add(rp);
                    _db.SaveChanges();
                    message = sz.Serialize(new
                    {
                        message = "Recipient added Successfully",
                        Results = _db.Recipients.OrderByDescending(x => x.RecipientId).Select(x => new
                        {
                            senderID = x.SenderId,
                            recipientID = x.RecipientId,
                            recipientFullname = x.FirstName + " " + x.LastName
                        }).FirstOrDefault()
                    });
                }
                catch (Exception ex)
                {
                    message = "Recipient could not be added!";
                }
            }
            return Json(message);//, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult LoadTrip(string data)
        {
            string[] itemNames = null;
            string[] itemQty = null;
            string msg = "Failed loading items";
            string message = "";
            if (data != null)
            {
                data = data.Substring(1, data.Length - 2);
                int BatchID = int.Parse(data.Split('/')[0].Split('~')[1]);
                int SenderId = int.Parse(data.Split('/')[0].Split('~')[3]);
                int RecipientId = int.Parse(data.Split('/')[0].Split('~')[4]);
                string ActualRef = data.Split('/')[0].Split('~')[2];
                DateTime orderDate = DateTime.Parse(data.Split('/')[0].Split('~')[0].Replace("-", "/"));

                itemNames = data.Split('/')[1].Substring(1).Split('~');
                itemQty = data.Split('/')[2].Substring(1).Split('~');

                try
                {
                    for (int i = 0; i < itemNames.Count(); i++)
                    {
                        TripDetails td = new TripDetails();
                        td.OrderDate = orderDate;
                        td.BatchId = BatchID;
                        td.ActualRef = ActualRef;
                        td.SenderId = SenderId;
                        td.RecipientId = RecipientId;
                        td.Name = itemNames[i];
                        td.Quantity = int.Parse(itemQty[i]);
                        _repository.AddTrip(td);
                    }

                    msg = "Order Added Successfully!!";
                }
                catch (Exception ex)
                {

                    msg = ex.Message;
                }


            }
            message = sz.Serialize(new
            {
                result = msg
            });
            return Json(message);
        }
        [HttpPost]
        public JsonResult LoadTrip2(CustomerOrderModel data)
        {
            string[] itemNames = null;
            string[] itemQty = null;
            string msg = "Failed loading items";
            string message = "";
            if (data != null)
            {
                try
                {
                    foreach (var t in data.ItemList)
                    {
                        TripDetails td = new TripDetails();
                        td.OrderDate = data.OrderDate;
                        td.BatchId = data.BatchId;
                        td.ActualRef = data.ActualRef;
                        td.SenderId = data.SenderId;
                        td.RecipientId = data.RecipientId;
                        //td.Name = t.Name;
                        //td.Quantity =double.Parse(t.quantity);
                        _repository.AddTrip(td);
                    }

                    msg = "Order Added Successfully!!";
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
            }
            message = sz.Serialize(new
            {
                result = msg
            });
            return Json(message);
        }
        [HttpGet]
        public JsonResult GetSenderRecipients(string data)
        {
            TripCreateModel eModel = new TripCreateModel();
            int actualSenderId = int.Parse(data.Substring(1, data.Length - 2));
            var result = _repository.GetSenderRecipients(actualSenderId);
            return Json(result);
        }
        [HttpGet]
        public IActionResult CustomerOrder()
        {
            ViewData["BatchId"] = new SelectList(_db.Batch, "BatchId", "ActualBatch");
            ViewData["ItemId"] = new SelectList(_db.Items, "ItemId", "ItemId");
            ViewData["RecipientId"] = new SelectList(_db.Recipients, "RecipientId", "RecipientId");
            ViewData["SenderId"] = new SelectList(_db.Senders, "SenderId", "SenderId");
            return View();
        }
        [HttpPost]
        public IActionResult AddCustomerOrder()
        {
            ViewData["BatchId"] = new SelectList(_db.Batch, "BatchId", "ActualBatch");
            ViewData["ItemId"] = new SelectList(_db.Items, "ItemId", "ItemId");
            ViewData["RecipientId"] = new SelectList(_db.Recipients, "RecipientId", "RecipientId");
            ViewData["SenderId"] = new SelectList(_db.Senders, "SenderId", "SenderId");
            return View();
        }
        public IEnumerable<SelectListItem> GetItemNames()
        {
            AdminModel ad = new AdminModel();
            return ad.ItemNames = _repository.GetAllItems();
            // return Json(result);
        }
        [HttpGet]
        public JsonResult GetSenderAndRecipientIdsByRefNo(string data)
        {
            string message = "failed getting item list";
            string refno = data.Substring(1, data.Length - 2);
            List<TripDetails> TripList = new List<TripDetails>();
            var result = _repository.GetItemToChargeByRefNo(refno);

            message = sz.Serialize(new
            {
                msg = "Successful",
                Results = result.Select(c => new
                {
                    SenderID = c.Sender.SenderId,
                    SenderName = c.Sender.FirstName,
                    SenderTelephone = c.Sender.Telephone,
                    RecipientID = c.Recipient.RecipientId,
                    RecipientName = c.Recipient.FirstName,
                    RecipientTelephone = c.Recipient.Telephone,
                    BatchID = c.BatchId,
                    TripId = c.TripId,
                    ItemName = c.Name,
                    RefNum = c.ActualRef,
                    Qty = c.Quantity,
                    TotalCost = c.Total
                })
            });

            return Json(message);
        }
        [HttpGet]
        public JsonResult GetOrderParamByRef(RefNum obj)
        {
            string message = "failed getting item list";
            //string refno = data.Substring(1, data.Length - 2);
            List<TripDetails> TripList = new List<TripDetails>();
            var result = _repository.GetItemToChargeByRefNo(obj.id);

            message = sz.Serialize(new
            {
                msg = "Successful",
                Results = result.Select(c => new
                {
                    SenderID = c.Sender.SenderId,
                    SenderName = c.Sender.FirstName,
                    SenderTelephone = c.Sender.Telephone,
                    SenderTown = c.Sender.PostTown,
                    RecipientID = c.Recipient.RecipientId,
                    RecipientName = c.Recipient.FirstName,
                    RecipientTelephone = c.Recipient.Telephone,
                    RecipientTown = c.Recipient.PostTown,
                    BatchID = c.BatchId,
                    TripId = c.TripId,
                    ItemName = c.Name,
                    RefNum = c.ActualRef,
                    Qty = c.Quantity,
                    TotalCost = c.Total,
                    status=c.Status                    
                })
            });

            return Json(message);
        }
        [HttpGet]
        //public JsonResult ChargeCustomerOders(string data)
        public JsonResult ChargeCustomerOders(InvoiceOrders obj)
        {
            string msg = "";
            string refNumber = obj.refNumber;
            string message = "Failed Charging orders";
            int gTot = int.Parse(obj.grandTotal);
            int bchId = int.Parse(obj.batchId);
            string hashString = "";
            var checkOrderCharge = _repository.CheckOrderCharges(obj);
            if (!checkOrderCharge)
            {
                //update income table with Amount
                msg = _repository.ChargeCustomerOrders(refNumber, gTot, bchId);
                //update tripDetails table with Amount
                _repository.updateOrderWithCost(refNumber, gTot);

                //update tripAudit table with status
                var getOrder = _repository.GetTripsByRefNo(refNumber).FirstOrDefault();
                _repository.AddTripToAudit(_repository.getAuditParam(getOrder));
                //_repository.UpdateLinkCreatorTable(hashString, int.Parse(refNumber));// I will check this later
            }
            else
            {
                //=================  Status doesn't change  ===============
                //update income table with Amount
                msg = _repository.updateIncomeWithCost(refNumber, gTot);

                //update tripDetails table with Amount
                _repository.updateOrderWithCost(refNumber, gTot);
            }

            try
            {
                message = sz.Serialize(new
                {
                    result = msg.Split('~')[0],
                    cost = msg.Split('~')[1]
                });
            }
            catch (Exception ex)
            {
                message = sz.Serialize(new
                {
                    result = ex.Message
                }) ;
            }
            
            return Json(message);
        }

        // GET: TripDetails/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var trip = _repository.GetTripsById(id);
            return View(trip);
        }

        // POST: Senders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TripDetails trip)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.UpdateTrip(trip);
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
                return RedirectToAction("Index", trip);
            }
            return View(trip);
        }
        public IActionResult MainSendersPanel()
        {
            return View();
        }
        // GET: Senders/Delete/5
        public IActionResult Delete(int id)
        {
            var senders = _repository.GetTripsById(id);
            if (senders == null)
            {
                return NotFound();
            }

            return View(senders);
        }

        // POST: Senders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(TripDetails trip)
        {
            var itemDeleted = _repository.DeleteTripItemById(trip);
            return RedirectToAction("Index");
        }

        private bool SendersExists(int id)
        {
            return _db.Senders.Any(e => e.SenderId == id);
        }
        public static string TrimSpacesBetweenString(string s)
        {
            string newString = string.Empty;
            string[] str = s.Split(' ');

            foreach (var item in str)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    newString = newString + item + " ";
                }
            }
            int strLen = newString.Length;
            return newString;

        }
        public ActionResult SendEmailToCustomer(string email, string title, string refNo, string telephone, string lastName)
        {
            RegexUtilities myvar = new RegexUtilities();
            if (myvar.IsValidEmail(email))
            {
                string messageBody = string.Format("Hello " + title + "  " + lastName + "{0} " + "Thank you for sending your goods with Paco Shipping Limited. {0} Your Reference number is: " + refNo + " and Items Details attached.{0} As our cherrished customr we will ensure that your beneficiary receives the goods in approximately 14 working days.{0} In the event of any complications or clarifications please contact us via email on: info@pacoshipping.com. {0} Thank you. {0} Kind regards {0} System Administrator", Environment.NewLine);
                string.Format("", messageBody);
                MailMessage myMessage = new MailMessage("rayaduboatjc@hotmail.com", "rayaduboat@yahoo.com", "Shipping Receipt", messageBody);
                MailMessage myMessage1 = new MailMessage("rayaduboatjc@hotmail.com", email, "Shipping Receipt", messageBody);
                NetworkCredential netCredentialAccounttFrom = new NetworkCredential("rabcomsolutions@gmail.com", "Ray4455rab");
                SmtpClient smtpobj = new SmtpClient("smtp.gmail.com", 587);
                smtpobj.EnableSsl = true;
                smtpobj.Credentials = netCredentialAccounttFrom;
                smtpobj.Send(myMessage);
                smtpobj.Send(myMessage1);
                DateTime mydate = new DateTime();
            }
            return View();
        }
        [HttpGet]
        public JsonResult searchSender(string fname, string lname, string address1, string town, string postcode, string telephone)
        {
            string message = "failed adding Sender";
            string myUniqueString = "";
            Senders s = new Senders();
            SHA512Managed str = new SHA512Managed();
            string Sendpostcode = postcode;// refinedData.Split('~')[0];
            string postcodeNoSpace = Sendpostcode.Replace(" ", "").ToUpper();
            int CurrentBatchID = _repository.GetCurrentBatch();
            var mySender = _db.Senders.Where(u => u.PostCode.ToUpper().Replace(" ", "") == postcodeNoSpace && u.Telephone.Replace(" ", "") == telephone.Replace(" ", "")).Select(x => new
            {
                ID = x.SenderId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                postcode = x.PostCode,
                telephone = x.Telephone,
                shipperId = x.ShippersId
            }).FirstOrDefault();


            try
            {
                if (mySender != null)
                {
                    myUniqueString = CurrentBatchID + "~" + mySender.ID + "~" + mySender.postcode + "~" + mySender.telephone;
                    string hashedLink = "";// protector.Protect(myUniqueString);

                    //check if no link created then add to linkCreator table
                    //======================================================

                    var linkTabledata = _repository.checkLinkCreatorTable();
                    LinkCreator isLinkAvailable = linkTabledata.Where(i => i.UniqueString == myUniqueString.ToUpper() && i.BatchId == CurrentBatchID).FirstOrDefault(); //need to add batchID
                    if (isLinkAvailable == null)
                    {
                        hashedLink = CreateLink(myUniqueString);// protector.Protect(myUniqueString);
                        LinkCreator lnk = new LinkCreator();
                        lnk.SenderId = mySender.ID;
                        lnk.BatchId = CurrentBatchID;// _repository.GetCurrentBatch();
                        lnk.UrlLink = hashedLink;
                        lnk.UniqueString = myUniqueString.ToUpper();
                        lnk.LinkStatus = 0;
                        lnk.OrderStatus = 0;
                        lnk.Date = DateTime.Now;//.ToString();
                        _repository.AddCreateCustomerLinkRecord(lnk);
                    }
                    else
                    {
                        hashedLink = isLinkAvailable.UrlLink;
                    }

                    //Testing
                    //==========================
                    //string DehashedLink = protector.Unprotect(gg);
                    //string hashedLink= EncryptCustomerLink(myUniqueString);
                    message = sz.Serialize(new
                    {
                        message = "Customer already Exist",
                        Results = mySender,
                        customerLinkHash = hashedLink
                    });
                }
                else
                {
                    s.ShippersId = _shipperRepository.GetShippersIdByEmail(User.Identity.Name).ShippersId; //4001;
                    s.FirstName = fname;
                    s.LastName = lname;
                    s.AddressLineOne = address1;
                    s.PostTown = town;
                    s.PostCode = postcode.Replace(" ", "").ToUpper();
                    s.Telephone = telephone.Replace(" ", "");
                    _db.Senders.Add(s);
                    _db.SaveChanges();

                    var newSenderId = _db.Senders.Max(c => c.SenderId);
                    myUniqueString = newSenderId + "~" + s.PostCode + "~" + s.Telephone;
                    string hashedLink = protector.Protect(myUniqueString);
                    //var hashedLink = Convert.ToBase64String(str.ComputeHash(Encoding.UTF32.GetBytes(myUniqueString)));

                    //Add Link to the Lincreator Table
                    //====================================================
                    LinkCreator lnk = new LinkCreator();
                    lnk.SenderId = newSenderId;
                    lnk.BatchId = CurrentBatchID;// _repository.GetCurrentBatch();
                    lnk.UrlLink = hashedLink;
                    lnk.UniqueString = myUniqueString.ToUpper();
                    lnk.LinkStatus = 0;
                    lnk.OrderStatus = 0;
                    lnk.Date = DateTime.Now;//.ToString();
                    _repository.AddCreateCustomerLinkRecord(lnk);


                    message = sz.Serialize(new
                    {
                        message = "Added Successfuly",
                        Results = s,
                        customerLinkHash = hashedLink
                    });
                    // message = "Sender not found. Please Add";
                }
            }
            catch (Exception ex)
            {

            }


            return Json(message);//, JsonRequestBehavior.AllowGet);
        }

        //convert unique string to protected hashed link
        public string CreateLink(string uniqueString)
        {
            string hashedLink = "";
            if (uniqueString != "")
            {
                hashedLink = protector.Protect(uniqueString);
            }
            else
            {
                hashedLink = "Empty String";
            }

            return hashedLink;
        }


        [HttpGet]
        public JsonResult quickSearchSender(string postcode, string telephone)
        {
            string myUniqueString = "";
            Senders s = new Senders();
            string message = "failed adding Sender";
            SHA256Managed str = new SHA256Managed();
            int CurrentBatch = _repository.GetCurrentBatch();
            //to decrypt need convert.from
            //================================
            string Sendpostcode = postcode;// refinedData.Split('~')[0];
            string postcodeNoSpace = Sendpostcode.Replace(" ", "").ToUpper();
            var mySender = _db.Senders.Where(u => u.PostCode.ToUpper().Replace(" ", "") == postcodeNoSpace && u.Telephone.Replace(" ", "") == telephone.Replace(" ", "")).Select(x => new
            {
                ID = x.SenderId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                telephone = telephone,
                postcode = postcode,
                shippersId = x.ShippersId

            }).FirstOrDefault();

            try
            {
                if (mySender != null)
                {
                    // customer link url
                    //===================================
                    string hashedLink = CreateUrlLink(mySender.ID,mySender.postcode,mySender.telephone, CurrentBatch);

                    //Testing
                    //==========================
                    //string DehashedLink = protector.Unprotect(gg);
                    //string hashedLink= EncryptCustomerLink(myUniqueString);
                    message = sz.Serialize(new
                    {
                        message = "Customer already Exist",
                        Results = mySender,
                        customerLinkHash = hashedLink
                    });
                }
                else
                {
                    message = "Sender not found. Please Add";
                }
            }
            catch (Exception ex)
            {

            }
            return Json(message);//, JsonRequestBehavior.AllowGet);
        }

        public ActionResult tripReportView()
        {
            return View();
        }
        public ActionResult GoodBye()
        {
            return View();
        }
        //public string EncryptCustomerLink(string data)
        //{
        //    //string str = data.Substring(1, data.Length - 2);
        //    string enccrypteString = protector.Protect(data);
        //    return enccrypteString = protector.Protect(data); ;
        //}
        //public string DecryptCustomerLink(string data)
        //{
        //   // string str = data.Substring(1, data.Length - 2);
        //    string enccrypteString = protector.Unprotect(data);
        //    return Json(enccrypteString);
        //}
        public string CreateUrlLink (int senderId, string postcode, string telephone, int CurrentBatch)
        {
           var myUniqueString = senderId + "~" + postcode + "~" + telephone;
            string hashedLink = "";// protector.Protect(myUniqueString);

            //check if no link created then add to linkCreator table
            //======================================================

            var linkTabledata = _repository.checkLinkCreatorTable();
            LinkCreator isLinkAvailable = linkTabledata.Where(i => i.UniqueString == myUniqueString.ToUpper() && i.BatchId == CurrentBatch).FirstOrDefault();
            if (isLinkAvailable == null)
            {
                hashedLink = protector.Protect(myUniqueString);
                LinkCreator lnk = new LinkCreator();
                lnk.SenderId = senderId;
                lnk.BatchId = CurrentBatch;// _repository.GetCurrentBatch();
                lnk.UrlLink = hashedLink;
                lnk.UniqueString = myUniqueString.ToUpper();
                lnk.LinkStatus = 0;
                lnk.OrderStatus = 0;
                lnk.Date = DateTime.Now;//.ToString();
                _repository.AddCreateCustomerLinkRecord(lnk);
            }
            else
            {
                hashedLink = isLinkAvailable.UrlLink;
            }

            return hashedLink;
        }
    }

}
