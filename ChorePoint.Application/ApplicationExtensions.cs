using ChorePoint.Application.Behaviours;
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

        services.AddValidatorsFromAssembly(typeof(ApplicationExtensions).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.Configure<ApiOptions>(builder.Configuration.GetSection(ApiOptions.ConfigurationSectionName));

        return builder;
    }

    public static IServiceCollection AddRequestHandlers(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<AddKidLoginCodeCommand, AddKidLoginCodeResponse>, AddKidLoginCodeHandler>();
        services.AddScoped<IRequestHandler<KidLoginCommand, KidLoginResponse>, KidLoginHandler>();
        services.AddScoped<IRequestHandler<ParentLoginCommand, ParentLoginResponse>, ParentLoginHandler>();
        services.AddScoped<IRequestHandler<RegisterCommand>, RegisterHandler>();

        services.AddScoped<IRequestHandler<CreateChoreCommand>, CreateChoreHandler>();
        services.AddScoped<IRequestHandler<DeleteChoreCommand>, DeleteChoreHandler>();
        services.AddScoped<IRequestHandler<GetChoreByIdQuery, GetChoreByIdResponse>, GetChoreByIdHandler>();
        services.AddScoped<IRequestHandler<GetChoresByKidQuery, IReadOnlyList<GetChoresByKidResponse>>, GetChoresByKidHandler>();
        services.AddScoped<IRequestHandler<GetChoresByParentQuery, IReadOnlyList<GetChoresByParentResponse>>, GetChoresByParentHandler>();
        services.AddScoped<IRequestHandler<UpdateChoreCommand>, UpdateChoreHandler>();

        services.AddScoped<IRequestHandler<CompleteChoreCommand>, CompleteChoreHandler>();
        services.AddScoped<IRequestHandler<GetLatestSubmissionByKidQuery, GetLatestSubmissionByKidResponse>, GetLatestSubmissionByKidHandler>();
        services.AddScoped<IRequestHandler<GetStatsByKidQuery, GetStatsByKidResponse>, GetStatsByKidHandler>();
        services.AddScoped<IRequestHandler<GetSubmissionsByParentQuery, IReadOnlyList<GetSubmissionsByParentResponse>>, GetSubmissionsByParentHandler>();
        services.AddScoped<IRequestHandler<ReviewSubmissionCommand>, ReviewSubmissionHandler>();

        services.AddScoped<IRequestHandler<CreateKidCommand>, CreateKidHandler>();
        services.AddScoped<IRequestHandler<DeleteKidCommand>, DeleteKidHandler>();
        services.AddScoped<IRequestHandler<GetKidByIdQuery, GetKidByIdResponse>, GetKidByIdHandler>();
        services.AddScoped<IRequestHandler<GetKidsByParentQuery, IReadOnlyList<GetKidsByParentResponse>>, GetKidsByParentHandler>();
        services.AddScoped<IRequestHandler<UpdateKidCommand>, UpdateKidHandler>();

        services.AddScoped<IRequestHandler<BuyShopItemCommand>, BuyShopItemHandler>();
        services.AddScoped<IRequestHandler<DeleteShopItemCommand>, DeleteShopItemHandler>();
        services.AddScoped<IRequestHandler<GetShopItemsByKidQuery, IReadOnlyList<GetShopItemsByKidResponse>>, GetShopItemsByKidHandler>();
        services.AddScoped<IRequestHandler<GetShopItemsByParentQuery, IReadOnlyList<GetShopItemsByParentResponse>>, GetShopItemsByParentHandler>();
        services.AddScoped<IRequestHandler<NewShopItemCommand>, NewShopItemHandler>();
        services.AddScoped<IRequestHandler<ReactivateShopItemCommand>, ReactivateShopItemHandler>();
        services.AddScoped<IRequestHandler<ReviewShopItemPurchaseCommand>, ReviewShopItemPurchaseHandler>();
        services.AddScoped<IRequestHandler<UpdateShopItemCommand>, UpdateShopItemHandler>();

        return services;
    }

    public static IServiceCollection AddDomainEventHandlers(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IMediator).Assembly));

        return services;
    }
}
