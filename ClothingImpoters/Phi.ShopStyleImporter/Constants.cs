
namespace Phi.ShopStyleImporter
{
    public class Constants
    {
        public const string INSERT_PROVIDER =
            "INSERT INTO [ItemProviders] ([Name],[PhisicalAddress],[Email],[Phone],[IsPublic],[EnumType]) VALUES ('{0}',NULL,NULL,NULL,1,0)";

        public const string INSERT_ITEMS =
            "INSERT INTO [Item]" +
            "([IsWardrobe],[ItemTypeId],[ActionTypeId],[Description],[Name],[LanguageId],[Season],[Gender],[MinAge],[MaxAge],[WaterProtectionPercent],[IceProtectionPercent],[ArmoringPercent],[SunProtectionPercent],[MadeBy],[ProvideBy],[SuggestionTerms],[IsPublic],[DefaultImageUri],[IsChild],[IsAvailable],[Price],[Referrer],[Currency],[Created],[AvailableTill])" +
            "VALUES (0," +
            "(SELECT [Id] FROM [ItemType] WHERE [Name] = '{0}' AND [ItemProviderId] = (SELECT [Id] FROM [ItemProviders] WHERE [Name] = '{25}')), " +
            "{1},'{2}','{3}',{4},{5},{6},{7},{8},{9},{10},{11},{12},'{13}','{14}',{15},{16},'{17}',{18},{19},{20},'{21}',{22},{23},{24})";

        public const string INSERT_ITEM_TYPE = "INSERT INTO [ItemType] ([Name],[LanguageId],[ItemProviderId],[EnumType]) VALUES ('{0}',1, (SELECT [Id] FROM [ItemProviders] WHERE [Name] = '{1}'), 0)";
    }
}
