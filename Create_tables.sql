-- COPYRIGHT 2014-2016 Anton Yarkov
-- Email: anton.yarkov@gmail.com
-- Skype: optiklab

-----------------------------------------------------------------

CREATE TABLE ActionType
( 
	Id                   int IDENTITY ( 1,1 ) ,
	Name                 nvarchar(255)  NULL ,
	Description          nvarchar(1000)  NULL ,
	LanguageId           integer  NULL ,
	ShowOrder            int  NOT NULL 
)
go



ALTER TABLE ActionType
	ADD CONSTRAINT XPKActionType PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE Alerts
( 
	Id                   integer IDENTITY ( 1,1 ) 
)
go



ALTER TABLE Alerts
	ADD CONSTRAINT XPKAlerts PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE Blog
( 
	Id                   integer IDENTITY ( 1,1 ) ,
	Theme                nvarchar(1000)  NULL ,
	Article              nvarchar(4000)  NULL 
)
go



ALTER TABLE Blog
	ADD CONSTRAINT XPKBlog PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE BlogComments
( 
	Id                   integer IDENTITY ( 1,1 ) ,
	Text                 nvarchar(4000)  NULL ,
	UserId               nvarchar(128)  NULL ,
	BlogId               integer  NULL 
)
go



ALTER TABLE BlogComments
	ADD CONSTRAINT XPKBlogComments PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE BlogStars
( 
	Id                   integer IDENTITY ( 1,1 ) ,
	BlogId               integer  NULL ,
	UserId               nvarchar(128)  NULL ,
	Stars                integer  NULL 
)
go



ALTER TABLE BlogStars
	ADD CONSTRAINT XPKBlogStars PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE ClimatType
( 
	Id                   int IDENTITY ( 1,1 ) ,
	Name                 nvarchar(255)  NULL ,
	Belt                 nvarchar(255)  NULL ,
	AlternativeNames     nvarchar(2000)  NULL 
)
go



ALTER TABLE ClimatType
	ADD CONSTRAINT XPKClimatType PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE ConditionDescription
( 
	Id                   int IDENTITY ( 1,1 ) ,
	ExtId                int  NOT NULL ,
	ShortDescription     nvarchar(50)  NULL ,
	Description          nvarchar(255)  NULL ,
	LanguageId           integer  NULL ,
	Icon                 nvarchar(255)  NULL 
)
go



ALTER TABLE ConditionDescription
	ADD CONSTRAINT XPKConditionDescription PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE DataProvider
( 
	Id                   integer IDENTITY ( 1,1 ) ,
	Name                 nvarchar(255)  NULL ,
	ConnectionType       int  NULL ,
	Connection           nvarchar(1000)  NULL ,
	Code                 nvarchar(255)  NULL ,
	Url                  nvarchar(1000)  NULL ,
	CreateDate           datetime  NULL ,
	IsActive             bit  NOT NULL 
)
go



ALTER TABLE DataProvider
	ADD CONSTRAINT XPKDataProviders PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE EmailAccount
( 
	Email                nvarchar(255)  NOT NULL ,
	DisplayName          nvarchar(255)  NOT NULL ,
	Id                   int IDENTITY ( 1,1 ) ,
	Host                 nvarchar(255)  NOT NULL ,
	Port                 int  NOT NULL ,
	Username             nvarchar(255)  NOT NULL ,
	Password             nvarchar(255)  NOT NULL ,
	EnableSsl            bit  NOT NULL ,
	UseDefaultCredentials bit  NOT NULL 
)
go



ALTER TABLE EmailAccount
	ADD CONSTRAINT XPKEmailAccount PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE Factor
( 
	Id                   int IDENTITY ( 1,1 ) ,
	Value                float  NULL ,
	FactorTypeId         int  NULL ,
	ActionTypeId         int  NULL 
)
go



ALTER TABLE Factor
	ADD CONSTRAINT XPKFactor PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE FactorType
( 
	Id                   int IDENTITY ( 1,1 ) ,
	Name                 nvarchar(255)  NOT NULL ,
	Description          nvarchar(1000)  NULL 
)
go



ALTER TABLE FactorType
	ADD CONSTRAINT XPKFactorType PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE GoodThoughts
( 
	Id                   int IDENTITY ( 1,1 ) ,
	Description          nvarchar(4000)  NULL ,
	Author               nvarchar(255)  NULL ,
	LanguageId           integer  NULL 
)
go



ALTER TABLE GoodThoughts
	ADD CONSTRAINT XPKGoodThoughts PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE Images
( 
	Id                   int IDENTITY ( 1,1 ) ,
	ImageUrl             nvarchar(255)  NULL ,
	Height               int  NULL ,
	Width                int  NULL ,
	ItemId               integer  NULL 
)
go



ALTER TABLE Images
	ADD CONSTRAINT XPKImages PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE Item
( 
	Id                   integer IDENTITY ( 1,1 ) ,
	Name                 nvarchar(255)  NULL ,
	Description          nvarchar(2000)  NULL ,
	MadeBy               nvarchar(1000)  NULL ,
	ProvideBy            nvarchar(1000)  NULL ,
	SuggestionTerms      int  NULL ,
	LanguageId           integer  NULL ,
	Gender               bit  NOT NULL ,
	Season               int  NULL ,
	WaterProtectionPercent int  NULL ,
	IceProtectionPercent bit  NULL ,
	ArmoringPercent      int  NULL ,
	MinAge               int  NULL ,
	MaxAge               int  NULL ,
	SunProtectionPercent int  NULL ,
	ActionTypeId         int  NULL ,
	Year                 datetime  NULL ,
	ItemTypeId           integer  NULL ,
	IsPublic             bit  NULL ,
	Root                 int  NULL ,
	DefaultImageUri      nvarchar(2000)  NULL ,
	IsChild              bit  NOT NULL ,
	IsAvailable          bit  NOT NULL ,
	Price                decimal(10,2)  NULL ,
	Referrer             nvarchar(2000)  NULL ,
	Currency             int  NULL ,
	Created              datetime  NULL ,
	AvailableTill        datetime  NULL ,
	Likes                int  NULL ,
	IsWardrobe           bit  NOT NULL 
)
go



ALTER TABLE Item
	ADD CONSTRAINT XPKSuggestionItem PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE ItemLikes
( 
	Id                   int IDENTITY ( 1,1 ) ,
	ItemId               integer  NULL ,
	PhiUserId            nvarchar(128)  NULL ,
	IsWish               bit  NULL ,
	IsLike               bit  NULL ,
	Created              datetime  NULL 
)
go



ALTER TABLE ItemLikes
	ADD CONSTRAINT XPKItemLikes PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE ItemParameters
( 
	Id                   int IDENTITY ( 1,1 ) ,
	Code                 nvarchar(255)  NULL ,
	Name                 nvarchar(1000)  NULL ,
	UnitId               integer  NULL 
)
go



ALTER TABLE ItemParameters
	ADD CONSTRAINT XPKItemParameters PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE ItemProviders
( 
	Id                   int IDENTITY ( 1,1 ) ,
	Name                 nvarchar(255)  NULL ,
	PhisicalAddress      nvarchar(255)  NULL ,
	Email                nvarchar(255)  NULL ,
	Phone                nvarchar(20)  NULL ,
	LocationId           integer  NULL ,
	IsPublic             bit  NOT NULL ,
	EnumType             int  NULL 
)
go



ALTER TABLE ItemProviders
	ADD CONSTRAINT XPKItemProviders PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE ItemsViaParameters
( 
	Id                   int IDENTITY ( 1,1 ) ,
	ItemId               integer  NULL ,
	ParameterId          int  NULL 
)
go



ALTER TABLE ItemsViaParameters
	ADD CONSTRAINT XPKItemsViaParameters PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE ItemType
( 
	Id                   integer IDENTITY ( 1,1 ) ,
	Name                 varchar(255)  NULL ,
	LanguageId           integer  NULL ,
	ItemProviderId       int  NULL ,
	EnumType             int  NULL 
)
go



ALTER TABLE ItemType
	ADD CONSTRAINT XPKItemType PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE Language
( 
	Id                   integer IDENTITY ( 1,1 ) ,
	Code                 nvarchar(10)  NULL ,
	Fullname             nvarchar(255)  NULL 
)
go



ALTER TABLE Language
	ADD CONSTRAINT XPKLanguage PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE Location
