using FinanceManager.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Model.Abstract
{
    public interface IRecipientsRepository
    {
        Recipients AddRecipient(Recipients recipients);
        IEnumerable<Recipients> GetAllRecipients();
        IEnumerable<Recipients> GetSenderRecipients(string senderFullName);
        Recipients UpdateRecipients(Recipients updateRecipients);
        RecipientsCreateModel GetRecipientSelectList();
        RecipientsCreateModel GetSendersRecipients(string sender);
        Recipients GetRecipientsById(int id);
        Recipients DeleteRecipients(Recipients recipients);
        bool CheckSenderRecipient(string senderId);
        Recipients CheckSenderRecipientByDetails(Recipients recipience);
    }
}
