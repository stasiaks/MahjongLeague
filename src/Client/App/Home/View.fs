module App.Home.View
module LT = Localization

open Fable.React
open Fulma

let render lstr = Container.container [] [ h1 [] [ lstr LT.HelloWorld ] ]
