using FluentValidation;
using TpMinimalAPI.Data.Models;
using TpMinimalAPI.DTO;

namespace TpMinimalAPI.Validation
{
    public class TodoValidation : AbstractValidator<TodoInPut>
    {
        public TodoValidation()
        {
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}
