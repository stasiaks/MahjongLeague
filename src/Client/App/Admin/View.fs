module App.Admin.View

open Fable.FontAwesome
open Fable.React
open Fable.React.Props
open Fulma

open Shared
open App.Admin.Types

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

let breadcrumb =
    Breadcrumb.breadcrumb []
        [ Breadcrumb.item [] [ a [] [ str "General" ] ]
          Breadcrumb.item [ Breadcrumb.Item.IsActive true ] [ a [] [ str "Dashboard" ] ] ]

let hero =
    Hero.hero
        [ Hero.Color IsInfo
          Hero.CustomClass "welcome" ]
        [ Hero.body [] [ Container.container [] [ Heading.h1 [] [ str "Hello, Admin." ] ] ] ]

let info =
    section [ Class "info-tiles" ]
        [ Tile.ancestor [ Tile.Modifiers [ Modifier.TextAlignment(Screen.All, TextAlignment.Centered) ] ]
              [ Tile.parent []
                    [ Tile.child []
                          [ Box.box' []
                                [ Heading.p [] [ str "12" ]
                                  Heading.p [ Heading.IsSubtitle ] [ str "Players" ] ] ] ]
                Tile.parent []
                    [ Tile.child []
                          [ Box.box' []
                                [ Heading.p [] [ str "54" ]
                                  Heading.p [ Heading.IsSubtitle ] [ str "Games" ] ] ] ]
                Tile.parent []
                    [ Tile.child []
                          [ Box.box' []
                                [ Heading.p [] [ str "2" ]
                                  Heading.p [ Heading.IsSubtitle ] [ str "Seasons" ] ] ] ]
                Tile.parent []
                    [ Tile.child []
                          [ Box.box' []
                                [ Heading.p [] [ str "1" ]
                                  Heading.p [ Heading.IsSubtitle ] [ str "Leagues" ] ] ] ] ] ]

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

let columns (model: State) (dispatch: Msg -> unit) =
    Columns.columns []
        [ Column.column [ Column.Width(Screen.All, Column.Is6) ]
              [ Card.card [ CustomClass "events-card" ]
                    [ Card.header []
                          [ Card.Header.title [] [ str "Events" ]
                            Card.Header.icon [] [ Icon.icon [] [ Fa.i [ Fa.Solid.AngleDown ] [] ] ] ]
                      div [ Class "card-table" ]
                          [ Content.content []
                                [ Table.table [ Table.IsFullWidth; Table.IsStriped ]
                                      [ tbody []
                                            [ for _ in 1 .. 10 ->
                                                tr []
                                                    [ td [ Style [ Width "5%" ] ]
                                                          [ Icon.icon [] [ Fa.i [ Fa.Regular.Bell ] [] ] ]
                                                      td [] [ str "Lorem ipsum dolor aire" ] ] ] ] ] ]
                      Card.footer [] [ Card.Footer.div [] [ str "View All" ] ] ] ]
          Column.column [ Column.Width(Screen.All, Column.Is6) ]
              [ Card.card []
                    [ Card.header []
                          [ Card.Header.title [] [ str "Counter" ]
                            Card.Header.icon [] [ Icon.icon [] [ Fa.i [ Fa.Solid.AngleDown ] [] ] ] ]
                      Card.content [] [ Content.content [] [ counter model dispatch ] ] ] ] ]

let render (model: State) (dispatch: Msg -> unit) =
    Container.container []
        [ Columns.columns []
              [ Column.column [ Column.Width(Screen.All, Column.Is3) ] [ menu ]
                Column.column [ Column.Width(Screen.All, Column.Is9) ]
                    [ breadcrumb
                      hero
                      info
                      columns model dispatch ] ] ]
