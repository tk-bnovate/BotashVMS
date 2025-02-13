using FluentValidation;

namespace BotashVMS.Models.Validators
{
    public class VendorValidator : AbstractValidator<Vendor>
    {
        public VendorValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.TradingAs)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}
