using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using jasondel;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SchoolContext>(options => 
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SchoolContext")));
await using var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapGet("/{name?}", (Func<string, string>)PrintHello);

app.MapGet("/person/{name}", 
    (Func<string, object>)((name) => new { Person = name }));

app.MapGet("/animal", (Func<string>)(() => "meow"));

app.MapGet("/students", (Func<IEnumerable<Student>>)GetStudents);

await app.RunAsync();

string PrintHello(string name)
{
    if (name?.ToLower() == "brian")
        return "You're Brian???";
    else
        return $"Hello {name}!";
}

IEnumerable<Student> GetStudents()
{
    var scope = app.Services.CreateScope();
    var school = scope.ServiceProvider.GetService<SchoolContext>();
    
    return school.Students;
}