## Install pre-requisites

You'll need to install the following pre-requisites in order to build SAFE applications

* The [.NET Core SDK](https://www.microsoft.com/net/download).
* The [Yarn](https://yarnpkg.com/lang/en/docs/install/) package manager.
* [Node LTS](https://nodejs.org/en/download/) installed for the front end components.

## Work with the application

Before you run the project **for the first time only** you should install its local tools with this command:

```bash
dotnet tool restore
```


To concurrently run the server and the client components in watch mode use the following command:

```bash
dotnet fake build -t run
```


You can use the included `Dockerfile` and `build.fsx` script to deploy your application as Docker container. You can find more regarding this topic in the [official template documentation](https://safe-stack.github.io/docs/template-docker/).
