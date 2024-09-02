using CommonTestUtilities.AutoMapperBuilder;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.Product;
using FluentAssertions;
using ProjectAspNet.Application.UseCases.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCase
{
    public class ProductCase
    {
        [Fact]
        public async Task Success()
        {
            var mapper = AutoMapperBuild.Build();
            var unitOfWork = UnitOfWorkBuild.Build();
            var productAdd = ProductAddBuild.Build();
            var request = RegisterProductRequestBuilder.Create();
            var useCase = new ProductUseCase(productAdd, unitOfWork, mapper);
            var result = await useCase.Register(request);

            result.ProductName.Should().Be(request.ProductName);
        }
    }
}
