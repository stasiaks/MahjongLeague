module User.Api

open System
open Utilities.Auth
open Utilities.Auth0
open Utilities.Database
open Shared
open Shared.Authentication

let database = databaseContext.Mahjong

let api =
    { Register =
          fun request ->
              async {
                  let! userInfo = getUserInfo request.Token

                  return handleAuth request [] (fun _ ->
                             query {
                                 for u in database.Users do
                                     where (u.Objectid = (userInfo.Sub |> Some))
                                     select (Some u)
                                     exactlyOneOrDefault
                             }
                             |> Option.defaultWith (fun () ->
                                 let user = database.Users.Create()
                                 user.Id <- Guid.NewGuid()
                                 user)
                             |> fun user ->
                                 user.Name <- userInfo.Name
                                 user.Objectid <- Some userInfo.Sub
                                 user.Email <- Some userInfo.Email
                                 databaseContext.SubmitUpdates()
                                 { Id = user.Id; Name = user.Name })
              }
      GetUsers =
          fun request ->
              async {
                  return handleAuth request [ Permissions.Users.Read ] (fun _ ->
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
                                     select (Some { Id = u.Id; Name = u.Name })
                                     exactlyOneOrDefault
                             })
              } }
