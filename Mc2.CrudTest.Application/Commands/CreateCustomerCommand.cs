using Mc2.CrudTest.Application.Dtos;
using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.SeedWorks;
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
        #region Fields
        private readonly IReadUnitOfWork _readUnitOfWork;
        private readonly IWriteUnitOfWork _writeUnitOfWork;
        #endregion

        #region Ctor
        public CreateCustomerHandler(IReadUnitOfWork readUnitOfWork, IWriteUnitOfWork writeUnitOfWork)
        {
            _readUnitOfWork = readUnitOfWork;
            _writeUnitOfWork = writeUnitOfWork;
        }
        #endregion

        #region Handle Method
        public async Task<CustomerResponseDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = request.Customer;
            var dbCustomer = _readUnitOfWork.CustomerReadRepository.Find(x => x.Email.ToLower() == customer.Email.ToLower()).FirstOrDefault();

            if (dbCustomer != null)
                throw new Exception("This email previously registered!");

            var createdCustomer = await _writeUnitOfWork.CustomerWriteRepository.AddAsync(
                                         Customer.Create(customer.FirstName, customer.LastName, customer.DateOfBirth,
                                                         customer.PhoneNumber, customer.Email, customer.BankAccountNumber));
            return await Task.FromResult(
                new CustomerResponseDto
                {
                    Id = createdCustomer.Id,
                    FirstName = createdCustomer.FirstName,
                    LastName = createdCustomer.LastName,
                    DateOfBirth = createdCustomer.DateOfBirth,
                    PhoneNumber = createdCustomer.PhoneNumber,
                    Email = createdCustomer.Email,
                    BankAccountNumber = createdCustomer.BankAccountNumber
                });
        }
        #endregion
    }
}
