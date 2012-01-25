using System;
using System.Net;
using System.Xml.Serialization;
using System.Windows.Browser;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace TimHeuer.Silverlight
{
    /// <summary>
    /// The main client for the interaction with Microsoft Translator APIs.  This class provides the methods and properties for 
    /// instantiating an HTTP-based communication with the API.
    /// </summary>
    public class TranslatorClient
    {
        /// <summary>
        /// Gets or sets the Microsoft Translator Application ID
        /// </summary>
        /// <value>Your Microsoft Translator API Application ID.</value>
        public string ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the ISO 639-1 two-letter code for the target translation language.
        /// </summary>
        /// <value>The target two letter ISO 639-1 language code.</value>
        public string TargetTwoLetterISOLanguage { get; set; }

        /// <summary>
        /// Occurs when <see cref="DetectAsync"/> method has completed.
        /// </summary>
        public event EventHandler<DetectCompletedEventArgs> DetectCompleted;

        /// <summary>
        /// Occurs when <see cref="TranslateAsync(string, string)"/> method has completed.
        /// </summary>
        public event EventHandler<TranslateCompletedEventArgs> TranslateCompleted;
        
        /// <summary>
        /// Occurs when <see cref="SpeakAsync"/> method has completed.
        /// </summary>
        public event EventHandler<SpeakCompletedEventArgs> SpeakCompleted;
        
        /// <summary>
        /// Occurs when <see cref="GetLanguagesForSpeakAsync"/> method has completed.
        /// </summary>
        public event EventHandler<GetLanguagesForSpeakEventArgs> SpeakLanguagesCompleted;
        
        /// <summary>
        /// Occurs when <see cref="GetLanguagesForTranslateAsync"/> method has completed.
        /// </summary>
        public event EventHandler<GetLanguagesForTranslateEventArgs> TranslateLanguagesCompleted;

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslatorClient"/> class.
        /// </summary>
        public TranslatorClient() { }


        /// <summary>
        /// Initializes a new instance of the <see cref="TranslatorClient"/> class with the Application ID already set.
        /// </summary>
        /// <param name="ApplicationId">The Microsoft Translator API Application ID.</param>
        public TranslatorClient(string ApplicationId)
        {
            this.ApplicationId = ApplicationId;
        }

        #region GetLanguagesForTranslate Method
        /// <summary>
        /// Gets the languages supported for translation by the Microsoft Translator API.
        /// </summary>
        public void GetLanguagesForTranslateAsync()
        {
            if (string.IsNullOrEmpty(this.ApplicationId))
            {
                throw new ArgumentNullException("ApplicationId", StringResources.Messages.ApplicationIdMissingMessage);
            }

            WebClient translateLanguagesClient = new WebClient();
            translateLanguagesClient.OpenReadCompleted += new OpenReadCompletedEventHandler(OnGetTranslateLanguagesCompleted);
            translateLanguagesClient.OpenReadAsync(new Uri(string.Format(StringResources.ApiUris.TRANSLATE_LANGUAGES_URI, ApplicationId)));

        }

        /// <summary>
        /// Called when <see cref="GetLanguagesForTranslateAsync"/> is completed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing a List&lt;T&gt; of <see cref="System.String"/> with two letter ISO 639-1 language codes.</param>
        void OnGetTranslateLanguagesCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                throw (e.Error);
            }
            else if (!e.Cancelled)
            {
                EventHandler<GetLanguagesForTranslateEventArgs> handler = TranslateLanguagesCompleted;

                if (handler != null)
                {
                    DataContractSerializer deserializer = new DataContractSerializer(typeof(List<string>));
                    List<string> translateLanguages = deserializer.ReadObject(e.Result) as List<string>;
                    GetLanguagesForTranslateEventArgs args = new GetLanguagesForTranslateEventArgs(translateLanguages);
                    handler(this, args);
                }
            }
        }
        #endregion

        #region GetLanguagesForSpeak Method
        /// <summary>
        /// Gets the languages supported for text-to-speech translation by the Microsoft Translator API.
        /// </summary>
        public void GetLanguagesForSpeakAsync()
        {
            if (string.IsNullOrEmpty(this.ApplicationId))
            {
                throw new ArgumentNullException("ApplicationId", StringResources.Messages.ApplicationIdMissingMessage);
            }

            WebClient speakLanguagesClient = new WebClient();
            speakLanguagesClient.OpenReadCompleted += new OpenReadCompletedEventHandler(OnGetSpeakLanguagesCompleted);
            speakLanguagesClient.OpenReadAsync(new Uri(string.Format(StringResources.ApiUris.SPEAK_LANGUAGES_URI, ApplicationId)));

        }

        /// <summary>
        /// Called when <see cref="GetLanguagesForSpeakAsync"/> has completed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing a List&lt;T&gt; of <see cref="System.String"/> with two letter ISO 639-1 language codes.</param>
        void OnGetSpeakLanguagesCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                throw (e.Error);
            }
            else if (!e.Cancelled)
            {
                EventHandler<GetLanguagesForSpeakEventArgs> handler = SpeakLanguagesCompleted;

                if (handler != null)
                {
                    DataContractSerializer deserializer = new DataContractSerializer(typeof(List<string>));

                    List<string> speakLanguages;
                    try
                    {
                        speakLanguages = deserializer.ReadObject(e.Result) as List<string>;
                    }
                    catch (SerializationException ex)
                    {
                        throw new Exception(StringResources.Messages.SerializationApiException, ex);
                    }

                    GetLanguagesForSpeakEventArgs args = new GetLanguagesForSpeakEventArgs(speakLanguages);
                    handler(this, args);
                }
            }
        }
        #endregion

        #region Speak Method
        /// <summary>
        /// Calls the Microsoft Translator Speak API with the text to speak and the language to use.  This does NOT perform 
        /// a translation on the text and will speak the text in the targeted language.  This should ideally be combined with <see cref="TranslateAsync(string, string)"/> 
        /// for translate and speak combination.
        /// </summary>
        /// <param name="text">The translated text to Speak.</param>
        /// <param name="language">The supported language to Speak the translated text.</param>
        public void SpeakAsync(string text, string language)
        {
            if (string.IsNullOrEmpty(this.ApplicationId))
            {
                throw new ArgumentNullException("ApplicationId", StringResources.Messages.ApplicationIdMissingMessage);
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text", StringResources.Messages.NoTextToTranslateMessage);
            }
            else
            {
                WebClient speakClient = new WebClient();
                speakClient.OpenReadCompleted += new OpenReadCompletedEventHandler(OnSpeakCompleted);
                speakClient.OpenReadAsync(new Uri(string.Format(StringResources.ApiUris.SPEAK_URI, ApplicationId, HttpUtility.UrlEncode(text), language)));
            }
        }

        /// <summary>
        /// Called when <see cref="SpeakAsync"/> has completed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the <see cref="System.IO.Stream"/> in audio/wav format.</param>
        void OnSpeakCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                throw (e.Error);
            }
            else if (!e.Cancelled)
            {
                EventHandler<SpeakCompletedEventArgs> handler = SpeakCompleted;

                if (handler != null)
                {
                    SpeakCompletedEventArgs args = new SpeakCompletedEventArgs(e.Result);
                    handler(this, args);
                }
            }
        }
        #endregion

        #region Detect Method
        /// <summary>
        /// Detects the language of the provided text.
        /// </summary>
        /// <param name="text">The text to identify the source language.</param>
        public void DetectAsync(string text)
        {
            if (string.IsNullOrEmpty(this.ApplicationId))
            {
                throw new ArgumentNullException("ApplicationId", StringResources.Messages.ApplicationIdMissingMessage);
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text", StringResources.Messages.NoTextToTranslateMessage);
            }
            else
            {
                WebClient detectionClient = new WebClient();
                detectionClient.OpenReadCompleted += new OpenReadCompletedEventHandler(OnDetectCompleted);
                detectionClient.OpenReadAsync(new Uri(string.Format(StringResources.ApiUris.DETECT_URI, ApplicationId, HttpUtility.UrlEncode(text))));
            }
        }

        /// <summary>
        /// Called when <see cref="DetectAsync"/> has completed.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the two letter ISO 639-1 language code detected.</param>
        void OnDetectCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                throw (e.Error);
            }
            else if (!e.Cancelled)
            {
                EventHandler<DetectCompletedEventArgs> handler = DetectCompleted;

                if (handler != null)
                {
                    DataContractSerializer deserializer = new DataContractSerializer(typeof(string));
                    string responseText;
                    try
                    {
                        responseText = deserializer.ReadObject(e.Result) as string;
                    }
                    catch (SerializationException ex)
                    {
                        throw new Exception(StringResources.Messages.SerializationApiException, ex);
                    }

                    DetectCompletedEventArgs args = new DetectCompletedEventArgs(responseText);
                    handler(this, args);
                }
            }
        }
        #endregion

        #region Translate Method
        /// <summary>
        /// Translates the provided text into the <see cref="TargetTwoLetterISOLanguage"/>.
        /// </summary>
        /// <param name="text">The text to translate.</param>
        /// <param name="sourceLanguage">The two letter ISO 639-1 language code of the SOURCE language.</param>.
        public void TranslateAsync(string text, string sourceLanguage)
        {
            TranslateAsync(text, sourceLanguage, this.TargetTwoLetterISOLanguage);
        }
        /// <summary>
        /// Translates the provided text into the two letter ISO 639-1 language code provided.
        /// </summary>
        /// <param name="text">The text to translate.</param>
        /// <param name="sourceLanguage">The two letter ISO 639-1 language code of the SOURCE language.</param>.
        /// <param name="targetLanguage">The two letter ISO 639-1 language code of the TARGET language.</param>.
        public void TranslateAsync(string text, string sourceLanguage, string targetLanguage)
        {
            this.TargetTwoLetterISOLanguage = targetLanguage;

            if (string.IsNullOrEmpty(this.ApplicationId))
            {
                throw new ArgumentNullException("ApplicationId", StringResources.Messages.ApplicationIdMissingMessage);
            }

            if (string.IsNullOrEmpty(text) || string.Compare(sourceLanguage, targetLanguage, StringComparison.OrdinalIgnoreCase) == 0)
            {
                //  throw new ArgumentNullException("text", "Some text must be provided in order to detect the language");
                // Nothing to translate
                TranslateCompleted(this, new TranslateCompletedEventArgs(targetLanguage, text));
            }
            else
            {
                WebClient translationClient = new WebClient();
                translationClient.OpenReadCompleted += new OpenReadCompletedEventHandler(OnTranslateCompleted);
                translationClient.OpenReadAsync(new Uri(string.Format(StringResources.ApiUris.TRANSLATE_URI, ApplicationId, sourceLanguage, targetLanguage, HttpUtility.UrlEncode(text))));
            }
        }

        /// <summary>
        /// Called when <see cref="TranslateAsync(string, string)"/> has completed
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the translated text.</param>
        void OnTranslateCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                throw (e.Error);
            }
            else if (!e.Cancelled)
            {
                EventHandler<TranslateCompletedEventArgs> handler = TranslateCompleted;

                if (handler != null)
                {
                    DataContractSerializer deserializer = new DataContractSerializer(typeof(string));
                    string responseText;
                    try
                    {
                        responseText = deserializer.ReadObject(e.Result) as string;
                    }
                    catch (SerializationException ex)
                    {
                        throw new Exception(StringResources.Messages.SerializationApiException, ex);
                    }

                    TranslateCompletedEventArgs args = new TranslateCompletedEventArgs(this.TargetTwoLetterISOLanguage, responseText);
                    handler(this, args);
                }
            }
        }
        #endregion

        #region TODO
        #region AddTranslation Method
        #endregion

        #region AddTranslationArray Method
        #endregion

        #region BreakSentences Method
        #endregion

        #region DetectArray Method
        #endregion

        #region GetAppIdToken Method
        #endregion

        #region GetTranslations Method
        #endregion

        #region GetTranslationsArray Method
        #endregion

        #region TranslateArray Method
        #endregion
        #endregion
    }
}