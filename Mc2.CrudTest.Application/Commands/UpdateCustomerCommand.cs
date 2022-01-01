using Mc2.CrudTest.Application.Dtos;
using Mc2.CrudTest.Domain.Customers;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Commands
{
    public record UpdateCustomerCommand(CustomerRequestDto Customer) : IRequest<CustomerResponseDto>;

    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, CustomerResponseDto>
    {
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;

        public UpdateCustomerHandler(ICustomerWriteRepository customerWriteRepository, ICustomerReadRepository customerReadRepository)
        {
            _customerWriteRepository = customerWriteRepository;
            _customerReadRepository = customerReadRepository;
        }

        public async Task<CustomerResponseDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = request.Customer;
            var dbCustomer = _customerReadRepository.GetById(customer.Id).Result;

            if (dbCustomer == null)
                throw new Exception("customer not found!");

            var existedEmail = _customerReadRepository.Find(x => x.Email == customer.Email).FirstOrDefault();
            if (existedEmail != null)
                throw new Exception("selected email is used before!");

            if (string.IsNullOrEmpty(customer.FirstName))
                throw new Exception("FirstName is required!");

            if (string.IsNullOrEmpty(customer.LastName))
                throw new Exception("Lastname is required!");

            if (customer.DateOfBirth >= DateTime.Now)
                throw new Exception("DateOfBirth is not correct!");

            //TODO:
            //if(validate(customer.mobile))

            var updateddbCustomer = _customerWriteRepository.Update(Customer.Update(dbCustomer, customer.FirstName, customer.LastName, customer.DateOfBirth, customer.PhoneNumber, customer.Email));

            return await Task.FromResult(
                new CustomerResponseDto
                {
                    Id = updateddbCustomer.Id,
                    FirstName = updateddbCustomer.FirstName,
                    LastName = updateddbCustomer.LastName,
                    DateOfBirth = updateddbCustomer.DateOfBirth,
                    PhoneNumber = updateddbCustomer.PhoneNumber,
                    Email = updateddbCustomer.Email
                });
        }
    }
}
