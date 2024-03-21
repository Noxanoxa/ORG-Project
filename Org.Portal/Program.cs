using Org.Apps;
using Org.Impl;
using Org.Portal;
using Org.Portal.Pages;
using Index = Org.Portal.Pages.Index;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAntiforgery();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddScoped<IOrgTypeService, OrgTypeService>();
builder.Services.AddScoped<IRoleService, RoleService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


app.Run();