using ProjectAspNet.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.RepositoriesUseCases.User
{
    public interface IGetProfileUseCase
    {
        public Task<ResponseUserProfile> Execute();
    }
}
