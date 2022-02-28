using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StylesXL
{
    /// <summary>
    /// Defines the available control styles.
    /// </summary>
    /// <remarks><i><b>IMPORTANT: </b> The name of the enumeration values are use in the <see cref="ResourceDictionary.Source"/> uri and have to match the file names in the styles folder.</i></remarks>
    public enum ControlStyle
    {
        Default,
        Dark,
        Integra
    }

    /// <summary>
    /// Defines the available control appearances.
    /// </summary>
    /// <remarks><i><b>IMPORTANT: </b> The name of the enumeration values are use in the <see cref="ResourceDictionary.Source"/> uri and have to match the file names in the appearances folder.</i></remarks>
    [Flags]
    public enum ControlAppearance
    {
        Flat = 0,
        Default = 1,
        Strong = 2,
        Rounded = 4,
        Raised = 8,
        Shadowed = 16,
        Spaced = 32

    }

    /// <summary>
    /// Class serves as namespace for <see cref="ComponentResourceKey"/>s and defines constant <see cref="ComponentResourceKey"/> names for use in code.
    /// </summary>
    /// <remarks><i>The defined constants are used for calculation of dynamic appearance properties that influence layout.</i></remarks>
    public static class Styles 
    {
        
    }

    public static class Layout
    {
        public const string BorderThickness = "BorderThickness";
        public const string CornerRadius = "CornerRadius";
        public const string Margin = "Margin";
    }

    public static class Brushes
    {
        // Constant values have to be defined in the style resource dictionaries as component resource key

        public const string App = "AppBrush";
        public const string AppAccent = "AppAccentBrush";
        public const string AppWorkspace = "AppWorkspaceBrush";

        public const string AppBorder = "AppBorderBrush";
        public const string AppBorderSelected = "AppBorderSelectedBrush";

        public const string AppHeader = "AppHeaderBrush";
        public const string AppHeaderSelected = "AppHeaderSelectedBrush";


        public const string Header = "HeaderBrush";
        public const string HeaderHighlight = "HeaderHighlightBrush";
        public const string HeaderSelected = "HeaderSelectedBrush";
        public const string HeaderPressed = "HeaderPressedBrush";
        public const string HeaderDisabled = "HeaderDisabledBrush";

        public const string Border = "BorderBrush";
        public const string BorderHighlight = "BorderHighlightBrush";
        public const string BorderSelected = "BorderSelectedBrush";
        public const string BorderPressed = "BorderPressedBrush";
        public const string BorderDisabled = "BorderDisabledBrush";

        public const string Control = "ControlBrush";
        public const string ControlHighlight = "ControlHighlightBrush";
        public const string ControlSelected = "ControlSelectedBrush";
        public const string ControlPressed = "ControlPressedBrush";
        public const string ControlDisabled = "ControlDisabledBrush";

        public const string Text = "TextBrush";
        public const string TextHighlight = "TextHighlightBrush";
        public const string TextSelected = "TextSelectedBrush";
        public const string TextPressed = "TextPressedBrush";
        public const string TextDisabled = "TextDisabledBrush";

        public const string TestBrush = "TestBrush";
        public const string Highlight = "HighlightBrush";
        public const string Tint = "TintBrush";

        public const string TintFadeUpper = "TintFadeUpperBrush";
        public const string TintFadeLower = "TintFadeLowerBrush";
        public const string HighlightFadeUpper = "HighlightFadeUpperBrush";
        public const string HighlightFadeLower = "HighlightFadeLowerBrush";

        public const string OpacityFadeUpper = "OpacityFadeUpperBrush";
        public const string OpacityFadeLower = "OpacityFadeLowerBrush";
    }
}
