using AutoMapper;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Entities.Recipes;
using ProjectAspNet.Domain.Enums;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.Services.AutoMapper
{
    public class ProfileMapper : Profile
    {
        private readonly SqidsEncoder<long> _sqidsEncoder;
        public ProfileMapper(SqidsEncoder<long> sqIds)
        {
            _sqidsEncoder = sqIds;

            CreateMap<UserEntitie, ResponseUserProfile>();
            CreateMap<Recipe, ResponseRecipe>()
                .ForMember(d => d.Id, f => f.MapFrom(ds => _sqidsEncoder.Encode(ds.Id)));

            CreateMap<RequestRecipe, Recipe>()
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients.Select(i => new IngredientEntitie { Item = i }).ToList()))
                .ForMember(dest => dest.DishTypes, opt => opt.MapFrom(src => src.DishTypes.Select(d => new DishTypeEntitie { Type = (DishType)d }).ToList()))
                .ForMember(dest => dest.Instructions, opt => opt.Ignore());


            CreateMap<string, IngredientEntitie>()
                .ForMember(i => i.Item, opt => opt.MapFrom(source => source));

            CreateMap<DishType, DishTypeEntitie>()
                .ForMember(i => i.Type, opt => opt.MapFrom(source => source));

            CreateMap<RequestInstructions, InstructionsEntitie>();

            CreateMap<Recipe, RecipeResponseJson>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => _sqidsEncoder.Encode(s.Id)))
                .ForMember(d => d.AmountIngredients, opt => opt.MapFrom(s => s.Ingredients.Count));

            CreateMap<Recipe, ResponeGetRecipe>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => _sqidsEncoder.Encode(s.Id)))
                .ForMember(d => d.DishTypes, opt => opt.MapFrom(s => s.DishTypes.Select(d => d.Type)));

            CreateMap<IngredientEntitie, IngredientsResponse>()
                .ForMember(i => i.Id, opt => opt.MapFrom(c => _sqidsEncoder.Encode(c.Id)));

            CreateMap<InstructionsEntitie, ResponseInstructions>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => _sqidsEncoder.Encode(s.Id)));
        }
    }
}
