using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Configuration;
using Infrastructure.Logger;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DB.Interactions.Clients
{
    public class SqlDbClient
    {
        private static readonly LogHelper LogHelper = LogHelper.Instance;

        protected SqlDbClient() { }

        protected static void InsertIntoDb<T, TContext>(T data)
            where T : class
            where TContext : DbContext
        {
            ModifyDb<T, TContext>(LogInsertDbAction, (dbSet, value) => dbSet.Add(value), data);
        }

        protected static void InsertIntoDb<T, TContext>(IEnumerable<T> data)
            where T : class
            where TContext : DbContext
        {
            ModifyDb<T, TContext>(LogInsertDbAction, (dbSet, value) => dbSet.AddRange(value), data);
        }

        protected static void DeleteFromDb<T, TContext>(T data)
            where T : class
            where TContext : DbContext
        {
            ModifyDb<T, TContext>(LogDeleteDbAction, (dbSet, value) => dbSet.Remove(value), data);
        }

        protected static void DeleteFromDb<T, TContext>(IEnumerable<T> data)
            where T : class
            where TContext : DbContext
        {
            ModifyDb<T, TContext>(LogDeleteDbAction, (dbSet, value) => dbSet.RemoveRange(value), data);
        }

        protected static void UpdateDb<T, TContext>(T data)
            where T : class
            where TContext : DbContext
        {
            ModifyDb<T, TContext>(LogUpdateDbAction, (dbSet, value) => dbSet.Update(value), data);
        }

        protected static void UpdateDb<T, TContext>(IEnumerable<T> data)
            where T : class
            where TContext : DbContext
        {
            ModifyDb<T, TContext>(LogUpdateDbAction, (dbSet, value) => dbSet.UpdateRange(value), data);
        }

        protected static void ClearTable<TContext, T>()
            where TContext : DbContext
            where T : class
        {
            using var dbContext = Activator.CreateInstance<TContext>();
            var table = GetTable<T>(dbContext);
            table.RemoveRange(table);
            dbContext.SaveChanges();
        }

        private static void ModifyDb<T, TContext>(
            Action<T> logAction,
            Action<DbSet<T>, T> dbAction,
            T data)
            where T : class
            where TContext : DbContext
        {
            using var dbContext = Activator.CreateInstance<TContext>();
            logAction.Invoke(data);
            var table = GetTable<T>(dbContext);
            dbAction.Invoke(table, data);
            dbContext.SaveChanges();
        }

        private static void ModifyDb<T, TContext>(
            Action<IEnumerable<T>> logAction,
            Action<DbSet<T>, IEnumerable<T>> dbAction,
            IEnumerable<T> data)
            where T : class
            where TContext : DbContext
        {
            using var dbContext = Activator.CreateInstance<TContext>();
            logAction.Invoke(data);
            var table = GetTable<T>(dbContext);
            dbAction.Invoke(table, data);
            dbContext.SaveChanges();
        }

        private static DbSet<T> GetTable<T>(DbContext dbContext) where T : class
        {
            if (!(dbContext
                .GetType()
                .GetProperties()
                .FirstOrDefault(x => x.PropertyType == typeof(DbSet<T>))?
                .GetValue(dbContext) is DbSet<T> table))
            {
                throw new ArgumentException(
                    $"There is no DbSet with type '{typeof(T).Name}' in '{dbContext.GetType().Name}'");
            }
            return table;
        }

        private static void LogInsertDbAction<T>(T data)
        {
            LogHelper.DB($"Inserting data into '{GetTableName(typeof(T))}' table");
            LogData(data);
        }

        private static void LogUpdateDbAction<T>(T data)
        {
            LogHelper.DB($"Updating data in '{GetTableName(typeof(T))}' table");
            LogData(data);
        }

        private static void LogDeleteDbAction<T>(T data)
        {
            LogHelper.DB($"Delete data from '{GetTableName(typeof(T))}' table");
            LogData(data);
        }

        private static string GetTableName(Type type)
        {
            return type.IsGenericType ? type.GetGenericArguments().First().Name : type.Name;
        }

        private static void LogData<T>(T data)
        {
            if (Configuration.LogDbData)
            {
                LogHelper.DB($"Data: {JsonConvert.SerializeObject(data)}");
            }
        }
    }
}