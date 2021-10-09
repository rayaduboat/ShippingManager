using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Model.Models
{
    public class TripDetailsModel
    {
 
            public int ExpReportID { get; set; }
            public Nullable<int> Batch { get; set; }
            //public string Name { get; set; }
            public string Description { get; set; }
            public string ModeOfPayment { get; set; }
            public string ExpenseType { get; set; }
            //public string ActualBatch { get; set; }
            public Nullable<decimal> Amount { get; set; }
            public System.DateTime CreatedDateEX { get; set; }

            public int BatchID { get; set; }
            public int TripID { get; set; }
            public string Title { get; set; }
            public Nullable<DateTime> CreatedDate { get; set; }
            public string ActualBatch { get; set; }
            public string ActualRef { get; set; }
            public int ItemID { get; set; }
            public int RecipientID { get; set; }
            public int BatchNo { get; set; }
            public string Name { get; set; }
            public string ItemName { get; set; }
            public string TheSender { get; set; }
            public string sendAddress { get; set; }
            public string TheRecipient { get; set; }
            public string SendTel { get; set; }
            public string RecTel { get; set; }
            public string Telephone { get; set; }
            public string SendTown { get; set; }
            public string RecTown { get; set; }
            public string Comment { get; set; }
            public string Status { get; set; }
            public Nullable<int> Quantity { get; set; }
            public Nullable<decimal> UnitCost { get; set; }
            public Nullable<decimal> Discount { get; set; }
            public Nullable<decimal> Total { get; set; }
            public Nullable<decimal> GrandTotal { get; set; }
            public int SenderID { get; set; }
            //public virtual Batch Batch { get; set; }
            public virtual Items Item { get; set; }
            public virtual Recipients Recipient { get; set; }
            public IEnumerable<TripDetails> trips { get; set; }
      
    }
}
