module App.Admin.Types

open Shared

type Page =
    | Dashboard
    | Users of Users.Types.Page

type InternalMsg =
    | UsersMsg of Users.Types.InternalMsg
    | OnNavigate of Page

type OutMsg =
    | NavigateTo of Page

type Msg =
    | ForSelf of InternalMsg
    | ForParent of OutMsg

type TranslationDictionary<'msg> =
    { OnInternalMsg: InternalMsg -> 'msg
      OnNavigateTo: Page -> 'msg }

let usersTranslator =
    Users.Types.translator
        { OnInternalMsg = UsersMsg >> ForSelf
          OnNavigateTo = Page.Users >> NavigateTo >> ForParent }

let translator dictionary msg =
    match msg with
        | ForSelf message -> dictionary.OnInternalMsg message
        | ForParent (NavigateTo page) -> dictionary.OnNavigateTo page

type State =
    { Users: Users.Types.State }