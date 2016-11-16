-- COPYRIGHT 2014-2016 Anton Yarkov
-- Email: anton.yarkov@gmail.com
-- Skype: optiklab

-----------------------------------------------------------------


INSERT INTO [dbo].[Roles] ([Id],[Name],[Active]) VALUES (NEWID(),'Administrators', 1)
INSERT INTO [dbo].[Roles] ([Id],[Name],[Active]) VALUES (NEWID(),'Registered', 1)
INSERT INTO [dbo].[Roles] ([Id],[Name],[Active]) VALUES (NEWID(),'Developers', 1)
INSERT INTO [dbo].[Roles] ([Id],[Name],[Active]) VALUES (NEWID(),'Guests', 1)
INSERT INTO [dbo].[Roles] ([Id],[Name],[Active]) VALUES (NEWID(),'Not activated', 1)
INSERT INTO [dbo].[Roles] ([Id],[Name],[Active]) VALUES (NEWID(),'Shop', 1)

GO

-- TODO Change e-mail addresses and passwords.
INSERT INTO [dbo].[EmailAccount] ([Email],[DisplayName],[Host],[Port],[Username],[Password],[EnableSsl],[UseDefaultCredentials])
     VALUES ('test@test.com', 'Admin', '', 995, 'test@test.com', 'password', 0, 1)
INSERT INTO [dbo].[EmailAccount] ([Email],[DisplayName],[Host],[Port],[Username],[Password],[EnableSsl],[UseDefaultCredentials])
     VALUES ('test@test.com', 'RegisterdUsers', '', 995, 'test@test.com', 'password', 0, 1)
INSERT INTO [dbo].[EmailAccount] ([Email],[DisplayName],[Host],[Port],[Username],[Password],[EnableSsl],[UseDefaultCredentials])
     VALUES ('test@test.com', 'Support', '', 995, 'test@test.com', 'password', 0, 1)
GO


INSERT INTO [dbo].[MessageTemplate] ([Name],[BccEmailAddresses],[Subject],[Body],[IsActive],[EmailAccountId])
     VALUES ( 'AccountActivation', '', 'Account activation on Clever Wearer', 'Hello, {0}! Please go to link {1} to activate your account.', 1, 3)
INSERT INTO [dbo].[MessageTemplate] ([Name],[BccEmailAddresses],[Subject],[Body],[IsActive],[EmailAccountId])
     VALUES ( 'WelcomeMessage', '', 'Welcome to Clever Wearer', 'You are welcome on Clever Wearer, {0}! Now you can create your own cloakroom and have comfort cloth every day!', 1, 3)
INSERT INTO [dbo].[MessageTemplate] ([Name],[BccEmailAddresses],[Subject],[Body],[IsActive],[EmailAccountId])
     VALUES ( 'PasswordRecovery', '', 'Password recovery on Clever Wearer', 'Hi, {0}. Go to link {1} to create new password.', 1, 3)

GO

INSERT INTO [dbo].[Setting] ([Name],[Value]) VALUES ('Media.Images.AvatarPictureSize', '85')
INSERT INTO [dbo].[Setting] ([Name],[Value]) VALUES ('Media.Images.ProductThumbPictureSize', '125')
INSERT INTO [dbo].[Setting] ([Name],[Value]) VALUES ('Media.Images.ProductDetailsPictureSize', '300')
INSERT INTO [dbo].[Setting] ([Name],[Value]) VALUES ('Media.Images.ProductThumbPictureSizeOnProductDetailsPage', '70')
INSERT INTO [dbo].[Setting] ([Name],[Value]) VALUES ('Media.Images.ProductVariantPictureSize', '125')
INSERT INTO [dbo].[Setting] ([Name],[Value]) VALUES ('Media.Images.CategoryThumbPictureSize', '125')
INSERT INTO [dbo].[Setting] ([Name],[Value]) VALUES ('Media.Images.ManufacturerThumbPictureSize', '125')
INSERT INTO [dbo].[Setting] ([Name],[Value]) VALUES ('Media.Images.CartThumbPictureSize', '80')
INSERT INTO [dbo].[Setting] ([Name],[Value]) VALUES ('Media.Images.MaximumImageSize', '1280')
INSERT INTO [dbo].[Setting] ([Name],[Value]) VALUES ('Media.Images.DefaultPictureZoomEnabled', 'false')

GO

SET IDENTITY_INSERT [dbo].[Language] ON
INSERT INTO [dbo].[Language] ([Id],[Code],[Fullname]) VALUES (1,'en','English')
INSERT INTO [dbo].[Language] ([Id],[Code],[Fullname]) VALUES (2,'ru','Russian')
SET IDENTITY_INSERT [dbo].[Language] OFF

