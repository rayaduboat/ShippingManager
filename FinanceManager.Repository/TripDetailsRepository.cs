using FinanceManager.Model.Abstract;
using FinanceManager.Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using MoreLinq;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using FinanceManager.Model.DataTransferModel;

namespace FinanceManager.Repository
{
    public class TripDetailsRepository : ITripDetailsRepository
    {
        enum OrderStatus
        {

        }
        //public class TripDetailsRepository : ITripDetailsRepository
        //{
        private FinanceManagerDbContext _context;

        public TripDetailsRepository(FinanceManagerDbContext context)
        {
            _context = context;
        }
        public TripDetails AddTrip(TripDetails trip)
        {
            _context.Add(trip);
            _context.SaveChanges();
            return trip;
        }

        public TripEditModel GetAllTrips()
        {
            var trip = CollectTrips();
            //var trip = _context.TripDetails
            //   .Include(s => s.Sender)
            //   .Include(s => s.Recipient)
            //   .Select(x => x)
            //   .ToList();
            TripEditModel tem = new TripEditModel();
            tem.AllTrips = trip;
            tem.RecipientsFullName = trip.Select(x => new SelectListItem
            {
                Text = x.Recipient.FirstName + " " + x.Recipient.LastName,
                Value = x.RecipientId.ToString()// x.FirstName + " " + x.LastName
            });
            tem.SendersFullName = trip.Select(x => new SelectListItem
            {
                Text = x.Sender.FirstName + " " + x.Sender.LastName,
                Value = x.SenderId.ToString()// x.FirstName + " " + x.LastName
            });
            tem.SelectedRecipientFullName = trip.Select(u => u.Recipient.FirstName + " " + u.Recipient.LastName).ToString();
            tem.SelectedSenderFullName = trip.Select(u => u.Sender.FirstName + " " + u.Sender.LastName).ToString();
            return tem;
            //return _context.TripDetails;
        }
        public TripEditModel GetOrderList()
        {
            //uncharged distinctinct list script
            //var trip = CollectTrips().Where(i => i.Total == null).Select(x => x).DistinctBy(u => u.ActualRef).ToList();

            //all distinct by Ref Number list
            var trip = CollectTrips().DistinctBy(u => u.ActualRef).ToList();
            //var trip = _context.TripDetails
            //   .Include(s => s.Sender)
            //   .Include(s => s.Recipient)//0540909153
            //   .Select(x => x)
            //   .ToList();
            TripEditModel tem = new TripEditModel();
            tem.AllTrips = trip;
            tem.RecipientsFullName = trip.Select(x => new SelectListItem
            {
                Text = x.Recipient.FirstName + " " + x.Recipient.LastName,
                Value = x.RecipientId.ToString()// x.FirstName + " " + x.LastName
            });
            tem.SendersFullName = trip.Select(x => new SelectListItem
            {
                Text = x.Sender.FirstName + " " + x.Sender.LastName,
                Value = x.SenderId.ToString()// x.FirstName + " " + x.LastName
            });
            tem.SelectedRecipientFullName = trip.Select(u => u.Recipient.FirstName + " " + u.Recipient.LastName).ToString();
            tem.SelectedSenderFullName = trip.Select(u => u.Sender.FirstName + " " + u.Sender.LastName).ToString();
            return tem;
            //return _context.TripDetails;
        }
        public TripEditModel GetAllUnChargedTrips()
        {
            //uncharged distinctinct list script
            var trip = CollectTrips().Where(i => i.Total == null).Select(x => x).DistinctBy(u => u.ActualRef).ToList();

            ////all distinct by Ref Number list
            //var trip = CollectTrips().DistinctBy(u => u.ActualRef).ToList();
            //var trip = _context.TripDetails
            //   .Include(s => s.Sender)
            //   .Include(s => s.Recipient)//0540909153
            //   .Select(x => x)
            //   .ToList();
            TripEditModel tem = new TripEditModel();
            tem.AllTrips = trip;
            tem.RecipientsFullName = trip.Select(x => new SelectListItem
            {
                Text = x.Recipient.FirstName + " " + x.Recipient.LastName,
                Value = x.RecipientId.ToString()// x.FirstName + " " + x.LastName
            });
            tem.SendersFullName = trip.Select(x => new SelectListItem
            {
                Text = x.Sender.FirstName + " " + x.Sender.LastName,
                Value = x.SenderId.ToString()// x.FirstName + " " + x.LastName
            });
            tem.SelectedRecipientFullName = trip.Select(u => u.Recipient.FirstName + " " + u.Recipient.LastName).ToString();
            tem.SelectedSenderFullName = trip.Select(u => u.Sender.FirstName + " " + u.Sender.LastName).ToString();
            return tem;
            //return _context.TripDetails;
        }
        public TripEditModel GetAllChargedTripsLoaded()
        {
            var trip = CollectTrips().Where(i => i.Total == null).Select(x => x).DistinctBy(u => u.ActualRef).ToList();
            //var trip = _context.TripDetails
            //   .Include(s => s.Sender)
            //   .Include(s => s.Recipient)//0540909153
            //   .Select(x => x)
            //   .ToList();
            TripEditModel tem = new TripEditModel();
            tem.AllTrips = trip;
            tem.RecipientsFullName = trip.Select(x => new SelectListItem
            {
                Text = x.Recipient.FirstName + " " + x.Recipient.LastName,
                Value = x.RecipientId.ToString()// x.FirstName + " " + x.LastName
            });
            tem.SendersFullName = trip.Select(x => new SelectListItem
            {
                Text = x.Sender.FirstName + " " + x.Sender.LastName,
                Value = x.SenderId.ToString()// x.FirstName + " " + x.LastName
            });
            tem.SelectedRecipientFullName = trip.Select(u => u.Recipient.FirstName + " " + u.Recipient.LastName).ToString();
            tem.SelectedSenderFullName = trip.Select(u => u.Sender.FirstName + " " + u.Sender.LastName).ToString();
            return tem;
            //return _context.TripDetails;
        }
        public List<TripDetails> CollectTrips()
        {
            List<TripDetails> trip = new List<TripDetails>();
            string msg = "";
            try
            {
                trip = _context.TripDetails
                               .Include(s => s.Sender)
                               .Include(s => s.Recipient)
                               .Select(x => x)
                               .ToList();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return trip;
        }
        public TripDetails GetTripsById(int tripId)
        {
            var trip = _context.TripDetails
                .Where(i => i.TripId == tripId)
               .Include(s => s.Sender)
               .Include(s => s.Recipient)
               .Select(x => x)
               .FirstOrDefault();
            return trip;
        }
        public IEnumerable<TripDetails> GetTripsByRefNo(string refnum)
        {
            AdminModel adm = new AdminModel();

            var trip = _context.TripDetails
                .Where(i => i.ActualRef == refnum)
               .Include(s => s.Sender)
               .Include(s => s.Recipient)
               .Select(x => x)
               .ToList();
            adm.TripList = trip;
            return trip;
        }
        public IEnumerable<TripDetails> GetAllClientOrdersByClientId(int clientId)
        {
            AdminModel adm = new AdminModel();

            var trip = _context.TripDetails
                .Where(i => i.SenderId == clientId)
               .Include(s => s.Sender)
               .Include(s => s.Recipient)
               .Select(x => x)
               .ToList();
            adm.TripList = trip;
            return trip;
        }

        public IEnumerable<TripDetails> GetItemToChargeByRefNo(string refnum)
        {
            string msg = "";
            // AdminModel adm = new AdminModel();
            List<TripDetails> trip = new List<TripDetails>();
            try
            {
                trip = _context.TripDetails
                               .Where(i => i.ActualRef == refnum)
                               .Include(s => s.Sender)
                               .Include(s => s.Recipient)
                               .Select(x => x)
                               .ToList();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            //adm.SenderID = trip.SenderId;
            //adm.RecipientID = trip.RecipientId;
            //adm.BatchID = trip.BatchId;
            //adm.TripList = trip;
            return trip;
        }
        public IEnumerable<SelectListItem> GetAllItems()
        {
            var allItems = _context.Items.OrderBy(o => o.Name);
            var allItemNames = allItems.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Name
            });
            return allItemNames;
        }

