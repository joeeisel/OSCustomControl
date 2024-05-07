namespace OSCustomControl
{
    using Microsoft.JSInterop;
    using System.Windows;
    using System.Windows.Controls;

    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // Enter construction logic here...
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The value is: " + NumericTextBox1.Value.ToString());
        }
        
        private void OpenDialog_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Dialog();
            dialog.Show();
        }
        
        private void OpenSignaturePadDialog_Click(object sender, RoutedEventArgs e)
        {
            var signaturePadDialog = new SignaturePadDialog();
            signaturePadDialog.Show();
        }

        private async void CheckSignaturePad_Click(object sender, RoutedEventArgs e)
        {
            await JSInterop.Runtime.InvokeVoidAsync("virtuoso.info");
        }
    }
}
