using AutoMapper;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Repositories;
using ProjectAspNet.Domain.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.Product
{
    public class ProductUseCase : IProductCase
    {
        private IProductAdd _productAdd;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ProductUseCase(IProductAdd productAdd, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productAdd = productAdd;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RegisterProductResponse> Register(RegisterProductRequest request)
        {
            var product = _mapper.Map<ProductEntitie>(request);
            await _productAdd.Add(product);
            await _unitOfWork.Commit();

            return new RegisterProductResponse() { ProductName = request.ProductName };
        }
    }
}
