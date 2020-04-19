module App.Types

open Locale

[<RequireQualifiedAccess>]
type Page =
    | Home
    | Admin of Admin.Types.Page
    | NotFound

type Msg =
    | AdminMsg of Admin.Types.Msg
    | NavigateTo of Page
    | ChangeLocale of Locale
    | Login

type State =
    { // Children state
      Admin: Admin.Types.State
      // App's state
      CurrentPage: Page
      Locale: Locale }