using System;
using System.IO;

namespace TimHeuer.Silverlight
{
    /// <summary>
    /// The <see cref="System.EventArgs"/> of the <see cref="TimHeuer.Silverlight.TranslatorClient.SpeakAsync"/> completed event.
    /// </summary>
    public class SpeakCompletedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the audio translation.
        /// </summary>
        /// <value>The audio translation in audio/wav format.</value>
        public Stream AudioTranslation { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpeakCompletedEventArgs"/> class.
        /// </summary>
        /// <param name="audio">The audio in audio/wav format.</param>
        internal SpeakCompletedEventArgs(Stream audio)
        {
            AudioTranslation = audio;
        }
    }
}