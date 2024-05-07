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


        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            SignaturePad1.Clear();
        }

        private async void CheckSignaturePad_Click(object sender, RoutedEventArgs e)
        {
            await JSInterop.Runtime.InvokeVoidAsync("virtuoso.info");
        }

        private void SetupSignaturePad_Click(object sender, RoutedEventArgs e)
        {
            this.SignaturePad1.SetupSignature();
        }
    }
}
