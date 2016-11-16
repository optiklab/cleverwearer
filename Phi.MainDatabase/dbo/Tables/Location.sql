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
GO
ALTER TABLE Location
	ADD CONSTRAINT R_193 FOREIGN KEY (ClimatId) REFERENCES ClimatType(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE Location
	ADD CONSTRAINT XPKLocation PRIMARY KEY  CLUSTERED (Id ASC)
GO
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
             @errmsg = '%s Cannot update Location because SeasonViaLocation exists.'
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
             @errmsg = '%s Cannot update Location because ItemProviders exists.'
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
             @errmsg = '%s Cannot update Location because WeatherConditions exists.'
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
             @errmsg = '%s Cannot update Location because UserProfile exists.'
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
             @errmsg = '%s Cannot update Location because ClimatType does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END
GO

CREATE INDEX [IX_Location_Town] ON [dbo].[Location] ([Town])

GO

CREATE INDEX [IX_Location_Country] ON [dbo].[Location] ([Country])
GO
