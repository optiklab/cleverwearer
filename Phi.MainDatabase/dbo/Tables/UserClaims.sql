CREATE TABLE UserClaims
( 
	Id                   int  NOT NULL ,
	ClaimType            nvarchar(MAX)  NULL ,
	ClaimValue           nvarchar(MAX)  NULL ,
	PhiUserId            nvarchar(128)  NULL 
)
GO
ALTER TABLE UserClaims
	ADD CONSTRAINT R_237 FOREIGN KEY (PhiUserId) REFERENCES PhiUsers(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE UserClaims
	ADD CONSTRAINT XPKUserClaims PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_UserClaims ON UserClaims FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on UserClaims */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* PhiUsers  UserClaims on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00016f17", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="UserClaims"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_237", FK_COLUMNS="PhiUserId" */
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
    select @nullcnt = count(*) from inserted where
      inserted.PhiUserId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '%s Cannot update UserClaims because PhiUsers does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END