module User.Api

open System

open Shared
open Shared.Authentication
open Utilities.JsonWebToken

let createSequentialUser n = { Id = Guid.NewGuid(); Name = sprintf "User %d" n }

let handleValidation { Token = token; Content = content } createResult =
    validateToken token
    |> function
    | InvalidToken -> TokenInvalid |> Error
    | Expired      -> TokenExpired |> Error
    | Valid        -> content |> createResult |> Ok


let api = {
    GetUsers = fun request -> async { return handleValidation request
        (fun content -> Seq.init (Random().Next(2,6)) (fun n -> createSequentialUser n) |> Seq.toList) }
    GetUser = fun request -> async { return handleValidation request
        (fun content -> { Id = Guid.Parse(content); Name = "Kevin" }) }
}
