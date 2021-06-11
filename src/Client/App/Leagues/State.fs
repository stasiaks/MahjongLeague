module App.Leagues.State

open Elmish

open Shared
open App.Leagues.Types

module Server =
    open Fable.Remoting.Client

    let api: ILeagueApi =
        Remoting.createApi()
        |> Remoting.withRouteBuilder Route.builder
        |> Remoting.buildProxy<ILeagueApi>

let init() =
    let state =
        { Leagues = [] }
    state, Cmd.none

let update msg state =
    match state, msg with
    | _, OnNavigate page ->
        let loadLeaguesCmd = Cmd.OfAsync.perform Server.api.GetLeagues () (GetLeagues >> OnApiResponse)
        state, loadLeaguesCmd
    | _, OnApiResponse msg ->
        match msg with
        | GetLeagues result ->
            let nextState = { state with Leagues = result }
            nextState, Cmd.none
