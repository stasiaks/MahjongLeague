module Server.Tests

open Expecto
open Hopac
open Logary.Configuration
open Logary.Adapters.Facade
open Logary.Targets

let server = testList "Server" [
    testCase "Failing test case" <| fun _ ->
        Expect.isFalse true "Failed!"
]

let all =
    testList "All"
        [
            Shared.Tests.shared
            server
        ]

[<EntryPoint>]
let main argv =
    let logary =
        Config.create "Server.Tests" "localhost"
        |> Config.targets [ LiterateConsole.create LiterateConsole.empty "console" ]
        |> Config.processing (Events.events |> Events.sink ["console";])
        |> Config.build
        |> run
    LogaryFacadeAdapter.initialise<Expecto.Logging.Logger> logary

    runTestsWithCLIArgs [] argv all
