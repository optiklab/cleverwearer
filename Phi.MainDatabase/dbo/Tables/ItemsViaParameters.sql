CREATE TABLE ItemsViaParameters
( 
	Id                   int IDENTITY ( 1,1 ) ,
	ItemId               integer  NULL ,
	ParameterId          int  NULL 
)
GO
ALTER TABLE ItemsViaParameters
	ADD CONSTRAINT R_211 FOREIGN KEY (ItemId) REFERENCES Item(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE ItemsViaParameters
	ADD CONSTRAINT R_212 FOREIGN KEY (ParameterId) REFERENCES ItemParameters(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE ItemsViaParameters
	ADD CONSTRAINT XPKItemsViaParameters PRIMARY KEY  CLUSTERED (Id ASC)
GO
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
             @errmsg = '%s Cannot update ItemsViaParameters because Item does not exist.'
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
             @errmsg = '%s Cannot update ItemsViaParameters because ItemParameters does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END