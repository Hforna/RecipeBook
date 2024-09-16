using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.RepositoriesUseCases.Product
{
    public interface IProductCase
    {
        public Task<RegisterProductResponse> Register(RegisterProductRequest request);
    }
}
