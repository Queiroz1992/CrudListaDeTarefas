using FluentValidation;
using ListaDeTarefas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Application.Validators
{
    public class SubTarefaValidator : AbstractValidator<SubTarefa>

    {
        public SubTarefaValidator()
        {
            RuleFor(st => st.Titulo)
                .NotEmpty().WithMessage("O título é obrigatório.")
                .MaximumLength(100).WithMessage("O título não deve exceder 100 caracteres.");

            RuleFor(st => st.Descricao)
                .NotEmpty().WithMessage("A descrição é obrigatória.")
                .MaximumLength(300).WithMessage("A descrição não deve exceder 300 caracteres.");
        }
    }
}
