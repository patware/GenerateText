using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenerateText.Services
{
    public class QaStringGeneratorService : GenerateText.Services.IQaStringGeneratorService
    {
        private class Suffix
        {
            public int TotalLength { get; private set; }
            public string Value { get; private set; }

            public Suffix()
            {

            }
            public static Suffix Parse(string input)
            {
                var suffix = new Suffix();

                suffix.Value = string.Concat("_", input.Length, "_");

                suffix.TotalLength = input.Length;

                return suffix;
            }
        }
        public const string AlphabetSoup = "abcdefghijklmnopqrstuvwxyz";

        private string doGenerate(int characters, string pattern,bool qaApproved)
        {
            var safePattern = string.IsNullOrEmpty(pattern) ? AlphabetSoup : pattern;
            var minimumCharacters = qaApproved ? 3 : 1;

            if (characters < minimumCharacters)
            {
                if (qaApproved)
                // abcdef...xyz_26_
                {
                    //A loop is needed because when we reach decimal limits (10 characters, 100 characters) funny thing happen
                    // Ex: 123456_9_    => 9 charaters
                    // Ex: 123456_10_   => 10 characters.
                    // Ex: 1234567_11_  => 11 characters.
                    // Ex: so, when we add the QA approved suffix, we might end up in the +1 digit number of characters
                    var runner = safePattern;
                    var bContinue = true;
                    do
                    {
                        var suffix = Suffix.Parse(runner);

                        runner = safePattern + suffix.Value;

                        bContinue = runner.Length != suffix.TotalLength;
                        
                    } while (bContinue);

                    return runner;
                }
                else
                    characters = safePattern.Length;
            }

            if (characters >= 9999) 
                characters = 9999;

            var sb = new System.Text.StringBuilder();

            while (sb.Length < characters)
            {
                sb.Append(safePattern);
            }

            var s = sb.ToString();

            if (qaApproved)
            {
                string suffix = string.Format("_{0}_", characters);
                if (characters >= suffix.Length)
                {
                    s = s.Substring(0, characters - suffix.Length);
                    s = string.Concat(s, suffix);
                }
                else
                {
                    s = s.Substring(0, characters);
                }
            }
            else
            {
                s = s.Substring(0, characters);
            }
            return s;

        }


        #region IQaStringGeneratorService Members

        string IQaStringGeneratorService.Generate(int characters)
        {
            return doGenerate(characters, AlphabetSoup, false);
        }

        string IQaStringGeneratorService.Generate(int characters, string pattern)
        {
            return doGenerate(characters, pattern, false);
        }

        string IQaStringGeneratorService.Generate(int characters, bool qaApproved)
        {
            return doGenerate(characters, AlphabetSoup, qaApproved);
        }

        string IQaStringGeneratorService.Generate(int characters, string pattern, bool qaApproved)
        {
            return doGenerate(characters, pattern, qaApproved);
        }

        #endregion
    }
}
