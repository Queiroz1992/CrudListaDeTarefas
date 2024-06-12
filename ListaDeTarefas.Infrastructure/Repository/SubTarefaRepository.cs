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
    public class SubTarefaRepository : ISubTarefaRepository    
    {
        private readonly TarefaDbContext _context;

        public SubTarefaRepository(TarefaDbContext context)
        {
            _context = context;
        }

        public async Task<SubTarefa> GetSubTarefaPorIdAsync(int id)
        {
            return await _context.SubTarefas.FindAsync(id);
        }

        public async Task UpdateSubTarefaAsync(SubTarefa subTarefa)
        {
            _context.Entry(subTarefa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubTarefaAsync(int id)
        {
            var subTarefa = await GetSubTarefaPorIdAsync(id);
            _context.SubTarefas.Remove(subTarefa);
            await _context.SaveChangesAsync();
        }
    }
}
