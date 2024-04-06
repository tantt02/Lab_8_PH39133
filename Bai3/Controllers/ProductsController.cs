using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bai3.Models;
using Newtonsoft.Json;

namespace Bai3.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MyDb _context;

        public ProductsController(MyDb context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduct,Name,Status,Price,Quantity")] Products products)
        {
            if (ModelState.IsValid)
            {
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProduct,Name,Status,Price,Quantity")] Products products)
        {
            if (id != products.IdProduct)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.IdProduct))
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
            return View(products);
        }

        public async Task<IActionResult> Cart( int id)
        {
            var db = new MyDb();
            var prod = db.Products.FirstOrDefault(a => a.IdProduct == id);
            List<Carts> cart;
            if (HttpContext.Session.GetString("listCart") != null)
            {
                cart = JsonConvert.DeserializeObject<List<Carts>>(HttpContext.Session.GetString("listCart"));
            }
            else
            {
                cart = new List<Carts>();
                HttpContext.Session.SetString("listCart", JsonConvert.SerializeObject(cart));
            }
            if (prod != null)
            {
                var check = false;
                foreach (var item in cart)
                {
                    if (item.IdProuducts == id)
                    {
                        check = true;
                        item.Quantity += 1;
                        HttpContext.Session.SetString("listCart", JsonConvert.SerializeObject(cart));
                    }
                }

                if (!check)
                {
                    cart.Add(new Carts() { Quantity = 1, IdProuducts = id });
                    HttpContext.Session.SetString("listCart", JsonConvert.SerializeObject(cart));
                }
            }
                return View();
        }

            // GET: Products/Delete/5
            public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products != null)
            {
                _context.Products.Remove(products);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.IdProduct == id);
        }
    }
}
