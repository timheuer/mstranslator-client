using System;
using System.Collections.Generic;

namespace TimHeuer.Silverlight
{
    /// <summary>
    /// The <see cref="System.EventArgs"/> of the <see cref="TimHeuer.Silverlight.TranslatorClient.GetLanguagesForTranslateAsync"/> completed event.
    /// </summary>
    public class GetLanguagesForTranslateEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the available languages.
        /// </summary>
        /// <value>The available languages for <see cref="TimHeuer.Silverlight.TranslatorClient.TranslateAsync(string, string)"/>.</value>
        public List<string> AvailableLanguages { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetLanguagesForTranslateEventArgs"/> class.
        /// </summary>
        /// <param name="languageList">The supported language list for <see cref="TimHeuer.Silverlight.TranslatorClient.TranslateAsync(string, string)"/>.</param>
        internal GetLanguagesForTranslateEventArgs(List<string> languageList)
        {
            AvailableLanguages = languageList;
        }
    }
}