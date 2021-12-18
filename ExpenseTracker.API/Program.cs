using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

var builder = WebApplication.CreateBuilder(args);

// Register the database context. 
builder.Services.AddDbContext<ExpenseDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed((host) => true)
            .AllowAnyHeader());
});

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.MapGet("/api/expenses", async (ExpenseDb db) => 
    await db.ExpenseItems.ToListAsync())
    .WithName("GetAllExpenses");

app.MapGet("/api/expenses/{id:int}", async (int id, ExpenseDb db) =>
    {
        var result = await db.ExpenseItems.FindAsync(id);
        if (result is null)
            return Results.NotFound();
        else
            return Results.Ok(result);
    })
    .WithName("GetExpenseById");

app.MapGet("/api/expenses/year/{year:int}", async (int year, ExpenseDb db) =>
    await db.ExpenseItems.Where(expense => expense.Date.Year == year).ToListAsync())
    .WithName("GetExpensesByYear");

app.MapPost("/api/expenses", async (ExpenseItem expense, ExpenseDb db) =>
    {
        expense.Id = default(int);
        expense.Date = expense.Date.Date;
        db.ExpenseItems.Add(expense);
        await db.SaveChangesAsync();

        return Results.CreatedAtRoute("GetExpenseById", new { id = expense.Id }, expense);
    })
    .WithName("PostExpense");

app.MapPut("/api/expenses/{id:int}", async (int id, ExpenseItem expense, ExpenseDb db) => 
    { 
        var savedExpense = await db.ExpenseItems.FindAsync(id);

        if (savedExpense is null)
            return Results.NotFound();

        savedExpense.Title = expense.Title;
        savedExpense.Amount = expense.Amount;
        savedExpense.Date = expense.Date.Date;

        await db.SaveChangesAsync();

        return Results.NoContent();
    })
    .WithName("PutExpense");

app.MapDelete("/api/expenses/{id:int}", async (int id, ExpenseDb db) =>
    {
        if (await db.ExpenseItems.FindAsync(id) is ExpenseItem expense)
        {
            db.ExpenseItems.Remove(expense);
            await db.SaveChangesAsync();
            return Results.Ok(expense);
        }

        return Results.NotFound();
    })
    .WithName("DeleteExpense");

app.Run();

class ExpenseItem
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Title { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }
}

class ExpenseDb : DbContext
{
    public ExpenseDb(DbContextOptions<ExpenseDb> options) : base(options)
    {
    }

    public DbSet<ExpenseItem> ExpenseItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExpenseItem>().HasData(
            new ExpenseItem { Id = 1, Title = "Toilet Paper", Amount = 24.14m, Date = new DateTime(2020, 8, 14) },
            new ExpenseItem { Id = 2, Title = "New TV", Amount = 799.49m, Date = new DateTime(2021, 3, 12) },
            new ExpenseItem { Id = 3, Title = "Cable Internet", Amount = 94.67m, Date = new DateTime(2021, 3, 28) },
            new ExpenseItem { Id = 4, Title = "New Desk (Wooden)", Amount = 450.00m, Date = new DateTime(2021, 5, 12) });
    }
}
