using Mc2.CrudTest.Application.Dtos;
using Mc2.CrudTest.Domain.Customers;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Commands
{
    public record CreateCustomerCommand(CustomerRequestDto Customer) : IRequest<CustomerResponseDto>;

    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CustomerResponseDto>
    {
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;

        public CreateCustomerHandler(ICustomerWriteRepository customerWriteRepository, ICustomerReadRepository customerReadRepository)
        {
            _customerWriteRepository = customerWriteRepository;
            _customerReadRepository = customerReadRepository;
        }

        public async Task<CustomerResponseDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = request.Customer;
            var dbCustomer = _customerReadRepository.Find(x => x.Email.ToLower() == customer.Email.ToLower()).FirstOrDefault();

            if (dbCustomer != null)
                throw new Exception("This email previously registered!");

            var createdCustomer = await _customerWriteRepository.AddAsync(Customer.Create(customer.FirstName, customer.LastName, customer.DateOfBirth, customer.PhoneNumber, customer.Email));
            return await Task.FromResult(
                new CustomerResponseDto
                {
                    Id = createdCustomer.Id,
                    FirstName = createdCustomer.FirstName,
                    LastName = createdCustomer.LastName,
                    DateOfBirth = createdCustomer.DateOfBirth,
                    PhoneNumber = createdCustomer.PhoneNumber,
                    Email = createdCustomer.Email
                });
        }
    }
}
