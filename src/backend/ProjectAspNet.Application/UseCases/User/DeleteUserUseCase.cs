using ProjectAspNet.Application.UseCases.Repositories.User;
using ProjectAspNet.Domain.Repositories;
using ProjectAspNet.Domain.Repositories.Storage;
using ProjectAspNet.Domain.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.User
{
    public class DeleteUserUseCase : IDeleteUserUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAzureStorageService _storageService;
        private readonly IDeleteUser _deleteUser;

        public DeleteUserUseCase(IUnitOfWork unitOfWork, IAzureStorageService storageService, IDeleteUser deleteUser)
        {
            _unitOfWork = unitOfWork;
            _storageService = storageService;
            _deleteUser = deleteUser;
        }

        public async Task Execute(Guid uid)
        {
            await _storageService.DeleteUser(uid);

            await _deleteUser.Delete(uid);

            await _unitOfWork.Commit();
        }
    }
}
