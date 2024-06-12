using ListaDeTarefas.Application.Interfaces;
using ListaDeTarefas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Application
{
    public class SubTarefaService : ISubTarefaService
    {
        private readonly ISubTarefaRepository _subTarefaRepository;
        private readonly ITarefaRepository _tarefaRepository;

        public SubTarefaService(ISubTarefaRepository subTarefaRepository, ITarefaRepository tarefaRepository)
        {
            _subTarefaRepository = subTarefaRepository;
            _tarefaRepository = tarefaRepository;
        }

        public async Task<SubTarefa> CreateSubTarefaAsync(int tarefaId, SubTarefa subTarefa)
        {
            var tarefa = await _tarefaRepository.GetTarefaPorIdAsync(tarefaId);
            if (tarefa == null)
            {
                throw new KeyNotFoundException("Tarefa não encontrada.");
            }

            tarefa.SubTarefas.Add(subTarefa);
            await _tarefaRepository.UpdateTarefaAsync(tarefa);
            return subTarefa;
        }

        public async Task<bool> UpdateSubTarefaAsync(SubTarefa subTarefa)
        {
            var existeSubTarefa = await _subTarefaRepository.GetSubTarefaPorIdAsync(subTarefa.Id);
            if (existeSubTarefa == null)
            {
                return false;
            }

            existeSubTarefa.Titulo = subTarefa.Titulo;
            existeSubTarefa.Descricao = subTarefa.Descricao;
            existeSubTarefa.Status = subTarefa.Status;

            await _subTarefaRepository.UpdateSubTarefaAsync(existeSubTarefa);
            return true;
        }

        public async Task<bool> DeleteSubTarefaAsync(int id)
        {
            var existingSubTarefa = await _subTarefaRepository.GetSubTarefaPorIdAsync(id);
            if (existingSubTarefa == null)
            {
                return false;
            }

            await _subTarefaRepository.DeleteSubTarefaAsync(id);
            return true;
        }

        public async Task<SubTarefa> GetSubTarefaPorIdAsync(int id)
        {
            return await _subTarefaRepository.GetSubTarefaPorIdAsync(id);
        }
    }
}
