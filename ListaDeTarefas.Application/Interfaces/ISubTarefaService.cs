using ListaDeTarefas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Application.Interfaces
{
    public interface ISubTarefaService
    {
        Task<SubTarefa> CreateSubTarefaAsync(int tarefaId, SubTarefa subTarefa);
        Task<SubTarefa> GetSubTarefaPorIdAsync(int id);
        Task<bool> UpdateSubTarefaAsync(SubTarefa subTarefa);
        Task<bool> DeleteSubTarefaAsync(int id);
    }
}
