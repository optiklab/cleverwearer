CREATE TABLE SeasonViaLocation
( 
	Id                   int IDENTITY ( 1,1 ) ,
	SeasonId             int  NULL ,
	LocationId           integer  NULL ,
	StartDate            datetime  NULL ,
	EndDate              datetime  NULL 
)
GO
ALTER TABLE SeasonViaLocation
	ADD CONSTRAINT R_195 FOREIGN KEY (SeasonId) REFERENCES SeasonType(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE SeasonViaLocation
	ADD CONSTRAINT R_196 FOREIGN KEY (LocationId) REFERENCES Location(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE SeasonViaLocation
	ADD CONSTRAINT XPKSeasonViaLocation PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_SeasonViaLocation ON SeasonViaLocation FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on SeasonViaLocation */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* SeasonType  SeasonViaLocation on child update no action */
  /* ERWIN_RELATION:CHECKSUM="0002e201", PARENT_OWNER="", PARENT_TABLE="SeasonType"
    CHILD_OWNER="", CHILD_TABLE="SeasonViaLocation"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_195", FK_COLUMNS="SeasonId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(SeasonId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,SeasonType
        WHERE
          /* %JoinFKPK(inserted,SeasonType) */
          inserted.SeasonId = SeasonType.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.SeasonId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '%s Cannot update SeasonViaLocation because SeasonType does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Location  SeasonViaLocation on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Location"
    CHILD_OWNER="", CHILD_TABLE="SeasonViaLocation"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_196", FK_COLUMNS="LocationId" */
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
             @errmsg = '%s Cannot update SeasonViaLocation because Location does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END