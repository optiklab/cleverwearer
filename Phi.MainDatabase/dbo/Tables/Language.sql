CREATE TABLE Language
( 
	Id                   integer IDENTITY ( 1,1 ) ,
	Code                 nvarchar(10)  NULL ,
	Fullname             nvarchar(255)  NULL 
)
GO
ALTER TABLE Language
	ADD CONSTRAINT XPKLanguage PRIMARY KEY  CLUSTERED (Id ASC)
GO
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
             @errmsg = '%s Cannot update Language because WeatherConditions exists.'
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
             @errmsg = '%s Cannot update Language because Item exists.'
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
             @errmsg = '%s Cannot update Language because Suggestions exists.'
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
             @errmsg = '%s Cannot update Language because ActionType exists.'
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
             @errmsg = '%s Cannot update Language because ConditionDescription exists.'
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
             @errmsg = '%s Cannot update Language because Units exists.'
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
             @errmsg = '%s Cannot update Language because GoodThoughts exists.'
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
             @errmsg = '%s Cannot update Language because ItemType exists.'
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
             @errmsg = '%s Cannot update Language because SuggestionTerm exists.'
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
             @errmsg = '%s Cannot update Language because SeasonType exists.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END