
module App.Admin.State

open Elmish

open Shared
open App.Admin.Types

module Server =
    open Fable.Remoting.Client

    /// A proxy you can use to talk to server directly
    let api: ICounterApi =
        Remoting.createApi()
        |> Remoting.withRouteBuilder Route.builder
        |> Remoting.buildProxy<ICounterApi>


// defines the initial state and initial command (= side-effect) of the application
let init() =
    let users, usersCmd = Users.State.init()
    let state =
        { Counter = None
          Users = users }
    state, Cmd.map UsersMsg usersCmd

let onNavigateMsgs page =
    match page with
    | Page.Users p -> [Users.Types.InternalMsg.OnNavigate p |> UsersMsg]
    | _ -> []

// The update function computes the next state of the application based on the current state and the incoming events/messages
// It can also run side-effects (encoded as commands) like calling the server via Http.
// these commands in turn, can dispatch messages to which the update function will react.
let update msg state =
    match state.Counter, msg with
    | _, UsersMsg msg ->
        let nextUsersState, usersCmd = Users.State.update msg state.Users
        let nextState = { state with Users = nextUsersState }
        let nextCmd = Cmd.map UsersMsg usersCmd
        nextState, nextCmd
    | Some counter, Increment ->
        let nextState = { state with Counter = Some { Value = counter.Value + 1 } }
        nextState, Cmd.none
    | Some counter, Decrement ->
        let nextState = { state with Counter = Some { Value = counter.Value - 1 } }
        nextState, Cmd.none
    | _, InitialCountLoaded initialCount ->
        let nextState = { state with Counter = Some initialCount }
        nextState, Cmd.none
    | _, OnNavigate destination ->
        let loadCountCmd = Cmd.OfAsync.perform Server.api.InitialCounter () InitialCountLoaded
        let onNavigateMsgs = destination |> onNavigateMsgs |> List.map Cmd.ofMsg |> Cmd.batch
        state, Cmd.batch
            [ loadCountCmd
              onNavigateMsgs ]
    | _ -> state, Cmd.none