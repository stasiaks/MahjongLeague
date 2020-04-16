module App.NotFound.View

open Fable.React
open Fulma
open App.NotFound.Localization

let render lstr = Container.container [] [ h1 [] [ lstr Oops ] ]
