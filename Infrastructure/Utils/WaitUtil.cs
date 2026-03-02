using System;
using System.Threading;

namespace Infrastructure.Utils
{
    public static class WaitUtil
    {
        public static void WaitActionWithRetry(
            Action action,
            int requestRetryCount = 5,
            int retryPollingIntervalInSec = 5)
        {
            for (int i = 0; i < requestRetryCount; i++)
            {
                if (i < requestRetryCount - 1)
                {
                    try
                    {
                        action.Invoke();
                        break;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(DateTimeUtil.GetSeconds(retryPollingIntervalInSec));
                    }
                }
                else
                {
                    action.Invoke();
                }
            }
        }

        public static T WaitActionWithRetry<T>(
            Func<T> action,
            int requestRetryCount = 5,
            int retryPollingIntervalInSec = 1)
        {
            T result = default;
            for (int i = 0; i < requestRetryCount; i++)
            {
                if (i < requestRetryCount - 1)
                {
                    try
                    {
                        return action.Invoke();
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(DateTimeUtil.GetSeconds(retryPollingIntervalInSec));
                    }
                }
                else
                {
                    result = action.Invoke();
                }
            }
            return result;
        }
    }
}