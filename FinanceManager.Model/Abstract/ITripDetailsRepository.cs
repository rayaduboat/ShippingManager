using FinanceManager.Model.DataTransferModel;
using FinanceManager.Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Model.Abstract
{
    public interface ITripDetailsRepository
    {
        TripDetails AddTrip(TripDetails trip);
        TripEditModel GetAllTrips();
        TripEditModel GetAllUnChargedTrips();
        TripEditModel GetOrderList();
        TripEditModel GetAllChargedTripsLoaded();
        TripDetails UpdateTrip(TripDetails updateTrip);
        SendersCreateModel GetSenderSelectList();
        IEnumerable<SelectListItem> GetSenderFullNames();
        IEnumerable<SelectListItem> GetRecipientFullNames();
        TripCreateModel GetTripInitialList();
        List<TripDetails> getAllDistinctOrders();
        List<TripDetails> getAllOrdersWithSenderRecipient();
        IEnumerable<OrderStatusModel> GetAllOrderStatusList();
        List<TripDetails> getAllOrders();
        IEnumerable<SelectListItem> GetAllItems();
        TripDetails GetTripsById(int id);
        IEnumerable<TripDetails> GetTripsByRefNo(string refnum);
        TripDetails DeleteTripItemById(TripDetails tripId);
        string DeleteOrderByRef(string refNum);
        IEnumerable<SelectListItem> GetSenderRecipients(int senderId);
        IEnumerable<TripDetails> GetItemToChargeByRefNo(string refnum);
        int GetNewRef();
        string ChargeCustomerOrders(string Refnumber, int GrandTotal, int BatchId);
        string updateOrderWithCost(string refnumber, decimal grandTotal);
        bool CheckOrderCharges(InvoiceOrders refnumber);
        string updateIncomeWithCost(string refnumber, decimal grandTotal);
        public int GetCurrentBatch();
        public CustomerOrderModel GetInitialOrderParam();
        //LinkCreator checkLinkCreatorTable(string actualString);
        IEnumerable<LinkCreator> checkLinkCreatorTable();
        void AddCreateCustomerLinkRecord(LinkCreator linkRecord);
        string UpdateLinkCreatorTable(string link, int refNum);//====I will check this later=====
        string EditLinkCreator(LinkCreator linkData);
        string GetOrderStatusByRefNumber(string RefNum);
        IEnumerable<TripDetails> GetAllClientOrdersByClientId(int clientId);
        TripAudit GetCurrentStatusByRefId(string actualRef);
        string GetStatusNameByValue(int ostatus);
        int GetStatusValueByName(string statusName);
        string GetNewStatusFromOriginalStatus(string originalStatus);
        public string GetOriginalStatusFromNewStatus(string newStatus);
        TripAudit getAuditParam(TripDetails td);
        TripAudit AddTripToAudit(TripAudit tripAudit);
        TripDetails UpdateOrderStatus(TripDetails orderstatus);


    }
}
