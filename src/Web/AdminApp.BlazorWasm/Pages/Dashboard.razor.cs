using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace AdminApp.BlazorWasm.Pages
{
    public partial class Dashboard
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeVoidAsync("renderParticleJS");
            await JSRuntime.InvokeVoidAsync("setTitle", "Dashboard");
            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
