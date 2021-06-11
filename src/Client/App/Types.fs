module App.Types

open Locale
open Auth0
open Shared.Authentication
open Shared.User

[<RequireQualifiedAccess>]
type Page =
    | Home
    | Admin of Admin.Types.Page
    | Leagues of Leagues.Types.Page
    | NotFound

type ApiResponseMsg =
    | Register of SecureResult<User>

type Msg =
    | AdminMsg of Admin.Types.InternalMsg
    | LeaguesMsg of Leagues.Types.InternalMsg
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

let leaguesTranslator =
    Leagues.Types.translator
        { OnInternalMsg = LeaguesMsg
          OnNavigateTo = Page.Leagues >> NavigateTo }

type State =
    { // Children state
      Admin: Admin.Types.State
      Leagues: Leagues.Types.State
      // App's state
      CurrentPage: Page
      Locale: Locale
      AccessToken: SecurityToken option
      UserInfo: IAuth0UserInfo option }
