module App.Home.View

open Fable.React
open Fulma
open App.Home.Localization

let render lstr = Container.container [] [ h1 [] [ lstr HelloWorld ] ]
