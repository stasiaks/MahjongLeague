
module App.Admin.Users.State

open Elmish

open Shared
open App.Admin.Users.Types

module Server =
    open Fable.Remoting.Client

    let api: IUserApi =
        Remoting.createApi()
        |> Remoting.withRouteBuilder Route.builder
        |> Remoting.buildProxy<IUserApi>

let init() =
    let state = { Users = [] }
    state, Cmd.none

let update msg state =
    match state, msg with
    | _ -> state, Cmd.none