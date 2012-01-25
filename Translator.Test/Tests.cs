using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimHeuer.Silverlight;

namespace Translator.Test
{
    [TestClass]
    public class Tests
    {
        TranslatorClient _translator;

        public Tests()
        {
            _translator = new TranslatorClient(App.Current.Resources["AppId"].ToString());
        }

        [TestMethod]
        public void VerifyTranslatorClientCreation()
        {
            Assert.IsNotNull(_translator, "No instance of TranslatorClient found");
        }

        [TestMethod]
        public void VerifyApplicationId()
        {
            Assert.IsNotNull(_translator.ApplicationId);
        }

        [TestMethod]
        public void VerifyEnglishDetection()
        {
            string englishText = "The quick brown fox jumped over the lazy dog.";

            _translator.DetectCompleted += ((s, args) =>
                {
                    Assert.AreEqual("en", args.TwoLetterISOLanguageName);
                });

            _translator.DetectAsync(englishText);
        }

        [TestMethod]
        public void VerifyGermanDetection()
        {
            string germanText = "Der schnelle braune Fuchs sprang über den faulen Hund.";

            _translator.DetectCompleted += ((s, args) =>
            {
                Assert.AreEqual("de", args.TwoLetterISOLanguageName);
            });

            _translator.DetectAsync(germanText);
        }

        [TestMethod]
        public void VerifyFrenchDetection()
        {
            string frenchText = "La rapide renard brun sauté sur le chien paresseux.";

            _translator.DetectCompleted += ((s, args) =>
            {
                Assert.AreEqual("fr", args.TwoLetterISOLanguageName);
            });

            _translator.DetectAsync(frenchText);
        }
    }
}