
module Shared.Season

open System

type CreateSeasonModel =
    { Name: string
      LeagueId: string }

[<RequireQualifiedAccess>]
type SeasonLeague =
    { Id: Guid
      Name: string }

type Season =
    { Id: Guid
      Name: string
      League: SeasonLeague }
