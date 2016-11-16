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
	[Year]                 datetime  NULL ,
	ItemTypeId           integer  NULL ,
	IsPublic             bit  NULL ,
	[Root]                 int  NULL ,
	DefaultImageUri      nvarchar(2000)  NULL ,
	IsChild              bit  NOT NULL ,
	IsAvailable          bit  NOT NULL ,
	Price                decimal(10,2)  NULL ,
	Referrer             nvarchar(2000)  NULL ,
	Currency             int  NULL ,
	Created              datetime  NULL ,
	AvailableTill        datetime  NULL ,
	Likes                int  NULL ,
	IsWardrobe           bit  NOT NULL,
	ShowedTimes          int  NULL
)
GO
ALTER TABLE Item
	ADD CONSTRAINT R_221 FOREIGN KEY (LanguageId) REFERENCES Language(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE Item
	ADD CONSTRAINT R_227 FOREIGN KEY (ActionTypeId) REFERENCES ActionType(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE Item
	ADD CONSTRAINT R_249 FOREIGN KEY (ItemTypeId) REFERENCES ItemType(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE Item
	ADD CONSTRAINT XPKSuggestionItem PRIMARY KEY  CLUSTERED (Id ASC)
GO
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
             @errmsg = '%s Cannot update Item because SuggestionItems exists.'
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
             @errmsg = '%s Cannot update Item because ProvidersItems exists.'
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
             @errmsg = '%s Cannot update Item because ItemsViaParameters exists.'
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
             @errmsg = '%s Cannot update Item because Images exists.'
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
             @errmsg = '%s Cannot update Item because ItemLikes exists.'
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
             @errmsg = '%s Cannot update Item because Language does not exist.'
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
             @errmsg = '%s Cannot update Item because ActionType does not exist.'
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
             @errmsg = '%s Cannot update Item because ItemType does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END