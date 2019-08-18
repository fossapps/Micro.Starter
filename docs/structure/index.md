## File Structure
```
$ tree -I 'docs|*bin*|*obj*'
.
├── Src
│   ├── Starter.Net.Api
│   │   ├── Controllers
│   │   │   └── ValuesController.cs
│   │   ├── Program.cs
│   │   ├── Properties
│   │   │   └── launchSettings.json
│   │   ├── Starter.Net.Api.csproj
│   │   ├── Startup.cs
│   │   ├── appsettings.Development.json
│   │   └── appsettings.json
│   └── Starter.Net.Startup
│       ├── Middlewares
│       ├── Services
│       ├── Starter.Net.Startup.csproj
│       └── StartupBase.cs
├── Starter.Net.sln
├── Starter.Net.sln.DotSettings
├── Starter.Net.sln.DotSettings.user
└── readme.md
```

## Starter.Net.Startup
This project contains whatever happens at the startup, this means configuring middleware,
adding services to service container, etc.

This already contains uuid provider and a uuid middleware,
any values added via HttpContext from middleware can be accessed in controller via same context

`Middlewares` are only to define middlewares, see `Uuid` middleware for an example,
`Services` are class definitions and interfaces which will be added to service container later

## Starter.Net.Api
This is the project which is actually booted, once it boots, it configures and starts listening for incoming requests.
`Controllers` are where requests will land in, they're not supposed to contain any business logic,
but rather extract data from requests and pass in to other services.
