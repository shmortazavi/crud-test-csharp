using Mc2.CrudTest.Application.Dtos;
using Mc2.CrudTest.Domain.SeedWorks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Queries
{
    public record GetAllCustomersQuery : IRequest<List<CustomerResponseDto>>;

    public class GetAllCustomerHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerResponseDto>>
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
        public Task<List<CustomerResponseDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var result = _unitOfWork.CustomerReadRepository.GetAll().Result;
            if (result == null || result.Count <= 0)
                throw new Exception("Any customer not found!");

            var response = result.Select(x => new CustomerResponseDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DateOfBirth = x.DateOfBirth,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber
            }).ToList();

            return Task.FromResult(response);
        }
        #endregion
    }
}
