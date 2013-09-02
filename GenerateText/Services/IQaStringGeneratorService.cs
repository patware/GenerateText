using System;
namespace GenerateText.Services
{
    public interface IQaStringGeneratorService
    {
        string Generate(int characters);
        string Generate(int characters, string pattern);

        string Generate(int characters, bool qaApproved);
        string Generate(int characters, string pattern,bool qaApproved);
    }
}
