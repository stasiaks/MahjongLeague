module App.Admin.View

open Fable.FontAwesome
open Fable.React
open Fable.React.Props
open Fulma

open Shared
open App.Admin.Types
open App.Admin.Localization

let show =
    function
    | { Counter = Some counter } -> string counter.Value
    | { Counter = None } -> "Loading..."

let menu =
    Menu.menu []
        [ Menu.label [] [ str "General" ]
          Menu.list []
              [ Menu.Item.a [] [ str "Dashboard" ]
                Menu.Item.a [] [ str "Users" ] ] ]

let counter (model: State) (dispatch: Msg -> unit) =
    Field.div [ Field.IsGrouped ]
        [ Control.p [ Control.IsExpanded ]
              [ Input.text
                  [ Input.Disabled true
                    Input.Value(show model) ] ]
          Control.p []
              [ Button.a
                  [ Button.Color IsInfo
                    Button.OnClick(fun _ -> dispatch Increment) ] [ str "+" ] ]
          Control.p []
              [ Button.a
                  [ Button.Color IsInfo
                    Button.OnClick(fun _ -> dispatch Decrement) ] [ str "-" ] ] ]

let main page lstr =
    match page with
    | Dashboard -> App.Admin.Dashboard.View.render (DashboardToken >> lstr)
    | Users -> Users.View.render (UsersToken >> lstr)

let render (model: State) (dispatch: Msg -> unit) lstr page =
    Container.container []
        [ Columns.columns []
              [ Column.column [ Column.Width(Screen.All, Column.Is3) ] [ menu ]
                Column.column [ Column.Width(Screen.All, Column.Is9) ]
                    [ main page lstr
                      counter model dispatch ] ] ]
