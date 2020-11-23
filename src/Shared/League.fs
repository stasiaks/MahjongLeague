module Shared.League

open System

type CreateLeagueModel = { Name: string; Description: string option }

[<RequireQualifiedAccess>]
type LeagueSeason = { Id: Guid; Name: string }

type League =
    { Id: Guid
      Name: string
      Description: string option }
