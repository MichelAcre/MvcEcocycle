using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcEcocycle.Data;
using MvcEcocycle.Models;

namespace MvcEcocycle.Controllers
{
    public class MovimentacoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovimentacoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movimentacoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Movimentacoes.Include(m => m.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Movimentacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacoes = await _context.Movimentacoes
                .Include(m => m.Usuario)
                .FirstOrDefaultAsync(m => m.MovimentacoesId == id);
            if (movimentacoes == null)
            {
                return NotFound();
            }

            return View(movimentacoes);
        }

        // GET: Movimentacoes/Create
        public IActionResult Create()
        {
            // CORRIGIDO: Mudar "Nome" para "UserName" na tabela _context.Users
            // IdentityUser (base de _context.Users) usa UserName para o nome de login.
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "UserName");


            ViewData["ColaboradoresId"] = new SelectList(_context.Colaboradores, "ColaboradoresId", "Nome");

            return View();
        }

        // POST: Movimentacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovimentacoesId,ColaboradoresId,Qtdmovimentada,Data,UsuarioId")] Movimentacoes movimentacoes)
        {
            // TODO: Adicione aqui a lógica de setar o UsuarioId da Movimentação se for necessário.

            // 1. VERIFICAR A VALIDADE DO MODELO (TUDO DEVE ESTAR AQUI DENTRO)
            if (ModelState.IsValid)
            {
                // 2. LOCALIZAR O COLABORADOR AFETADO
                var colaborador = await _context.Colaboradores
                    .FirstOrDefaultAsync(c => c.ColaboradoresId == movimentacoes.ColaboradoresId);

                if (colaborador != null)
                {
                    // 3. ATUALIZAR O CAMPO QTD
                    // Se Qtd for int? (nullable), use o operador ?? 0 para tratar o null.
                    colaborador.Qtd = (colaborador.Qtd ?? 0) + movimentacoes.Qtdmovimentada;

                    // O EF Core rastreia a alteração no objeto 'colaborador'.
                }
                else
                {
                    // Opcional: Adicionar um erro de modelo se o colaborador não for encontrado
                    ModelState.AddModelError("", "O colaborador selecionado não foi encontrado.");
                    // Se isso ocorrer, o código vai para o bloco final "return View(movimentacoes)"
                }

                // 4. ADICIONAR E SALVAR (Movimentação e Atualização do Colaborador)
                _context.Add(movimentacoes);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
                ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", movimentacoes.UsuarioId);
                return View(movimentacoes);
            }

        // GET: Movimentacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacoes = await _context.Movimentacoes.FindAsync(id);
            if (movimentacoes == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", movimentacoes.UsuarioId);
            return View(movimentacoes);
        }

        // POST: Movimentacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovimentacoesId,ColaboradoresId,Qtdmovimentada,Data,UsuarioId")] Movimentacoes movimentacoes)
        {
            if (id != movimentacoes.MovimentacoesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimentacoes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimentacoesExists(movimentacoes.MovimentacoesId))
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
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", movimentacoes.UsuarioId);
            return View(movimentacoes);
        }

        // GET: Movimentacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacoes = await _context.Movimentacoes
                .Include(m => m.Usuario)
                .FirstOrDefaultAsync(m => m.MovimentacoesId == id);
            if (movimentacoes == null)
            {
                return NotFound();
            }

            return View(movimentacoes);
        }

        // POST: Movimentacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimentacoes = await _context.Movimentacoes.FindAsync(id);
            if (movimentacoes != null)
            {
                _context.Movimentacoes.Remove(movimentacoes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimentacoesExists(int id)
        {
            return _context.Movimentacoes.Any(e => e.MovimentacoesId == id);
        }
    }
}
