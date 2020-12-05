# PharmaAPI
API part for Information Systems module

# How to Launch
[TBA]

# How to setup database

To set up the database credentials, run the following commands:

```bash
dotnet user-secrets init
dotnet user-secrets set "Server" "YourServerName"
dotnet user-secrets set "Database" "YourDatabaseName"
dotnet user-secrets set "Uid" "YourUsername"
dotnet user-secrets set "DbPassword" "YourPassword"
```

# How to start

```bash
dotnet tool install --global dotnet-ef
#fix
dotnet ef database drop
rm -rf Migrations/ # kazkokiu nesamoniu priburta
dotnet ef migrations add reset
dotnet ef database update

# update
dotnet ef migrations add MakeEmployeeUsernameEmail
dotnet ef database update

dotnet run
```
