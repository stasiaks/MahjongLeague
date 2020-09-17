module App.JwtDecode

open Fable.Core
open Fable.Core.JsInterop

type JwtPayload =
    { sub: string
      exp: int
      permissions: string seq }

type IJwtDecode =
    [<Emit "$0($1...)">]
    abstract DecodePayload: token: string -> JwtPayload

let JwtDecode: IJwtDecode = importDefault "jwt-decode"
