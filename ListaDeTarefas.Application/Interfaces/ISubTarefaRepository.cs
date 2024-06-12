using ListaDeTarefas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Application.Interfaces
{
    public interface ISubTarefaRepository
    {
        Task<SubTarefa> GetSubTarefaPorIdAsync(int id);
        Task UpdateSubTarefaAsync(SubTarefa subTarefa);
        Task DeleteSubTarefaAsync(int id);
    }
}
