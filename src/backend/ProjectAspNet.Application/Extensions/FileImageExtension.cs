using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.Extensions
{
    public static class FileImageExtension
    {
        public static (bool isImage, string extension) GetImageVerificationAndExtension(this Stream file)
        {
            var result = (false, string.Empty);

            if (file.Is<JointPhotographicExpertsGroup>())
                result = (true, GetTypeExtension(JointPhotographicExpertsGroup.TypeExtension));
            if(file.Is<PortableNetworkGraphic>())
                result = (true, GetTypeExtension(PortableNetworkGraphic.TypeExtension));

            file.Position = 0;

            return result;
        }

        private static string GetTypeExtension(string extension)
        {
            return extension.StartsWith(".") ? extension : $".{extension}";
        }
    }
}
