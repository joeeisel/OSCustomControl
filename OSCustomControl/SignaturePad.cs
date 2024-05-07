namespace OSCustomControl
{
    using CSHTML5.Native.Html.Controls;
    using Microsoft.JSInterop;
    using System;
    using System.Windows;

    public class SignaturePad : HtmlPresenter
    {
        public SignaturePad()
        {
            objRef = DotNetObjectReference.Create(this);

            //this.Html = @"<div id=""signature-pad"" class=""signature-pad"">
            //            <div class=""signature-pad--body"">SIGNATURE PAD CANVAS</div>
            //            </div>";
            this.Html = @"<div id=""signature-pad"" class=""signature-pad"">
                        <div class=""signature-pad--body"">
                            <canvas width=""540px"" height=""165px"" style=""width:540px;height:165px;""></canvas>
                        </div></div>";

            // Tried using Loaded instead of INTERNAL_OnAttachedToVisualTree(), but in both cases when the JavaScript method virtuoso.setupSignature() is called
            // from C#, the DOM doesn't have our signature-pad DIV - e.g. this fails:
            //          let _wrapper = document.getElementById("signature-pad");
            this.Loaded += SignaturePad_Loaded;
        }


        // The Javascript in virtuoso.setupSignature() is still failing to find the DIV with ID signature-pad
        private async void SignaturePad_Loaded(object sender, RoutedEventArgs e)
        {
            await JSInterop.Runtime.InvokeVoidAsync("virtuoso.setupSignature", objRef);
        }

        private readonly DotNetObjectReference<SignaturePad> objRef;

        public async void SetupSignature()
        {
            await JSInterop.Runtime.InvokeVoidAsync("virtuoso.setupSignature", objRef);
        }

        // The Javascript in virtuoso.setupSignature() is failing to find the DIV with ID signature-pad, so moved this call to the Loaded even to try it there
        //protected override async void INTERNAL_OnAttachedToVisualTree()
        //{
        //    base.INTERNAL_OnAttachedToVisualTree();
        //    await JSInterop.Runtime.InvokeVoidAsync("virtuoso.setupSignature", objRef);
        //}

        protected override async void INTERNAL_OnDetachedFromVisualTree()
        {
            base.INTERNAL_OnDetachedFromVisualTree();
            await JSInterop.Runtime.InvokeVoidAsync("virtuoso.cleanupSignature");
        }

        public async void Clear()
        {
            await JSInterop.Runtime.InvokeVoidAsync("virtuoso.clearSignature");
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
