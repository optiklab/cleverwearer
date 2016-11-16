CREATE TABLE UserLogins
( 
	LoginProvider        nvarchar(128)  NOT NULL ,
	ProviderKey          nvarchar(128)  NOT NULL ,
	PhiUserId            nvarchar(128)  NOT NULL 
)
GO
ALTER TABLE UserLogins
	ADD CONSTRAINT R_236 FOREIGN KEY (PhiUserId) REFERENCES PhiUsers(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE UserLogins
	ADD CONSTRAINT XPKUserLogins PRIMARY KEY  CLUSTERED (LoginProvider ASC,ProviderKey ASC,PhiUserId ASC)
GO
CREATE TRIGGER tU_UserLogins ON UserLogins FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on UserLogins */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insLoginProvider nvarchar(128), 
           @insProviderKey nvarchar(128), 
           @insPhiUserId nvarchar(128),
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* PhiUsers  UserLogins on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00014c06", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="UserLogins"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_236", FK_COLUMNS="PhiUserId" */
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
             @errmsg = '%s Cannot update UserLogins because PhiUsers does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END