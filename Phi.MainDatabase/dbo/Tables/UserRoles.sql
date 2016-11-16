CREATE TABLE UserRoles
( 
	AddRoleDate          datetime  NOT NULL ,
	PhiUserId            nvarchar(128)  NOT NULL ,
	RoleId               nvarchar(128)  NOT NULL 
)
GO
ALTER TABLE UserRoles
	ADD CONSTRAINT R_238 FOREIGN KEY (PhiUserId) REFERENCES PhiUsers(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE UserRoles
	ADD CONSTRAINT R_239 FOREIGN KEY (RoleId) REFERENCES Roles(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE UserRoles
	ADD CONSTRAINT XPKUserRoles PRIMARY KEY  CLUSTERED (PhiUserId ASC,RoleId ASC)
GO
CREATE TRIGGER tU_UserRoles ON UserRoles FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on UserRoles */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insPhiUserId nvarchar(128), 
           @insRoleId nvarchar(128),
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* PhiUsers  UserRoles on child update no action */
  /* ERWIN_RELATION:CHECKSUM="000287a2", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="UserRoles"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_238", FK_COLUMNS="PhiUserId" */
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
             @errmsg = '%s Cannot update UserRoles because PhiUsers does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Roles  UserRoles on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Roles"
    CHILD_OWNER="", CHILD_TABLE="UserRoles"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_239", FK_COLUMNS="RoleId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(RoleId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Roles
        WHERE
          /* %JoinFKPK(inserted,Roles) */
          inserted.RoleId = Roles.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '%s Cannot update UserRoles because Roles does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END