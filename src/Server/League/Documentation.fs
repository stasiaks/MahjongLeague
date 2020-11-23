module League.Documentation

open System
open Fable.Remoting.Server
open Fable.Remoting.Giraffe

open Shared

let documentation =
    let docs = Docs.createFor<ILeagueApi>()
    Remoting.documentation "Leagues API" [
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
