module App.Admin.View

open Fable.React
open Fulma

open Shared
open App.Admin.Types
open App.Admin.Localization

let show =
    function
    | { Counter = Some counter } -> string counter.Value
    | { Counter = None } -> "Loading..."

let menu dispatch =
    let menuDispatchProp msg = Menu.Item.OnClick(fun _ -> dispatch msg)
    Menu.menu []
        [ Menu.label [] [ str "General" ]
          Menu.list []
              [ Menu.Item.a [ menuDispatchProp (NavigateTo Dashboard |> ForParent) ] [ str "Dashboard" ]
                Menu.Item.a [ menuDispatchProp (NavigateTo Users |> ForParent) ] [ str "Users" ] ] ]

let counter (model: State) (dispatch: Msg -> unit) =
    Field.div [ Field.IsGrouped ]
        [ Control.p [ Control.IsExpanded ]
              [ Input.text
                  [ Input.Disabled true
                    Input.Value(show model) ] ]
          Control.p []
              [ Button.a
                  [ Button.Color IsInfo
                    Button.OnClick(fun _ -> Increment |> ForSelf |> dispatch) ] [ str "+" ] ]
          Control.p []
              [ Button.a
                  [ Button.Color IsInfo
                    Button.OnClick(fun _ -> Decrement |> ForSelf |> dispatch) ] [ str "-" ] ] ]

let main page lstr =
    match page with
    | Dashboard -> Dashboard.View.render (DashboardToken >> lstr)
    | Users -> Users.View.render (UsersToken >> lstr)

let render (state: State) (dispatch: Msg -> unit) lstr page =
    Container.container []
        [ Columns.columns []
              [ Column.column [ Column.Width(Screen.All, Column.Is3) ] [ menu dispatch ]
                Column.column [ Column.Width(Screen.All, Column.Is9) ]
                    [ main page lstr ] ] ]
