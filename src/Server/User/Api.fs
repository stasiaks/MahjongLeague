module User.Api

open System
open Utilities.Auth
open Utilities.Database
open Shared

let database = databaseContext.Mahjong

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
                             query {
                                 for u in database.Users do
                                     where (u.Id = Guid.Parse(content))
                                     take 1
                                     select { Id = u.Id; Name = u.Name }
                             }
                             |> Seq.head)
              } }
