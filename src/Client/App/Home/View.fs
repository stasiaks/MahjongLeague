module App.Home.View

open Fable.React
open App.Home.Localization

let render locale = h1 [] [ lstr locale HelloWorld ]
