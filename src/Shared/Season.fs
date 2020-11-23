
module Shared.Season

open System

type CreateSeasonModel =
    { Name: string
      LeagueId: string }

type Season =
    { Id: Guid
      Name: string
      LeagueId: Guid }
