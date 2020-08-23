
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
    let loadCountCmd = Cmd.OfAsync.perform Server.api.InitialCounter () InitialCountLoaded
    state, Cmd.batch
        [ loadCountCmd
          Cmd.map UsersMsg usersCmd ]

// The update function computes the next state of the application based on the current state and the incoming events/messages
// It can also run side-effects (encoded as commands) like calling the server via Http.
// these commands in turn, can dispatch messages to which the update function will react.
let update msg state =
    match state.Counter, msg with
    | Some counter, Increment ->
        let nextState = { state with Counter = Some { Value = counter.Value + 1 } }
        nextState, Cmd.none
    | Some counter, Decrement ->
        let nextState = { state with Counter = Some { Value = counter.Value - 1 } }
        nextState, Cmd.none
    | _, InitialCountLoaded initialCount ->
        let nextState = { state with Counter = Some initialCount }
        nextState, Cmd.none
    | _ -> state, Cmd.none