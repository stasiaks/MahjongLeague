open System
open System.IO

open Giraffe
open Saturn
open Shared

open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Shared.Authentication

let tryGetEnv = System.Environment.GetEnvironmentVariable >> function null | "" -> None | x -> Some x

let publicPath = Path.GetFullPath "../Client/public"

let port =
    "SERVER_PORT"
    |> tryGetEnv |> Option.map uint16 |> Option.defaultValue 8085us

let createSequentialUser n = { Id = Guid.NewGuid(); Name = sprintf "User %d" n }

let userApi = {
    GetUsers = fun _ -> async { return Seq.init (Random().Next(2,6)) (fun n -> createSequentialUser n) |> Seq.toList |> Ok }
    GetUser = fun { Content = id } -> async { return Ok { Id = Guid.Parse(id); Name = "Kevin" } }
}

[<Literal>]
let ExampleToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"

let userApiDocs =
    let docs = Docs.createFor<IUserApi>()
    Remoting.documentation "Users API" [
        docs.route <@ fun api -> api.GetUsers @>
        |> docs.alias "Get all users"
        |> docs.description "Returns all users in application"

        docs.route <@ fun api -> api.GetUser @>
        |> docs.alias "Get user"
        |> docs.description "Return user with specific ID"
        |> docs.example <@ fun api -> api.GetUser <| { Token = SecurityToken ExampleToken; Content = Guid.NewGuid().ToString() } @>
    ]

let userRouter =
    Remoting.createApi()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.fromValue userApi
    |> Remoting.withDocs "/docs/users" userApiDocs
    |> Remoting.buildHttpHandler

let combinedRouter = choose [ userRouter ]

let app = application {
    url ("http://0.0.0.0:" + port.ToString() + "/")
    use_router combinedRouter
    memory_cache
    use_static publicPath
    use_gzip
}

run app
