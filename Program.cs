using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<SomeContext>(options =>
{
    options.UseNpgsql("Host=localhost;Username=postgres;Password=postgres;Database=EfPqslTestNet10");
});

builder.Services.AddHostedService<TestService>();

var app = builder.Build();

app.Run();

//////////////////////////////////////////////////////////////

public class SomeContext : DbContext
{
    public DbSet<Something> Somethings { get; set; } = default!;

    public SomeContext(DbContextOptions options) : base(options) { }
}

//////////////////////////////////////////////////////////////

public class Something
{
    [Key]
    public ulong Id { get; set; }

    public string Whatever { get; set; } = default!;

    public Something(string whatever)
    {
        Whatever = whatever;
    }

    private Something() { }
}

//////////////////////////////////////////////////////////////

public class TestService(IDbContextFactory<SomeContext> someContextFactory) : BackgroundService
{
    private readonly IDbContextFactory<SomeContext> someContextFactory = someContextFactory;

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await using var context = await someContextFactory.CreateDbContextAsync(cancellationToken);

        await context.Database.MigrateAsync(cancellationToken);

        var newSomething = new Something(whatever: "Hello, World!");

        context.Somethings.Add(newSomething);

        await context.SaveChangesAsync(cancellationToken);

        Console.WriteLine("Added new Something to the database.");

        Environment.Exit(0);
    }
}

//////////////////////////////////////////////////////////////