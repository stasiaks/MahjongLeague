namespace Shared

type Permission = Permission of string

[<RequireQualifiedAccessAttribute>]
module Permissions =
    module Users =
        let Read = Permission "Users.Read"
        let Write = Permission "Users.Write"
