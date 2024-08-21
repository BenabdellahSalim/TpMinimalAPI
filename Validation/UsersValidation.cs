using FluentValidation;
using TpMinimalAPI.Data.Models;
using TpMinimalAPI.DTO;

namespace TpMinimalAPI.Validation
{
    public class UsersValidation: AbstractValidator<Users>
    {
        public UsersValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
