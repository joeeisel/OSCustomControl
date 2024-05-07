namespace OSCustomControl
{
    using CSHTML5.Native.Html.Controls;
    using Microsoft.JSInterop;
    using System;
    using System.Windows;

    public class SignaturePad : HtmlPresenter
    {
        private readonly DotNetObjectReference<SignaturePad> objRef;

        public SignaturePad()
        {
            objRef = DotNetObjectReference.Create(this);

            this.Html = @"<div id=""signature-pad"" class=""signature-pad"">
                        <div class=""signature-pad--body"">
                            <canvas width=""540px"" height=""165px"" style=""width:540px;height:165px;""></canvas>
                        </div></div>";

            // Using Loaded instead of INTERNAL_OnAttachedToVisualTree()
            this.Loaded += SignaturePad_Loaded;
        }


        // The Javascript in virtuoso.setupSignature() is still failing to find the DIV with ID signature-pad
        private async void SignaturePad_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("");
            Console.WriteLine("[SignaturePad_Loaded] BEGIN");

            // UNCOMMENT THIS TO FIX the control - shouldn't need to do this though.
            //object forceDomUpdate = this.DomElement;  // This property is deprecated
            // Adding this line seems to update the DOM, such that when
            // virtuoso.setupSignature is called document.getElementById("signature-pad") returns the DIV and not null?

            // Does not work like this.DomElement even though the tooltip for GetDiv() states the following: "Consider calling this
            // method from the 'Loaded' event to ensure that the element is in the visual tree."
            //object ourDiv = OpenSilver.Interop.GetDiv(this);

            await JSInterop.Runtime.InvokeVoidAsync("virtuoso.setupSignature", objRef);
            Console.WriteLine("[SignaturePad_Loaded] END");
        }

        public async void SetupSignature()
        {
            await JSInterop.Runtime.InvokeVoidAsync("virtuoso.setupSignature", objRef);
        }

        public override object CreateDomElement(object parentRef, out object domElementWhereToPlaceChildren)
        {
            try
            {
                Console.WriteLine("");
                Console.WriteLine("[CreateDomElement] BEGIN");
                return base.CreateDomElement(parentRef, out domElementWhereToPlaceChildren);
            }
            finally
            {
                Console.WriteLine("[CreateDomElement] END");
            }
        }

        // The Javascript in virtuoso.setupSignature() is failing to find the DIV with ID signature-pad, so moved this call to the Loaded even to try it there
        protected override async void INTERNAL_OnAttachedToVisualTree()
        {
            Console.WriteLine("");
            Console.WriteLine("[INTERNAL_OnAttachedToVisualTree] BEGIN");
            base.INTERNAL_OnAttachedToVisualTree();
            // await JSInterop.Runtime.InvokeVoidAsync("virtuoso.setupSignature", objRef);
            Console.WriteLine("[INTERNAL_OnAttachedToVisualTree] END");
        }

        protected override async void INTERNAL_OnDetachedFromVisualTree()
        {
            Console.WriteLine("");
            Console.WriteLine("[INTERNAL_OnDetachedFromVisualTree] BEGIN");
            base.INTERNAL_OnDetachedFromVisualTree();
            await JSInterop.Runtime.InvokeVoidAsync("virtuoso.cleanupSignature");
            Console.WriteLine("[INTERNAL_OnDetachedFromVisualTree] END");
        }

        public async void Clear()
        {
            await JSInterop.Runtime.InvokeVoidAsync("virtuoso.clearSignature");
        }

        public async void CleanupSignaturePad()
        {
            await JSInterop.Runtime.InvokeVoidAsync("virtuoso.cleanupSignature");
        }

        [JSInvokable]
        public void SetSignature(byte[] signatureBlob)
        {
            SignatureBytes = signatureBlob;
        }

        public static DependencyProperty SignatureBytesProperty =
            DependencyProperty.Register("SignatureBytes", typeof(byte[]), typeof(SignaturePad), null);
        public byte[] SignatureBytes
        {
            get
            {
                return (byte[])GetValue(SignatureBytesProperty);
            }
            set
            {
                SetValue(SignatureBytesProperty, value);
            }
        }

    }
}
