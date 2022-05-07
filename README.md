
# Introduction

Music Player Back-End Serivces

# Getting Started

1. Installation process

check on .../doc/readme_setup.md

2. Software dependencies

- ASP.NET Core 3.1
- SQL Server (sqllocaldb for development)
  

# Run database migration script (powershell)

- If a database named *MUSICPLAYER* has not been created, run:

> sqlcmd -S "(localdb)\MSSQLLocalDB" -E -Q "CREATE DATABASE MUSICPLAYER"

- Navigate to {project_root_directory}\Database\MusicPlayer\Scripts

> sqlcmd -S "(localdb)\MSSQLLocalDB" -E -d MUSICPLAYER -i runscript.sql -v FilePath="." ENV="local"
