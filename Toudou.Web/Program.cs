using EventStore.Client;
using Microsoft.Extensions.Options;
using Toudou.Web.Components;
using Touduo.EventSourcing.Contract.Repositories;
using Touduo.EventSourcing.Contract.Services;
using Touduo.EventSourcing.EventStore;

var builder = WebApplication.CreateBuilder(args);

var eventStoreSettings = EventStoreClientSettings.Create("esdb://10.0.0.100:32113?tls=false");

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddSingleton<EventStoreClient>(sp => new EventStoreClient(eventStoreSettings))
    .AddScoped<IDomainEventStore, DomainEventStore>();

builder.Services
    .AddScoped(typeof(IAggregateRepository<>), typeof(AggregateRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();