using ChorePoint.Application.Behaviours;
using ChorePoint.Application.DomainEventHandlers.CompletionStreakBroken;
using ChorePoint.Application.DomainEventHandlers.CompletionStreakIncreased;
using ChorePoint.Application.DomainEventHandlers.LoginCodeCreated;
using ChorePoint.Application.HangfireJobs;
using ChorePoint.Application.Policies;
using ChorePoint.Application.RequestHandlers.Auth.AddKidLoginCode;
using ChorePoint.Application.RequestHandlers.Auth.KidLogin;
using ChorePoint.Application.RequestHandlers.Auth.ParentLogin;
using ChorePoint.Application.RequestHandlers.Auth.Register;
using ChorePoint.Application.RequestHandlers.Chore.CreateChore;
using ChorePoint.Application.RequestHandlers.Chore.DeleteChore;
using ChorePoint.Application.RequestHandlers.Chore.GetChoreById;
using ChorePoint.Application.RequestHandlers.Chore.GetChoresByKid;
using ChorePoint.Application.RequestHandlers.Chore.GetChoresByParent;
using ChorePoint.Application.RequestHandlers.Chore.UpdateChore;
using ChorePoint.Application.RequestHandlers.ChoreSubmission.CompleteChore;
using ChorePoint.Application.RequestHandlers.ChoreSubmission.GetLatestSubmissionByKid;
using ChorePoint.Application.RequestHandlers.ChoreSubmission.GetStatsByKid;
using ChorePoint.Application.RequestHandlers.ChoreSubmission.GetSubmissionsByParent;
using ChorePoint.Application.RequestHandlers.ChoreSubmission.ReviewSubmission;
using ChorePoint.Application.RequestHandlers.Parent.CreateKid;
using ChorePoint.Application.RequestHandlers.Parent.DeleteKid;
using ChorePoint.Application.RequestHandlers.Parent.GetKidById;
using ChorePoint.Application.RequestHandlers.Parent.GetKidsByParent;
using ChorePoint.Application.RequestHandlers.Parent.UpdateKid;
using ChorePoint.Application.RequestHandlers.Shop.BuyShopItem;
using ChorePoint.Application.RequestHandlers.Shop.DeleteShopItem;
using ChorePoint.Application.RequestHandlers.Shop.GetShopItemsByKid;
using ChorePoint.Application.RequestHandlers.Shop.GetShopItemsByParent;
using ChorePoint.Application.RequestHandlers.Shop.NewShopItem;
using ChorePoint.Application.RequestHandlers.Shop.ReactivateShopItem;
using ChorePoint.Application.RequestHandlers.Shop.ReviewShopItemPurchase;
using ChorePoint.Application.RequestHandlers.Shop.UpdateShopItem;
using ChorePoint.Domain.DomainEventModels;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChorePoint.Application;