        public string ChargeCustomerOrders(string Refnumber, int GrandTotal, int BatchId)
        {
            string msg = "Failed adding cost";
            double cost = 0.00d;
            if (Refnumber != "" && GrandTotal != 0 && BatchId != 0)
            {

                Income inc = new Income();
                inc.BatchId = BatchId;
                inc.Amount = GrandTotal;
                inc.ActualRef = Refnumber;
                _context.Add(inc);
                _context.SaveChanges();

                //add grand total in trip table
                msg = "Customer Orders Charged Successfully";
                cost = (double)_context.Income.Where(u => u.ActualRef == Refnumber).Select(x => x.Amount).FirstOrDefault();
            }

            // _context.Income()
            return msg + "~" + cost;
        }
        public TripDetails UpdateTrip(TripDetails updateTrip)
        {
            var tripToUpdate = _context.TripDetails.Attach(updateTrip);
            tripToUpdate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            _context.TripDetails.ToList();
            return updateTrip;
        }
        public SendersCreateModel GetSenderSelectList()
        {
            SendersCreateModel ecv = new SendersCreateModel();
            ecv.SenderFullName = GetSenderFullNames();
            ecv.SenderTown = GetSenderTown();
            return ecv;
        }
        public string updateOrderWithCost(string refnumber, decimal grandTotal)
        {
            FinanceManagerDbContext _contextChk = new FinanceManagerDbContext();
            string msg = "";
            string orderStatus = "";
            var data = _context.TripDetails.Where(u => u.ActualRef == refnumber).Select(i => i);
            var checkTotal = data.Select(x => x.Total).FirstOrDefault();
            if (data != null)
            {
                if (checkTotal == null)
                {
                    foreach (TripDetails item in data)
                    {
                        item.Status = GetNewStatusFromOriginalStatus(item.Status);
                        item.Total = grandTotal;
                    }
                    _context.SaveChanges();
                    _context.TripDetails.ToList();
                }
                else
                {
                    foreach (TripDetails item in data)
                    {
                        item.Status = GetNewStatusFromOriginalStatus(item.Status);
                        item.Total = grandTotal;
                    }
                    _context.SaveChanges();
                    _context.TripDetails.ToList();
                }
                msg = "Order charges updated Successful";
            }
            return msg;
        }
        public string updateIncomeWithCost(string refnumber, decimal grandTotal)
        {
            string msg = "";
            var data = _context.Income.Where(u => u.ActualRef == refnumber).Select(i => i);
            foreach (Income item in data)
            {
                item.Amount = grandTotal;
                msg = "Order Charges updated Successful";
            }
            _context.SaveChanges();
            _context.Income.ToList();
            return msg;
        }
        public bool CheckOrderCharges(InvoiceOrders obj)
        {
            bool isCharged = false;
            int gTot = int.Parse(obj.grandTotal);
            int bchId = int.Parse(obj.batchId);
            var orderCharged = _context.Income.Where(u => u.ActualRef == obj.refNumber && u.Amount == gTot && u.BatchId == bchId).FirstOrDefault();
            if (orderCharged != null)
            {
                isCharged = true;
            }
            return isCharged;
        }
        public IEnumerable<SelectListItem> GetSenderFullNames()
        {
            var sendFullname = _context.Senders;
            var SenderFullNames = sendFullname.Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.SenderId.ToString()// x.FirstName + " " + x.LastName
            });
            return SenderFullNames;
        }
        public IEnumerable<SelectListItem> GetTripBatches()
        {
            var data = _context.Batch.OrderByDescending(u => u.BatchId);
            var tripBatch = data.Select(x => new SelectListItem
            {
                Text = x.ActualBatch,
                Value = x.BatchId.ToString()
            });

            return tripBatch;
        }
        public int GetCurrentBatch()
        {
            int currrentBatch = 0;
            //any Batch available
            //====================================
            var checkBatch = _context.Batch;
            if (checkBatch != null)
            {
                currrentBatch = checkBatch.Max(c => c.BatchId);
            }
            return currrentBatch;
        }
        public int GetNewRef()
        {
            int newRef = 0;
            var refNum = _context.TripDetails.Select(I => I.ActualRef).Distinct().Max();
            if (refNum != null)
            {
                var data = int.Parse(refNum);// u => u.ActualRef).ToList();//
                newRef = data + 1;
            }
            else
            {
                newRef = 8000;
            }
            return newRef;
        }
        public TripCreateModel GetTripInitialList()
        {
            TripCreateModel tcm = new TripCreateModel();
            tcm.SendersFullName = GetSenderFullNames();
            tcm.RecipientsFullName = GetRecipientFullNames();
            tcm.TripBatches = GetTripBatches();
            tcm.NewRef = GetNewRef();
            tcm.ShippingItemNames = GetAllItems();
            // tcm.SenderRecipientsFullName=GetSenderRecipients()
            return tcm;
        }
        public CustomerOrderModel GetInitialOrderParam()
        {
            CustomerOrderModel tcm = new CustomerOrderModel();
            //tcm.SendersFullName = GetSenderFullNames();
            //tcm.RecipientsFullName = GetRecipientFullNames();
            tcm.BatchId = GetCurrentBatch();
            tcm.ActualRef = GetNewRef().ToString();
            //tcm.ShippingItemNames = GetAllItems();
            // tcm.SenderRecipientsFullName=GetSenderRecipients()
            return tcm;
        }
        public IEnumerable<SelectListItem> GetRecipientFullNames()
        {
            var recipientData = _context.Recipients;
            var recipientsFullNames = recipientData.Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.RecipientId.ToString()// x.FirstName + " " + x.LastName
            });
            return recipientsFullNames;
        }
        public IEnumerable<SelectListItem> GetSenderRecipients(int senderId)
        {
            var sendersRecipientsFullName = _context.Recipients.Where(i => i.SenderId == senderId).Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.RecipientId.ToString()
            });
            return sendersRecipientsFullName;
        }

        public IEnumerable<SelectListItem> GetSenderTown()
        {
            var senderTown = _context.Ukcity;
            var senderTownList = senderTown.Select(x => new SelectListItem
            {
                Text = x.City,
                Value = x.City
            });
            return senderTownList;
        }
        public TripDetails DeleteTripItemById(TripDetails trip)
        {
            string msg = "";
            var tripToDelete = _context.TripDetails.Find(trip.TripId);
            if (tripToDelete != null)
            {
                _context.Remove(tripToDelete);
                _context.SaveChanges();
                msg = "Trip Deleted Successfully!!";
            }
            return tripToDelete;
        }
        public string DeleteOrderByRef(string refNum)
        {
            string msg = "Failed Deleting Order";
            var orderToDelete = _context.TripDetails.Where(u => u.ActualRef == refNum).ToList();
            if (orderToDelete != null)
            {
                foreach (var d in orderToDelete)
                {
                    _context.Remove(d);
                    _context.SaveChanges();
                }
                msg = "Deleted Successful";
            }
            return msg;
        }

        ////public LinkCreator checkLinkCreatorTable(string uniquestring)
        ////{
        ////    return  _context.LinkCreator.Where(u => u.UniqueString == uniquestring).FirstOrDefault(); ;
        ////}
        public IEnumerable<LinkCreator> checkLinkCreatorTable()
        {
            var data = _context.LinkCreator.Select(i => i).ToList();//.Where(u => u.UniqueString == uniquestring).FirstOrDefault(); ;
            return data;
        }
        public LinkCreator UpdateLinkCreatorTable(LinkCreator updateLink)
        {
            var linkToUpdate = _context.LinkCreator.Attach(updateLink);
            linkToUpdate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            //_context.TripDetails.ToList();
            return updateLink;
        }

        //************************************************************************ I will check this later
        //================================================================================================
        public string UpdateLinkCreatorTable(string hashString, int actualRef)
        {
            string msg = "";
            int newStatus = 0;
            LinkCreator data = new LinkCreator();
            if (hashString == "")
            {
                data = _context.LinkCreator.Where(u => u.RefNum == actualRef).Select(i => i).FirstOrDefault();
            }
            else
            {
                data = _context.LinkCreator.Where(u => u.UrlLink == hashString).Select(i => i).FirstOrDefault();
            }

            if (data != null)
            {
                switch (data.OrderStatus)
                {
                    case 0:
                        newStatus = 1;
                        break;
                    case 1:
                        newStatus = 2;
                        break;
                    case 2:
                        newStatus = 3;
                        break;
                    case 3:
                        newStatus = 4;
                        break;
                    case 4:
                        newStatus = 5;
                        break;
                    case 5:
                        newStatus = 6;
                        data.LinkStatus = 1;
                        break;
                    case 6:
                        newStatus = 7;
                        //data.LinkStatus = 1;
                        break;
                }
                data.OrderStatus = newStatus;
                data.RefNum = actualRef;
                data.Date = DateTime.Now;
                msg = "Order charges updated Successful";
                _context.SaveChanges();
            }
            else
            {

            }



            return msg;
        }
        public string EditLinkCreator(LinkCreator linkData)
        {
            var linkToUpdate = _context.LinkCreator.Attach(linkData);
            linkToUpdate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            _context.TripDetails.ToList();
            return "";
        }
        public void AddCreateCustomerLinkRecord(LinkCreator linkRecord)
        {
            _context.LinkCreator.Add(linkRecord);
            _context.SaveChanges();
        }
        public string GetOrderStatusByRefNumber(string RefNum)
        {
            string orderStatusStr = "";
            int referenceNumber = int.Parse(RefNum);
            var ostatus = _context.LinkCreator.Where(i => i.RefNum == referenceNumber).Select(x => x.OrderStatus).FirstOrDefault();
            double Cost = 0.00d;
            switch (ostatus)
            {
                case 0:
                    orderStatusStr = "Loaded";
                    break;
                case 1:
                    orderStatusStr = "Created";
                    break;
                case 2:
                    orderStatusStr = "Charged";
                    break;
                case 3:
                    orderStatusStr = "Picked";
                    break;
                case 4:
                    orderStatusStr = "Shipped";
                    break;
                case 5:
                    orderStatusStr = "Ghana";
                    break;
                case 6:
                    orderStatusStr = "Delivered";
                    break;
            }
            if (orderStatusStr != "Loaded" || orderStatusStr != "Created")
            {
                Cost = (double)_context.Income.Where(i => i.ActualRef == RefNum).Select(x => x.Amount).FirstOrDefault();
            }
            return orderStatusStr + "~" + Cost;
        }
        public TripAudit GetCurrentStatusByRefId(string actualRef)
        {
            FinanceManagerDbContext _contextAudit = new FinanceManagerDbContext();
            TripAudit trpAudit = new TripAudit();
            if (actualRef != "")
            {
                int refId = int.Parse(actualRef);
                var getStatusByRef = _contextAudit.TripAudit.Where(u => u.RefId == refId).Select(i => i).ToList();
                foreach (TripAudit audt in getStatusByRef)
                {
                    if (audt.Id == getStatusByRef.Max(d => d.Id))
                    {
                        trpAudit = audt;
                    }
                }
            }
            return trpAudit;
        }
        public string GetStatusNameByValue(int ostatus)
        {
            string orderStatusStr = "";
            switch (ostatus)
            {
                //case 0:
                //    orderStatusStr = "Loaded";
                //    break;
                case 1:
                    orderStatusStr = "Created";
                    break;
                case 2:
                    orderStatusStr = "Charged";
                    break;
                case 3:
                    orderStatusStr = "Picked";
                    break;
                case 4:
                    orderStatusStr = "Shipped";
                    break;
                case 5:
                    orderStatusStr = "Ghana";
                    break;
                case 6:
                    orderStatusStr = "Delivered";
                    break;
                case 7:
                    orderStatusStr = "Completed";
                    break;
            }
            return orderStatusStr;
        }
        public int GetStatusValueByName(string statusName)
        {
            int orderValue = 0;
            switch (statusName)
            {
                //case "Loaded":
                //    orderValue = 0;
                //    break;
                case "Created":
                    orderValue = 1;
                    break;
                case "Charged":
                    orderValue = 2;
                    break;
                case "Picked":
                    orderValue = 3;
                    break;
                case "Shipped":
                    orderValue = 4;
                    break;
                case "Ghana":
                    orderValue = 5;
                    break;
                case "Delivered":
                    orderValue = 6;
                    break;
                case "Completed":
                    orderValue = 7;
                    break;
            }
            return orderValue;
        }
        public string GetNewStatusFromOriginalStatus(string originalStatus)
        {
            string newStatus = "";
            switch (originalStatus)
            {
                case "Loaded":
                    newStatus = "Created";
                    break;
                case "Created":
                    newStatus = "Charged";
                    break;
                case "Charged":
                    newStatus = "Picked";
                    break;
                case "Picked":
                    newStatus = "Shipped";
                    break;
                case "Shipped":
                    newStatus = "Ghana";
                    break;
                case "Ghana":
                    newStatus = "Delivered";
                    break;
                case "Delivered":
                    newStatus = "Completed";
                    break;
            }
            return newStatus;
        }
        public string GetOriginalStatusFromNewStatus(string newStatus)
        {
            string originalStatus = "";
            switch (newStatus)
            {
                //case "Loaded":
                //    originalStatus = "Loaded";
                //    break;
                case "Created":
                    originalStatus = "Loaded";
                    break;
                case "Charged":
                    originalStatus = "Created";
                    break;
                case "Picked":
                    originalStatus = "Charged";
                    break;
                case "Shipped":
                    originalStatus = "Picked";
                    break;
                case "Ghana":
                    originalStatus = "Shipped";
                    break;
                case "Delivered":
                    originalStatus = "Ghana";
                    break;
                case "Completed":
                    originalStatus = "Delivered";
                    break;
            }
            return originalStatus;
        }
        public TripAudit getAuditParam(TripDetails td)
        {
            TripAudit aud = new TripAudit();
            //var currentStatus = _repository.GetCurrentStatusByRefId(td.ActualRef);

            aud.BatchId = td.BatchId;
            aud.SenderId = td.SenderId;
            aud.RecipientId = td.RecipientId;
            aud.RefId = int.Parse(td.ActualRef);
            aud.OrderDate = td.OrderDate;
            aud.NewStatus = GetStatusValueByName(td.Status);
            aud.OriginalStatus = GetStatusValueByName(GetOriginalStatusFromNewStatus(td.Status));
            //_context.TripAudit.Add(aud);
            //_context.SaveChanges();
            //if (currentStatus != null)
            //{
            //    aud.OriginalStatus = currentStatus.NewStatus;
            //}
            //else
            //{
            //    aud.OriginalStatus =int.Parse( td.Status;
            //}
            return aud;
        }
        public TripAudit AddTripToAudit(TripAudit tripAudit)
        {
            string msg = "";
            try
            {
                _context.TripAudit.Add(tripAudit);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return tripAudit;
        }
        public List<TripDetails> getAllDistinctOrders()
        {
              var  trip = _context.TripDetails
                               .Include(s => s.Sender)
                               .Include(s => s.Recipient)
                               .Select(x => x)
                               .ToList()
                               .DistinctBy(x => x.ActualRef)
                               .ToList();
            return trip;//_context.TripDetails.Select(x => x).ToList().DistinctBy(x => x.ActualRef).ToList();//.Distinct();
        }
        public List<TripDetails> getAllOrdersWithSenderRecipient()
        {
            var trip = _context.TripDetails
                             .Include(s => s.Sender)
                             .Include(s => s.Recipient)
                             .Select(x => x)
                             .ToList()
                             .ToList();
            return trip;//_context.TripDetails.Select(x => x).ToList().DistinctBy(x => x.ActualRef).ToList();//.Distinct();
        }
        public IEnumerable<OrderStatusModel> GetAllOrderStatusList()
        {
            OrderStatusModel odsm = new OrderStatusModel();
            var data = getAllOrdersWithSenderRecipient();
            var groupedData = data.GroupBy(x => x.ActualRef).Select(i => new OrderStatusModel
            {
                orderDate = DateTime.Parse(i.First().OrderDate.ToString()),
                RefNumber = i.First().ActualRef,
                SenderId=i.First().SenderId,
                Sender = i.First().Sender.FirstName + " " + i.First().Sender.LastName,
                recipientId=i.First().RecipientId,
                Recipient = i.First().Recipient.FirstName + " " + i.First().Recipient.LastName,
                Quantity = int.Parse(i.Sum(x => x.Quantity).ToString()),
                Total=i.First().Total,
                status = i.First().Status
            });

            return groupedData;
        }
        public List<TripDetails> getAllOrders()
        {
            return _context.TripDetails.Select(x => x).ToList();//.DistinctBy(x => x.ActualRef).ToList();//.Distinct();
        }
        public TripDetails UpdateOrderStatus(TripDetails orderstatus)
        {
            //var existingRecord = _context.TripDetails.Where(i => i.TripId == orderstatus.TripId).FirstOrDefault();
            var existingRecord = _context.TripDetails.Where(i => i.ActualRef == orderstatus.ActualRef).ToList();
            foreach (var item in existingRecord)
            {
                item.OrderDate = DateTime.Now;
                item.Status = orderstatus.Status;
                item.ActualRef = orderstatus.ActualRef;
            }
            //existingRecord.OrderDate = DateTime.Now;
            //existingRecord.Status = orderstatus.Status;
            //existingRecord.ActualRef = orderstatus.ActualRef;
            _context.SaveChanges();
            return orderstatus;
        }



        //private TripAudit UpdateAuditTable(TripAudit aud)
        //{
        //    return AddTripToAudit(aud);
        //}
        //public bool CheckRecipientHasTrip(string senderId)
        //{
        //    bool hasRecipient = false;
        //    if (senderId != null)
        //    {
        //        /*var recipient = _recRepository.GetSendersRecipients(senderId);*/// SenderRecipient(senderId);
        //        var recipient = GetSendersRecipients(senderId);
        //        if (recipient.AllRecipients != null)
        //        {
        //            foreach (var recep in recipient.AllRecipients)
        //            {
        //                DeleteRecipients(recep);
        //            }
        //            hasRecipient = true;
        //        }
        //    }
        //    return hasRecipient;
        //}
    }
}
