module User.Api

open System
open System.Security.Claims
open FSharp.Data.Sql
open Utilities.JsonWebToken
open Shared
open Shared.Authentication

[<Literal>]
let DbVendor = Common.DatabaseProviderTypes.POSTGRESQL

[<Literal>]
let ConnString =
    "Host=localhost;Database=mahjong;Username=test_app;Password=pass"

[<Literal>]
let ResPath = __SOURCE_DIRECTORY__ + @"./lib"

[<Literal>]
let Schema = "mahjong"

[<Literal>]
let IndividualAmount = 1000

[<Literal>]
let UseOptionTypes = true

type DB =
    SqlDataProvider<DatabaseVendor=DbVendor, ConnectionString=ConnString, ResolutionPath=ResPath, IndividualsAmount=IndividualAmount, UseOptionTypes=UseOptionTypes, Owner=Schema>

let context =
    DB.GetDataContext(selectOperations = SelectOperations.DatabaseSide)

let database = context.Mahjong

let createSequentialUser n =
    { Id = Guid.NewGuid()
      Name = sprintf "User %d" n }

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

let api =
    { GetUsers =
          fun request ->
              async {
                  return handleAuth request [ Permissions.Users.Read ] (fun content ->
                             query {
                                 for u in database.Users do
                                     select { Id = u.Id; Name = u.Name }
                             }
                             |> Seq.toList)
              }
      GetUser =
          fun request ->
              async {
                  return handleAuth request [ Permissions.Users.Read ] (fun content ->
                             { Id = Guid.Parse(content)
                               Name = "Kevin" })
              } }
