namespace OSCustomControl
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class SignaturePadDialog : ChildWindow
    {
        public SignaturePadDialog()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

