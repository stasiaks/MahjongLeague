module App.State

open Elmish

open Shared
open App.Types

module Server =
    open Fable.Remoting.Client

    /// A proxy you can use to talk to server directly
    let api: ICounterApi =
        Remoting.createApi()
        |> Remoting.withRouteBuilder Route.builder
        |> Remoting.buildProxy<ICounterApi>

// defines the initial state and initial command (= side-effect) of the application
let init(): State * Cmd<Msg> =
    let admin, adminCmd = Admin.State.init()

    let model =
        { Admin = admin
          CurrentPage = Admin }
    model, Cmd.batch [ Cmd.map AdminMsg adminCmd ]

// The update function computes the next state of the application based on the current state and the incoming events/messages
// It can also run side-effects (encoded as commands) like calling the server via Http.
// these commands in turn, can dispatch messages to which the update function will react.
let update (msg: Msg) (currentModel: State): State * Cmd<Msg> =
    match currentModel, msg with
    | _, (NavigateTo destination) ->
        let newModel = {currentModel with CurrentPage = destination}
        newModel, Cmd.none
    | _ -> currentModel, Cmd.none
