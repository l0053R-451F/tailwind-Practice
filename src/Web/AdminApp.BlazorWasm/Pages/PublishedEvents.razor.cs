using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.BlazorWasm.Pages
{
    public partial class PublishedEvents
    {
        [Inject]
        public IEventServiceClient EventServiceClient { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        private List<IEvent> events;

        protected override async Task OnInitializedAsync()
        {
            var result = await EventServiceClient.GetEventsAsync(4, null, true);
            events = result.Data.Events?.Nodes?.ToList();
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeVoidAsync("setTitle", "Published Events");
            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
