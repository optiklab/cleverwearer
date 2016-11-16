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
GO
ALTER TABLE Units
	ADD CONSTRAINT R_242 FOREIGN KEY (LanguageId) REFERENCES Language(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE Units
	ADD CONSTRAINT XPKUnits PRIMARY KEY  CLUSTERED (Id ASC)
GO
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
             @errmsg = '%s Cannot update Units because WeatherConditions exists.'
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
             @errmsg = '%s Cannot update Units because ItemParameters exists.'
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
             @errmsg = '%s Cannot update Units because Language does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END