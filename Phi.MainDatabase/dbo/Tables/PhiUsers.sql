CREATE TABLE PhiUsers
( 
	Id                   nvarchar(128)  NOT NULL ,
	UserName             nvarchar(256)  NULL ,
	Password             nvarchar(550)  NULL ,
	ReminderQuestion     nvarchar(255)  NULL ,
	ReminderAnswer       nvarchar(255)  NULL ,
	PasswordSalt         nvarchar(255)  NULL ,
	PhoneNumber          nvarchar(50)  NULL ,
	FirstName            nvarchar(550)  NULL ,
	LastName             nvarchar(550)  NULL ,
	Email                nvarchar(256)  NULL ,
	LastLoggedDate       datetime  NULL ,
	DateCreated          datetime  NULL ,
	Active               bit  NOT NULL ,
	UserType             int  NULL ,
	UserNameFormat       int  NULL ,
	PasswordFormat       int  NULL ,
	EmailConfirmed       bit  NOT NULL 
	CONSTRAINT Default_Bit_219892112
		 DEFAULT  0,
	PasswordHash         nvarchar(MAX)  NULL ,
	SecurityStamp        nvarchar(MAX)  NULL ,
	PhoneNumberConfirmed bit  NOT NULL 
	CONSTRAINT Default_Bit_1991901517
		 DEFAULT  0,
	TwoFactorEnabled     bit  NOT NULL 
	CONSTRAINT Default_Bit_88774114
		 DEFAULT  0,
	LockoutEndDateUtc    datetime  NULL ,
	LockoutEnabled       bit  NOT NULL 
	CONSTRAINT Default_Bit_957489570
		 DEFAULT  0,
	AccessFailedCount    int  NOT NULL 
	CONSTRAINT Default_Number_1430458855
		 DEFAULT  0
)
GO
ALTER TABLE PhiUsers
	ADD CONSTRAINT XPKPhiUser PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_PhiUsers ON PhiUsers FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on PhiUsers */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId nvarchar(128),
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* PhiUsers  UserAttribute on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00072d29", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="UserAttribute"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_234", FK_COLUMNS="PhiUserId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,UserAttribute
      WHERE
        /*  %JoinFKPK(UserAttribute,deleted," = "," AND") */
        UserAttribute.PhiUserId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update PhiUsers because UserAttribute exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* PhiUsers  UserLogins on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="UserLogins"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_236", FK_COLUMNS="PhiUserId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,UserLogins
      WHERE
        /*  %JoinFKPK(UserLogins,deleted," = "," AND") */
        UserLogins.PhiUserId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update PhiUsers because UserLogins exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* PhiUsers  UserClaims on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="UserClaims"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_237", FK_COLUMNS="PhiUserId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,UserClaims
      WHERE
        /*  %JoinFKPK(UserClaims,deleted," = "," AND") */
        UserClaims.PhiUserId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update PhiUsers because UserClaims exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* PhiUsers  UserRoles on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="UserRoles"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_238", FK_COLUMNS="PhiUserId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,UserRoles
      WHERE
        /*  %JoinFKPK(UserRoles,deleted," = "," AND") */
        UserRoles.PhiUserId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update PhiUsers because UserRoles exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* PhiUsers  UserProfile on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="UserProfile"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_240", FK_COLUMNS="PhiUserId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,UserProfile
      WHERE
        /*  %JoinFKPK(UserProfile,deleted," = "," AND") */
        UserProfile.PhiUserId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update PhiUsers because UserProfile exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* PhiUsers  BlogStars on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="BlogStars"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_255", FK_COLUMNS="UserId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,BlogStars
      WHERE
        /*  %JoinFKPK(BlogStars,deleted," = "," AND") */
        BlogStars.UserId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update PhiUsers because BlogStars exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* PhiUsers  BlogComments on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="BlogComments"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_256", FK_COLUMNS="UserId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,BlogComments
      WHERE
        /*  %JoinFKPK(BlogComments,deleted," = "," AND") */
        BlogComments.UserId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update PhiUsers because BlogComments exists.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END