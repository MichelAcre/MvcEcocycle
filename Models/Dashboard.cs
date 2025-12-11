using System;
using System.Collections.Generic;

namespace MvcEcocycle.Models
{
    public class Dashboard
    {
        public int TotalColaboradores { get; set; }
        public int TotalMovimentacoes { get; set; }
        public int QuantidadeTotalMovimentada { get; set; }
        public double MediaMovimentacaoPorColaborador { get; set; }

        public List<Colaboradores> Colaboradores { get; set; } = new List<Colaboradores>();
        public List<Movimentacoes> Movimentacoes { get; set; } = new List<Movimentacoes>();
        public List<MovimentacaoPorDia> MovimentacoesPorDia { get; set; } = new List<MovimentacaoPorDia>();
        public List<ColaboradorComMovimentacao> ColaboradoresComMaiorMovimentacao { get; set; } = new List<ColaboradorComMovimentacao>();
        public List<Movimentacoes> UltimasMovimentacoes { get; set; } = new List<Movimentacoes>();
    }

    /// <summary>
    /// Classe auxiliar para agrupar movimentações por dia
    /// </summary>
    public class MovimentacaoPorDia
    {
        public string Data { get; set; }
        public int Quantidade { get; set; }
        public int QuantidadeMovimentada { get; set; }
    }

    /// <summary>
    /// Classe auxiliar para exibir colaboradores com movimentação
    /// </summary>
    public class ColaboradorComMovimentacao
    {
        public string Nome { get; set; }
        public int QuantidadeMovimentacoes { get; set; }
        public int QuantidadeTotal { get; set; }
        public int QuantidadeAtual { get; set; }
    }
}
