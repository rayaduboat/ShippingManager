using FinanceManager.Model.Abstract;
using FinanceManager.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceManager.Repository
{
    public class ItemsRepository: IItemsRepository
    {
        private FinanceManagerDbContext _context;

        public ItemsRepository(FinanceManagerDbContext context)
        {
            _context = context;
        }

        public Items AddShippingItem(Items shippingItem)
        {
            _context.Add(shippingItem);
            _context.SaveChanges();
            return shippingItem;
        }

        public IEnumerable<Items> GetAllShippingItems()
        {
            return _context.Items;
        }
        public Items GetShippingItemById(int id)
        {
            return _context.Items.Where(i => i.ItemId == id).Select(x => x).FirstOrDefault();
        }
        public Items UpdateShippingItem(Items updateItem)
        {
               var itemToUpdate = _context.Items.Attach(updateItem);
            itemToUpdate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return updateItem;
        }
        public Items DeleteItem(Items shippingItem)
        {
            string msg = "";
            string itemId = shippingItem.ItemId.ToString();
           
            var itemToDelete = _context.Items.Find(shippingItem.ItemId);
            if (itemToDelete != null)
            {
                _context.Remove(itemToDelete);
                _context.SaveChanges();
                msg = "shippingItem Deleted Successfully!!";
            }
            return itemToDelete;
        }
    }
}
