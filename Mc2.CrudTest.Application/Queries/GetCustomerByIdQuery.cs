using Mc2.CrudTest.Application.Dtos;
using Mc2.CrudTest.Domain.SeedWorks;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Queries
{
    public record GetCustomerByIdQuery(Guid Id) : IRequest<CustomerResponseDto>;

    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, CustomerResponseDto>
    {
        #region Fields
        private readonly IReadUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public GetAllCustomerHandler(IReadUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Handle Method
        public Task<CustomerResponseDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customerId = request.Id;
            if (customerId == Guid.Empty)
                throw new Exception("Customer Id is not correct!");

            var dbCustomer = _unitOfWork.CustomerReadRepository.GetById(customerId).Result;
            if (dbCustomer == null)
                throw new Exception("customer not found!");

            return Task.FromResult(
                new CustomerResponseDto
                {
                    Id = dbCustomer.Id,
                    FirstName = dbCustomer.FirstName,
                    LastName = dbCustomer.LastName,
                    DateOfBirth = dbCustomer.DateOfBirth,
                    Email = dbCustomer.Email,
                    PhoneNumber = dbCustomer.PhoneNumber
                });
        }
        #endregion
    }
}
