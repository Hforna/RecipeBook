using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.Repositories.Login
{
    public interface IGoogleLoginUseCase
    {
        public Task<string> Execute(string name, string email);
    }
}
