module App.State

open Browser
open Elmish

open Locale
open App.Types
open App.Urls
open App.Auth0
open Shared.Authentication
open Elmish.Navigation

[<Literal>]
let LocaleStorageKey = "app.locale"

let localeFromStorage =
    localStorage.getItem LocaleStorageKey
    |> function
    | a when a = (string English) -> Some English
    | a when a = (string Polish) -> Some Polish
    | _ -> None

let options: IAuth0Options = { language = "en" }

let auth0Lock = Auth0Lock.Create (clientId, domain, options)

// defines the initial state and initial command (= side-effect) of the application
let init (page: Page option): State * Cmd<Msg> =
    let admin, adminCmd = Admin.State.init()

    let state =
        { Admin = admin
          // Application state
          CurrentPage = Option.defaultValue Page.NotFound page
          Locale = Option.defaultValue English localeFromStorage
          AccessToken = None }
    state, Cmd.batch [ Cmd.map AdminMsg adminCmd ]

let onAuthenticated state =
  let sub dispatch = auth0Lock.on_authenticated (fun result -> result |> Authenticated |> UserMsg |> dispatch)
  Cmd.ofSub sub

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
        history.pushState ((), "", (toUrl destination))
        nextState, Cmd.none
    | ChangeLocale locale ->
        let nextState = { state with Locale = locale }
        localStorage.setItem (LocaleStorageKey, (string locale))
        nextState, Cmd.none
    | Login ->
        auth0Lock.show()
        state, Cmd.none
    | Authenticated result ->
        let nextState = { state with AccessToken = result.accessToken |> SecurityToken |> Some }
        nextState, Cmd.none