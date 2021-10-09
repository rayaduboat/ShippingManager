using FinanceManager.Model.Abstract;
using FinanceManager.Model.Models;
using FinanceManager.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabantFinanceManager.Controllers
{
    public class TripListInContainerController : Controller
    {
        private FinanceManagerDbContext _context;
        private ITripDetailsRepository _trip;

        public TripListInContainerController(FinanceManagerDbContext context, ITripDetailsRepository trip)
        {
            _context = context;
        }
        // GET: TripListInContainer
        public ActionResult TripList(int batchId)
        {
            TripListInContainerModel data = new TripListInContainerModel();
            var getAlBatches = _context.Batch.ToList();

            string msg = "";
            if (batchId != 0)
            {
                //Clear selected from batch
                var Nullify = getAlBatches.SingleOrDefault(u => u.IsSelected == true);
                if (Nullify != null)
                {
                    Nullify.IsSelected = null;
                    _context.SaveChanges();
                }

                //Now set the currect selected
                var setSelected = getAlBatches.SingleOrDefault(u => u.BatchId == batchId);
                setSelected.IsSelected = true;
                _context.SaveChanges();

                var list = _context.TripDetails.Where(i => i.BatchId == batchId)
                                       .Include(s => s.Sender)
                                       .Include(s => s.Recipient)
                                       .Include(s => s.Batch)
                                       .Select(x => x)
                                       .ToList();
                data.tripdata = list;
                //data.BatchList = _context.Batch.Select(i => new SelectListItem//.OrderByDescending(u => u.BatchId)
                data.BatchList= getAlBatches.Select(i => new SelectListItem
                {
                    Text = i.ActualBatch,
                    Value = i.BatchId.ToString(),
                    Selected = i.IsSelected.HasValue ? i.IsSelected.Value : false
                });
            }
            else
            {
                data.BatchList = _context.Batch.OrderByDescending(u => u.BatchId).Select(i => new SelectListItem
                {
                    Text = i.ActualBatch,
                    Value = i.BatchId.ToString()
                });
                
            }
            if (data.tripdata == null || data.tripdata.Count==0)
            {
                ViewBag.error = "There is no data Available for this Batch";
            }
            else
            {
                ViewBag.error = "";
            }
            return View(data);
        }

        public ActionResult TripListByBatch(int batchId)
        {
            string msg = "";
            //if (batchId != 0)
            //{
                TripListInContainerModel data = new TripListInContainerModel();
                var list = _context.TripDetails.Where(i => i.BatchId == batchId)
                                       .Include(s => s.Sender)
                                       .Include(s => s.Recipient)
                                       .Include(s => s.Batch)
                                       .Select(x => x)
                                       .ToList();
                data.tripdata = list;
                data.BatchList = _context.Batch.Select(i => new SelectListItem//.OrderByDescending(u => u.BatchId)
                {
                    Text = i.ActualBatch,
                    Value = i.BatchId.ToString()
                });
                //return RedirectToAction("TripList", data);
            //}

            return View(data);
        }

        public ActionResult GHListView()
        {
            string msg = "";
            //if (batchId != 0)
            //{
            TripListInContainerModel data = new TripListInContainerModel();
            var list = _context.TripDetails//Where(i => i.BatchId == batchId)
                                   .Include(s => s.Sender)
                                   .Include(s => s.Recipient)
                                   .Include(s => s.Batch)
                                   .Select(x => x)
                                   .ToList();
            data.tripdata = list;
            data.BatchList = _context.Batch.Select(i => new SelectListItem//.OrderByDescending(u => u.BatchId)
            {
                Text = i.ActualBatch,
                Value = i.BatchId.ToString()
            });
            //return RedirectToAction("TripList", data);
            //}

            return View(data);
        }
        // GET: TripListInContainer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TripListInContainer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TripListInContainer/Create
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

        // GET: TripListInContainer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TripListInContainer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: TripListInContainer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TripListInContainer/Delete/5
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
    }
}
