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
    public record UpdateCustomerCommand(CustomerRequestDto Customer) : IRequest<CustomerResponseDto>;

    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, CustomerResponseDto>
    {
        #region Fields
        private readonly IReadUnitOfWork _readUnitOfWork;
        private readonly IWriteUnitOfWork _writeUnitOfWork;
        #endregion

        #region Ctor
        public UpdateCustomerHandler(IReadUnitOfWork readUnitOfWork, IWriteUnitOfWork writeUnitOfWork)
        {
            _readUnitOfWork = readUnitOfWork;
            _writeUnitOfWork = writeUnitOfWork;
        }
        #endregion

        #region Handle Method
        public async Task<CustomerResponseDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = request.Customer;
            var dbCustomer = _readUnitOfWork.CustomerReadRepository.GetById(customer.Id).Result;

            if (dbCustomer == null)
                throw new Exception("customer not found!");

            var existedEmail = _readUnitOfWork.CustomerReadRepository.Find(x => x.Email == customer.Email).FirstOrDefault();
            if (existedEmail != null)
                throw new Exception("selected email is used before!");

            if (string.IsNullOrEmpty(customer.FirstName))
                throw new Exception("FirstName is required!");

            if (string.IsNullOrEmpty(customer.LastName))
                throw new Exception("Lastname is required!");

            if (customer.DateOfBirth >= DateTime.Now)
                throw new Exception("DateOfBirth is not correct!");

            if (string.IsNullOrEmpty(customer.PhoneNumber))
                throw new Exception("PhoneNumber is required!");

            if (string.IsNullOrEmpty(customer.BankAccountNumber))
                throw new Exception("BankAccountNumber is required!");

            var updatedbCustomer = _writeUnitOfWork.CustomerWriteRepository.Update(
                                    Customer.Update(dbCustomer, customer.FirstName, customer.LastName,
                                                    customer.DateOfBirth, customer.PhoneNumber, customer.Email,
                                                    customer.BankAccountNumber));

            await _writeUnitOfWork.Commit();

            return await Task.FromResult(
                new CustomerResponseDto
                {
                    Id = updatedbCustomer.Id,
                    FirstName = updatedbCustomer.FirstName,
                    LastName = updatedbCustomer.LastName,
                    DateOfBirth = updatedbCustomer.DateOfBirth,
                    PhoneNumber = updatedbCustomer.PhoneNumber,
                    Email = updatedbCustomer.Email,
                    BankAccountNumber = updatedbCustomer.BankAccountNumber
                });
        }
        #endregion 
    }
}
