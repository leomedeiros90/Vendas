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
    public class TbProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TbProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TbProdutos
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbProduto.ToListAsync());
        }

        // GET: TbProdutos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProduto = await _context.TbProduto
                .FirstOrDefaultAsync(m => m.IdProduto == id);
            if (tbProduto == null)
            {
                return NotFound();
            }

            return View(tbProduto);
        }

        // GET: TbProdutos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbProdutos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduto,NomeProduto,DescricaoProduto,UnidadeProduto,PrecoCompraProduto,PrecoVendaProduto,EstoqueProduto,ImagemProduto")] TbProduto tbProduto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbProduto);
        }

        // GET: TbProdutos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProduto = await _context.TbProduto.FindAsync(id);
            if (tbProduto == null)
            {
                return NotFound();
            }
            return View(tbProduto);
        }

        // POST: TbProdutos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProduto,NomeProduto,DescricaoProduto,UnidadeProduto,PrecoCompraProduto,PrecoVendaProduto,EstoqueProduto,ImagemProduto")] TbProduto tbProduto)
        {
            if (id != tbProduto.IdProduto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbProduto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbProdutoExists(tbProduto.IdProduto))
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
            return View(tbProduto);
        }

        // GET: TbProdutos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProduto = await _context.TbProduto
                .FirstOrDefaultAsync(m => m.IdProduto == id);
            if (tbProduto == null)
            {
                return NotFound();
            }

            return View(tbProduto);
        }

        // POST: TbProdutos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbProduto = await _context.TbProduto.FindAsync(id);
            _context.TbProduto.Remove(tbProduto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbProdutoExists(int id)
        {
            return _context.TbProduto.Any(e => e.IdProduto == id);
        }
    }
}
