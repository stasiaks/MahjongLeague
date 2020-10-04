module Client.Tests

open Fable.Mocha

open App.State
open App.Types

open Client.Tests.Utility

let client =
    testList
        "Client"
        [ testCase "Default state values"
          <| fun _ ->
              let expectedPage = App.Types.Page.NotFound
              let expectedLocale = Locale.English

              let { CurrentPage = actualPage; Locale = actualLocale }, _ = init (None)

              Expect.equal actualPage expectedPage "Default page should be NotFound"
              Expect.equal actualLocale expectedLocale "Default locale should be English"

          testList
              "URL parser"
              [ testUrl "/" <| Page.Home
                testUrl "/home" <| Page.Home
                testUrl "/404" <| Page.NotFound
                testUrl "/admin"
                <| Page.Admin App.Admin.Types.Page.Dashboard
                testUrl "/admin/dashboard"
                <| Page.Admin App.Admin.Types.Page.Dashboard
                testUrl "/admin/users"
                <| Page.Admin(App.Admin.Types.Page.Users App.Admin.Users.Types.Page.List)
                testUrl "/admin/users/list"
                <| Page.Admin(App.Admin.Types.Page.Users App.Admin.Users.Types.Page.List) ] ]

let all =
    testList
        "All"
        [
#if FABLE_COMPILER // This preprocessor directive makes editor happy
          Shared.Tests.shared
          client
#endif
        ]

[<EntryPoint>]
let main _ = Mocha.runTests all
