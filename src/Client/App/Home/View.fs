module App.Home.View

open Fable.React
open Fulma
open App.Home.Localization

let render locale = Container.container [] [ h1 [] [ lstr locale HelloWorld ] ]
