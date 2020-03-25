module App.View

open Fable.FontAwesome
open Fable.React
open Fable.React.Props
open Fulma

open Shared
open App.Types

let show =
    function
    | { Counter = Some counter } -> string counter.Value
    | { Counter = None } -> "Loading..."

let safeComponents =
    let components =
        span []
            [ a [ Href "https://github.com/SAFE-Stack/SAFE-template" ]
                  [ str "SAFE  "
                    str Version.template ]
              str ", "
              a [ Href "https://saturnframework.github.io" ] [ str "Saturn" ]
              str ", "
              a [ Href "http://fable.io" ] [ str "Fable" ]
              str ", "
              a [ Href "https://elmish.github.io" ] [ str "Elmish" ]
              str ", "
              a [ Href "https://fulma.github.io/Fulma" ] [ str "Fulma" ]
              str ", "
              a [ Href "https://bulmatemplates.github.io/bulma-templates/" ] [ str "Bulma\u00A0Templates" ]
              str ", "
              a [ Href "https://zaid-ajaj.github.io/Fable.Remoting/" ] [ str "Fable.Remoting" ] ]

    span []
        [ str "Version "
          strong [] [ str Version.app ]
          str " powered by: "
          components ]

let navBrand =
    Navbar.navbar [ Navbar.Color IsWhite ]
        [ Container.container []
              [ Navbar.Brand.div [] [ Navbar.Item.a [ Navbar.Item.CustomClass "brand-text" ] [ str "SAFE Admin" ] ]
                Navbar.menu []
                    [ Navbar.Start.div []
                          [ Navbar.Item.a [] [ str "Home" ]
                            Navbar.Item.a [] [ str "Leeaderboard" ]
                            Navbar.Item.a [] [ str "Admin" ] ] ] ] ]

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
    div []
        [ navBrand
          Container.container []
              [ Columns.columns []
                    [ Column.column [ Column.Width(Screen.All, Column.Is3) ] [ menu ]
                      Column.column [ Column.Width(Screen.All, Column.Is9) ]
                          [ breadcrumb
                            hero
                            info
                            columns model dispatch ] ]
                footer [] [ safeComponents ] ] ]
