CREATE TABLE ItemType
( 
	Id                   integer IDENTITY ( 1,1 ) ,
	Name                 varchar(255)  NULL ,
	LanguageId           integer  NULL ,
	ItemProviderId       int  NULL ,
	EnumType             int  NULL 
)
GO
ALTER TABLE ItemType
	ADD CONSTRAINT R_250 FOREIGN KEY (LanguageId) REFERENCES Language(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE ItemType
	ADD CONSTRAINT R_263 FOREIGN KEY (ItemProviderId) REFERENCES ItemProviders(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE ItemType
	ADD CONSTRAINT XPKItemType PRIMARY KEY  CLUSTERED (Id ASC)
GO
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
             @errmsg = '%s Cannot update ItemType because Item exists.'
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
             @errmsg = '%s Cannot update ItemType because Language does not exist.'
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
             @errmsg = '%s Cannot update ItemType because ItemProviders does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END