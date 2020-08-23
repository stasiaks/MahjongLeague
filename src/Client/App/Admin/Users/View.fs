module App.Admin.Users.View

open Fable.React
open Fulma
open App.Admin.Users.Localization

let breadcrumb lstr =
    Breadcrumb.breadcrumb []
        [ Breadcrumb.item [] [ a [] [ lstr General ] ]
          Breadcrumb.item [ Breadcrumb.Item.IsActive true ] [ a [] [ lstr Users ] ] ]

let render lstr =
    Container.container []
        [ breadcrumb lstr ]
