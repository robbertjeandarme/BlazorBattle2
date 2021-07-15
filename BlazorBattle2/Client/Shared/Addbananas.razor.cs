using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlazorBattle2.Client.Shared
{
    public partial class Addbananas : ComponentBase
    {
        [Parameter]
        public EventCallback<int> BananasAdded { get; set; }

        public async Task IncreaseBananaCount()
        {
            await BananasAdded.InvokeAsync(10);
        }
    }
}
