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
            CreateMap<RecipeEntitie, ResponseRecipe>()
                .ForMember(d => d.Id, f => f.MapFrom(ds => _sqidsEncoder.Encode(ds.Id)));

            CreateMap<RequestRecipe, RecipeEntitie>()
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients.Select(i => new IngredientEntitie { Item = i }).ToList()))
                .ForMember(dest => dest.DishTypes, opt => opt.MapFrom(src => src.DishTypes.Select(d => new DishTypeEntitie { Type = (DishType)d }).ToList()))
                .ForMember(dest => dest.Instructions, opt => opt.Ignore());


            CreateMap<string, IngredientEntitie>()
                .ForMember(i => i.Item, opt => opt.MapFrom(source => source));

            CreateMap<DishType, DishTypeEntitie>()
                .ForMember(i => i.Type, opt => opt.MapFrom(source => source));

            CreateMap<RequestInstructions, InstructionsEntitie>();
        }
    }
}
