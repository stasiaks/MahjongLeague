module User.Api

open System

open Shared

let createSequentialUser n = { Id = Guid.NewGuid(); Name = sprintf "User %d" n }

let api = {
    GetUsers = fun _ -> async { return Seq.init (Random().Next(2,6)) (fun n -> createSequentialUser n) |> Seq.toList |> Ok }
    GetUser = fun { Content = id } -> async { return Ok { Id = Guid.Parse(id); Name = "Kevin" } }
}
