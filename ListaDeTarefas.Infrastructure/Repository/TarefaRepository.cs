using ListaDeTarefas.Application.Interfaces;
using ListaDeTarefas.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Infrastructure.Repository
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly TarefaDbContext _context;
        public TarefaRepository(TarefaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tarefa>> GetTodasTarefasAsync()
        {
            return await _context.Tarefas.Include(t => t.SubTarefas).ToListAsync();
        }

        public async Task<Tarefa> GetTarefaPorIdAsync(int id)
        {
            return await _context.Tarefas.Include(t => t.SubTarefas).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tarefa>> GetTarefasPorPrioridadeAsync(string prioridade)
        {
            return await _context.Tarefas.Include(t => t.SubTarefas).Where(t => t.Prioridade == prioridade).ToListAsync();
        }

        public async Task<IEnumerable<Tarefa>> GetTarefasPorStatusAsync(string status)
        {
            return await _context.Tarefas.Include(t => t.SubTarefas).Where(t => t.Status == status).ToListAsync();
        }

        public async Task<Tarefa> CreateTarefaAsync(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
            return tarefa;
        }

        public async Task<Tarefa> UpdateTarefaAsync(Tarefa tarefaAtualizada)
        {
            var tarefaExistente = await _context.Tarefas.Include(t => t.SubTarefas).FirstOrDefaultAsync(t => t.Id == tarefaAtualizada.Id);

            if (tarefaExistente == null)
                return null;

            // Preservar a data de criação
            tarefaAtualizada.DataCriacao = tarefaExistente.DataCriacao;

            // Verificar se o status "concluída" e todas as subtarefas estão concluídas
            if (tarefaAtualizada.Status == "concluída" && tarefaExistente.SubTarefas.Any(st => st.Status != "concluída"))
            {
                throw new InvalidOperationException("Todas as subtarefas devem estar concluídas antes de marcar a tarefa como concluída.");
            }

            // Verificar se o status "em progresso" e se há pelo menos uma subtarefa
            if (tarefaAtualizada.Status == "em progresso" && (tarefaExistente.SubTarefas == null || !tarefaExistente.SubTarefas.Any()))
            {
                throw new InvalidOperationException("Uma tarefa só pode ser marcada como 'em progresso' se tiver pelo menos uma subtarefa.");
            }

            if (tarefaAtualizada.Status == "concluída")
            {
                tarefaAtualizada.DataConclusao = DateTime.UtcNow;
            }

            _context.Entry(tarefaExistente).CurrentValues.SetValues(tarefaAtualizada);

            // Preservar a data de criação na entidade rastreada
            _context.Entry(tarefaExistente).Property(t => t.DataCriacao).IsModified = false;

            await _context.SaveChangesAsync();
            return tarefaExistente;
        }

        public async Task<bool> DeleteTarefaAsync(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
                return false;

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
            return true;

        }

    }
}
