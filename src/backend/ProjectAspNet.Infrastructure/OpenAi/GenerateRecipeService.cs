using OpenAI_API;
using ProjectAspNet.Domain.Repositories.OpenAi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI_API.Chat;
using ProjectAspNet.Domain.DTOs;
using ProjectAspNet.Domain.Enums;

namespace ProjectAspNet.Infrastructure.OpenAi
{
    public class GenerateRecipeService : IGenerateRecipeAi
    {
        public const string OPENAPI_TYPE = "gpt-4o-mini";

        private readonly IOpenAIAPI _openAi;

        public GenerateRecipeService(IOpenAIAPI openAi) => _openAi = openAi;

        public async Task<GenerateRecipeDto> Generate(IList<string> recipes)
        {
            var openAi = _openAi.Chat.CreateConversation();
            openAi.AppendSystemMessage("Você é um chef que conhece mutias receitas saborosas. Quando o usuário lhe fornecer uma lista de ingredientes separados por ';', você responderá com apenas uma receita que seja fácil de criar com os ingredientes informados.\r\n\r\n\r\n\r\nVocê criará uma receita com a seguinte estrutura e ordem:\r\n\r\n\r\n\r\n[Nome da Receita]\r\n\r\n\r\n\r\n[tempo necessário para prepará-la, em que 0 significa menos de 10 minutos, 1 significa entre 10 e 30 minutos, 2 significa entre 30 e 60 minutos e 3 significa mais de 60 minutos]\r\n\r\n\r\n\r\n[Lista de ingredientes fornecidos e adicionados por você separados por ';']\r\n\r\n\r\n\r\n[Instruções passo a passo para o preparo da receita em apenas uma linha, em ordem e separados por '@']\r\n\r\n\r\n\r\nNão adicione mais texto que o solicitado, por favor.");
            openAi.AppendUserInput(string.Join(";", recipes));

            var response = await openAi.GetResponseFromChatbotAsync();

            var responseSplit = response.Split("\n")
                .Where(s => string.IsNullOrWhiteSpace(s) == false)
                .Select(s => s.Replace("[", "").Replace("]", ""))
                .ToList();

            var step = 1;

            return new GenerateRecipeDto()
            {
                Title = responseSplit[0],
                CookingTime = (CookingTime)Enum.Parse(typeof(CookingTime), responseSplit[1]),
                Ingredients = responseSplit[2].Split(";"),
                Instructions = responseSplit[3].Split("@").Select(i => new InstructionsRecipeDto()
                {
                    Step = step++,
                    Text = i
                }).ToList()
            };
        }
    }
}
