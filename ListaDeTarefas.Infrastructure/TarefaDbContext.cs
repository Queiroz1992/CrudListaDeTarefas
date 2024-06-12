using ListaDeTarefas.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Infrastructure
{
    public class TarefaDbContext : DbContext
    {
        public TarefaDbContext(DbContextOptions<TarefaDbContext> options) : base(options) { }

        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<SubTarefa> SubTarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarefa>()
                .Property(t => t.Prioridade)
                .IsRequired();

            modelBuilder.Entity<Tarefa>()
                .Property(t => t.Titulo)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Tarefa>()
                .Property(t => t.Descricao)
                .HasMaxLength(500)
                .IsRequired();

            modelBuilder.Entity<SubTarefa>()
                .Property(st => st.Titulo)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<SubTarefa>()
                .Property(st => st.Descricao)
                .HasMaxLength(300)
                .IsRequired();
        }
    }
}
