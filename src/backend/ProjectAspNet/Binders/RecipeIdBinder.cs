using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sqids;

namespace ProjectAspNet.Binders
{
    public class RecipeIdBinder : IModelBinder
    {
        private readonly SqidsEncoder<long> _idEncoder;

        public RecipeIdBinder(SqidsEncoder<long> idEncoder) => _idEncoder = idEncoder;

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var modelName = bindingContext.ModelName;

            var valueBinder = bindingContext.ValueProvider.GetValue(modelName);

            if(valueBinder == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueBinder);

            var value = valueBinder.FirstValue;

            if (string.IsNullOrEmpty(value))
                return Task.CompletedTask;

            var decodeId = _idEncoder.Decode(value).Single();

            bindingContext.Result = ModelBindingResult.Success(decodeId);

            return Task.CompletedTask;
        }
    }
}
