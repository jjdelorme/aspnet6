using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", (Func<string>)(() => "Hello World!"));
app.MapGet("/{name?}", (Func<string, string>)PrintHello);

await app.RunAsync();

string PrintHello(string name)
{
    if (name?.ToLower() == "brian")
        return "You're Brian???";
    else
        return $"Hello {name}!";
}
