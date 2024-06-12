using FluentValidation;
using ListaDeTarefas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Application.Validators
{
    public class TarefaValidator : AbstractValidator<Tarefa>
    {
        public TarefaValidator()
        {
            RuleFor(t => t.Titulo)
                .NotEmpty().WithMessage("O título é obrigatório.")
                .MaximumLength(100).WithMessage("O título não deve exceder 100 caracteres.");

            RuleFor(t => t.Descricao)
                .NotEmpty().WithMessage("A descrição é obrigatória.")
                .MaximumLength(500).WithMessage("\r\nA descrição não deve exceder 500 caracteres.");

            RuleFor(t => t.Prioridade)
                .NotEmpty().WithMessage("Prioridade é obrigatória.")
                .Must(p => p == "baixa" || p == "média" || p == "alta")
                .WithMessage("A prioridade deve ser 'baixa', 'média', ou 'alta'.");

            RuleForEach(t => t.SubTarefas).SetValidator(new SubTarefaValidator());
        }
    }
}
