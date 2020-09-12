
module App.Auth0

open Fable.Core
open Fable.Core.JsInterop
open System

// fsharplint:disable MemberNames RecordFieldNames
// suppress required for due to JS Interop
type IAuth0Error =
  abstract member error: obj with get, set
  abstract member errorDescription: string with get, set

type IAuth0UserInfo =
  abstract member email: string with get, set
  abstract member name: string with get, set
  abstract member picture: string with get, set

type IAuthResult =
    abstract accessToken: string with get, set

type Auth0LogoutOptions =
    { clientId: string
      redirectTo: string
      federated: bool }

type Auth0AuthOptions =
    { audience: string
      redirect: bool
      redirectUrl: string
      responseType: string
      autoParseHash: bool
      sso: bool }

type Auth0Options =
    { language: string
      rememberLastLogin: bool
      auth: Auth0AuthOptions }

type IAuth0Lock =
    [<Emit "new $0($1...)">]
    abstract Create: clientId: string * domain: string * options: Auth0Options -> IAuth0Lock

    abstract show: unit -> unit;

    abstract logout: Auth0LogoutOptions -> unit;

    [<Emit "$0.on('authenticated', $1...)">]
    abstract on_authenticated: callback: Func<IAuthResult, unit> -> unit

    abstract getUserInfo: token: string * callback: Func<IAuth0Error, IAuth0UserInfo, unit> -> unit

let Auth0Lock: IAuth0Lock = importDefault "auth0-lock"
// fsharplint:enable