module App.Types

open Locale
open Auth0
open Shared.Authentication
open Shared

[<RequireQualifiedAccess>]
type Page =
    | Home
    | Admin of Admin.Types.Page
    | NotFound

type ApiResponseMsg =
    | Register of SecureResult<User>

type Msg =
    | AdminMsg of Admin.Types.InternalMsg
    | NavigateTo of Page
    | ChangeLocale of Locale
    | Login
    | Authenticated of IAuthResult
    | UserInfoLoaded of IAuth0UserInfo
    | Logout
    | RegisterUser
    | OnApiResponse of ApiResponseMsg

let adminTranslator =
    Admin.Types.translator
        { OnInternalMsg = AdminMsg
          OnNavigateTo = Page.Admin >> NavigateTo }

type State =
    { // Children state
      Admin: Admin.Types.State
      // App's state
      CurrentPage: Page
      Locale: Locale
      AccessToken: SecurityToken option
      UserInfo: IAuth0UserInfo option }
