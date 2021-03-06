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
        Dark
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
        public const string Margin = "Margin";
    }

    public static class Brushes
    {
        public const string TestBrush = "TestBrush";

        public const string App = "AppBrush";
        public const string AppAccent = "AppAccentBrush";
        public const string AppWorkspace = "AppWorkspaceBrush";

        public const string Control = "ControlBrush";
        public const string ControlHighlight = "ControlHighlightBrush";
        public const string ControlSelected = "ControlSelectedBrush";
        public const string ControlDisabled = "ControlDisabledBrush";
        public const string ControlBorder = "ControlBorderBrush";

        public const string Text = "TextBrush";
        public const string TextDisabled = "TextDisabledBrush";
        public const string TextSelected = "TextSelectedBrush";
    }
}
