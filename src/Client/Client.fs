module Client

open Elmish
open Elmish.React

open App.View
open App.State

#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif

Program.mkProgram init update render
#if DEBUG
|> Program.withConsoleTrace
|> Program.withDebugger
#endif
|> Program.withReactBatched "elmish-app"
|> Program.run
