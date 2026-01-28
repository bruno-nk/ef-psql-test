dotnet run wil fail with:

```
fail: Microsoft.EntityFrameworkCore.Database.Command[20102]
      Failed executing DbCommand (7ms) [Parameters=[@p0='?'], CommandType='Text', CommandTimeout='30']
      INSERT INTO "Somethings" ("Whatever")
      VALUES (@p0)
      RETURNING "Id";
fail: Microsoft.EntityFrameworkCore.Update[10000]
      An exception occurred in the database while saving changes for context type 'SomeContext'.
      Microsoft.EntityFrameworkCore.DbUpdateException: An error occurred while saving the entity changes. See the inner exception for details.
       ---> Npgsql.PostgresException (0x80004005): 23502: null value in column "Id" of relation "Somethings" violates not-null constraint

      DETAIL: Detail redacted as it may contain sensitive data. Specify 'Include Error Detail' in the connection string to include this information.
         at Npgsql.Internal.NpgsqlConnector.ReadMessageLong(Boolean async, DataRowLoadingMode dataRowLoadingMode, Boolean readingNotifications, Boolean isReadingPrependedMessage)
         at System.Runtime.CompilerServices.PoolingAsyncValueTaskMethodBuilder`1.StateMachineBox`1.System.Threading.Tasks.Sources.IValueTaskSource<TResult>.GetResult(Int16 token)
         at Npgsql.NpgsqlDataReader.NextResult(Boolean async, Boolean isConsuming, CancellationToken cancellationToken)
         at Npgsql.NpgsqlDataReader.NextResult(Boolean async, Boolean isConsuming, CancellationToken cancellationToken)
         at Npgsql.NpgsqlCommand.ExecuteReader(Boolean async, CommandBehavior behavior, CancellationToken cancellationToken)
         at Npgsql.NpgsqlCommand.ExecuteReader(Boolean async, CommandBehavior behavior, CancellationToken cancellationToken)
         at Npgsql.NpgsqlCommand.ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
         at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReaderAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken)
         at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReaderAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken)
         at Microsoft.EntityFrameworkCore.Update.ReaderModificationCommandBatch.ExecuteAsync(IRelationalConnection connection, CancellationToken cancellationToken)
        Exception data:
          Severity: ERROR
          SqlState: 23502
          MessageText: null value in column "Id" of relation "Somethings" violates not-null constraint
          Detail: Detail redacted as it may contain sensitive data. Specify 'Include Error Detail' in the connection string to include this information.
          SchemaName: public
          TableName: Somethings
          ColumnName: Id
          File: execMain.c
          Line: 2209
          Routine: ReportNotNullViolationError
         --- End of inner exception stack trace ---
```
