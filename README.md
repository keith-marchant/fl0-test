# fl0-test

## Installation

1. Install SQL Server. I recommend running a docker container so you can remove the image once done.

```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong(!)PasswordHere" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

2. Provision a SQL Database called `Demo` by running the `DemoDBScriptQuery.sql` provided in the repo.
3. Add your SQL connection string to your .NET Secrets for the project by running the following in the project directory.

```PowerShell
dotnet user-secrets set "ConnectionStrings:DemoDb" "Server=localhost;Database=Demo;User Id=sa;Password=YourStrong(!)PasswordHere;"
```

## Running the Demo

```PowerShell
dotnet restore
cd .\src\Demo.WebApp
dotnet build
dotnet run
```

### API Routes

* Swagger is available (link from home page or `/swagger`) and this can test the APIs.
* API route for job status by room type is `GET /api/JobStatus`

## Testing the Demo

```PowerShell
dotnet restore
dotnet test
```

## Architecture

This solution uses a variant of the *Clean Architecture* however the `Domain` layer has been compressed into the `Application` layer for simplicity.
* The `WebApp` is responsible only for rendering content and providing the API controllers, no business logic exists within.
* The `Application` is repsonsible for all business logic and is the target of unit testing.
* The `Infrastructure` is responsible for connecting to external systems, in this case the database via an EntityFramework context.

### Application Flow

Command Query Separation is achieved via convention whereby the `IQuery` interface defines all read actions and the `ICommand` interface 
defines all data manipulation operations. This is further accentuated with the usage of separate `Query` and `Command` folders under the domain
object folder (e.g. `Jobs`).

`Mediatr` is used to handle the execution of queries or commands from the `WebApp` such that it is decoupled from the business logic implementations.
Each domain object folder (e.g. `Jobs`) contains a `Dtos` folder containing objects that may be returned to the calling application(s).

Note that these `Dtos` are separate from the internal `Entities` that are used for business logic within the `Application` layer.

The application entities are translated into the database objects in the `Infrastructure` using the configuration files locates in 
`Persistence/Configurations`. This decouples the databased implementation from the business objects defined in the application.

Note that some implementation logic leaks through via the EF context methods added to `IDemoDbContext` but these could be removed in favour
of a more comprehensive abstraction if required. I have, for simplicity in the demo, allowed those entity framework constructs to leak 
through though.

### Logging

I have not explicitly registered any loggers for this application, however I have wired in a pipeline behaviour `UnhandledExceptionBehaviour` to 
log all unhandled exceptions.

### Error Handling

I have added exception handling middleware `UnhandledExceptionBehaviour` to log all exceptions. I have also added `Hellang.Middleware.ProblemDetails` 
to the `WebApp` to format any exceptions the application does produce. I have mapped validation exceptions to HTTP 400 (see `Program.cs` for implementation).
I can extend this as needed. If you execute in `Development` then you will see stack traces in the exceptions, however in `Production` only the
status codes will be displayed.

### Validation

I am using `FluentValidation` to handle validation and it is integrated into the `Mediatr` pipeline via `RequestValidationBehaviour` such
that the presence of an `...CommandValidator` will trigger the validation logic. This decouples validation from the `WebApp` and allows resuse
should the the `Application` be called from a non-web domain.

## UI

The UI in this demo is intentionally simple as the test indicated it was not the primary focus. Should we want to see a more complex 
UI I would do the following:

1. Convert all controller endppoints to API endpoints and load the UI as a separate executable
2. Implement pagination for any data retrieval to improve page load
3. Add filters to the list page to make it usable
4. Improve the design by making the colours palatable and adding a workflow aspect to transition jobs between stages
5. Remove the full page postback on status update
6. Consider using a JavaScript front-end technology instead of Razor pages, although this depends on business requirements and user expectations.
