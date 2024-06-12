using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Domain
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public string Prioridade { get; set; }
        public string Status { get; set; }
        public ICollection<SubTarefa> SubTarefas { get; set; }

        public Tarefa()
        {
            Status = "pentende";
        }
    }
}
