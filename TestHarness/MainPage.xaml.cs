using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Threading;

namespace TestHarness
{
    public partial class MainPage : UserControl
    {
        private string _appId;
        private string _currentLang;
        private TimHeuer.Silverlight.TranslatorClient _translator;
        private bool _audio = false;
        private string _targetLang;

        public MainPage()
        {
            InitializeComponent();

            _appId = App.Current.Resources["AppId"].ToString();
            _currentLang = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

            _translator = new TimHeuer.Silverlight.TranslatorClient(_appId);
            _translator.DetectCompleted += new EventHandler<TimHeuer.Silverlight.DetectCompletedEventArgs>(OnDetectCompleted);
            _translator.TranslateCompleted += new EventHandler<TimHeuer.Silverlight.TranslateCompletedEventArgs>(OnTranslateCompleted);
            _translator.TranslateLanguagesCompleted += new EventHandler<TimHeuer.Silverlight.GetLanguagesForTranslateEventArgs>(OnTranslateLanguagesCompleted);
            _translator.SpeakLanguagesCompleted += new EventHandler<TimHeuer.Silverlight.GetLanguagesForSpeakEventArgs>(OnSpeakLanguagesCompleted);
            _translator.SpeakCompleted += new EventHandler<TimHeuer.Silverlight.SpeakCompletedEventArgs>(OnSpeakCompleted);


            Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void OnSpeakCompleted(object sender, TimHeuer.Silverlight.SpeakCompletedEventArgs e)
        {
            WaveMSS.WaveMediaStreamSource mss = new WaveMSS.WaveMediaStreamSource(e.AudioTranslation);
            TranslatedPlayback.SetSource(mss);
        }

        void OnSpeakLanguagesCompleted(object sender, TimHeuer.Silverlight.GetLanguagesForSpeakEventArgs e)
        {
            SpeechLanguages.ItemsSource = e.AvailableLanguages;
            SpeechLanguages.SelectedIndex = 0;
        }

        void OnTranslateLanguagesCompleted(object sender, TimHeuer.Silverlight.GetLanguagesForTranslateEventArgs e)
        {
            TextLanguages.ItemsSource = e.AvailableLanguages;
            TextLanguages.SelectedIndex = 0;
        }

        void OnTranslateCompleted(object sender, TimHeuer.Silverlight.TranslateCompletedEventArgs e)
        {
            if (_audio)
            {
                _translator.SpeakAsync(e.TranslatedText, _targetLang);
            }
            else
            {
                MessageBox.Show(e.TranslatedText);
            }
        }

        void OnDetectCompleted(object sender, TimHeuer.Silverlight.DetectCompletedEventArgs e)
        {
            _translator.TargetTwoLetterISOLanguage = _targetLang;
            _translator.TranslateAsync(TextToTranslate.Text, Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName, _targetLang);
            
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // get list of languages available for speech
            _translator.GetLanguagesForSpeakAsync();

            // get list of languages available for text
            _translator.GetLanguagesForTranslateAsync();
        }

        private void TranslateTextToAudio(object sender, RoutedEventArgs e)
        {
            if (SpeechLanguages.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a language first...");
                return;
            }
            _audio = true;
            _targetLang = SpeechLanguages.SelectedItem.ToString();
            _translator.DetectAsync(TextToTranslate.Text);
        }

        private void TranslateText(object sender, RoutedEventArgs e)
        {
            if (TextLanguages.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a language first...");
                return;
            }
            _targetLang = TextLanguages.SelectedItem.ToString();
            _translator.DetectAsync(TextToTranslate.Text);
        }
    }
}