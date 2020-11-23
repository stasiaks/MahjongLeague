module Season.Api

open System
open Utilities.Auth
open Utilities.Database
open Shared
open Shared.Season

let database = databaseContext.Mahjong

let api =
    { CreateSeason =
          fun request ->
              async {
                  return handleAuth request [ Permissions.Leagues.Write ] (fun content ->
                             let season = database.Seasons.Create()
                             season.Id <- Guid.NewGuid()
                             season.Name <- content.Name
                             season.LeagueId <- Guid.Parse(content.LeagueId)
                             databaseContext.SubmitUpdates()
                             { Id = season.Id
                               Name = season.Name
                               LeagueId = season.LeagueId })
              }
      GetSeasonsForLeague =
          fun content ->
              async {
                  return query {
                             for s in database.Seasons do
                                 where (s.LeagueId.ToString() = content)
                                 select
                                     { Id = s.Id
                                       Name = s.Name
                                       LeagueId = s.LeagueId }
                         }
                         |> Seq.toList
              }
      GetSeasons =
          fun _ ->
              async {
                  return query {
                             for s in database.Seasons do
                                 select
                                     { Id = s.Id
                                       Name = s.Name
                                       LeagueId = s.LeagueId }
                         }
                         |> Seq.toList
              }
      GetSeason =
          fun content ->
              async {
                  return query {
                             for s in database.Seasons do
                                 where (s.Id.ToString() = content)
                                 select
                                     (Some
                                         { Id = s.Id
                                           Name = s.Name
                                           LeagueId = s.LeagueId })
                                 exactlyOneOrDefault
                         }
              } }
