﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30128.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TimHeuer.Silverlight.StringResources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ApiUris {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ApiUris() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TimHeuer.Silverlight.StringResources.ApiUris", typeof(ApiUris).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://api.microsofttranslator.com/V2/Http.svc/Detect?appId={0}&amp;text={1}.
        /// </summary>
        internal static string DETECT_URI {
            get {
                return ResourceManager.GetString("DETECT_URI", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://api.microsofttranslator.com/V2/Http.svc/GetLanguagesForSpeak?appId={0}.
        /// </summary>
        internal static string SPEAK_LANGUAGES_URI {
            get {
                return ResourceManager.GetString("SPEAK_LANGUAGES_URI", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://api.microsofttranslator.com/V2/Http.svc/Speak?appId={0}&amp;text={1}&amp;language={2}.
        /// </summary>
        internal static string SPEAK_URI {
            get {
                return ResourceManager.GetString("SPEAK_URI", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://api.microsofttranslator.com/V2/Http.svc/GetLanguagesForTranslate?appId={0}.
        /// </summary>
        internal static string TRANSLATE_LANGUAGES_URI {
            get {
                return ResourceManager.GetString("TRANSLATE_LANGUAGES_URI", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://api.microsofttranslator.com/V2/Http.svc/Translate?appId={0}&amp;from={1}&amp;to={2}&amp;text={3}.
        /// </summary>
        internal static string TRANSLATE_URI {
            get {
                return ResourceManager.GetString("TRANSLATE_URI", resourceCulture);
            }
        }
    }
}
