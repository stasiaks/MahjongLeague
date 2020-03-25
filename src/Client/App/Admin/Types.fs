
module App.Admin.Types

open Shared

type Msg =
    | Increment
    | Decrement
    | InitialCountLoaded of Counter

type State =
    { Counter: Counter option }