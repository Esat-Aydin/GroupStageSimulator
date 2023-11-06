using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Simulator;
using Simulator.Interfaces;
using Simulator.Services;
using Simulator.Utilities;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<SimulationService>();
builder.Services.AddScoped<INameValidator<string>, TeamNameValidator>();
builder.Services.AddScoped<TournamentStateService>();
builder.Services.AddScoped<MatchStateService>();



await builder.Build().RunAsync();
