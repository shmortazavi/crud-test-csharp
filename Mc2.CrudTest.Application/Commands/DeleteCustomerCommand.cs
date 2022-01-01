using Mc2.CrudTest.Domain.Customers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Commands
{
    public record DeleteCustomerCommand(Guid Id) : IRequest<bool>;

    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;

        public DeleteCustomerHandler(ICustomerWriteRepository customerWriteRepository, ICustomerReadRepository customerReadRepository)
        {
            _customerWriteRepository = customerWriteRepository;
            _customerReadRepository = customerReadRepository;
        }

        public Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var dbCustomer = _customerReadRepository.GetById(request.Id).Result;

            if (dbCustomer == null)
                throw new Exception("Customer not found!");

            _customerWriteRepository.Delete(dbCustomer);

            return Task.FromResult(true);
        }
    }
}
