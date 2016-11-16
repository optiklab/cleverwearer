CREATE TABLE Blog
( 
	Id                   integer IDENTITY ( 1,1 ) ,
	Theme                nvarchar(1000)  NULL ,
	Article              nvarchar(MAX)  NULL, 
    Header               NVARCHAR(1000) NULL, 
    Rating               INT NULL, 
    Tags                 NVARCHAR(1000) NULL, 
    PublishDate          DATETIME NULL, 
    UniqueId             NVARCHAR(100) NULL, 
    SourceUrl            NVARCHAR(500) NULL,
	LanguageId           integer  NULL, 
    ProviderName         NVARCHAR(500) NULL,
	Created              datetime  NULL 
)
GO
ALTER TABLE Blog
	ADD CONSTRAINT XPKBlog PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_Blog ON Blog FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on Blog */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId integer,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Blog  BlogStars on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="0001ff9e", PARENT_OWNER="", PARENT_TABLE="Blog"
    CHILD_OWNER="", CHILD_TABLE="BlogStars"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_254", FK_COLUMNS="BlogId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,BlogStars
      WHERE
        /*  %JoinFKPK(BlogStars,deleted," = "," AND") */
        BlogStars.BlogId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update Blog because BlogStars exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Blog  BlogComments on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Blog"
    CHILD_OWNER="", CHILD_TABLE="BlogComments"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_257", FK_COLUMNS="BlogId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,BlogComments
      WHERE
        /*  %JoinFKPK(BlogComments,deleted," = "," AND") */
        BlogComments.BlogId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update Blog because BlogComments exists.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END