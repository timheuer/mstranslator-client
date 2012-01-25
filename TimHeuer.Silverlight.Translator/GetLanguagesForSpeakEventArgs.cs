using System;
using System.Collections.Generic;

namespace TimHeuer.Silverlight
{
    /// <summary>
    /// The <see cref="System.EventArgs"/> of the <see cref="TimHeuer.Silverlight.TranslatorClient.GetLanguagesForSpeakAsync"/> completed event.
    /// </summary>
    public class GetLanguagesForSpeakEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the available languages.
        /// </summary>
        /// <value>The available languages for <see cref="TimHeuer.Silverlight.TranslatorClient.SpeakAsync"/>.</value>
        public List<string> AvailableLanguages { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetLanguagesForSpeakEventArgs"/> class.
        /// </summary>
        /// <param name="languageList">The supported language list for <see cref="TimHeuer.Silverlight.TranslatorClient.SpeakAsync"/>.</param>
        internal GetLanguagesForSpeakEventArgs(List<string> languageList)
        {
            AvailableLanguages = languageList;
        }
    }
}