( 
	Id                   integer IDENTITY ( 1,1 ) ,
	WOEID                nvarchar(255)  NULL ,
	Continent            nvarchar(255)  NULL ,
	Country              nvarchar(255)  NULL ,
	Admin                nvarchar(255)  NULL ,
	Admin2               nvarchar(255)  NULL ,
	Admin3               nvarchar(255)  NULL ,
	Town                 nvarchar(255)  NULL ,
	Suburb               nvarchar(255)  NULL ,
	Postal_Code          nvarchar(20)  NULL ,
	Supername            nvarchar(255)  NULL ,
	Colloquial           nvarchar(255)  NULL ,
	Time_Zone            int  NULL ,
	Longitude            double precision  NULL ,
	Latitude             double precision  NULL ,
	ClimatId             int  NULL ,
	ShortName            nvarchar(50)  NULL ,
	FlagFileName         nvarchar(255)  NULL ,
	SWLatitude           double precision  NULL ,
	SWLongitude          double precision  NULL ,
	NELatitude           double precision  NULL ,
	NELongitude          double precision  NULL ,
	Parent_WOEID         nvarchar(255)  NULL ,
	ProviderTimeZone     nvarchar(50)  NULL 
)
go



ALTER TABLE Location
	ADD CONSTRAINT XPKLocation PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE MessageTemplate
( 
	Name                 nvarchar(255)  NOT NULL ,
	BccEmailAddresses    nvarchar(255)  NOT NULL ,
	Id                   int IDENTITY ( 1,1 ) ,
	Subject              nvarchar(255)  NOT NULL ,
	Body                 nvarchar(4000)  NOT NULL ,
	IsActive             bit  NOT NULL ,
	EmailAccountId       int  NULL 
)
go



ALTER TABLE MessageTemplate
	ADD CONSTRAINT XPKMessageTemplate PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE PhiUsers
( 
	Id                   nvarchar(128)  NOT NULL ,
	UserName             nvarchar(256)  NULL ,
	Password             nvarchar(550)  NULL ,
	ReminderQuestion     nvarchar(255)  NULL ,
	ReminderAnswer       nvarchar(255)  NULL ,
	PasswordSalt         nvarchar(255)  NULL ,
	PhoneNumber          nvarchar(50)  NULL ,
	FirstName            nvarchar(550)  NULL ,
	LastName             nvarchar(550)  NULL ,
	Email                nvarchar(256)  NULL ,
	LastLoggedDate       datetime  NULL ,
	DateCreated          datetime  NULL ,
	Active               bit  NOT NULL ,
	UserType             int  NULL ,
	UserNameFormat       int  NULL ,
	PasswordFormat       int  NULL ,
	EmailConfirmed       bit  NOT NULL 
	CONSTRAINT Default_Bit_219892112
		 DEFAULT  0,
	PasswordHash         nvarchar(MAX)  NULL ,
	SecurityStamp        nvarchar(MAX)  NULL ,
	PhoneNumberConfirmed bit  NOT NULL 
	CONSTRAINT Default_Bit_1991901517
		 DEFAULT  0,
	TwoFactorEnabled     bit  NOT NULL 
	CONSTRAINT Default_Bit_88774114
		 DEFAULT  0,
	LockoutEndDateUtc    datetime  NULL ,
	LockoutEnabled       bit  NOT NULL 
	CONSTRAINT Default_Bit_957489570
		 DEFAULT  0,
	AccessFailedCount    int  NOT NULL 
	CONSTRAINT Default_Number_1430458855
		 DEFAULT  0
)
go



ALTER TABLE PhiUsers
	ADD CONSTRAINT XPKPhiUser PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE ProvidersItems
( 
	Id                   int IDENTITY ( 1,1 ) ,
	ItemProvidersId      int  NULL ,
	ItemId               int  NULL 
)
go



ALTER TABLE ProvidersItems
	ADD CONSTRAINT XPKProvidersItems PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE QueuedEmail
( 
	Priority             int  NOT NULL ,
	FromAddress          nvarchar(255)  NOT NULL ,
	Id                   int IDENTITY ( 1,1 ) ,
	FromName             nvarchar(255)  NOT NULL ,
	ToAddress            nvarchar(255)  NOT NULL ,
	ToName               nvarchar(255)  NOT NULL ,
	CC                   nvarchar(1000)  NOT NULL ,
	Bcc                  nvarchar(1000)  NOT NULL ,
	EmailSubject         nvarchar(255)  NOT NULL ,
	Body                 nvarchar(1000)  NOT NULL ,
	DateCreated          datetime  NOT NULL ,
	SentTries            int  NOT NULL ,
	SentUTC              datetime  NULL ,
	EmailAccountId       int  NULL 
)
go



ALTER TABLE QueuedEmail
	ADD CONSTRAINT XPKQueuedEmail PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE Roles
( 
	Name                 nvarchar(256)  NULL ,
	Active               bit  NOT NULL ,
	Id                   nvarchar(128)  NOT NULL 
)
go



ALTER TABLE Roles
	ADD CONSTRAINT XPKRoles PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE Rules
( 
	Id                   int IDENTITY ( 1,1 ) ,
	MinValue             float  NULL ,
	MaxValue             float  NULL ,
	Value                float  NULL ,
	FactorTypeId         int  NULL ,
	Result               int  NULL 
)
go



ALTER TABLE Rules
	ADD CONSTRAINT XPKRules PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE SeasonType
( 
	Id                   int IDENTITY ( 1,1 ) ,
	Code                 int  NOT NULL ,
	Name                 nvarchar(255)  NOT NULL ,
	LanguageId           integer  NULL 
)
go



ALTER TABLE SeasonType
	ADD CONSTRAINT XPKSeasonType PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE SeasonViaLocation
( 
	Id                   int IDENTITY ( 1,1 ) ,
	SeasonId             int  NULL ,
	LocationId           integer  NULL ,
	StartDate            datetime  NULL ,
	EndDate              datetime  NULL 
)
go



ALTER TABLE SeasonViaLocation
	ADD CONSTRAINT XPKSeasonViaLocation PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE Setting
( 
	Name                 nvarchar(255)  NOT NULL ,
	Value                nvarchar(255)  NOT NULL ,
	Id                   int IDENTITY ( 1,1 ) 
)
go



ALTER TABLE Setting
	ADD CONSTRAINT XPK_Settings PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE SuggestionItems
( 
	Id                   integer IDENTITY ( 1,1 ) ,
	SuggestionId         int  NULL ,
	ItemId               integer  NULL 
)
go



ALTER TABLE SuggestionItems
	ADD CONSTRAINT XPKSuggestionItems PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE Suggestions
( 
	Id                   int IDENTITY ( 1,1 ) ,
	ShortDescription     nvarchar(255)  NULL ,
	FullDescription      nvarchar(1000)  NULL ,
	WeatherConditionId   integer  NULL ,
	Created              datetime  NULL ,
	LanguageId           integer  NULL 
)
go



ALTER TABLE Suggestions
	ADD CONSTRAINT XPKSuggestions PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE SuggestionTerm
( 
	Id                   int IDENTITY ( 1,1 ) ,
	Code                 nvarchar(255)  NULL ,
	Name                 nvarchar(1000)  NULL ,
	Value                int  NULL ,
	LanguageId           integer  NULL 
)
go



ALTER TABLE SuggestionTerm
	ADD CONSTRAINT XPKSuggestionTerms PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE Units
( 
	Id                   integer IDENTITY ( 1,1 ) ,
	SystemName           nvarchar(50)  NULL ,
	Pressure             nvarchar(50)  NULL ,
	Temperature          nvarchar(50)  NULL ,
	Distance             nvarchar(50)  NULL ,
	Speed                nvarchar(50)  NULL ,
	Light                nvarchar(50)  NULL ,
	LanguageId           integer  NULL ,
	Humidity             nvarchar(50)  NOT NULL 
)
go



ALTER TABLE Units
	ADD CONSTRAINT XPKUnits PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE UserAttribute
( 
	Name                 nvarchar(255)  NULL ,
	Value                nvarchar(255)  NULL ,
	Id                   int IDENTITY ( 1,1 ) ,
	PhiUserId            nvarchar(128)  NULL 
)
go



ALTER TABLE UserAttribute
	ADD CONSTRAINT XPKUserAttribute PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE UserClaims
( 
	Id                   int  NOT NULL ,
	ClaimType            nvarchar(MAX)  NULL ,
	ClaimValue           nvarchar(MAX)  NULL ,
	PhiUserId            nvarchar(128)  NULL 
)
go



ALTER TABLE UserClaims
	ADD CONSTRAINT XPKUserClaims PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE UserLogins
