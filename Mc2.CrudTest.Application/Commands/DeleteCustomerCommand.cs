using Mc2.CrudTest.Domain.SeedWorks;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Commands
{
    public record DeleteCustomerCommand(Guid Id) : IRequest<bool>;

    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        #region Fields
        private readonly IReadUnitOfWork _readUnitOfWork;
        private readonly IWriteUnitOfWork _writeUnitOfWork;
        #endregion

        #region Ctor
        public DeleteCustomerHandler(IReadUnitOfWork readUnitOfWork, IWriteUnitOfWork writeUnitOfWork)
        {
            _readUnitOfWork = readUnitOfWork;
            _writeUnitOfWork = writeUnitOfWork;
        }
        #endregion

        #region Handle Method
        public Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var dbCustomer = _readUnitOfWork.CustomerReadRepository.GetById(request.Id).Result;

            if (dbCustomer == null)
                throw new Exception("Customer not found!");

            _writeUnitOfWork.CustomerWriteRepository.Delete(dbCustomer);

            return Task.FromResult(true);
        }
        #endregion
    }
}
