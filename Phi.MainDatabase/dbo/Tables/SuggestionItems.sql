CREATE TABLE SuggestionItems
( 
	Id                   integer IDENTITY ( 1,1 ) ,
	SuggestionId         int  NULL ,
	ItemId               integer  NULL 
)
GO
ALTER TABLE SuggestionItems
	ADD CONSTRAINT R_187 FOREIGN KEY (SuggestionId) REFERENCES Suggestions(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE SuggestionItems
	ADD CONSTRAINT R_188 FOREIGN KEY (ItemId) REFERENCES Item(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE SuggestionItems
	ADD CONSTRAINT XPKSuggestionItems PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_SuggestionItems ON SuggestionItems FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on SuggestionItems */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId integer,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Suggestions  SuggestionItems on child update no action */
  /* ERWIN_RELATION:CHECKSUM="0002db73", PARENT_OWNER="", PARENT_TABLE="Suggestions"
    CHILD_OWNER="", CHILD_TABLE="SuggestionItems"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_187", FK_COLUMNS="SuggestionId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(SuggestionId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Suggestions
        WHERE
          /* %JoinFKPK(inserted,Suggestions) */
          inserted.SuggestionId = Suggestions.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.SuggestionId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '%s Cannot update SuggestionItems because Suggestions does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Item  SuggestionItems on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Item"
    CHILD_OWNER="", CHILD_TABLE="SuggestionItems"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_188", FK_COLUMNS="ItemId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(ItemId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Item
        WHERE
          /* %JoinFKPK(inserted,Item) */
          inserted.ItemId = Item.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.ItemId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '%s Cannot update SuggestionItems because Item does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END