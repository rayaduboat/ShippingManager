using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinanceManager.Model.Models;
using FinanceManager.Repository;
using System.Security.Cryptography.X509Certificates;
using FinanceManager.Model.Abstract;

namespace RabantFinanceManager.Controllers
{
    public class LinkCreatorsController : Controller
    {
        private readonly FinanceManagerDbContext _context;
        private readonly ILinCreatorTableRepository _repository;

        public LinkCreatorsController(FinanceManagerDbContext context, ILinCreatorTableRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET: LinkCreators
        public IActionResult Index()
        {
            List<OrderStatusModel> osms = new List<OrderStatusModel>();
            string orderStatusStr = "";
            string linkStatusStr = "";
            //OrderStatusModel dataOderStatus = new OrderStatusModel();
            
            var result = _context.LinkCreator.Select(x => x);
            foreach (LinkCreator l in result)
            {
                OrderStatusModel osm = new OrderStatusModel();
                switch (l.OrderStatus)
                {
                    case 0:
                        orderStatusStr = "Loaded";
                        break;
                    case 1:
                        orderStatusStr = "Created";
                        break;                   
                    case 2:                      
                        orderStatusStr = "Charged";
                        break;                   
                    case 3:                      
                        orderStatusStr = "Picked";
                        break;                   
                    case 4:                      
                        orderStatusStr = "Shipped";
                        break;                   
                    case 5:                      
                        orderStatusStr = "In Ghana";
                        break;                   
                    case 6:                      
                        orderStatusStr = "Delivered";
                        break;
                    default:
                        break;
                }
                switch (l.LinkStatus)
                {
                    case 0:
                        linkStatusStr = "Opened";
                        break;
                    case 1:
                        linkStatusStr = "Closed";
                        break;
                    default:
                        break;
                }
                osm.Id = l.Id;
                osm.oStatus = orderStatusStr;
                osm.lStatus = linkStatusStr;
                osm.UrlLink = l.UrlLink;
                osm.RefNum = l.RefNum;
                osm.SenderId = l.SenderId;
                osm.UniqueString = l.UniqueString;
                osms.Add(osm);
            }
            IEnumerable<OrderStatusModel> myData = osms;
            return View(myData);
        }

        // GET: LinkCreators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linkCreator = await _context.LinkCreator
                .FirstOrDefaultAsync(m => m.Id == id);
            if (linkCreator == null)
            {
                return NotFound();
            }

            return View(linkCreator);
        }

        // GET: LinkCreators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LinkCreators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,SenderId,BatchId,RefNum,UrlLink,UniqueString,OrderStatus,LinkStatus")] LinkCreator linkCreator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(linkCreator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(linkCreator);
        }

        // GET: LinkCreators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linkCreator = await _context.LinkCreator.FindAsync(id);
            if (linkCreator == null)
            {
                return NotFound();
            }
            return View(linkCreator);
        }

        // POST: LinkCreators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, LinkCreator linkCreator)
        {
            if (id != linkCreator.Id)
            {
                return NotFound();
            }
            

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.UpdateLinkCreator(linkCreator);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LinkCreatorExists(linkCreator.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(linkCreator);
        }

        // GET: LinkCreators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linkCreator = await _context.LinkCreator
                .FirstOrDefaultAsync(m => m.Id == id);
            if (linkCreator == null)
            {
                return NotFound();
            }

            return View(linkCreator);
        }

        // POST: LinkCreators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var linkCreator = await _context.LinkCreator.FindAsync(id);
            _context.LinkCreator.Remove(linkCreator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LinkCreatorExists(int id)
        {
            return _context.LinkCreator.Any(e => e.Id == id);
        }
    }
}
