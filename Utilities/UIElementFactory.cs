using System.Drawing;
using System.Windows.Forms;

public class UIElementFactory
{
    private Size buttonSize = new Size(200, 40);
    private Font buttonFont = new Font("Consolas", 12, FontStyle.Bold);
    private Color textColor = Color.White;

    public UIElementFactory() { }

    public Button CreateButton(string text, string name = null)
    {
        return new Button
        {
            Name = name,
            Text = text,
            Size = buttonSize,
            Font = buttonFont,
            ForeColor = textColor,
            Margin = new Padding(0, 0, 0, 10) // Margin for spacing between buttons
        };
    }

    // Method to create a label
    public Label CreateLabel(string text, Font font = null, ContentAlignment textAlign = ContentAlignment.TopLeft, string name = null)
    {
        return new Label
        {
            Name = name,
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
    public DataGridView CreateDataGridView()
    {
        DataGridView dataGridView = new DataGridView
        {
            // Set properties for DataGridView
            BackgroundColor = Color.Black,
            ForeColor = Color.White,
            Font = buttonFont,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            ReadOnly = true,
            EnableHeadersVisualStyles = false,
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
            RowHeadersVisible = false,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToOrderColumns = true,
            AllowUserToResizeRows = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            // Consolidate DefaultCellStyle initialization
            DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                SelectionBackColor = Color.DarkGray,
                SelectionForeColor = Color.White
            },
            // Initialize ColumnHeadersDefaultCellStyle
            ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.Black,
                ForeColor = Color.White
            },
            // Initialize RowsDefaultCellStyle
            RowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.Black,
                ForeColor = Color.White
            }
        };

        return dataGridView;
    }


}
