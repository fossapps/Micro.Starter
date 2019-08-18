# Projects in this solution
As you might have noticed this repo has two projects (as of this writing),
namely `Starter.Net.Api` and `Starter.Net.Startup`

Each project has their own purpose, and everything must have a place.

## Starter.Net.Startup
This is where application boot happens, this is where we add services to service containers, add middlewares.
Only something very specific to `Starter.Net.Api` will be added from Api project which should happen rarely.
Services may be configured from here, but they will have their own place,
however anything very specific to http requests will stay in this project

## Starter.Net.Api
This is the main project which takes care of taking requests,
validating and forwarding to service, `Controllers` in this project won't have any business logic

