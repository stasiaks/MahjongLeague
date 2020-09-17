module User.Api

open System

open Shared
open Shared.Authentication
open Utilities.JsonWebToken
open System.Security.Claims

let createSequentialUser n =
    { Id = Guid.NewGuid()
      Name = sprintf "User %d" n }

let getAllPermissions (claims: ClaimsPrincipal) =
    claims.FindAll "permissions"
    |> Seq.map (fun claim -> claim.Value)

let handleAuth { Token = token; Content = content } requiredPermissions createResult =
    validateToken token
    |> function
    | InvalidToken -> TokenInvalid |> Error
    | Expired -> TokenExpired |> Error
    | Valid claims when Seq.containsAll requiredPermissions (getAllPermissions claims) -> content |> createResult |> Ok
    | Valid _ -> NoAccess |> Error

let api =
    { GetUsers =
          fun request ->
              async {
                  return handleAuth request [ "Users.Read" ] (fun content ->
                             Seq.init (Random().Next(2, 6)) (fun n -> createSequentialUser n)
                             |> Seq.toList)
              }
      GetUser =
          fun request ->
              async {
                  return handleAuth request [ "Users.Read" ] (fun content ->
                             { Id = Guid.Parse(content)
                               Name = "Kevin" })
              } }
