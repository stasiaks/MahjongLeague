module Season.Documentation

open System
open Fable.Remoting.Server
open Fable.Remoting.Giraffe

open Shared

let documentation =
    let docs = Docs.createFor<ISeasonApi>()
    Remoting.documentation "Seasons API" [
        docs.route <@ fun api -> api.CreateSeason @>
        |> docs.alias "Create season"
        |> docs.description "Create new season that will contain matches"

        docs.route <@ fun api -> api.GetSeasonsForLeague @>
        |> docs.alias "Get all seasons for given league"
        |> docs.description "Returns all seasons under league with given ID"
        |> docs.example <@ fun api -> api.GetSeasonsForLeague <| Guid.NewGuid().ToString() @>

        docs.route <@ fun api -> api.GetSeasons @>
        |> docs.alias "Get all seasons"
        |> docs.description "Returns all seasons in application"
        |> docs.example <@ fun api -> api.GetSeasons <| () @>

        docs.route <@ fun api -> api.GetSeason @>
        |> docs.alias "Get season"
        |> docs.description "Return season with specific ID"
        |> docs.example <@ fun api -> api.GetSeason <| Guid.NewGuid().ToString() @>
    ]
