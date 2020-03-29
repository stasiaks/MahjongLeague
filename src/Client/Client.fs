module Client

open Elmish
open Elmish.React
open Elmish.Navigation

open App.View
open App.State
open App.Urls

#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif

Program.mkProgram init update render
#if DEBUG
|> Program.withConsoleTrace
|> Program.withDebugger
#endif
|> Program.toNavigable parser urlUpdate
|> Program.withReactBatched "elmish-app"
|> Program.run
