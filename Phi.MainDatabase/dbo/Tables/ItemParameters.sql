CREATE TABLE ItemParameters
( 
	Id                   int IDENTITY ( 1,1 ) ,
	Code                 nvarchar(255)  NULL ,
	Name                 nvarchar(1000)  NULL ,
	UnitId               integer  NULL 
)
GO
ALTER TABLE ItemParameters
	ADD CONSTRAINT R_210 FOREIGN KEY (UnitId) REFERENCES Units(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE ItemParameters
	ADD CONSTRAINT XPKItemParameters PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_ItemParameters ON ItemParameters FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on ItemParameters */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* ItemParameters  ItemsViaParameters on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="0002839e", PARENT_OWNER="", PARENT_TABLE="ItemParameters"
    CHILD_OWNER="", CHILD_TABLE="ItemsViaParameters"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_212", FK_COLUMNS="ParameterId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,ItemsViaParameters
      WHERE
        /*  %JoinFKPK(ItemsViaParameters,deleted," = "," AND") */
        ItemsViaParameters.ParameterId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update ItemParameters because ItemsViaParameters exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Units  ItemParameters on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Units"
    CHILD_OWNER="", CHILD_TABLE="ItemParameters"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_210", FK_COLUMNS="UnitId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(UnitId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Units
        WHERE
          /* %JoinFKPK(inserted,Units) */
          inserted.UnitId = Units.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.UnitId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '%s Cannot update ItemParameters because Units does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END