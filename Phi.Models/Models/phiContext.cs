using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Phi.Models.Models.Mapping;

namespace Phi.Models.Models
{
    public partial class phiContext : DbContext, IDbContext
    {
        static phiContext()
        {
            Database.SetInitializer<phiContext>(null);
        }

        public phiContext()
            : base("Name=phiContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public DbSet<ActionType> ActionTypes { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<BlogStar> BlogStars { get; set; }
        public DbSet<ClimatType> ClimatTypes { get; set; }
        public DbSet<ConditionDescription> ConditionDescriptions { get; set; }
        public DbSet<DataProvider> DataProviders { get; set; }
        public DbSet<EmailAccount> EmailAccounts { get; set; }
        public DbSet<Factor> Factors { get; set; }
        public DbSet<FactorType> FactorTypes { get; set; }
        public DbSet<GoodThought> GoodThoughts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemLike> ItemLikes { get; set; }
        public DbSet<ItemParameter> ItemParameters { get; set; }
        public DbSet<ItemProvider> ItemProviders { get; set; }
        public DbSet<ItemsViaParameter> ItemsViaParameters { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<MessageTemplate> MessageTemplates { get; set; }
        public DbSet<PhiUser> PhiUsers { get; set; }
        public DbSet<ProvidersItem> ProvidersItems { get; set; }
        public DbSet<QueuedEmail> QueuedEmails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<SeasonType> SeasonTypes { get; set; }
        public DbSet<SeasonViaLocation> SeasonViaLocations { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<SuggestionItem> SuggestionItems { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<SuggestionTerm> SuggestionTerms { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<UserAttribute> UserAttributes { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserProfilesViaItemProvider> UserProfilesViaItemProviders { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserStat> UserStats { get; set; }
        public DbSet<WeatherCondition> WeatherConditions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ActionTypeMap());
            modelBuilder.Configurations.Add(new AlertMap());
            modelBuilder.Configurations.Add(new BlogMap());
            modelBuilder.Configurations.Add(new BlogCommentMap());
            modelBuilder.Configurations.Add(new BlogStarMap());
            modelBuilder.Configurations.Add(new ClimatTypeMap());
            modelBuilder.Configurations.Add(new ConditionDescriptionMap());
            modelBuilder.Configurations.Add(new DataProviderMap());
            modelBuilder.Configurations.Add(new EmailAccountMap());
            modelBuilder.Configurations.Add(new FactorMap());
            modelBuilder.Configurations.Add(new FactorTypeMap());
            modelBuilder.Configurations.Add(new GoodThoughtMap());
            modelBuilder.Configurations.Add(new ImageMap());
            modelBuilder.Configurations.Add(new ItemMap());
            modelBuilder.Configurations.Add(new ItemLikeMap());
            modelBuilder.Configurations.Add(new ItemParameterMap());
            modelBuilder.Configurations.Add(new ItemProviderMap());
            modelBuilder.Configurations.Add(new ItemsViaParameterMap());
            modelBuilder.Configurations.Add(new ItemTypeMap());
            modelBuilder.Configurations.Add(new LanguageMap());
            modelBuilder.Configurations.Add(new LocationMap());
            modelBuilder.Configurations.Add(new MessageTemplateMap());
            modelBuilder.Configurations.Add(new PhiUserMap());
            modelBuilder.Configurations.Add(new ProvidersItemMap());
            modelBuilder.Configurations.Add(new QueuedEmailMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new RuleMap());
            modelBuilder.Configurations.Add(new SeasonTypeMap());
            modelBuilder.Configurations.Add(new SeasonViaLocationMap());
            modelBuilder.Configurations.Add(new SettingMap());
            modelBuilder.Configurations.Add(new SuggestionItemMap());
            modelBuilder.Configurations.Add(new SuggestionMap());
            modelBuilder.Configurations.Add(new SuggestionTermMap());
            modelBuilder.Configurations.Add(new UnitMap());
            modelBuilder.Configurations.Add(new UserAttributeMap());
            modelBuilder.Configurations.Add(new UserClaimMap());
            modelBuilder.Configurations.Add(new UserLoginMap());
            modelBuilder.Configurations.Add(new UserProfileMap());
            modelBuilder.Configurations.Add(new UserProfilesViaItemProviderMap());
            modelBuilder.Configurations.Add(new UserRoleMap());
            modelBuilder.Configurations.Add(new UserStatMap());
            modelBuilder.Configurations.Add(new WeatherConditionMap());
        }
    }
}
