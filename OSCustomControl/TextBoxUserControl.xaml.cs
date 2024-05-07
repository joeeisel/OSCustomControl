namespace OSCustomControl
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class TextBoxUserControl : UserControl
    {
        public TextBoxUserControl()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The value is: " + NumericTextBox1.Value.ToString());
        }
    }
}
