using ExampleCompanyApp.Core.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCompanyApp.Service.Validations
{
    public class ProductDtoValidator:AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} alanı boş geçilemez")
                .MaximumLength(100).WithMessage("{PropertyName} alanı {MaxLength} karaktreden byük olamaz.");
            RuleFor(x => x.Stock).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} " +
                "alanı {From} ile {To} arasında olmalıdır.");
            RuleFor(x => x.Price).InclusiveBetween(1, decimal.MaxValue).WithMessage("{PropertyName} " +
                "alanı {From} ile {To} arasında olmalıdır.");

        }
    }
}
