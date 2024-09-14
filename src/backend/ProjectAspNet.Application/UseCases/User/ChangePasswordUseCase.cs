using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Repositories;
using ProjectAspNet.Domain.Repositories.Security;
using ProjectAspNet.Domain.Repositories.Users;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.User
{
    public class ChangePasswordUseCase : IChangePasswordUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetUserTracking _getUserTracking;
        private readonly IGetUserUpdate _updateUser;
        private readonly ILoggedUser _getUserLogged;
        private readonly ICryptography _cryptography;

        public ChangePasswordUseCase(IUnitOfWork unitOfWork, IGetUserTracking getUserTracking, IGetUserUpdate updateUser, ILoggedUser getUserLogged, ICryptography cryptography)
        {
            _unitOfWork = unitOfWork;
            _getUserTracking = getUserTracking;
            _updateUser = updateUser;
            _getUserLogged = getUserLogged;
            _cryptography = cryptography;
        }

        public async Task Execute(RequestChangeUserPassword request)
        {
            var userLogged = await _getUserLogged.getUser();
            Validate(request, userLogged);

            var user = await _getUserTracking.getUserById(userLogged.Id);
            var cryptoPassword = _cryptography.Encrypt(request.NewPassword);
            user.Password = cryptoPassword;

            _updateUser.Update(user);
            await _unitOfWork.Commit();
        }

        public void Validate(RequestChangeUserPassword request, UserEntitie user)
        {
            var validate = new ChangePasswordValidate();
            var result = validate.Validate(request);
            var cryptoPassword = _cryptography.Encrypt(request.Password);
            if(user.Password.Equals(cryptoPassword) == false)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceExceptMessages.WRONG_PASSWORD));
            }
            if(result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new RegisterUserError(errorMessages);
            }
        }
    }
}
