namespace Shared

module Seq =
    let containsAll values source =
        Seq.forall (fun x -> Seq.contains x source) values
