module App.Admin.Users.Types

open Shared
open Shared.Authentication
open Shared.User

type Page =
    | List

type ApiResponseMsg =
    | GetUsers of SecureResult<User list>

type InternalMsg =
    | OnNavigate of Page
    | OnApiResponse of ApiResponseMsg

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
    { Users: User list }
