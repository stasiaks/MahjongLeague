module Client.Tests

open Fable.Mocha

open App.State
open App.Types

let client = testList "Client" [
    testCase "Default page should be home" <| fun _ ->
        let expected = App.Types.Page.Home

        let { CurrentPage = acutal } , _ = init(None)

        Expect.equal expected acutal "Default page should be Home"
]

let all =
    testList "All"
        [
#if FABLE_COMPILER // This preprocessor directive makes editor happy
            Shared.Tests.shared
#endif
            client
        ]

[<EntryPoint>]
let main _ = Mocha.runTests all