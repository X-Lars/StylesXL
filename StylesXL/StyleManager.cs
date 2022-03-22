using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace StylesXL
{
    /// <summary>
    /// Manages the appearance of an application's controls.
    /// </summary>
    public class StyleManager : DynamicResourceExtension
    {
        #region Constants

        /// <summary>
        /// Defines the base uri for all resources.
        /// </summary>
        private const string _BaseURI = "/StylesXL;Component/Resources/";

        /// <summary>
        /// Defines the uri for all appearances.
        /// </summary>
        /// <remarks><i>Use a string format to inject the filename. <code>string.format(_AppearanceUri, <filename>)</code></i></remarks>
        private const string _AppearancesURI = _BaseURI + "Layouts/{0}.xaml";

        /// <summary>
        /// Defines the uri for all styles.
        /// </summary>
        /// <remarks><i>Use a string format to inject the filename. <code>string.format(_AppearanceUri, <filename>)</code></i></remarks>
        private const string _ThemesURI = _BaseURI + "Themes/{0}.xaml";

        #endregion

        #region Fields

        /// <summary>
        /// Stores the applied style.
        /// </summary>
        private static XLThemes _Theme = XLThemes.Default;

        /// <summary>
        /// Stores the applied layouts.
        /// </summary>
        private static XLLayoutOptions _Layout = XLLayoutOptions.Default;

        /// <summary>
        /// Stores the current theme's resource dictionary.
        /// </summary>
        private static readonly ResourceDictionary _CurrentTheme = new();

        /// <summary>
        /// Stores a list of resource dictionaries for the currently selected layout options.
        /// </summary>
        private static readonly List<ResourceDictionary> _CurrentLayoutOptions = new();

        /// <summary>
        /// Stores the instance's style property type used for XAML property type validation.
        /// </summary>
        private Type _StylePropertyType;

        /// <summary>
        /// Stores the instance's style resource ID.
        /// </summary>
        private string _StyleResourceID;

        #endregion

        #region Events

        /// <summary>
        /// Event to raise when a style is changed.
        /// </summary>
        public static event EventHandler StyleChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates the static <see cref="StyleManager"/> instance.
        /// </summary>
        static StyleManager() 
        {
            ApplyTheme();
            ApplyLayoutOptions();
        }

        #endregion

        #region Properties

        public IValueConverter Converter { get; set; }
        public object ConverterParameter { get; set; }
        /// <summary>
        /// Gets or sets the applied theme.
        /// </summary>
        public static XLThemes Theme
        {
            get => _Theme;
            set
            {
                if (_Theme != value)
                {
                    _Theme = value;
                    ApplyTheme();
                }
            }
        }

        /// <summary>
        /// Gets or sets the applied layouts.
        /// </summary>
        public static XLLayoutOptions Layouts
        {
            get => _Layout;
            set
            {
                if (_Layout != value)
                {
                    _Layout = value;
                    ApplyLayoutOptions();
                }
            }
        }

        /// <summary>
        /// Sets the dynamice resource's <see cref="ResourceKey"/>.
        /// </summary>
        [ConstructorArgument("resourceKey")]
        public XLBrushes BrushID
        {
            set
            {
                _StylePropertyType = typeof(Brush);
                _StyleResourceID = $"{value}Brush";

                ResourceKey = new ComponentResourceKey(typeof(Styles), _StyleResourceID);
            }
        }

        /// <summary>
        /// Sets the dynamic resource's <see cref="ResourceKey"/>.
        /// </summary>
        [ConstructorArgument("resourceKey")]
        public XLLayout LayoutID
        {
            set
            {
                _StyleResourceID = $"{value}";

                switch(value)
                {
                    case XLLayout.BorderThickness:
                    case XLLayout.Margin:
                    case XLLayout.Padding:
                        _StylePropertyType = typeof(Thickness);
                        break;
                    case XLLayout.CornerRadius:
                        _StylePropertyType = typeof(CornerRadius);
                        break;
                }

                ResourceKey = new ComponentResourceKey(typeof(Styles), _StyleResourceID);
            }
        }

        /// <summary>
        /// Gets wheter the application is in designmode.
        /// </summary>
        private static bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(new DependencyObject());

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the static <see cref="StyleManager"/> instance.
        /// </summary>
        public static void Initialize() {}

        /// <summary>
        /// Gets a style resource with given ID from the current application runtime resources.
        /// </summary>
        /// <param name="ID">A style resource ID.</param>
        /// <returns>The style object with the given ID.</returns>
        /// <exception cref="KeyNotFoundException">When a style resource with the given key cannot be found.</exception>
        public static object GetStyle(string ID)
        {
            try
            {
                return Application.Current.FindResource(new ComponentResourceKey(typeof(Styles), ID));
            }
            catch
            {
                throw new Exception($"[{nameof(StyleManager)}.{nameof(GetStyle)}]" +
                                    $"The style resource with ID {ID} cannot be resolved.");
            }
        }

        /// <summary>
        /// Gets the given brush from the current application runtime resources.
        /// </summary>
        /// <param name="ID">The brush ID.</param>
        /// <returns>The <see cref="System.Windows.Media.Brush"/> with the given ID.</returns>
        public static Brush Brush(XLBrushes ID)
        {
            return (Brush)GetStyle($"{ID}Brush");
        }

        /// <summary>
        /// Gets the value of the given layout property from the current application runtime resources.
        /// </summary>
        /// <param name="ID">The ID of the layout property.</param>
        /// <returns>The value of the layout property with the given ID.</returns>
        public static object GetLayoutValue(XLLayout ID)
        {
            return GetStyle($"{ID}");
        }

        /// <summary>
        /// Applies the currently selected theme.
        /// </summary>
        private static void ApplyTheme()
        {
            Collection<ResourceDictionary> dictionaries = Application.Current.Resources.MergedDictionaries;

            // REMOVE THE APPLIED THEME DICTIONARY
            dictionaries.Remove(_CurrentTheme);

            _CurrentTheme.Source = new Uri(string.Format(_ThemesURI, Theme), UriKind.Relative);
            
            // MERGE THE NEW THEME DICTIONARY INTO THE APPLICATION DICTIONARY
            dictionaries.Add(_CurrentTheme);

            StyleChanged?.Invoke(null, EventArgs.Empty);
        }

        /// <summary>
        /// Applies the currently selected layout options.
        /// </summary>
        private static void ApplyLayoutOptions()
        {
            Collection<ResourceDictionary> dictionaries = Application.Current.Resources.MergedDictionaries;

            Array options = Enum.GetValues(typeof(XLLayoutOptions));

            // REMOVE ALL APPLIED LAYOUT DICTIONARIES
            foreach (var dictionary in _CurrentLayoutOptions)
            {
                dictionaries.Remove(dictionary);
            }

            _CurrentLayoutOptions.Clear();

            // CREATE A DICTIONARY FOR EACH FLAGGED LAYOUT
            foreach (XLLayoutOptions option in options)
            {
                if ((_Layout & option) == option)
                {
                    switch (option)
                    {
                        case XLLayoutOptions.Flat:
                            _CurrentLayoutOptions.Add(new ResourceDictionary() { Source = new Uri(string.Format(_AppearancesURI, XLLayoutOptions.Flat), UriKind.Relative) });
                            break;
                        case XLLayoutOptions.Default:
                            _CurrentLayoutOptions.Add(new ResourceDictionary() { Source = new Uri(string.Format(_AppearancesURI, XLLayoutOptions.Default), UriKind.Relative) });
                            break;
                        case XLLayoutOptions.Strong:
                            _CurrentLayoutOptions.Add(new ResourceDictionary() { Source = new Uri(string.Format(_AppearancesURI, XLLayoutOptions.Strong), UriKind.Relative) });
                            break;
                        case XLLayoutOptions.Rounded:
                            _CurrentLayoutOptions.Add(new ResourceDictionary() { Source = new Uri(string.Format(_AppearancesURI, XLLayoutOptions.Rounded), UriKind.Relative) });
                            break;
                    }
                }
            }

            // MERGE THE LAYOUT DICTIONARIES INTO THE APPLICATION DICTIONARY
            foreach (var dictionary in _CurrentLayoutOptions)
            {
                dictionaries.Add(dictionary);
            }

            StyleChanged?.Invoke(null, EventArgs.Empty);
        }


        /// <summary>
        /// Gets a <see cref="StyleManager"/> controlled style for the given control type.
        /// </summary>
        /// <param name="controltype">The type of control.</param>
        /// <returns>A <see cref="Theme"/> for the control that is controlled by the <see cref="StyleManager"/>.</returns>
        /// <exception cref="NotImplementedException">When the style for the given control is not implemented.</exception>
        private static Style GetControlStyle(Type controltype)
        {
            try
            {
                return (Style)Application.Current.FindResource(new ComponentResourceKey(typeof(Styles), controltype.Name));
            }
            catch (Exception ex)
            {
                throw new NotImplementedException($"[{nameof(StyleManager)}.{nameof(GetControlStyle)}]" +
                                                  $"A style for {controltype.Name} is not implemented.", ex);
            }
        }

        /// <summary>
        /// Gets the <see cref="Color"/> from the given brush ID.
        /// </summary>
        /// <param name="brushID">The brush to retreive the color from.</param>
        /// <returns>The color from the given brush.</returns>
        /// <exception cref="Exception">When the brush with the given ID is not found or is not a <see cref="SolidColorBrush"/>.</exception>
        private static Color GetBrushColor(string brushID)
        {
            try
            {
                return ((SolidColorBrush)Application.Current.FindResource(new ComponentResourceKey(typeof(Styles), brushID))).Color;
            }
            catch (Exception ex)
            {
                throw new Exception($"[{nameof(StyleManager)}.{nameof(ProvideValue)}]" +
                                    $"The color cannot be extracted from the {brushID}.", ex);
            }
        }

        #endregion

        #region Overrides: DynamicResourceExtension

        /// <summary>
        /// Assigns a dynamic resource to the bound property.
        /// </summary>
        /// <param name="provider">The service provider providing services for the markup extension.</param>
        /// <returns>The object to set on the property.</returns>
        public override object ProvideValue(IServiceProvider provider)
        {
            IProvideValueTarget targetprovider = (IProvideValueTarget)provider.GetService(typeof(IProvideValueTarget));

            if (targetprovider.TargetObject is not DependencyObject targetobject)
                return null;

            if (targetprovider.TargetProperty is not DependencyProperty targetproperty)
                return null;

            // STYLE PROPERTY
            if (targetproperty.PropertyType == typeof(Style))
            {
                return GetControlStyle(targetobject.GetType());
            }

            // COLOR PROPERTY
            if(targetproperty.PropertyType == typeof(Color))
            {
                return GetBrushColor(_StyleResourceID);
            }

            // VALIDATE PROPERTY TYPE
            if (targetproperty.PropertyType != _StylePropertyType)
            {
                throw new Exception($"[{nameof(StyleManager)}.{nameof(ProvideValue)}]" +
                                    $"The {_StylePropertyType} cannot be set on a property of type {targetproperty.PropertyType}.");
            }

            return base.ProvideValue(provider);
        }

        #endregion
    }
}
