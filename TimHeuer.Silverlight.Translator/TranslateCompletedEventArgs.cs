using System;

namespace TimHeuer.Silverlight
{

    /// <summary>
    /// The <see cref="System.EventArgs"/> of the <see cref="TimHeuer.Silverlight.TranslatorClient.TranslateAsync(string,string)"/> completed event
    /// </summary>
    public sealed class TranslateCompletedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the translated text.
        /// </summary>
        /// <value>The translated text.</value>
        public string TranslatedText { get; private set; }
        /// <summary>
        /// Gets or sets the name of the target two letter ISO 639-1 language code.
        /// </summary>
        /// <value>The name of the target two letter ISO 639-1 language code.</value>
        public string TargetTwoLetterISOLanguageName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateCompletedEventArgs"/> class.
        /// </summary>
        /// <param name="targetLanguage">The target two letter ISO 639-1 language code.</param>
        /// <param name="translatedText">The translated text.</param>
        internal TranslateCompletedEventArgs(string targetLanguage, string translatedText)
        {
            TranslatedText = translatedText;
            TargetTwoLetterISOLanguageName = targetLanguage;
        }
    }
}