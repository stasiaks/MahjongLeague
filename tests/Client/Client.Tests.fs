module Client.Tests

open Fable.Mocha

open App.State
open App.Types

let client = testList "Client" [
    testCase "Default state values" <| fun _ ->
        let expectedPage = App.Types.Page.NotFound
        let expectedLocale = Locale.English

        let
            { CurrentPage = acutalPage
              Locale = actualLocale } , _ = init(None)

        Expect.equal expectedPage acutalPage "Default page should be NotFound"
        Expect.equal expectedLocale actualLocale "Default locale should be English"
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