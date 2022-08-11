using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Ordering.Application.PipelineBehaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        //fluentvalidation kutüphanesi eklendi, bunun sayesinde işlemler yapılacak
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //next=işlem bittiği zaman bir sonraki aşamaya geçmeye yarıyor
            var context = new ValidationContext<TRequest>(request);
            //hatalı olan validasyon değişkenlerini döndürdük
            var failures = _validators.Select(x => x.Validate(context))
                                      .SelectMany(x => x.Errors)
                                      .Where(x => x != null)
                                      .ToList();

            if (failures.Any())//içinde herhangi bir item varsa
            {
                throw new ValidationException(failures);
            }

            return next();//bir sonraki piple ya git yada ilgili handle metoduna git
        }
    }
}
