using System.Drawing;
using System.Windows.Forms;

namespace FormTetris
{
    public class FormWindowConfiguration
    {
        private Form form;
        private Size defaultSize;
        private bool isFullScreen = false;

        public FormWindowConfiguration(Form form, Size defaultSize)
        {
            this.form = form;
            this.defaultSize = defaultSize;
        }

        public void ToggleFullScreen()
        {
            if (!isFullScreen)
            {
                form.FormBorderStyle = FormBorderStyle.None;
                form.WindowState = FormWindowState.Maximized;
            }
            else
            {
                form.FormBorderStyle = FormBorderStyle.FixedSingle;
                form.WindowState = FormWindowState.Normal;
                form.ClientSize = defaultSize;
            }
            isFullScreen = !isFullScreen;
        }
    }
}
