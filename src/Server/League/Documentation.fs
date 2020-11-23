module League.Documentation

open System
open Fable.Remoting.Server
open Fable.Remoting.Giraffe

open Shared
open Shared.Authentication

[<Literal>]
let ExampleToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"

let documentation =
    let docs = Docs.createFor<ILeagueApi>()
    Remoting.documentation "Users API" [
        docs.route <@ fun api -> api.CreateLeague @>
        |> docs.alias "Create league"
        |> docs.description "Create new league that will contain seasons"

        docs.route <@ fun api -> api.GetLeagues @>
        |> docs.alias "Get all leagues"
        |> docs.description "Returns all leagues in application"
        |> docs.example <@ fun api -> api.GetLeagues <| () @>

        docs.route <@ fun api -> api.GetLeague @>
        |> docs.alias "Get league"
        |> docs.description "Return league with specific ID"
        |> docs.example <@ fun api -> api.GetLeague <| Guid.NewGuid().ToString() @>
    ]
