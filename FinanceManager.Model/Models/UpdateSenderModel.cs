using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Model.Models
{
    public class UpdateSenderModel
    {
        public string senderid { get; set; }
        public string title              {get;set;}
        public string gender             {get;set;}
        public string firstname          {get;set;}
        public string lastname           {get;set;}
        public string addresslineone     {get;set;}
        public string addresslinetwo     {get;set;}
        public string postcode           {get;set;}
        public string posttown           {get;set;}
        public string country            {get;set;}
        public string telephone          {get;set;}
        public string emailaddress { get; set; }
    }
}
