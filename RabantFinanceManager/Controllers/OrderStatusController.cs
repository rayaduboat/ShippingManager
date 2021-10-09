using FinanceManager.Model.Abstract;
using FinanceManager.Model.Models;
using FinanceManager.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabantFinanceManager.Controllers
{
    public class OrderStatusController : Controller
    {
        private ITripDetailsRepository _repository;
        private FinanceManagerDbContext _context;

        public OrderStatusController(FinanceManagerDbContext context, ITripDetailsRepository repository  )
        {
            _repository = repository;
            _context = context;

        }
        // GET: OrderStatusController
        public ActionResult Index()
        {
            var data =_repository.GetAllOrderStatusList();
            //OrderStatusModel odsm = new OrderStatusModel();
            //var data = _repository.getAllDistinctOrders();
            //var groupedData = data.GroupBy(x => x.ActualRef).Select(i => new OrderStatusModel
            //{
            //    orderDate = DateTime.Parse(i.First().OrderDate.ToString()),
            //    RefNumber = i.First().ActualRef,
            //    Sender = i.First().Sender.FirstName + " " + i.First().Sender.LastName,
            //    Recipient = i.First().Recipient.FirstName + " " + i.First().Recipient.LastName,
            //    Quantity = int.Parse(i.Sum(x => x.Quantity).ToString()),
            //    status=i.First().Status
            //}) ;
            //// var data = _repository.getAllOrders();
            return View(data);
        }
        //public IEnumerable<OrderStatusModel> GetAllOrderStatusList()
        //{
        //    OrderStatusModel odsm = new OrderStatusModel();
        //    var data = _repository.getAllOrdersWithSenderRecipient();
        //    var groupedData = data.GroupBy(x => x.ActualRef).Select(i => new OrderStatusModel
        //    {
        //        orderDate = DateTime.Parse(i.First().OrderDate.ToString()),
        //        RefNumber = i.First().ActualRef,
        //        Sender = i.First().Sender.FirstName + " " + i.First().Sender.LastName,
        //        Recipient = i.First().Recipient.FirstName + " " + i.First().Recipient.LastName,
        //        Quantity = int.Parse(i.Sum(x => x.Quantity).ToString()),
        //        status = i.First().Status
        //    });

        //    return groupedData;
        //}
        // GET: OrderStatusController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderStatusController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderStatusController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderStatusController/Edit/5
        public ActionResult Edit(int id)
        {
            string actualref = id.ToString();
            ////if (id == 0)
            ////{
            ////    return NotFound();
            ////}

            //var orderstatus = _context.TripDetails.Find(id);

            var orderstatus = _context.TripDetails.Where(i => i.ActualRef == actualref).FirstOrDefault();
            

            ////if (orderstatus == null)
            ////{
            ////    return NotFound();
            ////}
            return View(orderstatus);
        }

        // POST: OrderStatusController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TripDetails orderstatus)
        {
            ////if (id != orderstatus.TripId)
            ////{
            ////    return NotFound();
            ////}


            if (ModelState.IsValid)
            {
                try
                {
                    OrderStatusModel osm = new OrderStatusModel();
                    //update tripDetails table (orders) with new status 
                    var Data = _context.TripDetails.Where(i => i.ActualRef == orderstatus.ActualRef).ToList();
                    var getOtherTripValues = Data.Select(u => u).FirstOrDefault();
                    var orderStatusData= _repository.UpdateOrderStatus(orderstatus);
                    //osm.orderDate = DateTime.Parse(orderStatusData.OrderDate.ToString());
                    //osm.RefNumber = orderStatusData.ActualRef;
                    //osm.BatchId = orderStatusData.BatchId;
                    //osm.Quantity = Data.Select(x => x.Quantity).ToList().Sum(a => a.Value);
                    //Update Audit table with new status
                    _repository.AddTripToAudit(_repository.getAuditParam(getOtherTripValues));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderStatusExists(orderstatus.TripId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index",_repository.GetAllOrderStatusList());   //(nameof(Index));
            }
            return View(orderstatus);
        }

        // GET: OrderStatusController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderStatusController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private bool OrderStatusExists(int id)
        {
            return _context.TripDetails.Any(e => e.TripId == id);
        }
    }
}
