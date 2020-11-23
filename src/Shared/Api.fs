namespace Shared

open Authentication

module Route =
    /// Defines how routes are generated on server and mapped from client
    let builder typeName methodName =
        sprintf "/api/%s/%s" typeName methodName

type IUserApi =
    { Register : SecureRequest<unit> -> Async<SecureResult<User>>
      GetUsers : SecureRequest<unit> -> Async<SecureResult<User list>>
      GetUser : SecureRequest<string> -> Async<SecureResult<User option>> }
