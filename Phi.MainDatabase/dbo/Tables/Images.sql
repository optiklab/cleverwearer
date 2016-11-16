CREATE TABLE Images
( 
	Id                   int IDENTITY ( 1,1 ) ,
	ImageUrl             nvarchar(255)  NULL ,
	Height               int  NULL ,
	Width                int  NULL ,
	ItemId               integer  NULL 
)
GO
ALTER TABLE Images
	ADD CONSTRAINT R_224 FOREIGN KEY (ItemId) REFERENCES Item(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE Images
	ADD CONSTRAINT XPKImages PRIMARY KEY  CLUSTERED (Id ASC)
GO
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
             @errmsg = '%s Cannot update Images because Item does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END