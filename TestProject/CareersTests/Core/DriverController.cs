using OpenQA.Selenium;
using System.Collections.Concurrent;
using System.Threading;
using System;

namespace CareersTests.Core
{
    public static class DriverController
    {
        private static readonly ConcurrentDictionary<int, IWebDriver> drivers = new ConcurrentDictionary<int, IWebDriver>();

        public static void Add(int threadId, IWebDriver driver)
        {
            var isDriverAdded = drivers.TryAdd(threadId, driver);

            if (!isDriverAdded)
                throw new Exception("driver instance has not been added");
        }

        public static IWebDriver GetCurrentDriverInstance()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;

            var isDriverExists = drivers.TryGetValue(threadId, out var driver);

            if (isDriverExists)
                return driver;

            throw new Exception("driver instance doesn't exist");
        }
    }
}