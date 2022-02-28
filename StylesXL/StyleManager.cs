using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Diagnostics;
//using ToolsXL.Config;

namespace StylesXL
{
    /// <summary>
    /// Manages the appearance of the custom controls at runtime.
    /// </summary>
    public class StyleManager : MarkupExtension
    {
        #region Constants

        /// <summary>
        /// Defines the base uri for all resources.
        /// </summary>
        private const string _BaseUri = "/StylesXL;Component/Resources/";

        /// <summary>
        /// Defines the uri for all appearances.
        /// </summary>
        /// <remarks><i>Use a string format to inject the filename. <code>string.format(_AppearanceUri, <filename>)</code></i></remarks>
        private const string _AppearancesUri = _BaseUri + "Appearance/{0}.xaml";

        /// <summary>
        /// Defines the uri for all styles.
        /// </summary>
        /// <remarks><i>Use a string format to inject the filename. <code>string.format(_AppearanceUri, <filename>)</code></i></remarks>
        private const string _StylesUri = _BaseUri + "Style/{0}.xaml";

        #endregion

        #region Fields

        /// <summary>
        /// Stores the applied style.
        /// </summary>
        private static ControlStyle _Style = ControlStyle.Default;

        /// <summary>
        /// Stores the applied appearance.
        /// </summary>
        private static ControlAppearance _Appearance = ControlAppearance.Default;

        /// <summary>
        /// Stores the <see cref="ResourceDictionary"/> containing the dictionary keys for the current style.
        /// </summary>
        private static readonly ResourceDictionary _CurrentStyle = new();

        /// <summary>
        /// Stores the list of resource dictionaries containing the dictonary keys for all current appearances.
        /// </summary>
        private static readonly List<ResourceDictionary> _CurrentAppearances = new();

        #endregion

        #region Events

        /// <summary>
        /// Defines the event to raise when the applied style is changed.
        /// </summary>
        public static event EventHandler StyleChanged;

        /// <summary>
        /// Defines the event to raise when the applied appearance is changed.
        /// </summary>
        public static event EventHandler AppearanceChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Static constructor called before the instance of <see cref="StyleManager"/> is created.
        /// </summary>
        static StyleManager()
        {
            //Style = ControlStyle.Default;
            //if (!IsInDesignMode)
            //    Config<StyleConfig>.Print();
        }

