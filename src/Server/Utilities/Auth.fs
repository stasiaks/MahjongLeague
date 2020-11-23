module Utilities.Auth

open System.Security.Claims
open Utilities.JsonWebToken
open Shared
open Shared.Authentication

let getAllPermissions (claims: ClaimsPrincipal) =
    claims.FindAll "permissions"
    |> Seq.map (fun claim -> Permission claim.Value)

let handleAuth { Token = token; Content = content } requiredPermissions createResult =
    validateToken token
    |> function
    | InvalidToken -> TokenInvalid |> Error
    | Expired -> TokenExpired |> Error
    | Valid claims when Seq.containsAll requiredPermissions (getAllPermissions claims) -> content |> createResult |> Ok
    | Valid _ -> NoAccess |> Error
