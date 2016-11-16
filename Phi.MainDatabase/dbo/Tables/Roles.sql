CREATE TABLE Roles
( 
	Name                 nvarchar(256)  NULL ,
	Active               bit  NOT NULL ,
	Id                   nvarchar(128)  NOT NULL 
)
GO
ALTER TABLE Roles
	ADD CONSTRAINT XPKRoles PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_Roles ON Roles FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on Roles */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId nvarchar(128),
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Roles  UserRoles on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="0000f97e", PARENT_OWNER="", PARENT_TABLE="Roles"
    CHILD_OWNER="", CHILD_TABLE="UserRoles"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_239", FK_COLUMNS="RoleId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,UserRoles
      WHERE
        /*  %JoinFKPK(UserRoles,deleted," = "," AND") */
        UserRoles.RoleId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update Roles because UserRoles exists.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END