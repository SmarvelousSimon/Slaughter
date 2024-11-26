using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace slaughter.de.ExtensionMethods
{
    public static class ButtonExtensions
    {
        public static async Awaitable WaitForClickAsync(this Button button, CancellationToken token)
        {
            var source = new AwaitableCompletionSource();
            var registration = token.Register(source.SetCanceled);

            button.onClick.AddListener(source.SetResult);
            await source.Awaitable.SuppressCancelThrow();

            button.onClick.RemoveListener(source.SetResult);
            registration.Dispose();
        }
    }
}