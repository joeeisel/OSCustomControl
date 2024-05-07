namespace OSCustomControl
{
    using CSHTML5.Native.Html.Controls;
    using System;
    using System.Windows;

    // OS 2.2
    // [INTERNAL_OnAttachedToVisualTree] BEGIN
    // [INTERNAL_OnAttachedToVisualTree] END DomElement IS NULL: True
    // [NumericTextBox_Loaded] BEGIN
    // [NumericTextBox_Loaded] END DomElement IS NULL: False

    public class NumericTextBox : HtmlPresenter
    {
        private int _value = 0;

        // Added - not part of documentation - https://doc.opensilver.net/documentation/in-depth-topics/html-presenter.html
        protected override void INTERNAL_OnAttachedToVisualTree()
        {
            Console.WriteLine($"[INTERNAL_OnAttachedToVisualTree] BEGIN");

            base.INTERNAL_OnAttachedToVisualTree();

            object div = OpenSilver.Interop.GetDiv(this);

            // NOTE: this.DomElement is null
            Console.WriteLine($"[INTERNAL_OnAttachedToVisualTree] END DomElement IS NULL: {this.DomElement == null}, div is null = {div == null}, div: {div}");
        }

        // Added - not part of documentation - https://doc.opensilver.net/documentation/in-depth-topics/html-presenter.html
        protected override void INTERNAL_OnDetachedFromVisualTree()
        {
            Console.WriteLine($"[INTERNAL_OnDetachedFromVisualTree] BEGIN");
            base.INTERNAL_OnDetachedFromVisualTree();
            Console.WriteLine($"[INTERNAL_OnDetachedFromVisualTree] END");
        }

        public NumericTextBox()
        {
            this.Html = @"<input type=""number"" pattern=""[0-9]*"">";

            this.Loaded += NumericTextBox_Loaded;
        }

        public int Value
        {
            get
            {
                if (this.DomElement != null) //Note: the DOM element is null if the control has not been added to the visual tree yet.
                {
                    int valueInt;
                    string valueString = OpenSilver.Interop.ExecuteJavaScript("$0.value", this.DomElement).ToString();
                    if (Int32.TryParse(valueString, out valueInt))
                    {
                        _value = valueInt;
                    }
                }
                return _value;
            }
            set
            {
                _value = value;

                if (this.DomElement != null) //Note: the DOM element is null if the control has not been added to the visual tree yet.
                    OpenSilver.Interop.ExecuteJavaScript("$0.value = $1", this.DomElement, _value);
            }
        }

        void NumericTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine($"[NumericTextBox_Loaded] BEGIN");

            object div = OpenSilver.Interop.GetDiv(this);

            // div = CSHTML5.Internal.INTERNAL_HtmlDomElementReference (references the DIV wrapping the INPUT element)
            // this.DomElement = CSHTML5.Types.JSObjectRef (references the INPUT element)

            // Here, the control has been added to the visual tree, so the DOM element exists. We set the initial value:
            OpenSilver.Interop.ExecuteJavaScript("$0.value = $1", this.DomElement, _value);

            Console.WriteLine($"[NumericTextBox_Loaded] END DomElement IS NULL: {this.DomElement == null}, div is null = {div == null}, div: {div}");
        }
    }
}