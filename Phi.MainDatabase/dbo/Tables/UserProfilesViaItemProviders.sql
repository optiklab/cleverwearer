CREATE TABLE UserProfilesViaItemProviders
( 
	PhiUserId            nvarchar(128)  NULL ,
	Id                   int IDENTITY ( 1,1 ) ,
	ItemProviderId       int  NULL 
)
GO
ALTER TABLE UserProfilesViaItemProviders
	ADD CONSTRAINT R_246 FOREIGN KEY (PhiUserId) REFERENCES UserProfile(PhiUserId)
		ON UPDATE NO ACTION
GO
ALTER TABLE UserProfilesViaItemProviders
	ADD CONSTRAINT R_248 FOREIGN KEY (ItemProviderId) REFERENCES ItemProviders(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE UserProfilesViaItemProviders
	ADD CONSTRAINT XPKUserProfilesViaItemProviders PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_UserProfilesViaItemProviders ON UserProfilesViaItemProviders FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on UserProfilesViaItemProviders */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* UserProfile  UserProfilesViaItemProviders on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00032648", PARENT_OWNER="", PARENT_TABLE="UserProfile"
    CHILD_OWNER="", CHILD_TABLE="UserProfilesViaItemProviders"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_246", FK_COLUMNS="PhiUserId" */
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
             @errmsg = '%s Cannot update UserProfilesViaItemProviders because UserProfile does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* ItemProviders  UserProfilesViaItemProviders on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="ItemProviders"
    CHILD_OWNER="", CHILD_TABLE="UserProfilesViaItemProviders"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_248", FK_COLUMNS="ItemProviderId" */
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
             @errmsg = '%s Cannot update UserProfilesViaItemProviders because ItemProviders does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END