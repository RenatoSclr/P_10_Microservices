using DiabeticAssessmentAPI.Domain;
using DiabeticAssessmentAPI.Services;
using DiabeticAssessmentAPI.Services.IServices;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ServiceUrlOptions>(builder.Configuration.GetSection("ServiceUrl")); ;

builder.Services.AddHttpClient("PatientAPI", (serviceProvider, client) =>
{
    var options = serviceProvider.GetRequiredService<IOptions<ServiceUrlOptions>>().Value;
    client.BaseAddress = new Uri(options.PatientAPI);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("User-Agent", "DiabeticAssessmentAPI");
});

builder.Services.AddHttpClient("NoteAPI", (serviceProvider, client) =>
{
    var options = serviceProvider.GetRequiredService<IOptions<ServiceUrlOptions>>().Value;
    client.BaseAddress = new Uri(options.NoteAPI);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("User-Agent", "DiabeticAssessmentAPI");
});

builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IDiabetesAlgorihmeService, DiabetesAlgorihmeService>();
builder.Services.AddScoped<IDiabeteReportService, DiabeteReportService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
