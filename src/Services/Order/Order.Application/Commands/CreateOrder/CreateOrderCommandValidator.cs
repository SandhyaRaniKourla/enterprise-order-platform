using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Application.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(command => command.CustomerId).NotEmpty();
            RuleFor(command => command.Currency).NotEmpty().Length(3);
            RuleFor(command => command.Items).NotEmpty();
            RuleForEach(Command => Command.Items).ChildRules(item =>
            {
                item.RuleFor(x => x.ProductId).NotEmpty();
                item.RuleFor(x => x.Quantity).GreaterThan(0);
                item.RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0);
            });
        }
    }
}
