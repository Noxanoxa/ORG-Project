using Org.Apps;
using Org.Impl;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAntiforgery();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddScoped<IOrgTypeService, OrgTypeService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<INodeService, NodeService>();

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