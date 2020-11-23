module User.Documentation

open System
open Fable.Remoting.Server
open Fable.Remoting.Giraffe

open Shared
open Shared.Authentication

[<Literal>]
let ExampleToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"

let documentation =
    let docs = Docs.createFor<IUserApi>()
    Remoting.documentation "Users API" [
        docs.route <@ fun api -> api.Register @>
        |> docs.alias "Register yourself as user"
        |> docs.description "Nothing will really force user to call this, will be replaces with middleware"

        docs.route <@ fun api -> api.GetUsers @>
        |> docs.alias "Get all users"
        |> docs.description "Returns all users in application"

        docs.route <@ fun api -> api.GetUser @>
        |> docs.alias "Get user"
        |> docs.description "Return user with specific ID"
        |> docs.example <@ fun api -> api.GetUser <| { Token = SecurityToken ExampleToken; Content = Guid.NewGuid().ToString() } @>
    ]
