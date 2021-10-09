using FinanceManager.Model.Abstract;
using FinanceManager.Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceManager.Repository
{
    public class SendersRepository : ISendersRepository
    {
        private FinanceManagerDbContext _context;
        private IRecipientsRepository _recRepository;
        private IShippersRepository _shipperRepository;

        public SendersRepository(FinanceManagerDbContext context, IRecipientsRepository recRepository, IShippersRepository shipperRepository)
        {
            _context = context;
            _recRepository = recRepository;
            _shipperRepository = shipperRepository;
        }
        public Senders AddSender(Senders sender)
        {
            _context.Add(sender);
            _context.SaveChanges();
            return sender;
        }

        public IEnumerable<Senders> GetAllSenders()
        {
            //string msg = "";
            //SendersCreateModel sd = new SendersCreateModel();
            //try
            //{
            //    sd.AllSenders = _context.Senders;
            //}
            //catch (Exception ex)
            //{
            //    msg = ex.Message;
            //}
            return _context.Senders;
        }

        public Senders GetSendersById(int id)
        {
            return _context.Senders.FirstOrDefault(x => x.SenderId == id);
        }
        public Senders GetSendersByEmail(string email)
        {
            string msg = "";
            Senders user = new Senders();
            RegexUtilities r = new RegexUtilities();
            try
            {
                if (r.IsValidEmail(email))
                {
                    user = _context.Senders.FirstOrDefault(x => x.EmailAddress == email);
                }
                else
                {
                    msg = "Invalid Email!";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return user;
        }
        public Senders UpdateSenders(Senders updateSenders)
        {
            var senderToUpdate = _context.Senders.Attach(updateSenders);
            senderToUpdate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return updateSenders;
        }
        public SendersCreateModel GetSenderSelectList()
        {
            SendersCreateModel ecv = new SendersCreateModel();
            ecv.SenderFullName = GetSenderFullNames();
            ecv.SenderTown = GetSenderTown();
            ecv.ShippersId = GetShipperId();
            return ecv;
        }
        public IEnumerable<SelectListItem> GetSenderFullNames()
        {
            var sendFullname = _context.Senders;
            var SenderFullNames = sendFullname.Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.FirstName + " " + x.LastName
            });
            return SenderFullNames;
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
        public Senders DeleteSenders(Senders sender)
        {
            bool hasRecipient = false;
            string senderID = sender.SenderId.ToString();
            //hasRecipient= CheckSenderRecipient(senderID);
            hasRecipient = _recRepository.CheckSenderRecipient(senderID);
            string msg = "";
            //if (!hasRecipient)
            //{
            //    //delete recipients before deleting sender.

            //}
            var senderToDelete = _context.Senders.Find(sender.SenderId);
            if (senderToDelete != null)
            {
                _context.Remove(senderToDelete);
                _context.SaveChanges();
                msg = "Sender Deleted Successfully!!";
            }
            return senderToDelete;
        }
        public bool ValidateRegistration(SendersCreateModel sender)
        {
            bool isRegistered = false;
            var checkIfSenderValid = _context.Senders.Where(u => u.FirstName.Trim().ToLower() == sender.FirstName.Trim().ToLower() &&
                                                            u.LastName.Trim().ToLower() == sender.LastName.Trim().ToLower() &&
                                                            u.Telephone.Trim().Replace(" ", "") == sender.Telephone.Trim().Replace(" ", "") &&
                                                            u.PostCode.Trim().ToLower().Replace(" ", "") == sender.PostCode.Trim().ToLower().Replace(" ", "")).FirstOrDefault();
            if (checkIfSenderValid !=null)
            {
                //CheckForOtherLinks(checkIfSenderValid.SenderId);
                isRegistered = true;
            }
            else
            {
                isRegistered = false;
            }
            return isRegistered;
        }

        private void CheckForOtherLinks(int senderId)
        {
            //check if sender has orders

            //check if sender has link

            //check if sender has a recipient
        }

        private bool CheckSenderRecipient(string senderId)
        {
            bool hasRecipient = false;
            if (senderId != null)
            {
                var recipient = _recRepository.GetSendersRecipients(senderId);// SenderRecipient(senderId);
                if (recipient.AllRecipients != null)
                {
                    foreach (var recep in recipient.AllRecipients)
                    {
                        _recRepository.DeleteRecipients(recep);
                    }
                    hasRecipient = true;
                }
            }
            return hasRecipient;
        }
        public int GetShipperId()
        {
            return _shipperRepository.GetShipperId();
        }
    }
}
