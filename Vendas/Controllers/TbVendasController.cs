using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vendas.Data;
using Vendas.Models;

namespace Vendas.Controllers
{
    [Authorize]
    public class TbVendasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TbVendasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TbVendas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TbVenda.Include(t => t.IdClienteNavigation).Include(t => t.IdProdutoNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TbVendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbVenda = await _context.TbVenda
                .Include(t => t.IdClienteNavigation)
                .Include(t => t.IdProdutoNavigation)
                .FirstOrDefaultAsync(m => m.IdVenda == id);
            if (tbVenda == null)
            {
                return NotFound();
            }

            return View(tbVenda);
        }

        // GET: TbVendas/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.TbCliente, "IdCliente", "NomeCliente");
            ViewData["IdProduto"] = new SelectList(_context.TbProduto, "IdProduto", "NomeProduto");
            return View();
        }

        // POST: TbVendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVenda,IdCliente,IdProduto,QuantidadeProduto,TotalVenda,StatusVenda,DataVenda")] TbVenda tbVenda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbVenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.TbCliente, "IdCliente", "NomeCliente", tbVenda.IdCliente);
            ViewData["IdProduto"] = new SelectList(_context.TbProduto, "IdProduto", "NomeProduto", tbVenda.IdProduto);
            return View(tbVenda);
        }

        // GET: TbVendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbVenda = await _context.TbVenda.FindAsync(id);
            if (tbVenda == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.TbCliente, "IdCliente", "NomeCliente", tbVenda.IdCliente);
            ViewData["IdProduto"] = new SelectList(_context.TbProduto, "IdProduto", "NomeProduto", tbVenda.IdProduto);
            return View(tbVenda);
        }

        // POST: TbVendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVenda,IdCliente,IdProduto,QuantidadeProduto,TotalVenda,StatusVenda,DataVenda")] TbVenda tbVenda)
        {
            if (id != tbVenda.IdVenda)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbVenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbVendaExists(tbVenda.IdVenda))
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
            ViewData["IdCliente"] = new SelectList(_context.TbCliente, "IdCliente", "NomeCliente", tbVenda.IdCliente);
            ViewData["IdProduto"] = new SelectList(_context.TbProduto, "IdProduto", "NomeProduto", tbVenda.IdProduto);
            return View(tbVenda);
        }

        // GET: TbVendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbVenda = await _context.TbVenda
                .Include(t => t.IdClienteNavigation)
                .Include(t => t.IdProdutoNavigation)
                .FirstOrDefaultAsync(m => m.IdVenda == id);
            if (tbVenda == null)
            {
                return NotFound();
            }

            return View(tbVenda);
        }

        // POST: TbVendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbVenda = await _context.TbVenda.FindAsync(id);
            _context.TbVenda.Remove(tbVenda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbVendaExists(int id)
        {
            return _context.TbVenda.Any(e => e.IdVenda == id);
        }
    }
}
