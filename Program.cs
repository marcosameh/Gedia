using System.Net.Http;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient("Geidea", client =>
{
    client.BaseAddress = new Uri("https://api.merchant.geidea.net/");
  
    string merchantPublicKey = "32fcca09-11da-4438-aad7-6535c8ae7ac6";
    string apiPassword = "821dcb92-85a0-47be-9548-852349aa5682";
    string authHeaderValue = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{merchantPublicKey}:{apiPassword}"));
    client.DefaultRequestHeaders.Add("Authorization", $"Basic {authHeaderValue}");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
