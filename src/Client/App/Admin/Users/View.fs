module App.Admin.Users.View

open Fable.React
open Fable.React.Props
open Fulma
open Localization
open Types
open Fable.FontAwesome

let breadcrumb lstr =
    Breadcrumb.breadcrumb []
        [ Breadcrumb.item [] [ a [] [ lstr General ] ]
          Breadcrumb.item [ Breadcrumb.Item.IsActive true ] [ a [] [ lstr Users ] ] ]

let list state lstr =
    Table.table [ Table.IsFullWidth; Table.IsStriped; Table.IsHoverable ]
        [ tbody []
            [ for user in state.Users ->
                tr [ Style [ Cursor "pointer" ] ]
                    [ td [ Style [ Width "5%" ] ]
                          [ Icon.icon [] [ Fa.i [ Fa.Regular.User ] [] ] ]
                      td [] [ str user.Name ] ] ] ]


let render state dispatch lstr page =
    Container.container []
        [ breadcrumb lstr
          list state lstr ]
