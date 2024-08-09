using Coravel;
using Coravel.Invocable;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScheduler();
builder.Services.AddTransient<ScheduledTask>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Services.UseScheduler(scheduler =>
{ scheduler.Schedule<ScheduledTask>().EverySeconds(2); });

app.Run();


public class ScheduledTask : IInvocable
{
    public Task Invoke()
    {
        Console.WriteLine($"Executing scheduled task at: {DateTime.Now}");
        return Task.CompletedTask;
    }
}