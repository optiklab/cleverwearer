# cleverwearer

I built a project which you can find publicly in 'http://cleverwearer.com'. This project is about to help people to get dressed for the weather and plan users wardrobe for the trips, and also warning users about surprises of the nature.

http://cleverwearer.com

![1](https://optiklab.github.io/blog/img/cleverwearer.jpg)

I built it with ASP.NET MVC + WebAPI engine, it continues to grow and I have lots of ideas to implement. So, please visit it and share with me any suggestions, comments, etc.

![](http://cleverwearer.com/img/CleverWearerIcon_48px.png)

Prerequisites:
1. VS 2015 and .NET 4.6.1
2. SQL Server 2012

To run project you need to do 3 steps:

1. Open solution in Visual Studio 2015:

    a. Press right mouse button on Solution and select Restore Nuget Packages. It will download all necessary dependencies for you.
    
    b. Find all TODO comments and fix by your own values (You may skip this step for first time):
       - In SQL initialization script fix your e-mails and passwords
       - In Views\Shared\_Layout.chtml fix social API buttons and metrics API, which you should have your own.
   
2. To publish database:

    a. simply double click on Phi.MainDatabase/Phi.MainDatabase.publish.xml, choose settings of your database and click Publish.
    
    b. if you going to use russian lang as part of the system (it supports both english and russian), make sure you have collation Cyrillic_General_CI_AS in your database:
    
       SELECT CONVERT (varchar, SERVERPROPERTY('collation'));
       
       If not, change collaction to Cyrillic_General_CI_AS:
       
       ALTER DATABASE DATABASE_NAME COLLATE Cyrillic_General_CI_AS
       
       Or by these commands:
       
       ALTER DATABASE DATABASE_NAME SET SINGLE_USER WITH ROLLBACK IMMEDIATE
       ALTER DATABASE DATABASE_NAME COLLATE Cyrillic_General_CI_AS
       ALTER DATABASE DATABASE_NAME SET MULTI_USER
       
    c. Fill data by running 1 - Initial_data_cities.sql in database
    
    d. Fill data by running 2 - Initial_data_script.sql in database
    
    e. Fill data by running 3 - ShopStyle.sql in database

3. To publish source code:

    a. click right mouse button on Phi.MobileWebApp and choose Publish. If you don't have access to shared web server, then do next b, c, d steps.
    
    b. select Custom -> Folder to publish
    
    c. after publish is done all sources are in your Folder ready to upload to web server
    
    d. upload files from your publish folder to web server via ftp or simply by archive
    
4. Now you should be ready to go.
