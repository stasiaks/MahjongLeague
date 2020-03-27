module App.Types

open Locale

type Page =
    | Home
    | Admin

type Msg =
    | AdminMsg of Admin.Types.Msg
    | NavigateTo of Page

type State =
    { // Children state
      Admin: Admin.Types.State
      // App's state
      CurrentPage: Page
      Locale: Locale }