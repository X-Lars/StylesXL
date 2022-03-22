using System;

namespace StylesXL
{
    /// <summary>
    /// Class serves as namespace for the style resource keys.
    /// </summary>
    public static class Styles { }

    /// <summary>
    /// Defines the <see cref="StylesXL"/> brushes.
    /// </summary>
    /// <remarks><i>
    /// All themes must provide these brushes.
    /// </i></remarks>
    public enum XLBrushes
    {
        App,
        AppBorder,
        AppBorderSelected,
        AppHeader,
        AppHeaderSelected,
        AppWorkspace,
        AppAccent,

        Control,
        ControlHighlight,
        ControlDisabled,
        ControlPressed,
        ControlSelected,

        Header,
        HeaderHighlight,
        HeaderDisabled,
        HeaderPressed,
        HeaderSelected,

        Border,
        BorderHighlight,
        BorderDisabled,
        BorderPressed,
        BorderSelected,

        Text,
        TextDisabled,
        TextSelected,

        Tint,
        Highlight,

        Transparent,
        Test
    }

    /// <summary>
    /// Defines the available layout related property resource ID's.
    /// </summary>
    public enum XLLayout
    {
        BorderThickness,
        CornerRadius,
        Margin,
        Padding
    }

    /// <summary>
    /// Defines the available themes.
    /// </summary>
    /// <remarks>
    /// <b>IMPORTANT</b><br/>
    /// <i>
    /// The value names are used as URI parameter and have to match the file names in the Resources.Themes folder.
    /// </i></remarks>
    public enum XLThemes
    {
        Default,
        Dark,
        Integra
    }

    /// <summary>
    /// Defines the available layout options.
    /// </summary>
    /// <remarks>
    /// <b>IMPORTANT</b><br/>
    /// <i>
    /// The value names are used as URI parameter and have to match the file names in the Resources.Layouts folder.
    /// </i></remarks>
    [Flags]
    public enum XLLayoutOptions
    {
        Flat     = 0,
        Default  = 1,
        Strong   = 2,
        Rounded  = 4,
        Raised   = 8,
        Shadowed = 16,
        Spaced   = 32
    }

    

    //public static class XLColorKeys
    //{
    //    //public const string App = "DefaultApp";
    //    //public const string Control = "DefaultControl";
    //    //public const string Text = "DefaultText";
    //}
    //public static class Layout
    //{
    //    public const string BorderThickness = "BorderThickness";
    //    public const string CornerRadius = "CornerRadius";
    //    public const string Margin = "Margin";
    //}
   
    //public static class Brushes
    //{
    //    // Constant values have to be defined in the style resource dictionaries as component resource key
    //    //public const string App               = "AppBrush";
    //    //public const string AppAccent         = "AppAccentBrush";
    //    //public const string AppWorkspace      = "AppWorkspaceBrush";
    //    //public const string AppBorder         = "AppBorderBrush";
    //    //public const string AppBorderSelected = "AppBorderSelectedBrush";
    //    //public const string AppHeader         = "AppHeaderBrush";
    //    //public const string AppHeaderSelected = "AppHeaderSelectedBrush";
    //    //public const string Header            = "HeaderBrush";
    //    //public const string HeaderHighlight   = "HeaderHighlightBrush";
    //    //public const string HeaderSelected    = "HeaderSelectedBrush";
    //    //public const string HeaderPressed     = "HeaderPressedBrush";
    //    //public const string HeaderDisabled    = "HeaderDisabledBrush";
    //    //public const string Border            = "BorderBrush";
    //    //public const string BorderHighlight   = "BorderHighlightBrush";
    //    //public const string BorderSelected    = "BorderSelectedBrush";
    //    //public const string BorderPressed     = "BorderPressedBrush";
    //    //public const string BorderDisabled    = "BorderDisabledBrush";
    //    //public const string Control           = "ControlBrush";
    //    //public const string ControlHighlight  = "ControlHighlightBrush";
    //    //public const string ControlSelected   = "ControlSelectedBrush";
    //    //public const string ControlPressed    = "ControlPressedBrush";
    //    //public const string ControlDisabled   = "ControlDisabledBrush";
    //    //public const string Text              = "TextBrush";
    //    //public const string TextHighlight     = "TextHighlightBrush";
    //    //public const string TextSelected      = "TextSelectedBrush";
    //    //public const string TextPressed       = "TextPressedBrush";
    //    //public const string TextDisabled      = "TextDisabledBrush";
    //    //public const string TestBrush         = "TestBrush";
    //    //public const string Highlight         = "HighlightBrush";
    //    //public const string Tint              = "TintBrush";
    //    //public const string Transparent       = "Transparent";
    //}
}
