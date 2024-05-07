namespace OSCustomControl
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class Dialog : ChildWindow
    {
        public Dialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The value is: " + NumericTextBox1.Value.ToString());
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

