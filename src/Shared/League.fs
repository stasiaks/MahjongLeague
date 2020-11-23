module Shared.League

open System

type CreateLeagueModel = { Name: string; Description: string option }

type League =
    { Id: Guid
      Name: string
      Description: string option }
