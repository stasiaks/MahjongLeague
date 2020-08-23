namespace Shared

open System

module Route =
    /// Defines how routes are generated on server and mapped from client
    let builder typeName methodName =
        sprintf "/api/%s/%s" typeName methodName

/// A type that specifies the communication protocol between client and server
/// to learn more, read the docs at https://zaid-ajaj.github.io/Fable.Remoting/src/basics.html
type ICounterApi =
    { InitialCounter : unit -> Async<Counter> }

type IUserApi =
    { GetUsers : unit -> Async<User list>
      GetUser : string -> Async<User> }