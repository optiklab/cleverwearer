CREATE TABLE FactorType
( 
	Id                   int IDENTITY ( 1,1 ) ,
	Name                 nvarchar(255)  NOT NULL ,
	[Description]          nvarchar(1000)  NULL 
)
GO
ALTER TABLE FactorType
	ADD CONSTRAINT XPKFactorType PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_FactorType ON FactorType FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on FactorType */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* FactorType  Rules on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="0001f734", PARENT_OWNER="", PARENT_TABLE="FactorType"
    CHILD_OWNER="", CHILD_TABLE="Rules"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_190", FK_COLUMNS="FactorTypeId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,Rules
      WHERE
        /*  %JoinFKPK(Rules,deleted," = "," AND") */
        Rules.FactorTypeId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update FactorType because Rules exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* FactorType  Factor on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="FactorType"
    CHILD_OWNER="", CHILD_TABLE="Factor"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_218", FK_COLUMNS="FactorTypeId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,Factor
      WHERE
        /*  %JoinFKPK(Factor,deleted," = "," AND") */
        Factor.FactorTypeId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update FactorType because Factor exists.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END