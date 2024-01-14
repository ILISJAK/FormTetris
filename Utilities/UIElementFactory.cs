using System.Drawing;
using System.Windows.Forms;

public class UIElementFactory
{
    private Size buttonSize = new Size(200, 40);
    private Font buttonFont = new Font("Consolas", 12, FontStyle.Bold);
    private Color textColor = Color.White;

    public UIElementFactory() { }

    public Button CreateButton(string text)
    {
        return new Button
        {
            Text = text,
            Size = buttonSize,
            Font = buttonFont,
            ForeColor = textColor,
            Margin = new Padding(0, 0, 0, 10) // Margin for spacing between buttons
        };
    }

    // Method to create a label
    public Label CreateLabel(string text, Font font = null, ContentAlignment textAlign = ContentAlignment.TopLeft)
    {
        return new Label
        {
            Text = text,
            Font = font ?? buttonFont, // Use provided font or default
            ForeColor = textColor,
            AutoSize = true,
            TextAlign = textAlign
        };
    }

    // Method to create a TextBox
    public TextBox CreateTextBox(string text, bool multiline = false, bool readOnly = false)
    {
        return new TextBox
        {
            Text = text,
            Multiline = multiline,
            ReadOnly = readOnly,
            Font = buttonFont,
            ForeColor = textColor,
            Size = multiline ? new Size(300, 300) : new Size(200, 40), // Example sizes
            ScrollBars = multiline ? ScrollBars.Vertical : ScrollBars.None
        };
    }
}
