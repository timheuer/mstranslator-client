using System;

namespace TimHeuer.Silverlight
{
    /// <summary>
    /// The <see cref="System.EventArgs"/> of the <see cref="TimHeuer.Silverlight.TranslatorClient.DetectAsync"/> completed event.
    /// </summary>
    public sealed class DetectCompletedEventArgs : EventArgs
    {
        /// <summary>
        /// The ISO 639-1 two-letter code for the language
        /// </summary>
        public string TwoLetterISOLanguageName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DetectCompletedEventArgs"/> class.
        /// </summary>
        /// <param name="twoLetterISOLanguageName">Name of the two letter ISO 639-1 language detected.</param>
        internal DetectCompletedEventArgs(string twoLetterISOLanguageName)
        {
            TwoLetterISOLanguageName = twoLetterISOLanguageName;
        }
    }
}