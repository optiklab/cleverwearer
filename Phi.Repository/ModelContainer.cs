/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.Repository
{
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Phi.Models;
    using Phi.Models.Models;
    using Phi.Repository.Helpers;
    using Phi.Repository.Infrastructure;
    using Phi.Repository.Services;
    using Phi.Repository.Stores;
    using SimpleInjector;
    using SimpleInjector.Extensions;
    using System;
    using System.Collections.Generic;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Diagnostics;

    public class ModelContainer
    {
        private static readonly Container _instance;

        static ModelContainer()
        {
            _instance = new Container();

            _instance.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            var list = new List<Type>
                {
                    typeof(EfRepository<PhiUser>),
                    typeof(EfRepository<Role>),
                    typeof(EfRepository<UserAttribute>),
                    typeof(EfRepository<UserRole>),
                    typeof(EfRepository<UserClaim>),
                    typeof(EfRepository<UserLogin>),
                    typeof(EfRepository<UserProfile>),
                    typeof(EfRepository<Setting>),
                    typeof(EfRepository<EmailAccount>),
                    typeof(EfRepository<MessageTemplate>),
                    typeof(EfRepository<QueuedEmail>),
                    typeof(EfRepository<Location>),
                    typeof(EfRepository<ClimatType>),
                    typeof(EfRepository<DataProvider>),
                    typeof(EfRepository<Item>),
                    typeof(EfRepository<ItemParameter>),
                    typeof(EfRepository<ItemProvider>),
                    typeof(EfRepository<ItemsViaParameter>),
                    typeof(EfRepository<Language>),
                    typeof(EfRepository<ProvidersItem>),
                    typeof(EfRepository<Rule>),
                    typeof(EfRepository<SeasonType>),
                    typeof(EfRepository<SeasonViaLocation>),
                    typeof(EfRepository<Suggestion>),
                    typeof(EfRepository<SuggestionItem>),
                    typeof(EfRepository<SuggestionTerm>),
                    typeof(EfRepository<ActionType>),
                    typeof(EfRepository<Unit>),
                    typeof(EfRepository<WeatherCondition>),
                    typeof(EfRepository<FactorType>),
                    typeof(EfRepository<Factor>),
                    typeof(EfRepository<Image>),
                    typeof(EfRepository<ConditionDescription>),
                    typeof(EfRepository<UserProfilesViaItemProvider>),
                    typeof(EfRepository<GoodThought>),
                    typeof(EfRepository<ItemType>),
                    typeof(EfRepository<UserStat>),
                    typeof(EfRepository<ItemLike>),
                    typeof(EfRepository<Blog>),
                    typeof(EfRepository<BlogComment>),
                    typeof(EfRepository<BlogStar>)
                };

            _instance.Register<IDbContext, phiContext>(Lifestyle.Scoped);
            _instance.Register(typeof(IRepository<>), list, Lifestyle.Transient);
            _instance.Register<HttpContextBase>(() => new HttpContextWrapper(HttpContext.Current), Lifestyle.Transient);
            _instance.Register<IWebHelper, WebHelper>(Lifestyle.Transient);
            _instance.Register<IUserStore<PhiUser>, UserStore>(Lifestyle.Transient);
            _instance.Register<IUserClaimStore<PhiUser>, UserStore>(Lifestyle.Transient);
            _instance.Register<IUserLoginStore<PhiUser>, UserStore>(Lifestyle.Transient);
            _instance.Register<IUserRoleStore<PhiUser>, UserStore>(Lifestyle.Transient);
            _instance.Register<IUserPasswordStore<PhiUser>, UserStore>(Lifestyle.Transient);
            _instance.Register<IUserEmailStore<PhiUser>, UserStore>(Lifestyle.Transient);
            _instance.Register<IUserProfileStore, UserStore>(Lifestyle.Transient);
            _instance.Register<IRoleStore<Role>, RoleStore>(Lifestyle.Transient);
            _instance.Register<IDataStore, DataStore>(Lifestyle.Transient);
            _instance.Register<ISuggestionService, SuggestionService>(Lifestyle.Transient);
        }

        public static Container Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
