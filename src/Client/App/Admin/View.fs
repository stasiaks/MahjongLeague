module App.Admin.View

open Fable.React
open Fulma

open App.Admin.Types
open App.Admin.Localization

let menu dispatch =
    let menuDispatchProp msg = Menu.Item.OnClick(fun _ -> dispatch msg)
    Menu.menu []
        [ Menu.label [] [ str "General" ]
          Menu.list []
              [ Menu.Item.a [ menuDispatchProp (NavigateTo Dashboard |> ForParent) ] [ str "Dashboard" ]
                Menu.Item.a [ menuDispatchProp (NavigateTo <| Users Users.Types.Page.List |> ForParent) ] [ str "Users" ] ] ]

let main state dispatch lstr page =
    match page with
    | Dashboard -> Dashboard.View.render (DashboardToken >> lstr)
    | Users page -> Users.View.render state.Users dispatch (UsersToken >> lstr) page

let render (state: State) (dispatch: Msg -> unit) lstr page =
    Container.container []
        [ Columns.columns []
              [ Column.column [ Column.Width(Screen.All, Column.Is3) ] [ menu dispatch ]
                Column.column [ Column.Width(Screen.All, Column.Is9) ]
                    [ main state dispatch lstr page ] ] ]
