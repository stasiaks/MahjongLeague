module Server.Tests

open Expecto
open Swensen.Unquote

let server = testList "Server" []

let all =
    testList "All"
        [
            Shared.Tests.shared
            server
        ]

[<EntryPoint>]
let main argv =
    runTestsWithCLIArgs [] argv all
