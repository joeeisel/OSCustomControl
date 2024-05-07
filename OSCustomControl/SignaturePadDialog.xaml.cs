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

        public void Cleanup()
        {
            
        }

        private void ClearButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            signaturePadControl.Clear();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            signaturePadControl.CleanupSignaturePad();
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            signaturePadControl.CleanupSignaturePad();
            this.DialogResult = false;
        }
    }
}

