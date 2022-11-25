# ParkingMeters

### Run
```
dotnet run --project .\src\WebApi\WebApi.csproj
```

### Swagger url
```
http://localhost:5041/swagger/index.html
```

### DB
To change database string edit in
```
\src\WebApi\appsettings.json
```
`"DefaultConnection"` field. 

Or use InMemoryDB with `"UseInMemoryDatabase"` equal `"true"`.

#### Migrations
For migration `ef tool` must be installed:
```
dotnet ef database update --project src\Infrastructure --startup-project src\WebApi
```

### Tests
```
dotnet test
```

In memory DB is used to run tests.