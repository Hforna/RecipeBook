using CommonTestUtilities.AutoMapperBuilder;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.Recipe;
using FluentAssertions;
using ProjectAspNet.Application.UseCases.Recipe;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public class RecipeUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestRecipeBuild.Build();
            var user = UserEntitieTest.Build();

            var useCase = CreateRecipe(user.user);
            var result = await useCase.Execute(request);

            result.Title.Should().Be(request.Title);
            result.Id.Should().NotBe(user.user.Id.ToString());
        }

        [Fact]
        public async Task Error_Ingredient_Empty()
        {
            var request = RequestRecipeBuild.Build();
            var user = UserEntitieTest.Build();
            request.Ingredients = [];

            var useCase = CreateRecipe(user.user);
            Func<Task> result = async () => { await useCase.Execute(request); };

            await result.Should().ThrowAsync<CreateRecipeException>().Where(c => c.Errors.Count == 1 && c.Errors.Contains(ResourceExceptMessages.LIST_OUT_OF_RANGE));
        }

        public CreateRecipeUseCase CreateRecipe(UserEntitie user)
        {
            var mapper = AutoMapperBuild.Build();
            var unitOfWork = UnitOfWorkBuild.Build();
            var loggedUser = new LoggedUserBuilder().Build(user);
            var addRecipe = RecipeAdd.Build();

            return new CreateRecipeUseCase(mapper, unitOfWork, addRecipe, loggedUser);
        }
    }
}
