using FluentValidation;

namespace TektonLabs.Service.EventHandlers.Company.Validations
{
    public class ProductUpdateValidation : AbstractValidator<Domain.DTOs.Request.ProductUpdateRequest>
    {
        public ProductUpdateValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .Length(1, 50).WithMessage("La longitud del nombre debe estar entre 1 y 50 caracteres.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .Length(1, 50).WithMessage("La longitud del nombre debe estar entre 1 y 100 caracteres.");
            RuleFor(x => x.Stock)
                  .NotEmpty().WithMessage("El stock es obligatorio.");
            RuleFor(x => x.Price)
                  .NotEmpty().WithMessage("El precio es obligatorio.");
            RuleFor(x => x.Status)
                  .NotEmpty().WithMessage("El status es obligatorio.");
            RuleFor(x => x.ModificationUser)
                    .NotEmpty().WithMessage("El usuario es obligatorio.");   
        }
    }
}
