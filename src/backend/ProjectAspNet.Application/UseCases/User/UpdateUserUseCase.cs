using ProjectAspNet.Application.UseCases.RepositoriesUseCases.User;
using ProjectAspNet.Application.Validators.User;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Domain.Repositories;
using ProjectAspNet.Domain.Repositories.Users;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.User
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggedUser _loggedUser;
        private readonly IGetUserTracking _getUserTracking;
        private readonly IGetUserUpdate _userUpdate;
        private readonly IUserEmailExists _userEmailExists;

        public UpdateUserUseCase(IUnitOfWork unitOfWork, ILoggedUser loggedUser, IGetUserTracking getUserTracking, IGetUserUpdate userUpdate, IUserEmailExists userEmailExists)
        {
            _unitOfWork = unitOfWork;
            _loggedUser = loggedUser;
            _getUserTracking = getUserTracking;
            _userUpdate = userUpdate;
            _userEmailExists = userEmailExists;
        }

        public async Task Execute(RequestUpdateUser request)
        {
            await Validate(request);
            var loggedUser = await _loggedUser.getUser();
            var user = await _getUserTracking.getUserById(loggedUser.Id);
            user.Name = request.Name;
            user.Email = request.Email;
            _userUpdate.Update(user);
            await _unitOfWork.Commit();
        }

        public async Task Validate(RequestUpdateUser request)
        {
            var validator = new UpdateUserValidate();
            var result = validator.Validate(request);
            var userByToken = await _loggedUser.getUser();
            if(await _userEmailExists.EmailExists(request.Email))
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceExceptMessages.EMAIL_ALREADY_EXISTS));
            }
            if(result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new RegisterUserError(errorMessages);
            }
        }
    }
}
