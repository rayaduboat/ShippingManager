using FinanceManager.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Model.Abstract
{
    public interface ILinCreatorTableRepository
    {
        public LinkCreator UpdateLinkCreator(LinkCreator linkData);
    }
}
