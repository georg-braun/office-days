# Office days

A little tool to communicate office days :)
- everyone can add/delete people and mark the office/home days (no authentication, no checks, just trust 👮)
- toggle office/home by clicking the icons
- show current and next week
- weekends are highlighted 😎

![Office days example](example.png)

# Technical stuff

Svelte Frontend + ASP.NET Backend

## Configure backend
- add environment variable `DataFolder` to the folder where the data should be stored (stored in a simple json file)
  - Possibilities: edit `appsettings.json`, use `dotnet user-secrets`, environment variables, ...
- you can also use the `docker-compose` file to start the backend

- the backend caches the app data and write them every x minutes to a json file (see `DataFolder` environment variable)

## Configure frontend
- add environment variable `VITE_BACKEND_SERVER` with the address of the backend server.
- start/deploy svelte project
