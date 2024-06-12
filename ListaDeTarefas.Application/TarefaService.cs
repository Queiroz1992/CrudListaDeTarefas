using ListaDeTarefas.Application.Interfaces;
using ListaDeTarefas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Application
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaService(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public async Task<IEnumerable<Tarefa>> GetTodasTarefasAsync()
        {
            return await _tarefaRepository.GetTodasTarefasAsync();
        }

        public async Task<Tarefa> GetTarefaPorIdAsync(int id)
        {
            return await _tarefaRepository.GetTarefaPorIdAsync(id);
        }

        public async Task<IEnumerable<Tarefa>> GetTarefaPorPrioridadeAsync(string prioridade)
        {
            return await _tarefaRepository.GetTarefasPorPrioridadeAsync(prioridade);
        }

        public async Task<IEnumerable<Tarefa>> GetTarefasPorStatusAsync(string status)
        {
            return await _tarefaRepository.GetTarefasPorStatusAsync(status);
        }

        public async Task<Tarefa> CreateTarefaAsync(Tarefa tarefa)
        {
            tarefa.DataCriacao = DateTime.UtcNow;
            return await _tarefaRepository.CreateTarefaAsync(tarefa);
        }

        public async Task<Tarefa> UpdateTarefaAsync(int id, Tarefa tarefaAtualizada)
        {
            // Verificar se a tarefa existe
            var tarefaExistente = await _tarefaRepository.GetTarefaPorIdAsync(id);
            if (tarefaExistente == null)
            {
                return null;
            }

            // Atualizar apenas as propriedades permitidas
            tarefaExistente.Titulo = tarefaAtualizada.Titulo;
            tarefaExistente.Descricao = tarefaAtualizada.Descricao;
            tarefaExistente.Prioridade = tarefaAtualizada.Prioridade;
            tarefaExistente.Status = tarefaAtualizada.Status;

            // Verificar se o status é "concluída" e todas as subtarefas estão concluídas
            if (tarefaAtualizada.Status == "concluída" && tarefaExistente.SubTarefas.Any(st => st.Status != "concluída"))
            {
                throw new InvalidOperationException("Todas as subtarefas devem estar concluídas antes de marcar a tarefa como concluída.");
            }

            // Verificar se o status é "em progresso" e se há pelo menos uma subtarefa
            if (tarefaAtualizada.Status == "em progresso" && (tarefaExistente.SubTarefas == null || !tarefaExistente.SubTarefas.Any()))
            {
                throw new InvalidOperationException("Uma tarefa só pode ser marcada como 'em progresso' se tiver pelo menos uma subtarefa.");
            }

            if (tarefaAtualizada.Status == "concluída")
            {
                tarefaExistente.DataConclusao = DateTime.UtcNow;
            }

            var tarefaAtualizadaResult = await _tarefaRepository.UpdateTarefaAsync(tarefaExistente);
            return tarefaAtualizadaResult;
        }

        public async Task<bool> DeleteTarefaAsync(int id)
        {
            var tarefa = await _tarefaRepository.GetTarefaPorIdAsync(id);
            if (tarefa == null)
            {
                return false;
            }

            await _tarefaRepository.DeleteTarefaAsync(id);
            return true;
        }
    }
}
