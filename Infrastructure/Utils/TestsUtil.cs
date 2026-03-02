using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Infrastructure.Utils
{
    public static class TestsUtil
    {
        private static readonly ConcurrentQueue<int> PoolOfInstances = new(Configuration.Configuration.Company.MainCompanyIds);
        private static readonly ThreadLocal<int> MainCompanyIds = new();

        public static int GetMainCompanyId()
        {
            if (MainCompanyIds.Value == 0)
            {
                MainCompanyIds.Value = TakeInstance();
            }
            return MainCompanyIds.Value;
        }

        public static void ReleaseInstance()
        {
            PoolOfInstances.Enqueue(MainCompanyIds.Value);
            MainCompanyIds.Value = 0;
        }

        private static int TakeInstance()
        {
            if (PoolOfInstances.TryDequeue(out var freeInstance))
            {
                return freeInstance;
            }
            throw new ArgumentException(
                "There are no free instances. " +
                "Seems the number of instances is less than the number of threads. " +
                "Add more instances or reduce the number of threads.");
        }
    }
}