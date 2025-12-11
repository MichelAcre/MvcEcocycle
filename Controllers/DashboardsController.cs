using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcEcocycle.Data;
using MvcEcocycle.Models;

namespace MvcEcocycle.Controllers
{
    [Authorize]
    public class DashboardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            // Carrega todos os colaboradores e movimentações
            var colaboradores = await _context.Colaboradores.ToListAsync();
            var movimentacoes = await _context.Movimentacoes
                .Include(m => m.Colaboradores)
                .Include(m => m.Usuario)
                .ToListAsync();

            // Cria um Dashboard com os dados
            var viewModel = new Dashboard
            {
                Colaboradores = colaboradores,
                Movimentacoes = movimentacoes,
                TotalColaboradores = colaboradores.Count,
                TotalMovimentacoes = movimentacoes.Count,
                QuantidadeTotalMovimentada = movimentacoes.Sum(m => m.Qtdmovimentada),
                MediaMovimentacaoPorColaborador = colaboradores.Count > 0
                    ? Math.Round((double)movimentacoes.Sum(m => m.Qtdmovimentada) / colaboradores.Count, 2)
                    : 0,
                MovimentacoesPorDia = AgruparMovimentacoesPorDia(movimentacoes),
                ColaboradoresComMaiorMovimentacao = ObterColaboradoresComMaiorMovimentacao(colaboradores, movimentacoes),
                UltimasMovimentacoes = movimentacoes.OrderByDescending(m => m.Data).Take(10).ToList()
            };

            return View(viewModel);
        }

        /// <summary>
        /// Agrupa movimentações por dia
        /// </summary>
        private List<MovimentacaoPorDia> AgruparMovimentacoesPorDia(List<Movimentacoes> movimentacoes)
        {
            return movimentacoes
                .GroupBy(m => m.Data.Date)
                .OrderBy(g => g.Key)
                .Select(g => new MovimentacaoPorDia
                {
                    Data = g.Key.ToString("dd/MM/yyyy"),
                    Quantidade = g.Count(),
                    QuantidadeMovimentada = g.Sum(m => m.Qtdmovimentada)
                })
                .ToList();
        }

        /// <summary>
        /// Obtém colaboradores com maior movimentação
        /// </summary>
        private List<ColaboradorComMovimentacao> ObterColaboradoresComMaiorMovimentacao(
            List<Colaboradores> colaboradores,
            List<Movimentacoes> movimentacoes)
        {
            return colaboradores
                .Select(c => new ColaboradorComMovimentacao
                {
                    Nome = c.Nome,
                    QuantidadeMovimentacoes = movimentacoes.Count(m => m.ColaboradoresId == c.ColaboradoresId),
                    QuantidadeTotal = movimentacoes
                        .Where(m => m.ColaboradoresId == c.ColaboradoresId)
                        .Sum(m => m.Qtdmovimentada),
                    QuantidadeAtual = c.Qtd ?? 0
                })
                .OrderByDescending(c => c.QuantidadeTotal)
                .Take(5)
                .ToList();
        }
    }
}
