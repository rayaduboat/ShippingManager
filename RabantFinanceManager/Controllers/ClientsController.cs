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
using Nancy.Json;
using RabantFinanceManager.Models;
using FinanceManager.Model.DataTransferModel;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using SelectPdf;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using Grpc.Core;
using System.Reflection.Metadata;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.LayoutEngine.LayoutElements;
using ExpertPdf.HtmlToPdf;
using Microsoft.AspNetCore.DataProtection;
using RabantFinanceManager.Security;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;

namespace RabantFinanceManager.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        static JavaScriptSerializer sz = new JavaScriptSerializer();
        private readonly FinanceManagerDbContext _context;
        private readonly ITripDetailsRepository _repository;
        private readonly IRecipientsRepository _recRepository;
        private readonly IDataProtector protector;

        public ClientsController(FinanceManagerDbContext context, ITripDetailsRepository repository,
                                IDataProtectionProvider dataProtectionProvider, IRecipientsRepository recRepository,
                                CustomerLinkEncryption customerLinkEncryption)
        {
            _context = context;
            _repository = repository;
            _recRepository = recRepository;
            protector = dataProtectionProvider.CreateProtector(customerLinkEncryption.CustomerLinkEncryptionString);
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            //var financeManagerDbContext = _context.TripDetails.Include(t => t.Batch).Include(t => t.Item).Include(t => t.Recipient).Include(t => t.Sender);//.Where(i=>i.ActualRef== refNum);
            //return View(await financeManagerDbContext.ToListAsync());
            return View();
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripDetails = await _context.TripDetails
                .Include(t => t.Batch)
                .Include(t => t.Item)
                .Include(t => t.Recipient)
                .Include(t => t.Sender)
                .FirstOrDefaultAsync(m => m.TripId == id);
            if (tripDetails == null)
            {
                return NotFound();
            }

            return View(tripDetails);
        }
        [HttpGet]
        //public JsonResult LoadTrip2(int SenderID, int RecipientID,string ActualRef,string OrderDate, int BatchID,string OrderList)
        //public ActionResult LoadTrip2(string data)
        public ActionResult LoadTrip2(DataTransferModel obj)
        {
            string data = "";
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
                string hashString = data.Split('/')[0].Split('~')[5];
                DateTime orderDate = DateTime.Parse(data.Split('/')[0].Split('~')[0].Replace("-", "/"));
                int refNumber = int.Parse(ActualRef);
                itemNames = data.Split('/')[1].Substring(1).Split('~');
                itemQty = data.Split('/')[2].Substring(1).Split('~');
                try
                {
                    List<string> ItemList = new List<string>();
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
                    if (hashString != "" && ActualRef != "")//=== I will check this later
                    {
                        _repository.UpdateLinkCreatorTable(hashString, refNumber);
                    }
                    else
                    {
                        // msg = "Link update failed";
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
        public ActionResult CustomerCheckout(DataTransferModel obj)
        {
            string msg = "Failed loading items";
            string message = "";

            if (obj != null)
            {
                var Mydata = sz.Deserialize<IEnumerable<ItemModel>>(obj.orderList);
                try
                {
                    string orderStatus = "Created";
                    foreach (ItemModel m in Mydata)
                    {
                        TripDetails td = new TripDetails();
                        td.Status = "Created";
                        td.OrderDate = DateTime.Parse(obj.orderDate);
                        td.BatchId = int.Parse(obj.batchId);
                        td.ActualRef = obj.actualRef;
                        td.SenderId = int.Parse(obj.senderId);
                        td.RecipientId = int.Parse(obj.recipientId);
                        td.Name = m.name;
                        td.Quantity = int.Parse(m.quantity);
                        _repository.AddTrip(td);
                        
                    }
                    //===== Update customer link and order status ==============
                    if (obj.actualRef != "")
                    {
                        _repository.UpdateLinkCreatorTable(obj.urlLink, int.Parse(obj.actualRef));
                    }
                    else
                    {
                        // msg = "Link update failed";
                    }
                    msg = "Order Added Successfully!!";
                    //===== Update TripAudit table ===============
                    TripDetails trip = new TripDetails();
                    trip.Status = orderStatus;
                    trip.BatchId = int.Parse(obj.batchId);
                    trip.SenderId = int.Parse(obj.senderId);
                    trip.RecipientId = int.Parse(obj.recipientId);
                    trip.ActualRef = obj.actualRef;
                    trip.OrderDate = DateTime.Parse(obj.orderDate);
                    //getAuditParam(trip);
                    //trip.NewStatus = _repository.GetStatusValueByName(orderStatus);
                    //trip.OriginalStatus = _repository.GetStatusValueByName(_repository.GetOriginalStatusFromNewStatus(orderStatus));
                    //_context.TripAudit.Add(aud);
                    //_context.SaveChanges();

                    _repository.AddTripToAudit(_repository.getAuditParam(trip));
                    //UpdateAuditTable(getAuditParam(trip));

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

        ////////public TripAudit getAuditParam(TripDetails td)
        ////////{
        ////////    TripAudit aud = new TripAudit();
        ////////    //var currentStatus = _repository.GetCurrentStatusByRefId(td.ActualRef);

        ////////    aud.BatchId = td.BatchId;
        ////////    aud.SenderId = td.SenderId;
        ////////    aud.RecipientId = td.RecipientId;
        ////////    aud.RefId = int.Parse(td.ActualRef);
        ////////    aud.OrderDate = td.OrderDate;
        ////////    aud.NewStatus = _repository.GetStatusValueByName(td.Status);
        ////////    aud.OriginalStatus =_repository.GetStatusValueByName(_repository.GetOriginalStatusFromNewStatus(td.Status));
        ////////    return aud;
        ////////}


        ////////private TripAudit UpdateAuditTable(TripAudit aud)
        ////////{
        ////////    return _repository.AddTripToAudit(aud);
        ////////}

        public IActionResult Create()
        {
            //ViewBag.ShowTopBar = true;
            ////ViewData["BatchId"] = new SelectList(_context.Batch, "BatchId", "ActualBatch");
            ////ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemId");
            ////ViewData["RecipientId"] = new SelectList(_context.Recipients, "RecipientId", "RecipientId");
            ////ViewData["SenderId"] = new SelectList(_context.Senders, "SenderId", "SenderId");
            ////return View();
            var initialItems = _repository.GetInitialOrderParam();
            return View(initialItems);
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TripId,ActualRef,ItemId,SenderId,RecipientId,BatchId,Name,Comment,Quantity,Total,GrandTotal,OrderDate,Status")] CustomerOrderModel tripDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tripDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BatchId"] = new SelectList(_context.Batch, "BatchId", "ActualBatch", tripDetails.BatchId);
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemId", tripDetails.ItemId);
            ViewData["RecipientId"] = new SelectList(_context.Recipients, "RecipientId", "RecipientId", tripDetails.RecipientId);
            ViewData["SenderId"] = new SelectList(_context.Senders, "SenderId", "SenderId", tripDetails.SenderId);
            return View(tripDetails);
        }
        //////// GET: Clients/Create
        //////public IActionResult MyCustomers()
        //////{
        //////    ViewData["BatchId"] = new SelectList(_context.Batch, "BatchId", "ActualBatch");
        //////    ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemId");
        //////    ViewData["RecipientId"] = new SelectList(_context.Recipients, "RecipientId", "RecipientId");
        //////    ViewData["SenderId"] = new SelectList(_context.Senders, "SenderId", "SenderId");
        //////    return View();
        //////}
        //////// POST: Clients/Create
        //////// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //////// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //////[HttpPost]
        //////[ValidateAntiForgeryToken]
        //////public async Task<IActionResult> MyCustomers([Bind("TripId,ActualRef,ItemId,SenderId,RecipientId,BatchId,Name,Comment,Quantity,Total,GrandTotal,OrderDate,Status")] TripDetails tripDetails)
        //////{
        //////    if (ModelState.IsValid)
        //////    {
        //////        _context.Add(tripDetails);
        //////        await _context.SaveChangesAsync();
        //////        return RedirectToAction(nameof(Index));
        //////    }
        //////    ViewData["BatchId"] = new SelectList(_context.Batch, "BatchId", "ActualBatch", tripDetails.BatchId);
        //////    ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemId", tripDetails.ItemId);
        //////    ViewData["RecipientId"] = new SelectList(_context.Recipients, "RecipientId", "RecipientId", tripDetails.RecipientId);
        //////    ViewData["SenderId"] = new SelectList(_context.Senders, "SenderId", "SenderId", tripDetails.SenderId);
        //////    return View(tripDetails);
        //////}

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripDetails = await _context.TripDetails.FindAsync(id);
            if (tripDetails == null)
            {
                return NotFound();
            }
            ViewData["BatchId"] = new SelectList(_context.Batch, "BatchId", "ActualBatch", tripDetails.BatchId);
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemId", tripDetails.ItemId);
            ViewData["RecipientId"] = new SelectList(_context.Recipients, "RecipientId", "RecipientId", tripDetails.RecipientId);
            ViewData["SenderId"] = new SelectList(_context.Senders, "SenderId", "SenderId", tripDetails.SenderId);
            return View(tripDetails);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TripDetails tripDetails)
        {
            if (id != tripDetails.TripId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tripDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripDetailsExists(tripDetails.TripId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                int clientId = tripDetails.SenderId;
                //return RedirectToAction(nameof(LoadAllClientOrders));
                return RedirectToAction("LoadAllClientOrders", tripDetails);
            }
            ViewData["BatchId"] = new SelectList(_context.Batch, "BatchId", "ActualBatch", tripDetails.BatchId);
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemId", tripDetails.ItemId);
            ViewData["RecipientId"] = new SelectList(_context.Recipients, "RecipientId", "RecipientId", tripDetails.RecipientId);
            ViewData["SenderId"] = new SelectList(_context.Senders, "SenderId", "SenderId", tripDetails.SenderId);
            return View(tripDetails);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripDetails = await _context.TripDetails
                .Include(t => t.Batch)
                .Include(t => t.Item)
                .Include(t => t.Recipient)
                .Include(t => t.Sender)
                .FirstOrDefaultAsync(m => m.TripId == id);
            if (tripDetails == null)
            {
                return NotFound();
            }

            return View(tripDetails);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tripDetails = await _context.TripDetails.FindAsync(id);
            _context.TripDetails.Remove(tripDetails);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(LoadAllClientOrders));
            return RedirectToAction("LoadAllClientOrders",tripDetails.SenderId);
        }

        private bool TripDetailsExists(int id)
        {
            return _context.TripDetails.Any(e => e.TripId == id);
        }
        public JsonResult LoadTrip(string data)
        {
            string[] itemNames = null;
            string[] itemQty = null;
            string msg = "Failed loading items";
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
            return Json(msg);
        }
        public JsonResult GetSenderRecipients(string data)
        {
            TripCreateModel eModel = new TripCreateModel();
            int actualSenderId = int.Parse(data.Substring(1, data.Length - 2));
            var result = _repository.GetSenderRecipients(actualSenderId);
            return Json(result);
        }
        public JsonResult getSender(string data)
        {
            string refinedData = data.Substring(1, data.Length - 2);
            string postcode = refinedData.Split('~')[0];
            string postcodeNoSpace = postcode.Replace(" ", "").ToUpper();
            string telephone = refinedData.Split('~')[1];
            var mySender = _context.Senders.Where(u => u.PostCode.ToUpper().Replace(" ", "") == postcodeNoSpace && u.Telephone == telephone).Select(x => x.SenderId + "~" + x.FirstName + " " + x.LastName).FirstOrDefault();
            try
            {
                if (mySender != null)
                {
                    return Json(mySender);//, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mySender = "No Record Found!";
                }

            }
            catch (Exception)
            {


            }

            return Json(mySender);//, JsonRequestBehavior.AllowGet);

        }

        //public JsonResult LoadRecipientsToGrid(string Id)
        public JsonResult LoadRecipientsToGrid(string data)
        {
            string message = "failed getting Recipient";
            Recipients rc = new Recipients();
            if (data != null)
            //if (Id != "")
            {
                string refinedData = data.Substring(1, data.Length - 2);
                var id = int.Parse(refinedData);
                var recipients = _context.Recipients.Include(r => r.Sender).Where(u => u.SenderId == id).Select(x => new RecipientsModel
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
                //var finalData = recipients.Select(x => x.RecipientDetails).ToList();
                //put codes for populating SenderRecipients here
                var recipientData = recipients.Select(x => new SelectListItem
                {
                    Text = x.RecipientDetails,
                    Value = x.RecipientID.ToString()
                });
                //var sendersRecipientsFullName = recipients.Select(x => new SelectListItem
                //{
                //    Text = x.FirstName + " " + x.LastName,
                //    Value = x.RecipientId.ToString()
                //});



                message = sz.Serialize(new
                {
                    message = "Sender Added Successfully",
                    Results = recipientData
                });


                return Json(message);//, JsonRequestBehavior.AllowGet);
            }
            return Json("");
        }
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
        public ActionResult getOrderJS(string myref, int batch)
        {
            string message = "failed getting items";
            List<TripDetailsModel> myData = new List<TripDetailsModel>();
            try
            {
                //ViewBag.BatchID = new SelectList(_context.Batches, "BatchID", "BatchID");
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
        public List<TripDetailsModel> searchTrip(string id)
        {
            var data = _context.TripDetails.Include(t => t.Batch).Include(t => t.Item).Include(t => t.Recipient).Include(t => t.Sender).Select(x => new TripDetailsModel
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
            var checkSenderExistence = _context.Senders.Where(u => u.PostCode.Trim().ToUpper() == SenderPostcode.ToUpper() && u.Telephone == SenderTel).Select(x => x).FirstOrDefault();


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
                    _context.Senders.Add(sd);
                    _context.SaveChanges();

                    message = sz.Serialize(new
                    {
                        message = "Sender Added Successfully",
                        Results = _context.Senders.OrderByDescending(x => x.SenderId).Select(x => new
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



            var data = _context.Senders.ToList();
            return Json(message);//, JsonRequestBehavior.AllowGet);
        }

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
            ////int senId = _context.Senders.Where(u => u.FirstName.Replace(" ", "").Trim().ToUpper() + u.LastName.Replace(" ", "").Trim().ToUpper() == recipSender).Select(x => x.SenderId).FirstOrDefault();
            var checkExistence = _context.Recipients.Where(i => i.FirstName == RecipientFirstName && i.Telephone == RecipientTel && i.LastName == RecipientLastName).Select(x => x).FirstOrDefault();
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
                    _context.Recipients.Add(rp);
                    _context.SaveChanges();
                    message = sz.Serialize(new
                    {
                        message = "Recipient added Successfully",
                        Results = _context.Recipients.OrderByDescending(x => x.RecipientId).Select(x => new
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
        public JsonResult AddSenderRecipient(AddRecipientModel obj)
        {
            string message = "Failed Adding Recipient";
            if (ModelState.IsValid)
            {
                int sendID = int.Parse(obj.sendId);
                Recipients rp = new Recipients();
                rp.SenderId = sendID;// int.Parse(obj.sendId);
                rp.Title = obj.title;
                rp.FirstName = obj.fname;
                rp.LastName = obj.lname;
                rp.AddressLineOne = obj.add1;
                rp.PostTown = obj.town;
                rp.Country = obj.country;
                rp.Telephone = obj.telephone;
                _recRepository.AddRecipient(rp);
            }
            var result = _recRepository.GetSendersRecipients(obj.sendId).AllRecipients.OrderByDescending(i => i.RecipientId);
            message = sz.Serialize(new
            {
                message = "Recipient added Successfully",
                allSenderRecipients = result.Select(t => new SelectListItem
                {
                    Text = t.FirstName + " " + t.LastName + " " + t.Telephone,
                    Value = t.RecipientId.ToString(),
                }),
                newRecipient = result.Select(x => new
                {
                    sendId = x.SenderId,
                    recipientID = x.RecipientId,
                    recipientFullname = x.FirstName + " " + x.LastName + " " + x.Telephone
                }).FirstOrDefault()
            });

            //  }
            return Json(message);
        }
        public JsonResult EncryptCustomerLink(string data)
        {
            string str = data.Substring(1, data.Length - 2);
            string enccrypteString = protector.Protect(data);
            return Json(enccrypteString);
        }
        public JsonResult DecryptCustomerLink(string data)
        {
            //string str = data.Substring(1, data.Length - 2);
            string enccrypteString = protector.Unprotect(data);
            return Json(enccrypteString);
        }
        public JsonResult UpdateCustomerLink(string data)
        {
            //string str = data.Substring(1, data.Length - 2);
            string encrypteString = protector.Unprotect(data);
            return Json(encrypteString);
        }
        public JsonResult IsLinkAvailable(string Link)
        {
            List<string> linkParameters = new List<string>();
            string message = "Link Not Available!";
            string msg = "";
            int orderStatus = 0;
            int linkStatus = 0;
            string uniqueString = "";
            string viewString = "";


            var checkLink = _context.LinkCreator.Where(i => i.UrlLink == Link).FirstOrDefault();
            if (checkLink != null)
            {
                if (checkLink.LinkStatus == 0)
                {
                    switch (checkLink.OrderStatus)
                    {
                        case 0:
                            viewString = "Create";

                            break;

                        case 1:
                            viewString = "Index";
                            break;
                        case 2:

                            break;
                        case 3:

                            break;
                        case 4:

                            break;
                        case 5:

                            break;
                        case 6:

                            break;
                        case 7:

                            break;
                    }
                }
                else
                {
                    //Link closed
                }
                message = "Link Exists";
                message = sz.Serialize(new
                {
                    message = message,
                    orderStatus = checkLink.OrderStatus,
                    linkStatus = checkLink.LinkStatus,
                    uniqueString = checkLink.UniqueString,
                    batchId = checkLink.BatchId,
                    senderId = checkLink.SenderId,
                    refNum = checkLink.RefNum,
                });
            }
            else
            {
                //msg = "Link Not Available!";
            }


            return Json(message);
        }
        public IEnumerable<SelectListItem> GetItemNames()
        {
            AdminModel ad = new AdminModel();
            return ad.ItemNames = _repository.GetAllItems();
            // return Json(result);
        }

        public IActionResult ExistingOrders()
        {
            return View();
        }
        ////[HttpGet]
        ////public JsonResult HtmlToPdf(string data)
        ////{
        ////    string url = data.Substring(1, data.Length - 2);
        ////    string msg = "";
        ////    // instantiate a html to pdf converter object
        ////    HtmlToPdf converter = new HtmlToPdf();

        ////    // create a new pdf document converting an url
        ////    PdfDocument doc = converter.ConvertUrl(url);
        ////    // save pdf document
        ////    doc.Save("Receipt.pdf");
        ////    msg = "Pdf file created Successfully!";
        ////    // close pdf document
        ////    doc.Close();
        ////    return Json(msg);
        ////}
        [HttpGet]
        public JsonResult HtmlToPdf2(string data)
        {
            string url = data.Substring(1, data.Length - 2);
            string msg = "";
            PdfConverter pdfConverter = new PdfConverter();

            pdfConverter.PdfDocumentOptions.PdfPageSize = ExpertPdf.HtmlToPdf.PdfPageSize.A4;
            byte[] downloadBytes = pdfConverter.GetPdfFromUrlBytes(url);
            //pdfConverter.SavePdfFromHtmlFileToFile(url, "Receipt.pdf");
            msg = "Pdf file created Successfully!";
            return Json(msg);
        }
        public JsonResult CustomersExistingOrders(string RefNum)
        {
            IEnumerable<TripDetails> orders = null;
            string message = "Failed Getting Orders";
            string msg = "";
            string orderStatus = "";
            if (RefNum != "")
            {
                orders = _repository.GetTripsByRefNo(RefNum).ToList();
                msg = "Existing Orders Found!";

                //RedirectToAction("ExistingOrders", orders);

            }

            orderStatus = _repository.GetOrderStatusByRefNumber(RefNum);

            if (orders != null)
            {
                message = sz.Serialize(new
                {
                    message = msg,
                    result = orders.Select(x => new
                    {
                        refNum = x.ActualRef,
                        tripId = x.TripId,
                        batchId = x.BatchId,
                        name = x.Name,
                        quantity = x.Quantity,
                        totalCost = x.GrandTotal,
                    }),
                    oStatus = orderStatus.Split('~')[0],
                    cost = orderStatus.Split('~')[1]
                });
            }

            return Json(message);
        }
        public IActionResult LoadAllClientOrders(int clientId)
        {
            //TripDetails tr = new TripDetails();
            var model = _repository.GetAllClientOrdersByClientId(clientId).ToList();
            var data = model.GroupBy(i => i.ActualRef).Select(x => new TripDetails
            {
                ActualRef = x.First().ActualRef,
                OrderDate = x.First().OrderDate,
                Quantity = x.Sum(u => u.Quantity),
                Total = x.First().Total,
                Status = x.First().Status,
                Recipient = x.First().Recipient
            });
            //var model = _repository.GetAllOrderStatusList().Where(i=>i.SenderId== clientId).ToList();
            //List<TripDetails> dataList = new List<TripDetails>();
            //foreach (OrderStatusModel os in model)
            //{
            //    TripDetails tr = new TripDetails();
            //    tr.ActualRef = os.RefNumber;
            //    tr.OrderDate = os.orderDate;
            //    tr.Quantity = os.Quantity;
            //    tr.Status = os.status;
            //    tr.Total = os.Total;
            //    tr.Recipient.FirstName=
            //    dataList.Add(tr);
            //}
            
            return View(data);
        }

    }
}