        // TODO: Remove or use for binding styles
        public StyleManager() 
        {
            //Style = ControlStyle.Default;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the style to apply.
        /// </summary>
        public static ControlStyle Style
        {
            get { return _Style; }
            set
            {
                if (_Style != value)
                {
                    _Style = value;
                    ApplySkinStyle();
                }
            }
        }

        /// <summary>
        /// Gets or sets the appearance to apply.
        /// </summary>
        public static ControlAppearance Appearance
        {
            get { return _Appearance; }
            set
            {
                if (_Appearance != value)
                {
                    _Appearance = value;
                    InvalidateAppearance();
                    ApplyAppearance();
                }
            }
        }

        /// <summary>
        /// Gets wheter the application is in designmode.
        /// </summary>
        private static bool IsInDesignMode
        {
            get { return DesignerProperties.GetIsInDesignMode(new DependencyObject()); }
        }

        /// <summary>
        /// Gets or sets the key used by the <see cref="ProvideValue(IServiceProvider)"/> method.
        /// </summary>
        public string Key { get; set; }

        #region Properties: Appearance

        public static CornerRadius CornerRadius 
        { 
            get
            {
                return (CornerRadius)GetStyleValue(Layout.CornerRadius);
            }
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Constructs and initializes the manager is constructed.
        /// </summary>
        public static void Initialize() { }

        /// <summary>
        /// Gets the value of the specified component resource from the current application runtime resources.
        /// </summary>
        /// <param name="ID">A <see cref="string"/> specifying the component resource key ID.</param>
        /// <returns>A <see cref="object"/> containing the requested component's value.</returns>
        public static object GetStyleValue(string ID)
        {
            return Application.Current.FindResource(new ComponentResourceKey(typeof(Styles), ID));
        }

        /// <summary>
        /// Gets the specified brush from the current application runtime resources.
        /// </summary>
        /// <param name="ID">A constant string defined in the <see cref="Styles"/> class specifying the brush to get.</param>
        /// <returns></returns>
        public static Brush Brush(string ID)
        {
            //if (!IsInDesignMode)
            //{
            //    // Check if user color is saved in config
            //    string configColor = Config<StyleConfig>.GetProperty(ID);

            //    if (configColor != null && configColor != string.Empty)
            //    {
            //        if (TypeDescriptor.GetConverter(typeof(Color)).IsValid(configColor))
            //        {
            //            Color color = (Color)ColorConverter.ConvertFromString(configColor);

            //            // Return user brush
            //            return new SolidColorBrush(color);
            //        }
            //    }
            //}
            return (Brush)GetStyleValue(ID);
            //return (Brush)Application.Current.FindResource(new ComponentResourceKey(typeof(Styles), ID));
        }

        /// <summary>
        /// Gets the specified color from the current application runtime resources.
        /// </summary>
        /// <param name="ID">A constant string defined in the <see cref="Styles"/> class specifying the color to get.</param>
        /// <returns></returns>
        public static Color GetColor(string ID)
        {
            //if (!IsInDesignMode)
            //{
            //    // Check if user color is saved in config
            //    string configColor = Config<StyleConfig>.GetProperty(ID);

            //    if (configColor != null && configColor != string.Empty)
            //    {
            //        if (TypeDescriptor.GetConverter(typeof(Color)).IsValid(configColor))
            //        {
            //            Color color = (Color)ColorConverter.ConvertFromString(configColor);

            //            // Return user color
            //            return color;
            //        }
            //    }
            //}

            return (Color)GetStyleValue(ID);
            //return (Color)Application.Current.FindResource(new ComponentResourceKey(typeof(Styles), ID));
        }

        /// <summary>
        /// Sets the specified color to override the style manager defaults.
        /// </summary>
        /// <param name="ID">A constant string defined in the <see cref="Styles"/> class specifying the color to override.</param>
        /// <param name="color">The color to set.</param>
        public static void SetColor(string ID, Color color)
        {
            //if (!IsInDesignMode)
            //{
            //    Config<StyleConfig>.SetProperty(ID, color.ToString());

            //    StyleChanged?.Invoke(null, EventArgs.Empty);
            //}
        }

        /// <summary>
        /// Clears the specified color override.
        /// </summary>
        /// <param name="ID">A constant string defined in the <see cref="Styles"/> class specifying the color to override.</param>
        public static void ClearColor(string ID)
        {
            //if (!IsInDesignMode)
            //{
            //    Config<StyleConfig>.SetProperty(ID, null);

            //    StyleChanged?.Invoke(null, EventArgs.Empty);
            //}
        }

        /// <summary>
        /// Applies the currently selected skin.
        /// </summary>
        private static void ApplySkinStyle()
        {
            Collection<ResourceDictionary> dictionaries = Application.Current.Resources.MergedDictionaries;

            dictionaries.Remove(_CurrentStyle);

            _CurrentStyle.Source = new Uri(string.Format(_StylesUri, Style), UriKind.Relative);
            
            dictionaries.Add(_CurrentStyle);

            // Raise the style changed event
            StyleChanged?.Invoke(null, EventArgs.Empty);
        }

        /// <summary>
        /// Applies the currently selected appearance.
        /// </summary>
        private static void ApplyAppearance()
        {
            Collection<ResourceDictionary> dictionaries = Application.Current.Resources.MergedDictionaries;

            Array appearances = Enum.GetValues(typeof(ControlAppearance));

            // Remove all current appearance dictionaries
            foreach (ResourceDictionary dictionary in _CurrentAppearances)
            {
                dictionaries.Remove(dictionary);
            }

            _CurrentAppearances.Clear();

            // Create a resource dictionary for each control appearance flag
            foreach (ControlAppearance appearance in appearances)
            {
                if ((_Appearance & appearance) == appearance)
                {
                    switch (appearance)
                    {
                        case ControlAppearance.Flat:
                            _CurrentAppearances.Add(new ResourceDictionary() { Source = new Uri(string.Format(_AppearancesUri, ControlAppearance.Flat), UriKind.Relative) });
                            break;
                        case ControlAppearance.Default:
                            _CurrentAppearances.Add(new ResourceDictionary() { Source = new Uri(string.Format(_AppearancesUri, ControlAppearance.Default), UriKind.Relative) });
                            break;
                        case ControlAppearance.Strong:
                            _CurrentAppearances.Add(new ResourceDictionary() { Source = new Uri(string.Format(_AppearancesUri, ControlAppearance.Strong), UriKind.Relative) });
                            break;
                        case ControlAppearance.Rounded:
                            _CurrentAppearances.Add(new ResourceDictionary() { Source = new Uri(string.Format(_AppearancesUri, ControlAppearance.Rounded), UriKind.Relative) });
                            break;
                    }
                }
            }

            // Add all dictionaries to the application's merged dictionaries
            foreach (ResourceDictionary dictionary in _CurrentAppearances)
            {
                dictionaries.Add(dictionary);
            }

            // Raise the appearance changed event
            AppearanceChanged?.Invoke(null, EventArgs.Empty);
        }

        /// <summary>
        /// Invalidates the current appearance by removing colliding appearances.
        /// </summary>
        /// <remarks><i>Remove colliding appearances using XOR assignment operator.</i></remarks>
        private static void InvalidateAppearance()
        {
            // Strong overrides default
            //if (_Appearance.HasFlag(ControlAppearance.Default) && _Appearance.HasFlag(ControlAppearance.Strong))
            //{
            //    // Toggle default off
            //    _Appearance ^= ControlAppearance.Default;
            //}

            // Rounded overrides default
            //if(_Appearance.HasFlag(ControlAppearance.Rounded))
            //{
            //    _Appearance ^= ControlAppearance.Default;
            //}
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Implements the abstract <see cref="MarkupExtension.ProvideValue(IServiceProvider)"/> method to return the component resource specified by the <see cref="Key"/> property.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
          
            return GetStyleValue(Key);
        }

        #endregion

        
    }
}
