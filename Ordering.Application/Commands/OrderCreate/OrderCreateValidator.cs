using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Commands.OrderCreate
{
    public class OrderCreateValidator : AbstractValidator<OrderCreateCommand>
    {
        public OrderCreateValidator()
        {
            RuleFor(v => v.SellerUserName)
                .EmailAddress()
                .NotEmpty();//SellerUserName den gelen veri email olacak ve boş olmayacak

            RuleFor(v => v.ProductId)
                .NotEmpty();
        }
    }
}
