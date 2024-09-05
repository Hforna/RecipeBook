using AutoMapper;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Repositories;
using ProjectAspNet.Domain.Repositories.Products;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.Product
{
    public class ProductUseCase : IProductCase
    {
        private readonly IProductAdd _productAdd;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductUseCase(IProductAdd productAdd, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productAdd = productAdd;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RegisterProductResponse> Register(RegisterProductRequest request)
        {
            Validate(request);
            var product = _mapper.Map<ProductEntitie>(request);
            await _productAdd.Add(product);
            await _unitOfWork.Commit();

            return new RegisterProductResponse() { ProductName = request.ProductName };
        }

        public static void Validate(RegisterProductRequest request)
        {
            var validate = new RegisterProductValidate();
            var result = validate.Validate(request);

            if(!result.IsValid)
            {
                var errorList = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new RegisterProductError(errorList);
            }
        }
    }
}
