module App.Leagues.View
module LT = Localization

open Fable.FontAwesome
open Fable.React
open Fable.React.Props
open Fulma
open Types

let breadcrumb lstr =
    Breadcrumb.breadcrumb []
        [ Breadcrumb.item [] [ a [] [ lstr LT.Leagues ] ]
          Breadcrumb.item [ Breadcrumb.Item.IsActive true ] [ a [] [ lstr LT.List ] ] ]

let list state lstr =
    Table.table [ Table.IsFullWidth; Table.IsStriped; Table.IsHoverable ]
        [ tbody []
            [ for user in state.Leagues ->
                tr [ Style [ Cursor "pointer" ] ]
                    [ td [ Style [ Width "5%" ] ]
                          [ Icon.icon [] [ Fa.i [ Fa.Regular.Flag ] [] ] ]
                      td [] [ str user.Name ]
                      td [] [ str <| Option.defaultValue "" user.Description ] ] ] ]

let render state dispatch lstr page =
    Container.container []
        [ breadcrumb lstr
          list state lstr ]
