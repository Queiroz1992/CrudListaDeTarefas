using ListaDeTarefas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Application.Interfaces
{
    public interface ITarefaService
    {
        Task<IEnumerable<Tarefa>> GetTodasTarefasAsync();
        Task<Tarefa> GetTarefaPorIdAsync(int id);
        Task<IEnumerable<Tarefa>> GetTarefasPorStatusAsync(string status);
        Task<IEnumerable<Tarefa>> GetTarefaPorPrioridadeAsync(string prioridade);
        Task <Tarefa> CreateTarefaAsync(Tarefa tarefa);
        Task <Tarefa> UpdateTarefaAsync(int id, Tarefa tarefa);
        Task <bool> DeleteTarefaAsync(int id);
    }
}
