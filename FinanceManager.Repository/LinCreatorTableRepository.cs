using FinanceManager.Model.Abstract;
using FinanceManager.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceManager.Repository
{
    public class LinCreatorTableRepository: ILinCreatorTableRepository
    {
        private FinanceManagerDbContext _context;

        public LinCreatorTableRepository(FinanceManagerDbContext context)
        {
            _context = context;
        }
        public LinkCreator UpdateLinkCreator(LinkCreator linkData)
        {
            //LinkCreator lnk = new LinkCreator();
            var existingRecord = _context.LinkCreator.Where(i => i.Id == linkData.Id).FirstOrDefault();
            existingRecord.Date = DateTime.Now;
            existingRecord.OrderStatus = linkData.OrderStatus;
            existingRecord.LinkStatus = linkData.LinkStatus;
            //existingRecord.Id = id;
            //existingRecord.SenderId = existingRecord.SenderId;
            //existingRecord.RefNum = existingRecord.RefNum;
            //existingRecord.UniqueString = existingRecord.UniqueString;
            //existingRecord.UrlLink = existingRecord.UrlLink;
            //existingRecord.BatchId = existingRecord.BatchId;

            //var linkToUpdate = _context.LinkCreator.Attach(linkData);
            //linkToUpdate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return linkData;
        }
    }
}
