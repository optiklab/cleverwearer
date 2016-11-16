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
GO
ALTER TABLE WeatherConditions
	ADD CONSTRAINT R_178 FOREIGN KEY (UnitsId) REFERENCES Units(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE WeatherConditions
	ADD CONSTRAINT R_179 FOREIGN KEY (LanguageId) REFERENCES Language(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE WeatherConditions
	ADD CONSTRAINT R_214 FOREIGN KEY (LocationId) REFERENCES Location(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE WeatherConditions
	ADD CONSTRAINT R_215 FOREIGN KEY (DataProviderId) REFERENCES DataProvider(Id)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
GO
ALTER TABLE WeatherConditions
	ADD CONSTRAINT XPKWeatherConditions PRIMARY KEY  CLUSTERED (Id ASC)
GO
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
             @errmsg = '%s Cannot update WeatherConditions because Suggestions exists.'
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
             @errmsg = '%s Cannot update WeatherConditions because Units does not exist.'
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
             @errmsg = '%s Cannot update WeatherConditions because Language does not exist.'
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
             @errmsg = '%s Cannot update WeatherConditions because Location does not exist.'
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
             @errmsg = '%s Cannot update WeatherConditions because DataProvider does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END