GO

INSERT INTO [dbo].[GoodThoughts] ([LanguageId],[Author],[Description]) VALUES (1,'Tommy Dewar','Do nothing, be nothing, say nothing and you will avoid criticism.')
INSERT INTO [dbo].[GoodThoughts] ([LanguageId],[Author],[Description]) VALUES (1,'Eleanor Roosevelt','Great minds discuss ideas.<br />Average minds discuss events.<br />Small minds discuss people.')
INSERT INTO [dbo].[GoodThoughts] ([LanguageId],[Author],[Description]) VALUES (1,'Neil deGrasse Tyson','The good thing about science is that it’s true whether or not you believe in it')
INSERT INTO [dbo].[GoodThoughts] ([LanguageId],[Author],[Description]) VALUES (1,'Murphy''s law','Anything that can go wrong will go wrong.')
INSERT INTO [dbo].[GoodThoughts] ([LanguageId],[Author],[Description]) VALUES (1,'Sturgeon''s Law','Nothing is always absolutely so')
INSERT INTO [dbo].[GoodThoughts] ([LanguageId],[Author],[Description]) VALUES (1,'The “duck test”','If it looks like a duck, swims like a duck, and quacks like a duck, then it probably is a duck.')
INSERT INTO [dbo].[GoodThoughts] ([LanguageId],[Author],[Description]) VALUES (2,'Закон Парето','20 % усилий дают 80 % результата, а остальные 80 % усилий — лишь 20 % результата')
INSERT INTO [dbo].[GoodThoughts] ([LanguageId],[Author],[Description]) VALUES (2,'Закон Паркинсона','Работа заполняет время, отпущенное на неё')
INSERT INTO [dbo].[GoodThoughts] ([LanguageId],[Author],[Description]) VALUES (2,'Отто Фон Бисмарк','Никогда не врут так, как во время войны, после охоты и перед выборами')
INSERT INTO [dbo].[GoodThoughts] ([LanguageId],[Author],[Description]) VALUES (2,'Томми Дюар','Ничего не делай, ничего не говори, и тогда ты избежишь критики')
INSERT INTO [dbo].[GoodThoughts] ([LanguageId],[Author],[Description]) VALUES (2,'Брюс Ли','Освободите свой разум, станьте бесформенными, текучими и изменчивыми, как вода. Если вы нальете воду в кружку, она станет кружкой, если перельете в бутылку, она станет бутылкой, если нальете в чайник — она станет чайником. Вода может течь, но может и сокрушать. Стань водой, мой друг.')
INSERT INTO [dbo].[GoodThoughts] ([LanguageId],[Author],[Description]) VALUES (2,'Льюис Кэрол, Алиса в Стране Чудес','Нужно делать так, как нужно. А как не нужно, делать не нужно!')
INSERT INTO [dbo].[GoodThoughts] ([LanguageId],[Author],[Description]) VALUES (2,'Льюис Кэрол, Алиса в Стране Чудес','Нужно бежать со всех ног, чтобы только оставаться на месте, а чтобы куда-то попасть, надо бежать как минимум вдвое быстрее!')
INSERT INTO [dbo].[GoodThoughts] ([LanguageId],[Author],[Description]) VALUES (2,'Льюис Кэрол, Алиса в Стране Чудес','Если бы это было так, это бы еще ничего, а если бы ничего, оно бы так и было, но так как это не так, так оно и не этак! Такова логика вещей!')
INSERT INTO [dbo].[GoodThoughts] ([LanguageId],[Author],[Description]) VALUES (2,'Льюис Кэрол, Алиса в Стране Чудес','Одна из самых серьезных потерь в битве — это потеря головы.')
INSERT INTO [dbo].[GoodThoughts] ([LanguageId],[Author],[Description]) VALUES (2,'Льюис Кэрол, Алиса в Стране Чудес','Сначала раздай всем пирога, а потом разрежь его!')
INSERT INTO [dbo].[GoodThoughts] ([LanguageId],[Author],[Description]) VALUES (2,'Льюис Кэрол, Алиса в Стране Чудес','От перца, верно, начинают всем перечить. От уксуса — куксятся, от горчицы — огорчаются, от лука — лукавят, от вина — винятся, а от сдобы — добреют. Как жалко, что никто об этом не знает... Все было бы так просто. Ели бы сдобу — и добрели!')
INSERT INTO [dbo].[GoodThoughts] ([LanguageId],[Author],[Description]) VALUES (2,'Льюис Кэрол, Алиса в Стране Чудес','Никогда не думай, что ты иная, чем могла бы быть иначе, чем будучи иной в тех случаях, когда иначе нельзя не быть.')
INSERT INTO [dbo].[GoodThoughts] ([LanguageId],[Author],[Description]) VALUES (2,'Льюис Кэрол, Алиса в Стране Чудес','Отсюда мораль: что-то не соображу.')
--INSERT INTO [dbo].[GoodThoughts] ([LanguageId],[Author],[Description]) VALUES (1,'','')

