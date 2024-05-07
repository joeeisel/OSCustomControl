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

