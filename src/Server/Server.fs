open System
open System.IO
open System.Threading.Tasks

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open FSharp.Control.Tasks.V2
open Giraffe
open Saturn
open Shared

open Fable.Remoting.Server
open Fable.Remoting.Giraffe

let tryGetEnv = System.Environment.GetEnvironmentVariable >> function null | "" -> None | x -> Some x

let publicPath = Path.GetFullPath "../Client/public"

let port =
    "SERVER_PORT"
    |> tryGetEnv |> Option.map uint16 |> Option.defaultValue 8085us

let counterApi = {
    InitialCounter = fun () -> async { return { Value = 42 } }
}

let userApi = {
    GetUsers = fun () -> async { return [ { Id = Guid.NewGuid(); Name = "Kevin" } ] }
    GetUser = fun id -> async { return { Id = Guid.Parse(id); Name = "Kevin" } }
}

let counterApiDocs =
    let docs = Docs.createFor<ICounterApi>()
    Remoting.documentation "Counter API" [
        docs.route <@ fun api -> api.InitialCounter @>
        |> docs.alias "Get initial counter"
        |> docs.description "Returns initial value for counter"
    ]

let userApiDocs =
    let docs = Docs.createFor<IUserApi>()
    Remoting.documentation "Users API" [
        docs.route <@ fun api -> api.GetUsers @>
        |> docs.alias "Get all users"
        |> docs.description "Returns all users in application"

        docs.route <@ fun api -> api.GetUser @>
        |> docs.alias "Get user"
        |> docs.description "Return user with specific ID"
        |> docs.example <@ fun api -> api.GetUser <| Guid.NewGuid().ToString() @>
    ]

let counterRouter =
    Remoting.createApi()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.fromValue counterApi
    |> Remoting.withDocs "/docs/counter" counterApiDocs
    |> Remoting.buildHttpHandler

let userRouter =
    Remoting.createApi()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.fromValue userApi
    |> Remoting.withDocs "/docs/users" userApiDocs
    |> Remoting.buildHttpHandler

let combinedRouter = choose [ counterRouter; userRouter ]

let app = application {
    url ("http://0.0.0.0:" + port.ToString() + "/")
    use_router combinedRouter
    memory_cache
    use_static publicPath
    use_gzip
}

run app
