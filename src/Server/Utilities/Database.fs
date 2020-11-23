module Utilities.Database

open FSharp.Data.Sql

[<Literal>]
let DbVendor = Common.DatabaseProviderTypes.POSTGRESQL

/// Connection string for type provider during compilation
[<Literal>]
let ConnString =
    "Host=localhost;Database=mahjong;Username=test_app;Password=pass"

[<Literal>]
let ResPath = __SOURCE_DIRECTORY__ + @"./lib"

[<Literal>]
let Schema = "mahjong"

[<Literal>]
let IndividualAmount = 1000

[<Literal>]
let UseOptionTypes = true

type DB =
    SqlDataProvider<DatabaseVendor=DbVendor, ConnectionString=ConnString, ResolutionPath=ResPath, IndividualsAmount=IndividualAmount, UseOptionTypes=UseOptionTypes, Owner=Schema>

let databaseContext =
    DB.GetDataContext(selectOperations = SelectOperations.DatabaseSide)
