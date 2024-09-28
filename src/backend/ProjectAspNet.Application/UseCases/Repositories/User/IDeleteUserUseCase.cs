using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.Repositories.User
{
    public interface IDeleteUserUseCase
    {
        public Task Execute(Guid uid);
    }
}
