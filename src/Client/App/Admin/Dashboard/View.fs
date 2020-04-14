module App.Admin.Dashboard.View

open Fable.FontAwesome
open Fable.React
open Fable.React.Props
open Fulma
open App.Admin.Dashboard.Localization

let breadcrumb lstr =
    Breadcrumb.breadcrumb []
        [ Breadcrumb.item [] [ a [] [ lstr General ] ]
          Breadcrumb.item [ Breadcrumb.Item.IsActive true ] [ a [] [ lstr Dashboard ] ] ]

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

let events =
    Card.card [ CustomClass "events-card" ]
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
          Card.footer [] [ Card.Footer.div [] [ str "View All" ] ] ]

let render lstr =
    Container.container []
        [ breadcrumb lstr
          info
          events ]
