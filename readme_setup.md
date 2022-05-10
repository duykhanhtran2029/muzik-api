# Setup Environment - Guideline

## I. Setup Database (Required)
**_a. Step 1:_** Download & Install Microsoft SQL Server (database engine) & Microsoft SQL Server Management Studio (tool)

**_b. Step 2:_** Initialize database of project by database first approach OR get latest update from new scripts of DBA by run these commands on CMD (Window)
_// change directory to your source repo_
`> cd {project_root_directory}\Database\MusicPlayer\Scripts` 
_// run script to initialize database_
`> sqlcmd -S (LocalDB)\MSSQLLocalDB -E -d MUSICPLAYER -i runscript.sql -v FilePath="." ENV="local"`

## II. Update latest schema from Database (Optional)
**_a. Step 1:_** Install these packages to the project storing database scripts and generated entities of DbContext
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.EntityFrameworkCore.Designs

**_b. Step 2:_** Install dotnet-ef tool by run command in CMD
`> dotnet tool install --global dotnet-ef`

**_c. Step 3:_** Generate all exist Tables in Database to Entity classes in Source Repo by run these commands in CMD (Window)
_// change directory to your source repo_
`> {project_root_directory}\Database`
_// generate entity classes (scaffold db context)_
`> Scaffold-DbContext "Server=(localdb)\MSSQLLocalDB;Database=MUSICPLAYER;Trusted_Connection=True" Microsoft.EntityFrameworkCore.SqlServer -Context MusicPlayerDbContext -OutputDir MusicPlayer/Models`
