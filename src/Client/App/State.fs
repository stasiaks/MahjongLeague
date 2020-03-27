module App.State

open Elmish

open Locale
open App.Types

// defines the initial state and initial command (= side-effect) of the application
let init(): State * Cmd<Msg> =
    let admin, adminCmd = Admin.State.init()

    let state =
        { Admin = admin
          CurrentPage = Admin
          Locale = English }
    state, Cmd.batch [ Cmd.map AdminMsg adminCmd ]

// The update function computes the next state of the application based on the current state and the incoming events/messages
// It can also run side-effects (encoded as commands) like calling the server via Http.
// these commands in turn, can dispatch messages to which the update function will react.
let update (msg: Msg) (state: State): State * Cmd<Msg> =
    match msg with
    | AdminMsg msg ->
        let nextAdminState, adminCmd = Admin.State.update msg state.Admin
        let nextState = { state with Admin = nextAdminState }
        let nextCmd = Cmd.map AdminMsg adminCmd
        nextState, nextCmd
    | NavigateTo destination ->
        let nextState = { state with CurrentPage = destination }
        nextState, Cmd.none
    | ChangeLocale locale ->
        let nextState = { state with Locale = locale }
        nextState, Cmd.none
