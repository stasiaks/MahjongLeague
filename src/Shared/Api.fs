namespace Shared

open Authentication

module Route =
    /// Defines how routes are generated on server and mapped from client
    let builder typeName methodName =
        sprintf "/api/%s/%s" typeName methodName

/// A type that specifies the communication protocol between client and server
/// to learn more, read the docs at https://zaid-ajaj.github.io/Fable.Remoting/src/basics.html
type ICounterApi =
    { InitialCounter : unit -> Async<Counter> }

type IUserApi =
    { GetUsers : SecureRequest<unit> -> Async<SecureResult<User list>>
      GetUser : SecureRequest<string> -> Async<SecureResult<User>> }