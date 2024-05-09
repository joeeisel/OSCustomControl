namespace OSCustomControl.Browser.Pages
{
    using DotNetForHtml5;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Rendering;
    using Microsoft.JSInterop;
    using Microsoft.JSInterop.WebAssembly;
    using OSCustomControl.Browser.Interop;
    using System;
    using System.Threading.Tasks;

    [Route("/")]
    public class Index : ComponentBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder __builder)
        {
        }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            JSInterop.Runtime = JSRuntime as WebAssemblyJSRuntime;

            Cshtml5Initializer.Initialize(new UnmarshalledJavaScriptExecutionHandler(JSRuntime));
            Program.RunApplication();
        }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }
    }
}