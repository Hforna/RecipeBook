using Bogus;
using ProjectAspNet.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Request.Product
{
    public class RegisterProductRequestBuilder
    {
        public static RegisterProductRequest Create(int quantityPrice = 1)
        {
            return new Faker<RegisterProductRequest>()
                .RuleFor(pd => pd.ProductName, (f) => f.Commerce.ProductName())
                .RuleFor(pd => pd.Price, (f) => double.Parse(f.Commerce.Price(quantityPrice, 100000)))
                .RuleFor(pd => pd.Description, (f) => f.Commerce.ProductDescription())
                .RuleFor(pd => pd.Quantity, (f) => f.Random.Int(10, 10000))
                .RuleFor(pd => pd.Brand, (f) => f.Company.CompanyName());


        }
    }
}
