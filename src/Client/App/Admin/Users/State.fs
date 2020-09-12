
module App.Admin.Users.State

open Elmish

open System
open Shared
open App.Admin.Users.Types

module Server =
    open Fable.Remoting.Client

    let api: IUserApi =
        Remoting.createApi()
        |> Remoting.withRouteBuilder Route.builder
        |> Remoting.buildProxy<IUserApi>

let init() =
    let state =
        { Users = [] }
    state, Cmd.none

let update msg state =
    match state, msg with
    | _, OnNavigate page ->
        let nextState =
            { state with
                Users =
                    [ { Id = Guid.NewGuid(); Name = "Adam" }
                      { Id = Guid.NewGuid(); Name = "Kevin"}
                      { Id = Guid.NewGuid(); Name = "Asia" } ] }
        nextState, Cmd.none
    | _ -> state, Cmd.none