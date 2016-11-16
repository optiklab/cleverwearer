CREATE TABLE ItemProviders
( 
	Id                   int IDENTITY ( 1,1 ) ,
	Name                 nvarchar(255)  NULL ,
	PhisicalAddress      nvarchar(255)  NULL ,
	Email                nvarchar(255)  NULL ,
	Phone                nvarchar(20)  NULL ,
	LocationId           integer  NULL ,
	IsPublic             bit  NOT NULL ,
	EnumType             int  NULL 
)
GO
ALTER TABLE ItemProviders
	ADD CONSTRAINT R_213 FOREIGN KEY (LocationId) REFERENCES Location(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE ItemProviders
	ADD CONSTRAINT XPKItemProviders PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_ItemProviders ON ItemProviders FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on ItemProviders */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* ItemProviders  ProvidersItems on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="0004d757", PARENT_OWNER="", PARENT_TABLE="ItemProviders"
    CHILD_OWNER="", CHILD_TABLE="ProvidersItems"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_202", FK_COLUMNS="ItemProvidersId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,ProvidersItems
      WHERE
        /*  %JoinFKPK(ProvidersItems,deleted," = "," AND") */
        ProvidersItems.ItemProvidersId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update ItemProviders because ProvidersItems exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* ItemProviders  UserProfilesViaItemProviders on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="ItemProviders"
    CHILD_OWNER="", CHILD_TABLE="UserProfilesViaItemProviders"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_248", FK_COLUMNS="ItemProviderId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,UserProfilesViaItemProviders
      WHERE
        /*  %JoinFKPK(UserProfilesViaItemProviders,deleted," = "," AND") */
        UserProfilesViaItemProviders.ItemProviderId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update ItemProviders because UserProfilesViaItemProviders exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* ItemProviders  ItemType on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="ItemProviders"
    CHILD_OWNER="", CHILD_TABLE="ItemType"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_263", FK_COLUMNS="ItemProviderId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,ItemType
      WHERE
        /*  %JoinFKPK(ItemType,deleted," = "," AND") */
        ItemType.ItemProviderId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update ItemProviders because ItemType exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Location  ItemProviders on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Location"
    CHILD_OWNER="", CHILD_TABLE="ItemProviders"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_213", FK_COLUMNS="LocationId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(LocationId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Location
        WHERE
          /* %JoinFKPK(inserted,Location) */
          inserted.LocationId = Location.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.LocationId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '%s Cannot update ItemProviders because Location does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END