GO


INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (3200,'Не доступно',NULL,2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (3200,'Not available',NULL,1)

INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (200,'thunderstorm with light rain','thunderstorm.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (201,'thunderstorm with rain','thunderstorm.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (202,'thunderstorm with heavy rain'	,'thunderstorm.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (210,'light thunderstorm','thunderstorm.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (211,'thunderstorm','thunderstorm.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (212,'heavy thunderstorm','thunderstorm.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (221,'ragged thunderstorm','day-thunderstorm.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (230,'thunderstorm with light drizzle','thunderstorm.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (231,'thunderstorm with drizzle','thunderstorm.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (232,'thunderstorm with heavy drizzle','thunderstorm.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (200,'Гроза и легкий дождь','thunderstorm.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (201,'Гроза и дождь','thunderstorm.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (202,'Гроза и сильный дождь','thunderstorm.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (210,'Легкая гроза','thunderstorm.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (211,'Гроза','thunderstorm.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (212,'Сильная гроза','thunderstorm.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (221,'Переменные грозы','day-thunderstorm.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (230,'Гроза и легкая морось','thunderstorm.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (231,'Гроза и морось','thunderstorm.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (232,'Гроза и сильная морось','thunderstorm.svg',2)

INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (300,	'light intensity drizzle','sprinkles.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (301,	'drizzle','sprinkles.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (302,	'heavy intensity drizzle','sprinkles.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (310,	'light intensity drizzle rain','sprinkles.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (311,	'drizzle rain','sprinkles.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (312,	'heavy intensity drizzle rain','sprinkles.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (313,	'shower rain and drizzle','sprinkles.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (314,	'heavy shower rain and drizzle','sprinkles.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (321,	'shower drizzle','sprinkles.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (300,	'Морось слабой интенсивности','sprinkles.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (301,	'Морось','sprinkles.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (302,	'Морось сильной интенсивности','sprinkles.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (310,	'Моросящий дождь слабой интенсивности','sprinkles.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (311,	'Моросящий дождь','sprinkles.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (312,	'Моросящий дождь сильной интенсивности','sprinkles.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (313,	'Ливневый дождь и морось','sprinkles.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (314,	'Сильная ливневая морось и дождь','sprinkles.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (321,	'Ливневая морось','sprinkles.svg',2)

INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (500,	'light rain','showers.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (501,	'moderate rain','showers.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (502,	'heavy intensity rain','showers.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (503,	'very heavy rain','showers.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (504,	'extreme rain','showers.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (511,	'freezing rain','rain-mix-1.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (520,	'light intensity shower rain','showers.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (521,	'shower rain','showers.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (522,	'heavy intensity shower rain','showers.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (531,	'ragged shower rain','showers.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (500,	'Лёгкий дождь','showers.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (501,	'Дождь','showers.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (502,	'Дождь сильной интенсивности','showers.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (503,	'Очень сильный дождь','showers.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (504,	'Дождина, ух!','showers.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (511,	'Ледяной дождь','rain-mix-1.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (520,	'Ливневый дождь средней интенсивности','showers.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (521,	'Ливневый дождь','showers.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (522,	'Ливневый дождь сильной интенсивности','showers.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (531,	'Переменный ливневый дождь','showers.svg',2)

INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (600,	'light snow','snow.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (601,	'snow','snow.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (602,	'heavy snow','blowing-snow.png',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (611,	'sleet','mixed-snow-and-sleet.png',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (612,	'shower sleet','mixed-snow-and-sleet.png',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (615,	'light rain and snow','mixed-snow-and-sleet.png',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (616,	'rain and snow','day-rain-mix.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (620,	'light shower snow','light-snow-shower.png',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (621,	'shower snow','snow.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (622,	'heavy shower snow',	'blowing-snow.png', 1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (600,	'Легкий снег','snow.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (601,	'Снег','snow.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (602,	'Сильный снег','blowing-snow.png',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (611,	'Дождь со снегом','mixed-snow-and-sleet.png',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (612,	'Ливневый дождь со снегом','mixed-snow-and-sleet.png',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (615,	'Легкий дождь со снегом','mixed-snow-and-sleet.png',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (616,	'Дождь со снегом','mixed-snow-and-sleet.png',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (620,	'Легкий ливневый снег','light-snow-shower.png',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (621,	'Ливневый снег','snow.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (622,	'Сильный ливневый снег',	'blowing-snow.png', 2)

INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (701,	'mist','dust.png',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (711,	'smoke','dust.png',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (721,	'haze','day-fog.png',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (731,	'sand, dust whirls','dust.png',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (741,	'fog','day-fog.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (751,	'sand','dust.png',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (761,	'dust','dust.png',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (762,	'volcanic ash','dust.png',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (771,	'squalls','dust.png',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (781,	'tornado','tornado.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (701,	'Туман','dust.png',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (711,	'Дымка','dust.png',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (721,	'Легкий туман','day-fog.png',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (731,	'Пылевые бури','dust.png',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (741,	'Туман','day-fog.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (751,	'Песок','dust.png',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (761,	'Пыль','dust.png',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (762,	'Вулканические породы','dust.png',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (771,	'Шквалистый ветер','strong-wind.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (781,	'Торнадо','tornado.svg',2)

INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (800,	'clear sky','sun.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (800,	'Чистое небо','sun.svg',2)


INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (801,	'few clouds','day-cloudy.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (802,	'scattered clouds','day-cloudy.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (803,	'broken clouds','cloudy.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (804,	'overcast clouds','cloudy.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (801,	'Облачно','day-cloudy.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (802,	'Частичная облачность','day-cloudy.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (803,	'Частичная облачность','cloudy.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (804,	'Пасмурно','cloudy.svg',2)

INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (900,	'tornado','tornado.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (901,	'tropical storm','tornado.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (902,	'hurricane','tornado.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (903,	'cold','thermometer-exterior.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (904,	'hot','thermometer.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (905,	'windy','windy.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (906,	'hail','hail.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (781,	'Торнадо','tornado.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (901,	'Тропический шторм','tornado.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (902,	'Ураган','tornado.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (903,	'Холодно','thermometer-exterior.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (904,	'Жарко','thermometer.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (905,	'Ветренно','windy.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (906,	'Дождь с градом','hail.svg',2)


--INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (8,'Freezing drizzle','freezing-drizzle.png',1)
--INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (8,'Изморозь','freezing-drizzle.png',2)
--(6,'Mixed rain and sleet','day-rain-mix.svg',1) 'mixed-snow-and-sleet.png'
--(6,'Дождь и мокрый снег','day-rain-mix.svg',2)
--(35,'Mixed rain and hail','hail.svg',1)
--(45,'Ливни с грозой','rain-wind.svg',2)
--(45,'Thundershowers','rain-wind.svg',1)


--(33,'Fair (night)','night-clear.svg',1)
--(33,'Светлая ночь','night-clear.svg',2)

--(36,'Hot','thermometer.svg',1)
--(25,'Cold','thermometer-exterior.svg',1)
--(29,'Partly cloudy (night)','night-cloud.svg',1)
----(29,'Частично облачная ночь','night-cloud.svg',2)

--(17,'Hail','day-hail.svg',1)
--(17,'Град','day-hail.svg',2)
--(23,'Blustery','strong-wind.svg',1)
--(23,'Буря','strong-wind.svg',2)

--(13,'Snow flurries','snow-gusts.png',1)
--(13,'Снегопад','snow-gusts.png',2)

--(18,'Sleet','sleet.png',1)
--(18,'Мокрый снег','sleet.png',2)


INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (951,	'calm','sun.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (952,	'light breeze','sun.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (953,	'gentle breeze','sun.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (954,	'moderate breeze','sun.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (955,	'fresh breeze','sun.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (956,	'strong breeze','strong-wind.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (957,	'high wind, near gale','strong-wind.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (958,	'gale','strong-wind.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (959,	'severe gale','strong-wind.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (960,	'storm','strong-wind.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (961,	'violent storm','strong-wind.svg',1)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (962,	'hurricane','tornado.svg',1)

INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (951,	'Спокойно','sun.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (952,	'Легкий бриз','sun.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (953,	'Нежный бриз','sun.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (954,	'Умеренный бриз','sun.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (955,	'Свежий бриз','sun.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (956,	'Сильный бриз','strong-wind.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (957,	'Ветренно, почти шторм','strong-wind.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (958,	'Штормовой ветер','strong-wind.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (959,	'Ураганный штормовой ветер','strong-wind.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (960,	'Шторм','strong-wind.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (961,	'Сильный шторм','strong-wind.svg',2)
INSERT INTO [dbo].[ConditionDescription] ([ExtId],[ShortDescription],[Icon],[LanguageId]) VALUES (962,	'Ураган','tornado.svg',2)

GO

-- India have 6 seasons.

-- Demiseasn is Autumn&Spring = 20
INSERT INTO [dbo].[SeasonType] ([Code],[Name],[LanguageId]) VALUES (2,'Summer',1)
INSERT INTO [dbo].[SeasonType] ([Code],[Name],[LanguageId]) VALUES (4,'Autumn',1)
INSERT INTO [dbo].[SeasonType] ([Code],[Name],[LanguageId]) VALUES (8,'Winter',1)
INSERT INTO [dbo].[SeasonType] ([Code],[Name],[LanguageId]) VALUES (16,'Spring',1)
INSERT INTO [dbo].[SeasonType] ([Code],[Name],[LanguageId]) VALUES (2,'Лето',2)
INSERT INTO [dbo].[SeasonType] ([Code],[Name],[LanguageId]) VALUES (4,'Осень',2)
INSERT INTO [dbo].[SeasonType] ([Code],[Name],[LanguageId]) VALUES (8,'Зима',2)
INSERT INTO [dbo].[SeasonType] ([Code],[Name],[LanguageId]) VALUES (16,'Весна',2)

GO

INSERT INTO [dbo].[SeasonViaLocation] ([SeasonId],[LocationId],[StartDate],[EndDate]) VALUES (1,1,'','')
INSERT INTO [dbo].[SeasonViaLocation] ([SeasonId],[LocationId],[StartDate],[EndDate]) VALUES (2,1,'','')
INSERT INTO [dbo].[SeasonViaLocation] ([SeasonId],[LocationId],[StartDate],[EndDate]) VALUES (3,1,'','')
INSERT INTO [dbo].[SeasonViaLocation] ([SeasonId],[LocationId],[StartDate],[EndDate]) VALUES (4,1,'','')

GO

INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Equatorial climate','Tropical rainforest climate', 'Equatorial belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Humidus Tropic climate','Humidus Phreatic climate','Equatorial belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Tropical monsoon climate','Subequatorial climate, trade-wind littoral climate','Subequatorial belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Tropical monsoon climate on the plateau','','Subequatorial belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Tropical wet climate','','Tropic belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Tropical dry climate','','Tropic belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Humidus Polar climate','','Temperate belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Mediterranean climate','','Subtropic belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Continental climate','','Subtropic belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Subtropic monsoon climate','','Subtropic belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Subtropic high plateaus climate','','Subtropic belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Subtropic oceanic climate','','Subtropic belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Temperate oceanic climate','Sea or Marine temperate climate','Temperate belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Temperate continental climate','','Temperate belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Temperate sharply continental climate','','Temperate belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Temperate monsoon climate','','Temperate belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Humidus Polar climate','','Subpolar belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Subarctic climate','Boreal climate','Subpolar belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Subantarctic climate','','Subpolar belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Polar climate',' climate','Polar belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Antarctic climate',' climate','Polar belt')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Oceanic climate','Sea, Marine, West Coast, Maritime climate','')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Mountain climate','','')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Arid climate','Desert climate','')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Nivalis  climate','','')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Solar climate','Radiation climate','')
INSERT INTO [dbo].[ClimatType] ([Name],[AlternativeNames],[Belt]) VALUES ('Passat climate','Tropical wet climate','')
		
GO

INSERT INTO [dbo].[Units] ([SystemName],[Distance],[Speed],[Pressure],[Humidity],[Temperature],[Light],[LanguageId]) VALUES ('Metric','Km','Km/h','mBar','%','°C','candel',1)
INSERT INTO [dbo].[Units] ([SystemName],[Distance],[Speed],[Pressure],[Humidity],[Temperature],[Light],[LanguageId]) VALUES ('Metric','Км','Км/ч','мБар','%','°C','кандел',2)

INSERT INTO [dbo].[Units] ([SystemName],[Distance],[Speed],[Pressure],[Humidity],[Temperature],[Light],[LanguageId]) VALUES ('USA','M','Mph','inHg','%','°F','candel',1)
INSERT INTO [dbo].[Units] ([SystemName],[Distance],[Speed],[Pressure],[Humidity],[Temperature],[Light],[LanguageId]) VALUES ('USA','Миль','М/ч','д.рт.ст.','%','°F','кандел',2)

GO

INSERT INTO [dbo].[DataProvider] ([Name],[ConnectionType],[Connection],[Code],[Url],[CreateDate],[IsActive]) VALUES ('Yahoo',1,'','Yahoo','',GETDATE(),1)

GO

SET IDENTITY_INSERT [dbo].[FactorType] ON
INSERT INTO [dbo].[FactorType] ([Id],[Name],[Description]) VALUES (1,'Temperature','')
INSERT INTO [dbo].[FactorType] ([Id],[Name],[Description]) VALUES (2,'TemperatureMin','')
INSERT INTO [dbo].[FactorType] ([Id],[Name],[Description]) VALUES (3,'TemperatureMax','')
INSERT INTO [dbo].[FactorType] ([Id],[Name],[Description]) VALUES (4,'EffectiveTemperature','')
INSERT INTO [dbo].[FactorType] ([Id],[Name],[Description]) VALUES (5,'WindChill','')
INSERT INTO [dbo].[FactorType] ([Id],[Name],[Description]) VALUES (6,'WindSpeed','')
INSERT INTO [dbo].[FactorType] ([Id],[Name],[Description]) VALUES (7,'WindDirection','')
INSERT INTO [dbo].[FactorType] ([Id],[Name],[Description]) VALUES (8,'AtmosphereHumidity','')
INSERT INTO [dbo].[FactorType] ([Id],[Name],[Description]) VALUES (9,'AtmospherePressure','')
INSERT INTO [dbo].[FactorType] ([Id],[Name],[Description]) VALUES (10,'AtmosphereVisibility','')
INSERT INTO [dbo].[FactorType] ([Id],[Name],[Description]) VALUES (11,'AtmosphereRising','')
INSERT INTO [dbo].[FactorType] ([Id],[Name],[Description]) VALUES (12,'Condition','')
INSERT INTO [dbo].[FactorType] ([Id],[Name],[Description]) VALUES (13,'Precipitation','')
INSERT INTO [dbo].[FactorType] ([Id],[Name],[Description]) VALUES (14,'Sunrise','')
INSERT INTO [dbo].[FactorType] ([Id],[Name],[Description]) VALUES (15,'Sunset','')
INSERT INTO [dbo].[FactorType] ([Id],[Name],[Description]) VALUES (16,'SeaLevel','')
INSERT INTO [dbo].[FactorType] ([Id],[Name],[Description]) VALUES (17,'GroundLevel','')
SET IDENTITY_INSERT [dbo].[FactorType] OFF

GO

SET IDENTITY_INSERT [dbo].[ActionType] ON
INSERT INTO [dbo].[ActionType] ([Id],[Name],[Code],[Description],[ShowOrder],[LanguageId]) VALUES (1,'Neutral',4,'',-1,1)
INSERT INTO [dbo].[ActionType] ([Id],[Name],[Code],[Description],[ShowOrder],[LanguageId]) VALUES (2,'Нейтральный',4,'',-1,2)
INSERT INTO [dbo].[ActionType] ([Id],[Name],[Code],[Description],[ShowOrder],[LanguageId]) VALUES (3,'Sport',8,'During physical loads human body is heated to a temperature of from 37° to 41° in different disciplines.',4,1)
INSERT INTO [dbo].[ActionType] ([Id],[Name],[Code],[Description],[ShowOrder],[LanguageId]) VALUES (4,'Спорт',8,'При спортивных нагрузках тело человека разогревается до температуры от 37° до 41° в разных дисциплинах.',4,2)
INSERT INTO [dbo].[ActionType] ([Id],[Name],[Code],[Description],[ShowOrder],[LanguageId]) VALUES (9,'In office / Static',16,'In the absence of stress or stationary finding outside the human body is affected by the external environment only.',1,1)
INSERT INTO [dbo].[ActionType] ([Id],[Name],[Code],[Description],[ShowOrder],[LanguageId]) VALUES (10,'В офисе / Неподвижен',16,'При отсутствии нагрузок или неподвижном нахождении на улице тело человека подвержено влиянию лишь внешней среды.',1,2)
INSERT INTO [dbo].[ActionType] ([Id],[Name],[Code],[Description],[ShowOrder],[LanguageId]) VALUES (11,'Children',32,'Babies are not developed thermoregulatory apparatus and heat load falls mainly on the skin. Therefore, it is important to dress your baby not overheat and supercool.',2,1)
INSERT INTO [dbo].[ActionType] ([Id],[Name],[Code],[Description],[ShowOrder],[LanguageId]) VALUES (12,'Дети',32,'У маленьких детей не развит терморегуляционный аппарат и нагрузка теплоотдачу ложится в основном на кожный покров. Поэтому важно правильно одеть ребенка: не перегреть и не переохладить.',2,2)
INSERT INTO [dbo].[ActionType] ([Id],[Name],[Code],[Description],[ShowOrder],[LanguageId]) VALUES (13,'Walk',64,'Walking down the street in a peaceful mode of motion has minimal impact on the heating body.',0,1)
INSERT INTO [dbo].[ActionType] ([Id],[Name],[Code],[Description],[ShowOrder],[LanguageId]) VALUES (14,'Прогулка',64,'Прогулка по улице в режиме спокойного движения имеет минимальное воздействие на разогрев тела человека.',0,2)
INSERT INTO [dbo].[ActionType] ([Id],[Name],[Code],[Description],[ShowOrder],[LanguageId]) VALUES (15,'Road trip',128,'',-1,1)        -- Включить когда будет что-то кроме одежды.
INSERT INTO [dbo].[ActionType] ([Id],[Name],[Code],[Description],[ShowOrder],[LanguageId]) VALUES (16,'Дорожная поездка',128,'',-1,2) -- Включить когда будет что-то кроме одежды.
INSERT INTO [dbo].[ActionType] ([Id],[Name],[Code],[Description],[ShowOrder],[LanguageId]) VALUES (17,'Camping',256,'Camping involves protection from external factors like sun, rain, insects, wild animals.',3,1)
INSERT INTO [dbo].[ActionType] ([Id],[Name],[Code],[Description],[ShowOrder],[LanguageId]) VALUES (18,'Отдых на природе',256,'Отдых на природе подразумевает защиту от внешних факторов вроде солнца, дождя, насекомых, диких животных.',3,2)
SET IDENTITY_INSERT [dbo].[ActionType] OFF

GO

--EffectiveTemperature
INSERT INTO [dbo].[Factor] ([ActionTypeId],[FactorTypeId],[Value]) VALUES (3,4,3) -- SPORT
INSERT INTO [dbo].[Factor] ([ActionTypeId],[FactorTypeId],[Value]) VALUES (4,4,3) -- SPORT
INSERT INTO [dbo].[Factor] ([ActionTypeId],[FactorTypeId],[Value]) VALUES (9,4,0) --STATIC_LONG_TIME
INSERT INTO [dbo].[Factor] ([ActionTypeId],[FactorTypeId],[Value]) VALUES (10,4,0) --STATIC_LONG_TIME
INSERT INTO [dbo].[Factor] ([ActionTypeId],[FactorTypeId],[Value]) VALUES (11,4,3) -- CHILD
INSERT INTO [dbo].[Factor] ([ActionTypeId],[FactorTypeId],[Value]) VALUES (12,4,3) -- CHILD
INSERT INTO [dbo].[Factor] ([ActionTypeId],[FactorTypeId],[Value]) VALUES (13,4,1) -- WALK
INSERT INTO [dbo].[Factor] ([ActionTypeId],[FactorTypeId],[Value]) VALUES (14,4,1) -- WALK
INSERT INTO [dbo].[Factor] ([ActionTypeId],[FactorTypeId],[Value]) VALUES (17,4,0) --Camping
INSERT INTO [dbo].[Factor] ([ActionTypeId],[FactorTypeId],[Value]) VALUES (18,4,0) --Camping

--AtmosphereVisibility
--INSERT INTO [dbo].[Factor] ([ActionTypeId],[FactorTypeId],[Value]) VALUES (15,10,10) -- ANTI_FOG_LAMPS 
--INSERT INTO [dbo].[Factor] ([ActionTypeId],[FactorTypeId],[Value]) VALUES (16,10,10) -- ANTI_FOG_LAMPS

GO

-- http://newsby.org/by/2011/02/15/text18433.htm
-- http://www.dpva.info/Guide/GuideTricks/WindChillingEffect/
-- http://www.dpva.info/Guide/Engineers/Holidays/WBGTi/
-- http://www.dpva.info/Guide/GuideMedias/GuideAir/BofortWindScale/
-- http://www.teachengineering.org/collection/cub_/activities/cub_earth/cub_earth_lesson3_clothing_and_food_notecards.pdf
-- http://gazeta.aif.ru/oldsite/952/art019.html

-- http://www.dpva.info/Guide/Engineers/Holidays/ClothSizing/

-- TODO Add probability? Add several factors as one rule?


INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (1,'NOT_DETERMINED','',1)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (1,'ACTIVE_HEAT','You will need clothe with active heat!',2)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (1,'STATIC_HEAT','There are cold outside, so take some warm clothes',4)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (1,'LIGHT_HEAT','Now there are deceptive weather outside: may be not so cold, but still windy or possible rainfall',8)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (1,'EYE_PROTECTION','Take glasses for eye protection in windy conditions',16)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (1,'FACE_PROTECTION','Take something for face protection, like shawl',32)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (1,'SUN_PROTECTION','Take headdress or umbrella for sun protection',64)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (1,'WATER_PROTECTION','Take umbrella and waterproof clothe.',128)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (1,'NO_PROTECTION','Take light clothes',256)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (1,'SWIMMING_TOOLS','Take a dress for swiming',512)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (1,'STATIC_COOLING','There are sunny, take very light clothes',1024)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (1,'ACTIVE_COOLING','You will need really active cooling out there, like pocket air conditioner :)',2048)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (1,'INSECTS_PROTECTION','Take protection against insects',4096)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (1,'ANIMALS_PROTECTION','Be careful to animals',8192)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (1,'LIFE_PROTECTION','Threat to life! Just get out of there',16384)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (1,'RADIATION_PROTECTION','Take your anti-radiation suit',32768)

INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (2,'NOT_DETERMINED','',1)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (2,'ACTIVE_HEAT','Вам понадобится одежда с активным подогревом!',2)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (2,'STATIC_HEAT','На улице холодно, так что одевайтесь тепло',4)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (2,'LIGHT_HEAT','Погода обманчива, может быть не так холодно, но ветренно или влажно',8)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (2,'EYE_PROTECTION','Возьмите очки для защиты глаз от ветра',16)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (2,'FACE_PROTECTION','Возьмите защиту для лица, вроде платка и т.п.',32)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (2,'SUN_PROTECTION','Возьмите головной убор или зонтик от солнца',64)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (2,'WATER_PROTECTION','Нужна защита от дождя: зонт, сапоги, и т.д.',128)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (2,'NO_PROTECTION','Одевайтесь легко',256)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (2,'SWIMMING_TOOLS','Возьмите одежду для купания',512)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (2,'STATIC_COOLING','На улице солнечно, одевайтесь очень легко',1024)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (2,'ACTIVE_COOLING','Вам понадобится активное охлаждение, вроде карманного кондиционера :)',2048)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (2,'INSECTS_PROTECTION','Возьмите защиту от насекомых',4096)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (2,'ANIMALS_PROTECTION','Будьте аккуратны с дикими животными и обитателями вод',8192)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (2,'LIFE_PROTECTION','Угроза жизни! Уносите ноги оттуда',16384)
INSERT INTO [dbo].[SuggestionTerm] ([LanguageId],[Code],[Name],[Value]) VALUES (2,'RADIATION_PROTECTION','Возьмите свинцовый костюм от радиации',32768)

GO

-- CLOTHES:
-- -273...+10000 EffectiveTemperature (Humidity is already mean).
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (-51,   -273,    NULL,4,65538) -- 4 + 2 + 65532
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (-25,   -50.999, NULL,4,6)     -- 4+2
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (-15,   -24.999, NULL,4,12)    -- 4+8
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (-10,   -14.999, NULL,4,4)
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (-5,    -9.999,  NULL,4,8)
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (0.999, -4.999,  NULL,4,8)
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (4.999,  1,      NULL,4,8)
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (9.999,  5,      NULL,4,8)
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (14.999, 10,     NULL,4,8)
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (17.999, 15,     NULL,4,256)
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (21.999, 18,     NULL,4,256)
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (26.999, 22,     NULL,4,256)
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (31.999, 27,     NULL,4,256)
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (34.999, 32,     NULL,4,1280)  --  256 + 1024 = 1280
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (39.999, 35,     NULL,4,1280)  --  256 + 1024 = 1280
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (49.999, 40,     NULL,4,3328)  --  1280 + 2048 = 3328
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (10000,  50,     NULL,4,68604) --  1024 + 2048 + 65532 = 68604
-- Wind speed.
-- INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (15,10,NULL,6,16)
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (50.999,35.999,NULL,6,16)
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (90.999,51,NULL,6,48) --16+32
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (10000,91,NULL,6,65580) -- 16 + 32 + 65532 
--Clouds.
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (NULL,NULL,30,12,64) --Condition
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (NULL,NULL,32,12,64)
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (NULL,NULL,34,12,64)
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (NULL,NULL,36,12,64)
-- Rain?
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (NULL,NULL,10,12,128) --Condition
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (NULL,NULL,35,12,128)
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (12,3,NULL,12,128)
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (NULL,NULL,18,12,128)
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (40,37,NULL,12,128)
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (NULL,NULL,45,12,128)
INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (NULL,NULL,47,12,128)
--INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (100,80,NULL,8,128) --AtmosphereHumidity

-- LIFE:
-- Medical problems with pressure. 1 mbar = 0.75006375541921 mmHg
--INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (NULL,NULL,0,9,16384)
--INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (NULL,NULL,1,11,16384)
--INSERT INTO [dbo].[Rules] ([MaxValue],[MinValue],[Value],[FactorTypeId],[Result]) VALUES (NULL,NULL,2,11,16384)

GO

INSERT INTO [dbo].[ItemProviders] ([Name],[PhisicalAddress],[Email],[Phone],[IsPublic],[LocationId],[EnumType]) VALUES ('All shops', 'All shops',NULL,NULL,1,NULL,0)

GO