( 
	LoginProvider        nvarchar(128)  NOT NULL ,
	ProviderKey          nvarchar(128)  NOT NULL ,
	PhiUserId            nvarchar(128)  NOT NULL 
)
go



ALTER TABLE UserLogins
	ADD CONSTRAINT XPKUserLogins PRIMARY KEY  CLUSTERED (LoginProvider ASC,ProviderKey ASC,PhiUserId ASC)
go



CREATE TABLE UserProfile
( 
	Gender               bit  NULL ,
	LocationId           integer  NULL ,
	PhiUserId            nvarchar(128)  NOT NULL ,
	AvatarPictureUrl     nvarchar(255)  NULL ,
	IsCompany            bit  NOT NULL ,
	MainCompanyUrl       nvarchar(255)  NULL ,
	CompanyName          nvarchar(255)  NULL ,
	CompanyCEO           nvarchar(255)  NULL ,
	CompanyEmail         nvarchar(255)  NULL ,
	CompanyPhone         nvarchar(50)  NULL ,
	CompanyFax           nvarchar(50)  NULL ,
	AdditionalInfo       nvarchar(2000)  NULL ,
	SellCompanyUrl       nvarchar(255)  NULL ,
	NotifyMeAboutSuddenWeatherEvents bit  NOT NULL 
)
go



ALTER TABLE UserProfile
	ADD CONSTRAINT XPKUserProfile PRIMARY KEY  CLUSTERED (PhiUserId ASC)
go



CREATE TABLE UserProfilesViaItemProviders
( 
	PhiUserId            nvarchar(128)  NULL ,
	Id                   int IDENTITY ( 1,1 ) ,
	ItemProviderId       int  NULL 
)
go



ALTER TABLE UserProfilesViaItemProviders
	ADD CONSTRAINT XPKUserProfilesViaItemProviders PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE UserRoles
( 
	AddRoleDate          datetime  NOT NULL ,
	PhiUserId            nvarchar(128)  NOT NULL ,
	RoleId               nvarchar(128)  NOT NULL 
)
go



ALTER TABLE UserRoles
	ADD CONSTRAINT XPKUserRoles PRIMARY KEY  CLUSTERED (PhiUserId ASC,RoleId ASC)
go



CREATE TABLE UserStat
( 
	Id                   int IDENTITY ( 1,1 ) ,
	Browser              nvarchar(255)  NULL ,
	IPAddress            nvarchar(255)  NULL ,
	UserName             nvarchar(255)  NULL ,
	UserId               nvarchar(255)  NULL ,
	UserEmail            nvarchar(255)  NULL ,
	DateTime             datetime  NULL ,
	UrlReferrer          nvarchar(1000)  NULL ,
	Action               nvarchar(255)  NULL ,
	ActionPage           nvarchar(255)  NULL 
)
go



ALTER TABLE UserStat
	ADD CONSTRAINT XPKUserStat PRIMARY KEY  CLUSTERED (Id ASC)
go



CREATE TABLE WeatherConditions
( 
	Id                   integer IDENTITY ( 1,1 ) ,
	Temperature          float  NULL ,
	TemperatureMin       float  NULL ,
	TemperatureMax       float  NULL ,
	FullDescription      nvarchar(1000)  NULL 
	CONSTRAINT Default_Bit_257377450
		 DEFAULT  0,
	ShortDescription     nvarchar(255)  NULL ,
	WindSpeed            float  NULL ,
	WindDirection        float  NULL ,
	AthmosphereHumidity  float  NULL ,
	AthmospherePressure  float  NULL ,
	AthmosphereVisibility float  NULL ,
	AthmosphereRising    float  NULL ,
	Precipitation        int  NOT NULL ,
	GenereationDate      datetime  NULL ,
	IsForecast           bit  NOT NULL 
	CONSTRAINT Default_Bit_1254743264
		 DEFAULT  0,
	ForecastGuid         char(36)  NULL ,
	ForecastDate         datetime  NULL ,
	Sunrise              datetime  NULL ,
	Sunset               datetime  NULL ,
	GroundLevel          float  NULL ,
	UnitsId              integer  NULL ,
	LanguageId           integer  NULL ,
	SeaLevel             float  NULL ,
	IsPrecalculatedEffectiveTemperature bit  NOT NULL ,
	EffectiveTemperature float  NULL ,
	LocationId           integer  NULL ,
	GenerationDateString nvarchar(255)  NULL ,
	ForecastDateString   nvarchar(255)  NULL ,
	DataProviderId       integer  NULL ,
	Condition            int  NULL 
)
go



ALTER TABLE WeatherConditions
	ADD CONSTRAINT XPKWeatherConditions PRIMARY KEY  CLUSTERED (Id ASC)
go




