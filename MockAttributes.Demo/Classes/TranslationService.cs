using System;
using System.Globalization;

namespace MockAttributes.Demo.Classes
{
    public interface ITranslationService
    {
        string Translate(string text, CultureInfo culture);
    }

    public class TranslationService : ITranslationService
    {
        public string Translate(string text, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
