module League.Api

open System
open Utilities.Auth
open Utilities.Database
open Shared
open Shared.League

let database = databaseContext.Mahjong

let api =
    { CreateLeague =
          fun request ->
              async {
                  return handleAuth request [ Permissions.Leagues.Write ] (fun content ->
                             let league = database.Leagues.Create()
                             league.Id <- Guid.NewGuid()
                             league.Name <- content.Name
                             league.Description <- content.Description
                             databaseContext.SubmitUpdates()
                             { Id = league.Id
                               Name = league.Name
                               Description = league.Description })
              }
      GetLeagues =
          fun _ ->
              async {
                  return query {
                             for l in database.Leagues do
                                 select
                                     { Id = l.Id
                                       Name = l.Name
                                       Description = l.Description }
                         }
                         |> Seq.toList
              }
      GetLeague =
          fun content ->
              async {
                  return query {
                             for l in database.Leagues do
                                 where (l.Id.ToString() = content)
                                 select
                                     (Some
                                         { Id = l.Id
                                           Name = l.Name
                                           Description = l.Description })
                                 exactlyOneOrDefault
                         }
              } }
