using FinanceManager.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Model.Abstract
{
    public interface IItemsRepository
    {
        IEnumerable<Items> GetAllShippingItems();
        Items AddShippingItem(Items shippingItem);
        Items GetShippingItemById(int id);
        Items UpdateShippingItem(Items shippingItem);
        Items DeleteItem(Items shippingItem);
    }
}
