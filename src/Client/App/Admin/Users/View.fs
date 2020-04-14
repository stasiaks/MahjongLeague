module App.Admin.Users.View

open Fable.React
open Fulma
open App.Admin.Users.Localization

let render lstr = Container.container [] [ h1 [] [ lstr Users ] ]
