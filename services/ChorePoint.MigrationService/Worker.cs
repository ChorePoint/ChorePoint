using System.Diagnostics;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Enums;
using ChorePoint.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.MigrationService;

public class Worker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime
) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource ActivitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = ActivitySource.StartActivity(
            "Migrating database",
            ActivityKind.Client
        );

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await RunMigrationAsync(dbContext, cancellationToken);

            if (
                bool.TryParse(
                    Environment.GetEnvironmentVariable("SEED_TEST_DATA"),
                    out var seedData
                ) && seedData
            )
            {
                var passwordHasher = scope.ServiceProvider.GetRequiredService<
                    PasswordHasher<Parent>
                >();
                await SeedDatabaseAsync(dbContext, passwordHasher, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private static async Task RunMigrationAsync(
        AppDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.MigrateAsync(cancellationToken);
        });
    }

    private static async Task SeedDatabaseAsync(
        AppDbContext dbContext,
        PasswordHasher<Parent> passwordHasher,
        CancellationToken cancellationToken
    )
    {
        Parent parent = new()
        {
            Email = "test.parent@dev.com",
            FirstName = "Jeff",
            LastName = "Bayzoz",
        };
        parent.SetPassword(passwordHasher.HashPassword(parent, "test"));

        ParentSettings parentSettings = new()
        {
            ParentId = 1,
            AutoApproveChores = false,
            ApprovePurchases = true,
            RequirePhotoEvidence = false,
            ShopOpeningDays =
            [
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday,
                DayOfWeek.Sunday,
            ],
        };

        Kid kidOne = new()
        {
            ParentId = 1,
            Name = "Billy Bob",
            Avatar = "🧒",
            Age = 12,
            DayStreak = 2,
            LifetimePoints = 350,
            SpendablePoints = 350,
        };
        Kid kidTwo = new()
        {
            ParentId = 1,
            Name = "Feddy Fazbear",
            Avatar = "🐻",
            Age = 6,
            DayStreak = 0,
            LifetimePoints = 1000,
            SpendablePoints = 50,
        };
        Kid kidThree = new()
        {
            ParentId = 1,
            Name = "Waluigi",
            Avatar = "🧙‍♂️",
            Age = 6,
            DayStreak = 0,
            LifetimePoints = 1000,
            SpendablePoints = 0,
        };

        Chore choreOne = new()
        {
            ParentId = 1,
            Name = "Make Bed",
            Icon = "🛏️",
            Description = "Make your bed so that it looks presentable in the morning",
            Points = 10,
            Difficulty = ChoreDifficulty.Easy,
            Frequency = ChoreFrequency.Daily,
            CompletionCount = 0,
        };
        KidChore kidChoreOne = new()
        {
            KidId = 1,
            ChoreId = 1,
            IsVisible = true,
        };
        KidChore kidChoreTwo = new()
        {
            KidId = 2,
            ChoreId = 1,
            IsVisible = true,
        };
        KidChore kidChoreThree = new()
        {
            KidId = 3,
            ChoreId = 1,
            IsVisible = false,
        };
        choreOne.KidChores.Add(kidChoreOne);
        choreOne.KidChores.Add(kidChoreTwo);
        choreOne.KidChores.Add(kidChoreThree);

        Chore choreTwo = new()
        {
            ParentId = 1,
            Name = "Clean Your Room",
            Icon = "🧹",
            Description = "Clean your room to a nice standard every week",
            Points = 100,
            Difficulty = ChoreDifficulty.Medium,
            Frequency = ChoreFrequency.Weekly,
            CompletionCount = 0,
        };
        KidChore kidChoreFour = new()
        {
            KidId = 3,
            ChoreId = 2,
            DueDay = DayOfWeek.Saturday,
            IsVisible = true,
        };
        choreTwo.KidChores.Add(kidChoreFour);

        ShopItem shopItemOne = new()
        {
            ParentId = 1,
            Name = "Lynx Africa",
            Icon = "👕",
            Description = "Something to make you not smell",
            Cost = 50,
            Quantity = 2,
        };
        KidShopItem kidShopItemOne = new()
        {
            KidId = 1,
            ShopItemId = 1,
            Status = ShopItemStatus.Available,
        };
        KidShopItem kidShopItemTwo = new()
        {
            KidId = 2,
            ShopItemId = 1,
            Status = ShopItemStatus.Available,
        };
        KidShopItem kidShopItemThree = new()
        {
            KidId = 3,
            ShopItemId = 1,
            Status = ShopItemStatus.Available,
        };
        shopItemOne.KidShopItems.Add(kidShopItemOne);
        shopItemOne.KidShopItems.Add(kidShopItemTwo);
        shopItemOne.KidShopItems.Add(kidShopItemThree);

        ShopItem shopItemTwo = new()
        {
            ParentId = 1,
            Name = "Purple Hat",
            Icon = "🎁",
            Description = "Wah.",
            Cost = 1,
            Quantity = 50,
        };
        KidShopItem kidShopItemFour = new()
        {
            KidId = 3,
            ShopItemId = 2,
            Status = ShopItemStatus.Available,
        };
        shopItemTwo.KidShopItems.Add(kidShopItemFour);

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync(
                cancellationToken
            );

            await dbContext.Parents.AddAsync(parent, cancellationToken);
            // Extra SaveChangesAsync() as otherwise Parent.ParentId [1] does not exist yet for foreign key constraints
            await dbContext.SaveChangesAsync(cancellationToken);

            await dbContext.ParentSettings.AddAsync(parentSettings, cancellationToken);
            await dbContext.Kids.AddAsync(kidOne, cancellationToken);
            await dbContext.Kids.AddAsync(kidTwo, cancellationToken);
            await dbContext.Kids.AddAsync(kidThree, cancellationToken);
            await dbContext.Chores.AddAsync(choreOne, cancellationToken);
            await dbContext.Chores.AddAsync(choreTwo, cancellationToken);
            await dbContext.ShopItems.AddAsync(shopItemOne, cancellationToken);
            await dbContext.ShopItems.AddAsync(shopItemTwo, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }
}
