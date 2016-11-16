CREATE TABLE UserProfile
( 
	Gender               bit  NULL ,
	LocationId           integer  NULL ,
	PhiUserId            nvarchar(128)  NOT NULL ,
	AvatarPictureUrl     nvarchar(255)  NULL ,
	IsCompany            bit  NOT NULL ,
	MainCompanyUrl       nvarchar(255)  NULL ,
	CompanyName          nvarchar(255)  NULL ,
	CompanyCEO           nvarchar(255)  NULL ,
	CompanyEmail         nvarchar(255)  NULL ,
	CompanyPhone         nvarchar(50)  NULL ,
	CompanyFax           nvarchar(50)  NULL ,
	AdditionalInfo       nvarchar(2000)  NULL ,
	SellCompanyUrl       nvarchar(255)  NULL ,
	NotifyMeAboutSuddenWeatherEvents bit  NOT NULL 
)
GO
ALTER TABLE UserProfile
	ADD CONSTRAINT R_225 FOREIGN KEY (LocationId) REFERENCES Location(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE UserProfile
	ADD CONSTRAINT R_240 FOREIGN KEY (PhiUserId) REFERENCES PhiUsers(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE UserProfile
	ADD CONSTRAINT XPKUserProfile PRIMARY KEY  CLUSTERED (PhiUserId ASC)
GO
CREATE TRIGGER tU_UserProfile ON UserProfile FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on UserProfile */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insPhiUserId nvarchar(128),
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* UserProfile  UserProfilesViaItemProviders on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="0004e9df", PARENT_OWNER="", PARENT_TABLE="UserProfile"
    CHILD_OWNER="", CHILD_TABLE="UserProfilesViaItemProviders"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_246", FK_COLUMNS="PhiUserId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(PhiUserId)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,UserProfilesViaItemProviders
      WHERE
        /*  %JoinFKPK(UserProfilesViaItemProviders,deleted," = "," AND") */
        UserProfilesViaItemProviders.PhiUserId = deleted.PhiUserId
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update UserProfile because UserProfilesViaItemProviders exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* UserProfile  ItemLikes on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="UserProfile"
    CHILD_OWNER="", CHILD_TABLE="ItemLikes"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_266", FK_COLUMNS="PhiUserId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(PhiUserId)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,ItemLikes
      WHERE
        /*  %JoinFKPK(ItemLikes,deleted," = "," AND") */
        ItemLikes.PhiUserId = deleted.PhiUserId
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update UserProfile because ItemLikes exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Location  UserProfile on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Location"
    CHILD_OWNER="", CHILD_TABLE="UserProfile"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_225", FK_COLUMNS="LocationId" */
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
    
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '%s Cannot update UserProfile because Location does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* PhiUsers  UserProfile on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="UserProfile"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_240", FK_COLUMNS="PhiUserId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(PhiUserId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,PhiUsers
        WHERE
          /* %JoinFKPK(inserted,PhiUsers) */
          inserted.PhiUserId = PhiUsers.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '%s Cannot update UserProfile because PhiUsers does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END