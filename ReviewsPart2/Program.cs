﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReviewsPart2.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ReviewsPart2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ReviewsPart2Context") ?? throw new InvalidOperationException("Connection string 'ReviewsPart2Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Reviews}/{action=Search}/{id?}");

app.Run();
