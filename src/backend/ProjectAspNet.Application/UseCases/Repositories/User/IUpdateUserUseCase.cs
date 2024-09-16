using ProjectAspNet.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.RepositoriesUseCases.User
{
    public interface IUpdateUserUseCase
    {
        public Task Execute(RequestUpdateUser request);
    }
}
