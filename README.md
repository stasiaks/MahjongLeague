![.NET Core](https://github.com/stasiaks/MahjongLeague/workflows/.NET%20Core/badge.svg)

## Install pre-requisites

You'll need to install the following pre-requisites in order to build SAFE applications

* The [.NET Core SDK](https://www.microsoft.com/net/download) 3.1 or higher.
* [npm](https://nodejs.org/en/download/) package manager.
* [Node LTS](https://nodejs.org/en/download/) installed for the front end components.

## Work with the application

Before you run the project **for the first time only** you should install its local tools with this command:

```sh
dotnet tool restore
```
To concurrently run the server and the client components in watch mode use the following command:

```sh
dotnet fake build -t run
```

Then open [`http://localhost:8080`](http://localhost:8080) in your browser.

To run concurrently server and client tests in watch mode (run in a new terminal):

```sh
dotnet fake build -t runtests
```

Client tests are available under [`http://localhost:8081`](http://localhost:8081) in your browser and server tests are running in watch mode in console.
