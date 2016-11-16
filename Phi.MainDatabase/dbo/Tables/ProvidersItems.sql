CREATE TABLE ProvidersItems
( 
	Id                   int IDENTITY ( 1,1 ) ,
	ItemProvidersId      int  NULL ,
	ItemId               int  NULL 
)
GO
ALTER TABLE ProvidersItems
	ADD CONSTRAINT R_202 FOREIGN KEY (ItemProvidersId) REFERENCES ItemProviders(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE ProvidersItems
	ADD CONSTRAINT R_204 FOREIGN KEY (ItemId) REFERENCES Item(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE ProvidersItems
	ADD CONSTRAINT XPKProvidersItems PRIMARY KEY  CLUSTERED (Id ASC)
GO
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
             @errmsg = '%s Cannot update ProvidersItems because ItemProviders does not exist.'
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
             @errmsg = '%s Cannot update ProvidersItems because Item does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END