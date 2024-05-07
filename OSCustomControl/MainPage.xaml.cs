namespace OSCustomControl
{
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
        
        private void ButtonOpenDialog_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ChildWindow();
            dialog.Show();
        }
    }
}
