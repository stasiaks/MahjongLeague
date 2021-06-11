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
    let state =
        { Users = [] }
    state, Cmd.none

let update msg state createSecureRequest =
    match state, msg with
    | _, OnNavigate page ->
        let request = createSecureRequest()
        let loadCountCmd = Cmd.OfAsync.perform Server.api.GetUsers request (GetUsers >> OnApiResponse)
        state, loadCountCmd
    | _, OnApiResponse msg ->
        match msg with
        | GetUsers (Ok result) ->
            let nextState = { state with Users = result }
            nextState, Cmd.none
        | GetUsers (Error err) ->
            Browser.Dom.console.log(err)
            state, Cmd.none
