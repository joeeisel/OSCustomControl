namespace OSCustomControl
{
    using CSHTML5.Native.Html.Controls;
    using Microsoft.JSInterop;
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

            this.Loaded += SignaturePad_Loaded;
        }

        private async void SignaturePad_Loaded(object sender, RoutedEventArgs e)
        {
            await JSInterop.Runtime.InvokeVoidAsync("virtuoso.setupSignature", objRef);
        }

        private readonly DotNetObjectReference<SignaturePad> objRef;

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
