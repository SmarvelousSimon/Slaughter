using System;
using UnityEngine;

namespace slaughter.de.ExtensionMethods
{
    public static class UnityAwaitableExtensions
    {
        public static void Forget<T>(this Awaitable<T> awaitable)
        {
            awaitable.GetAwaiter().OnCompleted(() =>
            {
                try
                {
                    awaitable.GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            });
        }

        public static void Forget(this Awaitable _)
        {
            
        }

        public static async Awaitable SuppressCancelThrow(this Awaitable awaitable)
        {
            try
            {
                await awaitable;

            }
            catch (OperationCanceledException)
            {
                
            }
        }

        public static async Awaitable LogAllExceptions(this Awaitable awaitable)
        {
            try
            {
                await awaitable;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}