open System.IO

open Giraffe
open Saturn


let combinedRouter =
    choose
        [ User.Router.router
          League.Router.router ]

let app = application {
    url ("http://0.0.0.0:8085")
    use_router combinedRouter
    memory_cache
    use_static "public"
    use_gzip
}

run app
