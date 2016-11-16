CREATE TABLE Suggestions
( 
	Id                   int IDENTITY ( 1,1 ) ,
	ShortDescription     nvarchar(255)  NULL ,
	FullDescription      nvarchar(1000)  NULL ,
	WeatherConditionId   integer  NULL ,
	Created              datetime  NULL ,
	LanguageId           integer  NULL 
)
GO
ALTER TABLE Suggestions
	ADD CONSTRAINT R_189 FOREIGN KEY (WeatherConditionId) REFERENCES WeatherConditions(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE Suggestions
	ADD CONSTRAINT R_222 FOREIGN KEY (LanguageId) REFERENCES Language(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE Suggestions
	ADD CONSTRAINT XPKSuggestions PRIMARY KEY  CLUSTERED (Id ASC)
GO
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
             @errmsg = '%s Cannot update Suggestions because SuggestionItems exists.'
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
             @errmsg = '%s Cannot update Suggestions because WeatherConditions does not exist.'
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
             @errmsg = '%s Cannot update Suggestions because Language does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END