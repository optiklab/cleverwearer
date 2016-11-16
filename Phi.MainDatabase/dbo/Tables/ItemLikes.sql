CREATE TABLE ItemLikes
( 
	Id                   int IDENTITY ( 1,1 ) ,
	ItemId               integer  NULL ,
	PhiUserId            nvarchar(128)  NULL ,
	IsWish               bit  NULL ,
	IsLike               bit  NULL ,
	Created              datetime  NULL 
)
GO
ALTER TABLE ItemLikes
	ADD CONSTRAINT R_265 FOREIGN KEY (ItemId) REFERENCES Item(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE ItemLikes
	ADD CONSTRAINT R_266 FOREIGN KEY (PhiUserId) REFERENCES UserProfile(PhiUserId)
		ON UPDATE NO ACTION
GO
ALTER TABLE ItemLikes
	ADD CONSTRAINT XPKItemLikes PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_ItemLikes ON ItemLikes FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on ItemLikes */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Item  ItemLikes on child update no action */
  /* ERWIN_RELATION:CHECKSUM="0002c535", PARENT_OWNER="", PARENT_TABLE="Item"
    CHILD_OWNER="", CHILD_TABLE="ItemLikes"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_265", FK_COLUMNS="ItemId" */
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
             @errmsg = '%s Cannot update ItemLikes because Item does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* UserProfile  ItemLikes on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="UserProfile"
    CHILD_OWNER="", CHILD_TABLE="ItemLikes"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_266", FK_COLUMNS="PhiUserId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(PhiUserId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,UserProfile
        WHERE
          /* %JoinFKPK(inserted,UserProfile) */
          inserted.PhiUserId = UserProfile.PhiUserId
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.PhiUserId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '%s Cannot update ItemLikes because UserProfile does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END