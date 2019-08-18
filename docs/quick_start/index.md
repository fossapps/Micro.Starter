## Getting Started

Starter.Net is an opinionated starter kit for .NET projects which aims to be batteries included but removable project.
Everything used on this project should be fairly easy to replace or remove completely. It aims to include most
of production needs which includes monitoring, linting, building docker images, social logins etc pre-built

### Quick Start
```bash
git clone git@github.com:fossapps/Starter.Net
cd Starter.Net
dotnet restore
dotnet run --project ./Src/Starter.Net.Api/Starter.Net.Api.csproj
```
App start listening on `http://localhost:5000` & `https://localhost:5001` (you might have to confirm security warning)
