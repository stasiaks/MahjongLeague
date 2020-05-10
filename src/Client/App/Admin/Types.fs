
module App.Admin.Types

open Shared

type Page =
    | Dashboard
    | Users

type InternalMsg =
    | Increment
    | Decrement
    | InitialCountLoaded of Counter

type OutMsg =
    | NavigateTo of Page

type Msg =
    | ForSelf of InternalMsg
    | ForParent of OutMsg

type TranslationDictionary<'msg> =
    { OnInternalMsg: InternalMsg -> 'msg
      OnNavigateTo: Page -> 'msg }

let translator dictionary msg =
    match msg with
        | ForSelf message -> dictionary.OnInternalMsg message
        | ForParent (NavigateTo page) -> dictionary.OnNavigateTo page

type State =
    { Counter: Counter option }