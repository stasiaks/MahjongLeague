module App.Admin.State

open Elmish

open Shared
open App.Admin.Types

// defines the initial state and initial command (= side-effect) of the application
let init() =
    let users, usersCmd = Users.State.init()
    let state =
        { Users = users }
    state, Cmd.map UsersMsg usersCmd

let onNavigateMsgs page =
    match page with
    | Page.Users p -> [Users.Types.InternalMsg.OnNavigate p |> UsersMsg]
    | _ -> []

// The update function computes the next state of the application based on the current state and the incoming events/messages
// It can also run side-effects (encoded as commands) like calling the server via Http.
// these commands in turn, can dispatch messages to which the update function will react.
let update msg state createSecureRequest =
    match state, msg with
    | _, UsersMsg msg ->
        let nextUsersState, usersCmd = Users.State.update msg state.Users createSecureRequest
        let nextState = { state with Users = nextUsersState }
        let nextCmd = Cmd.map UsersMsg usersCmd
        nextState, nextCmd
    | _, OnNavigate destination ->
        let onNavigateMsgs = destination |> onNavigateMsgs |> List.map Cmd.ofMsg |> Cmd.batch
        state, Cmd.batch
            [ onNavigateMsgs ]
