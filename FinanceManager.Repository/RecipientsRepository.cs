using System;
using System.Collections.Generic;
using System.Text;
using FinanceManager.Model.Abstract;
using FinanceManager.Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
namespace FinanceManager.Repository
{
    public class RecipientsRepository : IRecipientsRepository
    {
        private FinanceManagerDbContext _context;

        public RecipientsRepository(FinanceManagerDbContext context)
        {
            _context = context;
        }
        public Recipients AddRecipient(Recipients recipient)
        {
            if (CheckSenderRecipientByDetails(recipient) == null)
            {
                _context.Add(recipient);
                _context.SaveChanges();
            }
            return recipient;
        }

        public IEnumerable<Recipients> GetAllRecipients()
        {
            return _context.Recipients;
        }

        public Recipients GetRecipientsById(int id)
        {
            return _context.Recipients.FirstOrDefault(x => x.RecipientId == id);
        }

        public Recipients UpdateRecipients(Recipients updateRecipients)
        {
            var recipientToUpdate = _context.Recipients.Attach(updateRecipients);
            recipientToUpdate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return updateRecipients;
        }
        public RecipientsCreateModel GetRecipientSelectList()
        {

            RecipientsCreateModel ecv = new RecipientsCreateModel();
            ecv.RecipientSender = GetSenderFullName();
            ecv.RecipientTown = GetRecipientTown();
            ecv.AllRecipients = _context.Recipients;
            return ecv;
        }
        public RecipientsCreateModel GetSendersRecipients(string sender)
        {
            RecipientsCreateModel ecv = new RecipientsCreateModel();
            try
            {
                int senderID = int.Parse(sender);
                var sendRecipients = _context.Recipients.Where(i => i.SenderId == senderID).ToList();
                ecv.RecipientSender = GetSenderFullName();
                ecv.RecipientTown = GetRecipientTown();
                ecv.AllRecipients = sendRecipients;
                ecv.SelectedSenderFullName = GetSenderFullNameById(senderID);
                
            }
            catch (Exception ex)
            {
                ecv.RecMsg="There was an error: " + ex.Message;
            }

            return ecv;
        }

        public string GetSenderFullNameById(int senderid)
        {
            var senderFullName = _context.Senders.Where(i => i.SenderId == senderid)
                .Select(x => x.FirstName + " " + x.LastName).FirstOrDefault().ToString();
            return senderFullName;
        }
        public IEnumerable<SelectListItem> GetSenderFullName()
        {
            var sendFullname = _context.Senders;
            var RecipientSender = sendFullname.Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.SenderId.ToString() //x.FirstName + " " + x.LastName
            });
            return RecipientSender;
        }
        public IEnumerable<Recipients> GetSenderRecipients(string senderFullName)
        {
            string firstname = senderFullName.Split(' ')[0];
            string lastname = senderFullName.Split(' ')[1];
            var senderID = _context.Senders.Where(x => x.FirstName.ToUpper() == firstname.ToUpper() &&
                x.LastName.ToUpper() == lastname.ToUpper()).Select(i => i.SenderId).FirstOrDefault();
            var senderRecipients = _context.Recipients.Where(u => u.SenderId == senderID).ToList();
            return senderRecipients;
        }

        public IEnumerable<SelectListItem> GetRecipientTown()
        {
            var recipientTown = _context.Ghcity;
            var recipientTownList = recipientTown.Select(x => new SelectListItem
            {
                Text = x.City,
                Value = x.City
            });
            return recipientTownList;
        }
        public Recipients DeleteRecipients(Recipients recipient)
        {
            string msg = "";
            var recipientToDelete = _context.Recipients.Find(recipient.RecipientId);
            if (recipientToDelete != null)
            {
                _context.Remove(recipientToDelete);
                _context.SaveChanges();
                msg = "Recipient Deleted Successfully!!";
            }
            return recipientToDelete;
        }
        public bool CheckSenderRecipient(string senderId)
        {
            bool hasRecipient = false;
            if (senderId != null)
            {
                /*var recipient = _recRepository.GetSendersRecipients(senderId);*/// SenderRecipient(senderId);
                var recipient = GetSendersRecipients(senderId);
                if (recipient.AllRecipients != null)
                {
                    foreach (var recep in recipient.AllRecipients)
                    {
                        DeleteRecipients(recep);
                    }
                    hasRecipient = true;
                }
            }
            return hasRecipient;
        }
        public Recipients CheckSenderRecipientByDetails(Recipients recipient)
        {
            var checkExistence = _context.Recipients.Where(i => i.FirstName.ToUpper() == recipient.FirstName.ToUpper() &&
                                                                i.Telephone == recipient.Telephone &&
                                                                i.LastName.ToUpper() == recipient.LastName.ToUpper()).Select(x => x).FirstOrDefault();
            return checkExistence;
        }
    }
}



