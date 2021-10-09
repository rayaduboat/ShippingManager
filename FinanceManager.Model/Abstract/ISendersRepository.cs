using FinanceManager.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Model.Abstract
{
    public interface ISendersRepository
    {
        Senders AddSender(Senders senders);
        IEnumerable<Senders> GetAllSenders();
        Senders UpdateSenders(Senders updateSenders);
        SendersCreateModel GetSenderSelectList();
        Senders GetSendersById(int id);
        Senders GetSendersByEmail(string email);
        Senders DeleteSenders(Senders senders);
        bool ValidateRegistration(SendersCreateModel sender);
        int GetShipperId();
    }
}
