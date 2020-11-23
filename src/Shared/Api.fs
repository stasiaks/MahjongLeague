namespace Shared

open System
open Authentication
open Shared.User
open Shared.League
open Shared.Season

module Route =
    /// Defines how routes are generated on server and mapped from client
    let builder typeName methodName =
        sprintf "/api/%s/%s" typeName methodName

type IUserApi =
    { Register : SecureRequest<unit> -> Async<SecureResult<User>>
      GetUsers : SecureRequest<unit> -> Async<SecureResult<User list>>
      GetUser : SecureRequest<string> -> Async<SecureResult<User option>> }

type ILeagueApi =
    { CreateLeague : SecureRequest<CreateLeagueModel> -> Async<SecureResult<League>>
      GetLeagues : unit -> Async<League list>
      GetLeague : string -> Async<League option> }

type ISeasonApi =
    { CreateSeason : SecureRequest<CreateSeasonModel> -> Async<SecureResult<Season>>
      GetSeasonsForLeague : string -> Async<Season list>
      GetSeasons : unit -> Async<Season list>
      GetSeason : string -> Async<Season option> }