ALTER TABLE ActionType
	ADD CONSTRAINT R_230 FOREIGN KEY (LanguageId) REFERENCES Language(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE BlogComments
	ADD CONSTRAINT R_256 FOREIGN KEY (UserId) REFERENCES PhiUsers(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE BlogComments
	ADD CONSTRAINT R_257 FOREIGN KEY (BlogId) REFERENCES Blog(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE BlogStars
	ADD CONSTRAINT R_254 FOREIGN KEY (BlogId) REFERENCES Blog(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE BlogStars
	ADD CONSTRAINT R_255 FOREIGN KEY (UserId) REFERENCES PhiUsers(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE ConditionDescription
	ADD CONSTRAINT R_241 FOREIGN KEY (LanguageId) REFERENCES Language(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE Factor
	ADD CONSTRAINT R_218 FOREIGN KEY (FactorTypeId) REFERENCES FactorType(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE Factor
	ADD CONSTRAINT R_228 FOREIGN KEY (ActionTypeId) REFERENCES ActionType(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE GoodThoughts
	ADD CONSTRAINT R_243 FOREIGN KEY (LanguageId) REFERENCES Language(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE Images
	ADD CONSTRAINT R_224 FOREIGN KEY (ItemId) REFERENCES Item(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE Item
	ADD CONSTRAINT R_221 FOREIGN KEY (LanguageId) REFERENCES Language(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE Item
	ADD CONSTRAINT R_227 FOREIGN KEY (ActionTypeId) REFERENCES ActionType(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE Item
	ADD CONSTRAINT R_249 FOREIGN KEY (ItemTypeId) REFERENCES ItemType(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE ItemLikes
	ADD CONSTRAINT R_265 FOREIGN KEY (ItemId) REFERENCES Item(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE ItemLikes
	ADD CONSTRAINT R_266 FOREIGN KEY (PhiUserId) REFERENCES UserProfile(PhiUserId)
		ON UPDATE NO ACTION
go




ALTER TABLE ItemParameters
	ADD CONSTRAINT R_210 FOREIGN KEY (UnitId) REFERENCES Units(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE ItemProviders
	ADD CONSTRAINT R_213 FOREIGN KEY (LocationId) REFERENCES Location(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE ItemsViaParameters
	ADD CONSTRAINT R_211 FOREIGN KEY (ItemId) REFERENCES Item(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE ItemsViaParameters
	ADD CONSTRAINT R_212 FOREIGN KEY (ParameterId) REFERENCES ItemParameters(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE ItemType
	ADD CONSTRAINT R_250 FOREIGN KEY (LanguageId) REFERENCES Language(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE ItemType
	ADD CONSTRAINT R_263 FOREIGN KEY (ItemProviderId) REFERENCES ItemProviders(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE Location
	ADD CONSTRAINT R_193 FOREIGN KEY (ClimatId) REFERENCES ClimatType(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE MessageTemplate
	ADD CONSTRAINT R_163 FOREIGN KEY (EmailAccountId) REFERENCES EmailAccount(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE ProvidersItems
	ADD CONSTRAINT R_202 FOREIGN KEY (ItemProvidersId) REFERENCES ItemProviders(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE ProvidersItems
	ADD CONSTRAINT R_204 FOREIGN KEY (ItemId) REFERENCES Item(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE QueuedEmail
	ADD CONSTRAINT R_162 FOREIGN KEY (EmailAccountId) REFERENCES EmailAccount(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE Rules
	ADD CONSTRAINT R_190 FOREIGN KEY (FactorTypeId) REFERENCES FactorType(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE SeasonType
	ADD CONSTRAINT R_262 FOREIGN KEY (LanguageId) REFERENCES Language(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE SeasonViaLocation
	ADD CONSTRAINT R_195 FOREIGN KEY (SeasonId) REFERENCES SeasonType(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE SeasonViaLocation
	ADD CONSTRAINT R_196 FOREIGN KEY (LocationId) REFERENCES Location(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE SuggestionItems
	ADD CONSTRAINT R_187 FOREIGN KEY (SuggestionId) REFERENCES Suggestions(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE SuggestionItems
	ADD CONSTRAINT R_188 FOREIGN KEY (ItemId) REFERENCES Item(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE Suggestions
	ADD CONSTRAINT R_189 FOREIGN KEY (WeatherConditionId) REFERENCES WeatherConditions(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE Suggestions
	ADD CONSTRAINT R_222 FOREIGN KEY (LanguageId) REFERENCES Language(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE SuggestionTerm
	ADD CONSTRAINT R_253 FOREIGN KEY (LanguageId) REFERENCES Language(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE Units
	ADD CONSTRAINT R_242 FOREIGN KEY (LanguageId) REFERENCES Language(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE UserAttribute
	ADD CONSTRAINT R_234 FOREIGN KEY (PhiUserId) REFERENCES PhiUsers(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE UserClaims
	ADD CONSTRAINT R_237 FOREIGN KEY (PhiUserId) REFERENCES PhiUsers(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE UserLogins
	ADD CONSTRAINT R_236 FOREIGN KEY (PhiUserId) REFERENCES PhiUsers(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE UserProfile
	ADD CONSTRAINT R_225 FOREIGN KEY (LocationId) REFERENCES Location(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE UserProfile
	ADD CONSTRAINT R_240 FOREIGN KEY (PhiUserId) REFERENCES PhiUsers(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE UserProfilesViaItemProviders
	ADD CONSTRAINT R_246 FOREIGN KEY (PhiUserId) REFERENCES UserProfile(PhiUserId)
		ON UPDATE NO ACTION
go




ALTER TABLE UserProfilesViaItemProviders
	ADD CONSTRAINT R_248 FOREIGN KEY (ItemProviderId) REFERENCES ItemProviders(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE UserRoles
	ADD CONSTRAINT R_238 FOREIGN KEY (PhiUserId) REFERENCES PhiUsers(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE UserRoles
	ADD CONSTRAINT R_239 FOREIGN KEY (RoleId) REFERENCES Roles(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE WeatherConditions
	ADD CONSTRAINT R_178 FOREIGN KEY (UnitsId) REFERENCES Units(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE WeatherConditions
	ADD CONSTRAINT R_179 FOREIGN KEY (LanguageId) REFERENCES Language(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE WeatherConditions
	ADD CONSTRAINT R_214 FOREIGN KEY (LocationId) REFERENCES Location(Id)
		ON UPDATE NO ACTION
go




ALTER TABLE WeatherConditions
	ADD CONSTRAINT R_215 FOREIGN KEY (DataProviderId) REFERENCES DataProvider(Id)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




CREATE TRIGGER tU_ActionType ON ActionType FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on ActionType */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* ActionType  Item on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00036732", PARENT_OWNER="", PARENT_TABLE="ActionType"
    CHILD_OWNER="", CHILD_TABLE="Item"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_227", FK_COLUMNS="ActionTypeId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,Item
      WHERE
        /*  %JoinFKPK(Item,deleted," = "," AND") */
        Item.ActionTypeId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update ActionType because Item exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* ActionType  Factor on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="ActionType"
    CHILD_OWNER="", CHILD_TABLE="Factor"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_228", FK_COLUMNS="ActionTypeId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,Factor
      WHERE
        /*  %JoinFKPK(Factor,deleted," = "," AND") */
        Factor.ActionTypeId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update ActionType because Factor exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Language  ActionType on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="ActionType"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_230", FK_COLUMNS="LanguageId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(LanguageId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Language
        WHERE
          /* %JoinFKPK(inserted,Language) */
          inserted.LanguageId = Language.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.LanguageId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update ActionType because Language does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_Blog ON Blog FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on Blog */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId integer,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Blog  BlogStars on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="0001ff9e", PARENT_OWNER="", PARENT_TABLE="Blog"
    CHILD_OWNER="", CHILD_TABLE="BlogStars"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_254", FK_COLUMNS="BlogId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,BlogStars
      WHERE
        /*  %JoinFKPK(BlogStars,deleted," = "," AND") */
        BlogStars.BlogId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Blog because BlogStars exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Blog  BlogComments on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Blog"
    CHILD_OWNER="", CHILD_TABLE="BlogComments"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_257", FK_COLUMNS="BlogId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,BlogComments
      WHERE
        /*  %JoinFKPK(BlogComments,deleted," = "," AND") */
        BlogComments.BlogId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Blog because BlogComments exists.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_BlogComments ON BlogComments FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on BlogComments */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId integer,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* PhiUsers  BlogComments on child update no action */
  /* ERWIN_RELATION:CHECKSUM="0002b871", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="BlogComments"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_256", FK_COLUMNS="UserId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(UserId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,PhiUsers
        WHERE
          /* %JoinFKPK(inserted,PhiUsers) */
          inserted.UserId = PhiUsers.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.UserId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update BlogComments because PhiUsers does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Blog  BlogComments on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Blog"
    CHILD_OWNER="", CHILD_TABLE="BlogComments"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_257", FK_COLUMNS="BlogId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(BlogId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Blog
        WHERE
          /* %JoinFKPK(inserted,Blog) */
          inserted.BlogId = Blog.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.BlogId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update BlogComments because Blog does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_BlogStars ON BlogStars FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on BlogStars */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId integer,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Blog  BlogStars on child update no action */
  /* ERWIN_RELATION:CHECKSUM="0002c30b", PARENT_OWNER="", PARENT_TABLE="Blog"
    CHILD_OWNER="", CHILD_TABLE="BlogStars"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_254", FK_COLUMNS="BlogId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(BlogId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Blog
        WHERE
          /* %JoinFKPK(inserted,Blog) */
          inserted.BlogId = Blog.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.BlogId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update BlogStars because Blog does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* PhiUsers  BlogStars on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="BlogStars"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_255", FK_COLUMNS="UserId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(UserId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,PhiUsers
        WHERE
          /* %JoinFKPK(inserted,PhiUsers) */
          inserted.UserId = PhiUsers.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.UserId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update BlogStars because PhiUsers does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_ClimatType ON ClimatType FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on ClimatType */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* ClimatType  Location on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="0000fddf", PARENT_OWNER="", PARENT_TABLE="ClimatType"
    CHILD_OWNER="", CHILD_TABLE="Location"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_193", FK_COLUMNS="ClimatId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,Location
      WHERE
        /*  %JoinFKPK(Location,deleted," = "," AND") */
        Location.ClimatId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update ClimatType because Location exists.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_ConditionDescription ON ConditionDescription FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on ConditionDescription */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Language  ConditionDescription on child update no action */
  /* ERWIN_RELATION:CHECKSUM="000171f0", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="ConditionDescription"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_241", FK_COLUMNS="LanguageId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(LanguageId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Language
        WHERE
          /* %JoinFKPK(inserted,Language) */
          inserted.LanguageId = Language.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.LanguageId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update ConditionDescription because Language does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tD_DataProvider ON DataProvider FOR DELETE AS
/* ERwin Builtin Trigger */
/* DELETE trigger on DataProvider */
BEGIN
  DECLARE  @errno   int,
           @errmsg  varchar(255)
    /* ERwin Builtin Trigger */
    /* DataProvider  WeatherConditions on parent delete no action */
    /* ERWIN_RELATION:CHECKSUM="00010f2f", PARENT_OWNER="", PARENT_TABLE="DataProvider"
    CHILD_OWNER="", CHILD_TABLE="WeatherConditions"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_215", FK_COLUMNS="DataProviderId" */
    IF EXISTS (
      SELECT * FROM deleted,WeatherConditions
      WHERE
        /*  %JoinFKPK(WeatherConditions,deleted," = "," AND") */
        WeatherConditions.DataProviderId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30001,
             @errmsg = '% Cannot delete DataProvider because WeatherConditions exists.'
      GOTO ERROR
    END


    /* ERwin Builtin Trigger */
    RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 

CREATE TRIGGER tU_DataProvider ON DataProvider FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on DataProvider */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId integer,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* DataProvider  WeatherConditions on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00011964", PARENT_OWNER="", PARENT_TABLE="DataProvider"
    CHILD_OWNER="", CHILD_TABLE="WeatherConditions"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_215", FK_COLUMNS="DataProviderId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,WeatherConditions
      WHERE
        /*  %JoinFKPK(WeatherConditions,deleted," = "," AND") */
        WeatherConditions.DataProviderId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update DataProvider because WeatherConditions exists.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_EmailAccount ON EmailAccount FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on EmailAccount */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* EmailAccount  QueuedEmail on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00023416", PARENT_OWNER="", PARENT_TABLE="EmailAccount"
    CHILD_OWNER="", CHILD_TABLE="QueuedEmail"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_162", FK_COLUMNS="EmailAccountId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,QueuedEmail
      WHERE
        /*  %JoinFKPK(QueuedEmail,deleted," = "," AND") */
        QueuedEmail.EmailAccountId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update EmailAccount because QueuedEmail exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* EmailAccount  MessageTemplate on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="EmailAccount"
    CHILD_OWNER="", CHILD_TABLE="MessageTemplate"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_163", FK_COLUMNS="EmailAccountId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,MessageTemplate
      WHERE
        /*  %JoinFKPK(MessageTemplate,deleted," = "," AND") */
        MessageTemplate.EmailAccountId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update EmailAccount because MessageTemplate exists.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_Factor ON Factor FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on Factor */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* FactorType  Factor on child update no action */
  /* ERWIN_RELATION:CHECKSUM="0002d1b1", PARENT_OWNER="", PARENT_TABLE="FactorType"
    CHILD_OWNER="", CHILD_TABLE="Factor"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_218", FK_COLUMNS="FactorTypeId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(FactorTypeId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,FactorType
        WHERE
          /* %JoinFKPK(inserted,FactorType) */
          inserted.FactorTypeId = FactorType.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.FactorTypeId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update Factor because FactorType does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* ActionType  Factor on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="ActionType"
    CHILD_OWNER="", CHILD_TABLE="Factor"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_228", FK_COLUMNS="ActionTypeId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(ActionTypeId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,ActionType
        WHERE
          /* %JoinFKPK(inserted,ActionType) */
          inserted.ActionTypeId = ActionType.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.ActionTypeId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update Factor because ActionType does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_FactorType ON FactorType FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on FactorType */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* FactorType  Rules on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="0001f734", PARENT_OWNER="", PARENT_TABLE="FactorType"
    CHILD_OWNER="", CHILD_TABLE="Rules"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_190", FK_COLUMNS="FactorTypeId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,Rules
      WHERE
        /*  %JoinFKPK(Rules,deleted," = "," AND") */
        Rules.FactorTypeId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update FactorType because Rules exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* FactorType  Factor on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="FactorType"
    CHILD_OWNER="", CHILD_TABLE="Factor"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_218", FK_COLUMNS="FactorTypeId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,Factor
      WHERE
        /*  %JoinFKPK(Factor,deleted," = "," AND") */
        Factor.FactorTypeId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update FactorType because Factor exists.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_GoodThoughts ON GoodThoughts FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on GoodThoughts */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Language  GoodThoughts on child update no action */
  /* ERWIN_RELATION:CHECKSUM="000171e7", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="GoodThoughts"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_243", FK_COLUMNS="LanguageId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(LanguageId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Language
        WHERE
          /* %JoinFKPK(inserted,Language) */
          inserted.LanguageId = Language.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.LanguageId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update GoodThoughts because Language does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_Images ON Images FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on Images */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Item  Images on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00014a86", PARENT_OWNER="", PARENT_TABLE="Item"
    CHILD_OWNER="", CHILD_TABLE="Images"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_224", FK_COLUMNS="ItemId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(ItemId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Item
        WHERE
          /* %JoinFKPK(inserted,Item) */
          inserted.ItemId = Item.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.ItemId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update Images because Item does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_Item ON Item FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on Item */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId integer,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Item  SuggestionItems on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00091c36", PARENT_OWNER="", PARENT_TABLE="Item"
    CHILD_OWNER="", CHILD_TABLE="SuggestionItems"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_188", FK_COLUMNS="ItemId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,SuggestionItems
      WHERE
        /*  %JoinFKPK(SuggestionItems,deleted," = "," AND") */
        SuggestionItems.ItemId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Item because SuggestionItems exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Item  ProvidersItems on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Item"
    CHILD_OWNER="", CHILD_TABLE="ProvidersItems"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_204", FK_COLUMNS="ItemId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,ProvidersItems
      WHERE
        /*  %JoinFKPK(ProvidersItems,deleted," = "," AND") */
        ProvidersItems.ItemId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Item because ProvidersItems exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Item  ItemsViaParameters on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Item"
    CHILD_OWNER="", CHILD_TABLE="ItemsViaParameters"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_211", FK_COLUMNS="ItemId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,ItemsViaParameters
      WHERE
        /*  %JoinFKPK(ItemsViaParameters,deleted," = "," AND") */
        ItemsViaParameters.ItemId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Item because ItemsViaParameters exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Item  Images on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Item"
    CHILD_OWNER="", CHILD_TABLE="Images"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_224", FK_COLUMNS="ItemId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,Images
      WHERE
        /*  %JoinFKPK(Images,deleted," = "," AND") */
        Images.ItemId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Item because Images exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Item  ItemLikes on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Item"
    CHILD_OWNER="", CHILD_TABLE="ItemLikes"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_265", FK_COLUMNS="ItemId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,ItemLikes
      WHERE
        /*  %JoinFKPK(ItemLikes,deleted," = "," AND") */
        ItemLikes.ItemId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Item because ItemLikes exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Language  Item on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="Item"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_221", FK_COLUMNS="LanguageId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(LanguageId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Language
        WHERE
          /* %JoinFKPK(inserted,Language) */
          inserted.LanguageId = Language.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.LanguageId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update Item because Language does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* ActionType  Item on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="ActionType"
    CHILD_OWNER="", CHILD_TABLE="Item"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_227", FK_COLUMNS="ActionTypeId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(ActionTypeId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,ActionType
        WHERE
          /* %JoinFKPK(inserted,ActionType) */
          inserted.ActionTypeId = ActionType.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.ActionTypeId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update Item because ActionType does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* ItemType  Item on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="ItemType"
    CHILD_OWNER="", CHILD_TABLE="Item"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_249", FK_COLUMNS="ItemTypeId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(ItemTypeId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,ItemType
        WHERE
          /* %JoinFKPK(inserted,ItemType) */
          inserted.ItemTypeId = ItemType.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.ItemTypeId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update Item because ItemType does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_ItemLikes ON ItemLikes FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on ItemLikes */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Item  ItemLikes on child update no action */
  /* ERWIN_RELATION:CHECKSUM="0002c535", PARENT_OWNER="", PARENT_TABLE="Item"
    CHILD_OWNER="", CHILD_TABLE="ItemLikes"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_265", FK_COLUMNS="ItemId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(ItemId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Item
        WHERE
          /* %JoinFKPK(inserted,Item) */
          inserted.ItemId = Item.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.ItemId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update ItemLikes because Item does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* UserProfile  ItemLikes on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="UserProfile"
    CHILD_OWNER="", CHILD_TABLE="ItemLikes"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_266", FK_COLUMNS="PhiUserId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(PhiUserId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,UserProfile
        WHERE
          /* %JoinFKPK(inserted,UserProfile) */
          inserted.PhiUserId = UserProfile.PhiUserId
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.PhiUserId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update ItemLikes because UserProfile does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_ItemParameters ON ItemParameters FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on ItemParameters */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* ItemParameters  ItemsViaParameters on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="0002839e", PARENT_OWNER="", PARENT_TABLE="ItemParameters"
    CHILD_OWNER="", CHILD_TABLE="ItemsViaParameters"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_212", FK_COLUMNS="ParameterId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,ItemsViaParameters
      WHERE
        /*  %JoinFKPK(ItemsViaParameters,deleted," = "," AND") */
        ItemsViaParameters.ParameterId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update ItemParameters because ItemsViaParameters exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Units  ItemParameters on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Units"
    CHILD_OWNER="", CHILD_TABLE="ItemParameters"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_210", FK_COLUMNS="UnitId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(UnitId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Units
        WHERE
          /* %JoinFKPK(inserted,Units) */
          inserted.UnitId = Units.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.UnitId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update ItemParameters because Units does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_ItemProviders ON ItemProviders FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on ItemProviders */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* ItemProviders  ProvidersItems on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="0004d757", PARENT_OWNER="", PARENT_TABLE="ItemProviders"
    CHILD_OWNER="", CHILD_TABLE="ProvidersItems"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_202", FK_COLUMNS="ItemProvidersId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,ProvidersItems
      WHERE
        /*  %JoinFKPK(ProvidersItems,deleted," = "," AND") */
        ProvidersItems.ItemProvidersId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update ItemProviders because ProvidersItems exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* ItemProviders  UserProfilesViaItemProviders on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="ItemProviders"
    CHILD_OWNER="", CHILD_TABLE="UserProfilesViaItemProviders"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_248", FK_COLUMNS="ItemProviderId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,UserProfilesViaItemProviders
      WHERE
        /*  %JoinFKPK(UserProfilesViaItemProviders,deleted," = "," AND") */
        UserProfilesViaItemProviders.ItemProviderId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update ItemProviders because UserProfilesViaItemProviders exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* ItemProviders  ItemType on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="ItemProviders"
    CHILD_OWNER="", CHILD_TABLE="ItemType"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_263", FK_COLUMNS="ItemProviderId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,ItemType
      WHERE
        /*  %JoinFKPK(ItemType,deleted," = "," AND") */
        ItemType.ItemProviderId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update ItemProviders because ItemType exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Location  ItemProviders on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Location"
    CHILD_OWNER="", CHILD_TABLE="ItemProviders"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_213", FK_COLUMNS="LocationId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(LocationId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Location
        WHERE
          /* %JoinFKPK(inserted,Location) */
          inserted.LocationId = Location.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.LocationId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update ItemProviders because Location does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_ItemsViaParameters ON ItemsViaParameters FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on ItemsViaParameters */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Item  ItemsViaParameters on child update no action */
  /* ERWIN_RELATION:CHECKSUM="0002f1f3", PARENT_OWNER="", PARENT_TABLE="Item"
    CHILD_OWNER="", CHILD_TABLE="ItemsViaParameters"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_211", FK_COLUMNS="ItemId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(ItemId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Item
        WHERE
          /* %JoinFKPK(inserted,Item) */
          inserted.ItemId = Item.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.ItemId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update ItemsViaParameters because Item does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* ItemParameters  ItemsViaParameters on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="ItemParameters"
    CHILD_OWNER="", CHILD_TABLE="ItemsViaParameters"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_212", FK_COLUMNS="ParameterId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(ParameterId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,ItemParameters
        WHERE
          /* %JoinFKPK(inserted,ItemParameters) */
          inserted.ParameterId = ItemParameters.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.ParameterId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update ItemsViaParameters because ItemParameters does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_ItemType ON ItemType FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on ItemType */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId integer,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* ItemType  Item on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="0003e985", PARENT_OWNER="", PARENT_TABLE="ItemType"
    CHILD_OWNER="", CHILD_TABLE="Item"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_249", FK_COLUMNS="ItemTypeId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,Item
      WHERE
        /*  %JoinFKPK(Item,deleted," = "," AND") */
        Item.ItemTypeId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update ItemType because Item exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Language  ItemType on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="ItemType"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_250", FK_COLUMNS="LanguageId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(LanguageId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Language
        WHERE
          /* %JoinFKPK(inserted,Language) */
          inserted.LanguageId = Language.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.LanguageId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update ItemType because Language does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* ItemProviders  ItemType on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="ItemProviders"
    CHILD_OWNER="", CHILD_TABLE="ItemType"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_263", FK_COLUMNS="ItemProviderId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(ItemProviderId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,ItemProviders
        WHERE
          /* %JoinFKPK(inserted,ItemProviders) */
          inserted.ItemProviderId = ItemProviders.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.ItemProviderId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update ItemType because ItemProviders does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_Language ON Language FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on Language */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId integer,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Language  WeatherConditions on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="000a3058", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="WeatherConditions"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_179", FK_COLUMNS="LanguageId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,WeatherConditions
      WHERE
        /*  %JoinFKPK(WeatherConditions,deleted," = "," AND") */
        WeatherConditions.LanguageId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Language because WeatherConditions exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Language  Item on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="Item"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_221", FK_COLUMNS="LanguageId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,Item
      WHERE
        /*  %JoinFKPK(Item,deleted," = "," AND") */
        Item.LanguageId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Language because Item exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Language  Suggestions on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="Suggestions"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_222", FK_COLUMNS="LanguageId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,Suggestions
      WHERE
        /*  %JoinFKPK(Suggestions,deleted," = "," AND") */
        Suggestions.LanguageId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Language because Suggestions exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Language  ActionType on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="ActionType"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_230", FK_COLUMNS="LanguageId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,ActionType
      WHERE
        /*  %JoinFKPK(ActionType,deleted," = "," AND") */
        ActionType.LanguageId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Language because ActionType exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Language  ConditionDescription on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="ConditionDescription"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_241", FK_COLUMNS="LanguageId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,ConditionDescription
      WHERE
        /*  %JoinFKPK(ConditionDescription,deleted," = "," AND") */
        ConditionDescription.LanguageId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Language because ConditionDescription exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Language  Units on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="Units"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_242", FK_COLUMNS="LanguageId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,Units
      WHERE
        /*  %JoinFKPK(Units,deleted," = "," AND") */
        Units.LanguageId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Language because Units exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Language  GoodThoughts on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="GoodThoughts"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_243", FK_COLUMNS="LanguageId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,GoodThoughts
      WHERE
        /*  %JoinFKPK(GoodThoughts,deleted," = "," AND") */
        GoodThoughts.LanguageId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Language because GoodThoughts exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Language  ItemType on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="ItemType"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_250", FK_COLUMNS="LanguageId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,ItemType
      WHERE
        /*  %JoinFKPK(ItemType,deleted," = "," AND") */
        ItemType.LanguageId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Language because ItemType exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Language  SuggestionTerm on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="SuggestionTerm"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_253", FK_COLUMNS="LanguageId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,SuggestionTerm
      WHERE
        /*  %JoinFKPK(SuggestionTerm,deleted," = "," AND") */
        SuggestionTerm.LanguageId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Language because SuggestionTerm exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Language  SeasonType on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="SeasonType"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_262", FK_COLUMNS="LanguageId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,SeasonType
      WHERE
        /*  %JoinFKPK(SeasonType,deleted," = "," AND") */
        SeasonType.LanguageId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Language because SeasonType exists.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_Location ON Location FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on Location */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId integer,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Location  SeasonViaLocation on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="0005c715", PARENT_OWNER="", PARENT_TABLE="Location"
    CHILD_OWNER="", CHILD_TABLE="SeasonViaLocation"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_196", FK_COLUMNS="LocationId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,SeasonViaLocation
      WHERE
        /*  %JoinFKPK(SeasonViaLocation,deleted," = "," AND") */
        SeasonViaLocation.LocationId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Location because SeasonViaLocation exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Location  ItemProviders on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Location"
    CHILD_OWNER="", CHILD_TABLE="ItemProviders"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_213", FK_COLUMNS="LocationId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,ItemProviders
      WHERE
        /*  %JoinFKPK(ItemProviders,deleted," = "," AND") */
        ItemProviders.LocationId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Location because ItemProviders exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Location  WeatherConditions on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Location"
    CHILD_OWNER="", CHILD_TABLE="WeatherConditions"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_214", FK_COLUMNS="LocationId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,WeatherConditions
      WHERE
        /*  %JoinFKPK(WeatherConditions,deleted," = "," AND") */
        WeatherConditions.LocationId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Location because WeatherConditions exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Location  UserProfile on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Location"
    CHILD_OWNER="", CHILD_TABLE="UserProfile"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_225", FK_COLUMNS="LocationId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,UserProfile
      WHERE
        /*  %JoinFKPK(UserProfile,deleted," = "," AND") */
        UserProfile.LocationId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Location because UserProfile exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* ClimatType  Location on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="ClimatType"
    CHILD_OWNER="", CHILD_TABLE="Location"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_193", FK_COLUMNS="ClimatId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(ClimatId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,ClimatType
        WHERE
          /* %JoinFKPK(inserted,ClimatType) */
          inserted.ClimatId = ClimatType.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.ClimatId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update Location because ClimatType does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_MessageTemplate ON MessageTemplate FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on MessageTemplate */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* EmailAccount  MessageTemplate on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00018cc8", PARENT_OWNER="", PARENT_TABLE="EmailAccount"
    CHILD_OWNER="", CHILD_TABLE="MessageTemplate"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_163", FK_COLUMNS="EmailAccountId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(EmailAccountId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,EmailAccount
        WHERE
          /* %JoinFKPK(inserted,EmailAccount) */
          inserted.EmailAccountId = EmailAccount.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.EmailAccountId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update MessageTemplate because EmailAccount does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_PhiUsers ON PhiUsers FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on PhiUsers */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId nvarchar(128),
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* PhiUsers  UserAttribute on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00072d29", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="UserAttribute"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_234", FK_COLUMNS="PhiUserId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,UserAttribute
      WHERE
        /*  %JoinFKPK(UserAttribute,deleted," = "," AND") */
        UserAttribute.PhiUserId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update PhiUsers because UserAttribute exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* PhiUsers  UserLogins on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="UserLogins"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_236", FK_COLUMNS="PhiUserId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,UserLogins
      WHERE
        /*  %JoinFKPK(UserLogins,deleted," = "," AND") */
        UserLogins.PhiUserId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update PhiUsers because UserLogins exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* PhiUsers  UserClaims on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="UserClaims"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_237", FK_COLUMNS="PhiUserId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,UserClaims
      WHERE
        /*  %JoinFKPK(UserClaims,deleted," = "," AND") */
        UserClaims.PhiUserId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update PhiUsers because UserClaims exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* PhiUsers  UserRoles on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="UserRoles"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_238", FK_COLUMNS="PhiUserId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,UserRoles
      WHERE
        /*  %JoinFKPK(UserRoles,deleted," = "," AND") */
        UserRoles.PhiUserId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update PhiUsers because UserRoles exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* PhiUsers  UserProfile on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="UserProfile"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_240", FK_COLUMNS="PhiUserId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,UserProfile
      WHERE
        /*  %JoinFKPK(UserProfile,deleted," = "," AND") */
        UserProfile.PhiUserId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update PhiUsers because UserProfile exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* PhiUsers  BlogStars on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="BlogStars"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_255", FK_COLUMNS="UserId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,BlogStars
      WHERE
        /*  %JoinFKPK(BlogStars,deleted," = "," AND") */
        BlogStars.UserId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update PhiUsers because BlogStars exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* PhiUsers  BlogComments on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="BlogComments"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_256", FK_COLUMNS="UserId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,BlogComments
      WHERE
        /*  %JoinFKPK(BlogComments,deleted," = "," AND") */
        BlogComments.UserId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update PhiUsers because BlogComments exists.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_ProvidersItems ON ProvidersItems FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on ProvidersItems */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* ItemProviders  ProvidersItems on child update no action */
  /* ERWIN_RELATION:CHECKSUM="0002e520", PARENT_OWNER="", PARENT_TABLE="ItemProviders"
    CHILD_OWNER="", CHILD_TABLE="ProvidersItems"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_202", FK_COLUMNS="ItemProvidersId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(ItemProvidersId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,ItemProviders
        WHERE
          /* %JoinFKPK(inserted,ItemProviders) */
          inserted.ItemProvidersId = ItemProviders.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.ItemProvidersId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update ProvidersItems because ItemProviders does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Item  ProvidersItems on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Item"
    CHILD_OWNER="", CHILD_TABLE="ProvidersItems"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_204", FK_COLUMNS="ItemId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(ItemId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Item
        WHERE
          /* %JoinFKPK(inserted,Item) */
          inserted.ItemId = Item.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.ItemId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update ProvidersItems because Item does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_QueuedEmail ON QueuedEmail FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on QueuedEmail */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* EmailAccount  QueuedEmail on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00018d0e", PARENT_OWNER="", PARENT_TABLE="EmailAccount"
    CHILD_OWNER="", CHILD_TABLE="QueuedEmail"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_162", FK_COLUMNS="EmailAccountId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(EmailAccountId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,EmailAccount
        WHERE
          /* %JoinFKPK(inserted,EmailAccount) */
          inserted.EmailAccountId = EmailAccount.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.EmailAccountId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update QueuedEmail because EmailAccount does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_Roles ON Roles FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on Roles */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId nvarchar(128),
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Roles  UserRoles on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="0000f97e", PARENT_OWNER="", PARENT_TABLE="Roles"
    CHILD_OWNER="", CHILD_TABLE="UserRoles"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_239", FK_COLUMNS="RoleId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,UserRoles
      WHERE
        /*  %JoinFKPK(UserRoles,deleted," = "," AND") */
        UserRoles.RoleId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Roles because UserRoles exists.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_Rules ON Rules FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on Rules */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* FactorType  Rules on child update no action */
  /* ERWIN_RELATION:CHECKSUM="000179d2", PARENT_OWNER="", PARENT_TABLE="FactorType"
    CHILD_OWNER="", CHILD_TABLE="Rules"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_190", FK_COLUMNS="FactorTypeId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(FactorTypeId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,FactorType
        WHERE
          /* %JoinFKPK(inserted,FactorType) */
          inserted.FactorTypeId = FactorType.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.FactorTypeId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update Rules because FactorType does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_SeasonType ON SeasonType FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on SeasonType */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* SeasonType  SeasonViaLocation on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00028573", PARENT_OWNER="", PARENT_TABLE="SeasonType"
    CHILD_OWNER="", CHILD_TABLE="SeasonViaLocation"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_195", FK_COLUMNS="SeasonId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,SeasonViaLocation
      WHERE
        /*  %JoinFKPK(SeasonViaLocation,deleted," = "," AND") */
        SeasonViaLocation.SeasonId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update SeasonType because SeasonViaLocation exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Language  SeasonType on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="SeasonType"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_262", FK_COLUMNS="LanguageId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(LanguageId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Language
        WHERE
          /* %JoinFKPK(inserted,Language) */
          inserted.LanguageId = Language.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.LanguageId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update SeasonType because Language does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_SeasonViaLocation ON SeasonViaLocation FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on SeasonViaLocation */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* SeasonType  SeasonViaLocation on child update no action */
  /* ERWIN_RELATION:CHECKSUM="0002e201", PARENT_OWNER="", PARENT_TABLE="SeasonType"
    CHILD_OWNER="", CHILD_TABLE="SeasonViaLocation"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_195", FK_COLUMNS="SeasonId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(SeasonId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,SeasonType
        WHERE
          /* %JoinFKPK(inserted,SeasonType) */
          inserted.SeasonId = SeasonType.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.SeasonId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update SeasonViaLocation because SeasonType does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Location  SeasonViaLocation on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Location"
    CHILD_OWNER="", CHILD_TABLE="SeasonViaLocation"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_196", FK_COLUMNS="LocationId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(LocationId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Location
        WHERE
          /* %JoinFKPK(inserted,Location) */
          inserted.LocationId = Location.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.LocationId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update SeasonViaLocation because Location does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_SuggestionItems ON SuggestionItems FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on SuggestionItems */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId integer,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Suggestions  SuggestionItems on child update no action */
  /* ERWIN_RELATION:CHECKSUM="0002db73", PARENT_OWNER="", PARENT_TABLE="Suggestions"
    CHILD_OWNER="", CHILD_TABLE="SuggestionItems"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_187", FK_COLUMNS="SuggestionId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(SuggestionId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Suggestions
        WHERE
          /* %JoinFKPK(inserted,Suggestions) */
          inserted.SuggestionId = Suggestions.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.SuggestionId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update SuggestionItems because Suggestions does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Item  SuggestionItems on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Item"
    CHILD_OWNER="", CHILD_TABLE="SuggestionItems"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_188", FK_COLUMNS="ItemId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(ItemId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Item
        WHERE
          /* %JoinFKPK(inserted,Item) */
          inserted.ItemId = Item.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.ItemId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update SuggestionItems because Item does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_Suggestions ON Suggestions FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on Suggestions */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Suggestions  SuggestionItems on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="0004264d", PARENT_OWNER="", PARENT_TABLE="Suggestions"
    CHILD_OWNER="", CHILD_TABLE="SuggestionItems"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_187", FK_COLUMNS="SuggestionId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,SuggestionItems
      WHERE
        /*  %JoinFKPK(SuggestionItems,deleted," = "," AND") */
        SuggestionItems.SuggestionId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Suggestions because SuggestionItems exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* WeatherConditions  Suggestions on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="WeatherConditions"
    CHILD_OWNER="", CHILD_TABLE="Suggestions"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_189", FK_COLUMNS="WeatherConditionId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(WeatherConditionId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,WeatherConditions
        WHERE
          /* %JoinFKPK(inserted,WeatherConditions) */
          inserted.WeatherConditionId = WeatherConditions.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.WeatherConditionId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update Suggestions because WeatherConditions does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Language  Suggestions on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="Suggestions"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_222", FK_COLUMNS="LanguageId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(LanguageId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Language
        WHERE
          /* %JoinFKPK(inserted,Language) */
          inserted.LanguageId = Language.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.LanguageId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update Suggestions because Language does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_SuggestionTerm ON SuggestionTerm FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on SuggestionTerm */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Language  SuggestionTerm on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00016c31", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="SuggestionTerm"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_253", FK_COLUMNS="LanguageId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(LanguageId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Language
        WHERE
          /* %JoinFKPK(inserted,Language) */
          inserted.LanguageId = Language.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.LanguageId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update SuggestionTerm because Language does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_Units ON Units FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on Units */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId integer,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Units  WeatherConditions on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00039fd1", PARENT_OWNER="", PARENT_TABLE="Units"
    CHILD_OWNER="", CHILD_TABLE="WeatherConditions"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_178", FK_COLUMNS="UnitsId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,WeatherConditions
      WHERE
        /*  %JoinFKPK(WeatherConditions,deleted," = "," AND") */
        WeatherConditions.UnitsId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Units because WeatherConditions exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Units  ItemParameters on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Units"
    CHILD_OWNER="", CHILD_TABLE="ItemParameters"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_210", FK_COLUMNS="UnitId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,ItemParameters
      WHERE
        /*  %JoinFKPK(ItemParameters,deleted," = "," AND") */
        ItemParameters.UnitId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update Units because ItemParameters exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Language  Units on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="Units"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_242", FK_COLUMNS="LanguageId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(LanguageId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Language
        WHERE
          /* %JoinFKPK(inserted,Language) */
          inserted.LanguageId = Language.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.LanguageId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update Units because Language does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_UserAttribute ON UserAttribute FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on UserAttribute */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* PhiUsers  UserAttribute on child update no action */
  /* ERWIN_RELATION:CHECKSUM="000178a6", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="UserAttribute"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_234", FK_COLUMNS="PhiUserId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(PhiUserId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,PhiUsers
        WHERE
          /* %JoinFKPK(inserted,PhiUsers) */
          inserted.PhiUserId = PhiUsers.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.PhiUserId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update UserAttribute because PhiUsers does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_UserClaims ON UserClaims FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on UserClaims */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* PhiUsers  UserClaims on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00016f17", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="UserClaims"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_237", FK_COLUMNS="PhiUserId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(PhiUserId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,PhiUsers
        WHERE
          /* %JoinFKPK(inserted,PhiUsers) */
          inserted.PhiUserId = PhiUsers.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.PhiUserId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update UserClaims because PhiUsers does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_UserLogins ON UserLogins FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on UserLogins */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insLoginProvider nvarchar(128), 
           @insProviderKey nvarchar(128), 
           @insPhiUserId nvarchar(128),
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* PhiUsers  UserLogins on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00014c06", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="UserLogins"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_236", FK_COLUMNS="PhiUserId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(PhiUserId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,PhiUsers
        WHERE
          /* %JoinFKPK(inserted,PhiUsers) */
          inserted.PhiUserId = PhiUsers.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update UserLogins because PhiUsers does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_UserProfile ON UserProfile FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on UserProfile */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insPhiUserId nvarchar(128),
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* UserProfile  UserProfilesViaItemProviders on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="0004e9df", PARENT_OWNER="", PARENT_TABLE="UserProfile"
    CHILD_OWNER="", CHILD_TABLE="UserProfilesViaItemProviders"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_246", FK_COLUMNS="PhiUserId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(PhiUserId)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,UserProfilesViaItemProviders
      WHERE
        /*  %JoinFKPK(UserProfilesViaItemProviders,deleted," = "," AND") */
        UserProfilesViaItemProviders.PhiUserId = deleted.PhiUserId
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update UserProfile because UserProfilesViaItemProviders exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* UserProfile  ItemLikes on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="UserProfile"
    CHILD_OWNER="", CHILD_TABLE="ItemLikes"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_266", FK_COLUMNS="PhiUserId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(PhiUserId)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,ItemLikes
      WHERE
        /*  %JoinFKPK(ItemLikes,deleted," = "," AND") */
        ItemLikes.PhiUserId = deleted.PhiUserId
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update UserProfile because ItemLikes exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Location  UserProfile on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Location"
    CHILD_OWNER="", CHILD_TABLE="UserProfile"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_225", FK_COLUMNS="LocationId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(LocationId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Location
        WHERE
          /* %JoinFKPK(inserted,Location) */
          inserted.LocationId = Location.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update UserProfile because Location does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* PhiUsers  UserProfile on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="UserProfile"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_240", FK_COLUMNS="PhiUserId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(PhiUserId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,PhiUsers
        WHERE
          /* %JoinFKPK(inserted,PhiUsers) */
          inserted.PhiUserId = PhiUsers.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update UserProfile because PhiUsers does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_UserProfilesViaItemProviders ON UserProfilesViaItemProviders FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on UserProfilesViaItemProviders */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* UserProfile  UserProfilesViaItemProviders on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00032648", PARENT_OWNER="", PARENT_TABLE="UserProfile"
    CHILD_OWNER="", CHILD_TABLE="UserProfilesViaItemProviders"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_246", FK_COLUMNS="PhiUserId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(PhiUserId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,UserProfile
        WHERE
          /* %JoinFKPK(inserted,UserProfile) */
          inserted.PhiUserId = UserProfile.PhiUserId
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.PhiUserId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update UserProfilesViaItemProviders because UserProfile does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* ItemProviders  UserProfilesViaItemProviders on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="ItemProviders"
    CHILD_OWNER="", CHILD_TABLE="UserProfilesViaItemProviders"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_248", FK_COLUMNS="ItemProviderId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(ItemProviderId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,ItemProviders
        WHERE
          /* %JoinFKPK(inserted,ItemProviders) */
          inserted.ItemProviderId = ItemProviders.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.ItemProviderId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update UserProfilesViaItemProviders because ItemProviders does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_UserRoles ON UserRoles FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on UserRoles */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insPhiUserId nvarchar(128), 
           @insRoleId nvarchar(128),
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* PhiUsers  UserRoles on child update no action */
  /* ERWIN_RELATION:CHECKSUM="000287a2", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="UserRoles"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_238", FK_COLUMNS="PhiUserId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(PhiUserId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,PhiUsers
        WHERE
          /* %JoinFKPK(inserted,PhiUsers) */
          inserted.PhiUserId = PhiUsers.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update UserRoles because PhiUsers does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Roles  UserRoles on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Roles"
    CHILD_OWNER="", CHILD_TABLE="UserRoles"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_239", FK_COLUMNS="RoleId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(RoleId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Roles
        WHERE
          /* %JoinFKPK(inserted,Roles) */
          inserted.RoleId = Roles.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update UserRoles because Roles does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 



CREATE TRIGGER tU_WeatherConditions ON WeatherConditions FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on WeatherConditions */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId integer,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* WeatherConditions  Suggestions on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="0006fc5f", PARENT_OWNER="", PARENT_TABLE="WeatherConditions"
    CHILD_OWNER="", CHILD_TABLE="Suggestions"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_189", FK_COLUMNS="WeatherConditionId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,Suggestions
      WHERE
        /*  %JoinFKPK(Suggestions,deleted," = "," AND") */
        Suggestions.WeatherConditionId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '% Cannot update WeatherConditions because Suggestions exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Units  WeatherConditions on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Units"
    CHILD_OWNER="", CHILD_TABLE="WeatherConditions"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_178", FK_COLUMNS="UnitsId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(UnitsId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Units
        WHERE
          /* %JoinFKPK(inserted,Units) */
          inserted.UnitsId = Units.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.UnitsId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update WeatherConditions because Units does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Language  WeatherConditions on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="WeatherConditions"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_179", FK_COLUMNS="LanguageId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(LanguageId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Language
        WHERE
          /* %JoinFKPK(inserted,Language) */
          inserted.LanguageId = Language.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.LanguageId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update WeatherConditions because Language does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Location  WeatherConditions on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Location"
    CHILD_OWNER="", CHILD_TABLE="WeatherConditions"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_214", FK_COLUMNS="LocationId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(LocationId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Location
        WHERE
          /* %JoinFKPK(inserted,Location) */
          inserted.LocationId = Location.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.LocationId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update WeatherConditions because Location does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* DataProvider  WeatherConditions on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="DataProvider"
    CHILD_OWNER="", CHILD_TABLE="WeatherConditions"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_215", FK_COLUMNS="DataProviderId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(DataProviderId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,DataProvider
        WHERE
          /* %JoinFKPK(inserted,DataProvider) */
          inserted.DataProviderId = DataProvider.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.DataProviderId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '% Cannot update WeatherConditions because DataProvider does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END

go
 