public static class ApplicationExtensions
{
    public static IHostApplicationBuilder AddApplication(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddTransient<ILoginCodeDeletionJob, LoginCodeDeletionJob>();
        services.AddScoped<IShopOperationPolicy, ShopOperationPolicy>();

        // Domain handlers specific to the API so do not want to be registered to all services
        services.AddTransient<INotificationHandler<LoginCodeCreatedDomainEvent>, OnLoginCodeCreatedDomainEventScheduleDeletionJobHandler>();
        services.AddTransient<INotificationHandler<LoginCodeCreatedDomainEvent>, OnLoginCodeCreatedDomainEventWriteIntegrationEventHandler>();

        services.AddValidatorsFromAssembly(typeof(ApplicationExtensions).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.Configure<ApiOptions>(builder.Configuration.GetSection(ApiOptions.ConfigurationSectionName));

        return builder;
    }

    extension(IServiceCollection services)
    {
        public IServiceCollection AddMediatR()
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IMediator).Assembly));

            return services;
        }

        public IServiceCollection AddRequestHandlers()
        {
            services.AddTransient<IRequestHandler<AddKidLoginCodeCommand, AddKidLoginCodeResponse>, AddKidLoginCodeHandler>();
            services.AddTransient<IRequestHandler<KidLoginCommand, KidLoginResponse>, KidLoginHandler>();
            services.AddTransient<IRequestHandler<ParentLoginCommand, ParentLoginResponse>, ParentLoginHandler>();
            services.AddTransient<IRequestHandler<RegisterCommand>, RegisterHandler>();

            services.AddTransient<IRequestHandler<CreateChoreCommand>, CreateChoreHandler>();
            services.AddTransient<IRequestHandler<DeleteChoreCommand>, DeleteChoreHandler>();
            services.AddTransient<IRequestHandler<GetChoreByIdQuery, GetChoreByIdResponse>, GetChoreByIdHandler>();
            services.AddTransient<IRequestHandler<GetChoresByKidQuery, IReadOnlyList<GetChoresByKidResponse>>, GetChoresByKidHandler>();
            services.AddTransient<IRequestHandler<GetChoresByParentQuery, IReadOnlyList<GetChoresByParentResponse>>, GetChoresByParentHandler>();
            services.AddTransient<IRequestHandler<UpdateChoreCommand>, UpdateChoreHandler>();

            services.AddTransient<IRequestHandler<CompleteChoreCommand>, CompleteChoreHandler>();
            services.AddTransient<IRequestHandler<GetLatestSubmissionByKidQuery, GetLatestSubmissionByKidResponse>, GetLatestSubmissionByKidHandler>();
            services.AddTransient<IRequestHandler<GetStatsByKidQuery, GetStatsByKidResponse>, GetStatsByKidHandler>();
            services.AddTransient<IRequestHandler<GetSubmissionsByParentQuery, IReadOnlyList<GetSubmissionsByParentResponse>>, GetSubmissionsByParentHandler>();
            services.AddTransient<IRequestHandler<ReviewSubmissionCommand>, ReviewSubmissionHandler>();

            services.AddTransient<IRequestHandler<CreateKidCommand>, CreateKidHandler>();
            services.AddTransient<IRequestHandler<DeleteKidCommand>, DeleteKidHandler>();
            services.AddTransient<IRequestHandler<GetKidByIdQuery, GetKidByIdResponse>, GetKidByIdHandler>();
            services.AddTransient<IRequestHandler<GetKidsByParentQuery, IReadOnlyList<GetKidsByParentResponse>>, GetKidsByParentHandler>();
            services.AddTransient<IRequestHandler<UpdateKidCommand>, UpdateKidHandler>();

            services.AddTransient<IRequestHandler<BuyShopItemCommand>, BuyShopItemHandler>();
            services.AddTransient<IRequestHandler<DeleteShopItemCommand>, DeleteShopItemHandler>();
            services.AddTransient<IRequestHandler<GetShopItemsByKidQuery, IReadOnlyList<GetShopItemsByKidResponse>>, GetShopItemsByKidHandler>();
            services.AddTransient<IRequestHandler<GetShopItemsByParentQuery, IReadOnlyList<GetShopItemsByParentResponse>>, GetShopItemsByParentHandler>();
            services.AddTransient<IRequestHandler<NewShopItemCommand>, NewShopItemHandler>();
            services.AddTransient<IRequestHandler<ReactivateShopItemCommand>, ReactivateShopItemHandler>();
            services.AddTransient<IRequestHandler<ReviewShopItemPurchaseCommand>, ReviewShopItemPurchaseHandler>();
            services.AddTransient<IRequestHandler<UpdateShopItemCommand>, UpdateShopItemHandler>();

            return services;
        }

        public IServiceCollection AddDomainEventHandlers()
        {
            services.AddTransient<INotificationHandler<CompletionStreakBrokenDomainEvent>, OnCompletionStreakBrokenDomainEventWriteIntegrationEventHandler>();

            services.AddTransient<INotificationHandler<CompletionStreakIncreasedDomainEvent>, OnCompletionStreakIncreasedDomainEventWriteIntegrationEventHandler>();

            return services;
        }
    }
}
