using System;
using System.Threading;
using slaughter.de.ExtensionMethods;
using UnityEngine;
using UnityEngine.UI;

namespace slaughter.de.UI
{
    public class ConfirmationMenu : MonoBehaviour
    {
        [SerializeReference] protected Button confirmButton;
        [SerializeReference] private Button quitGameButton;

        private void Awake()
        {
            gameObject.SetActive(false);
            quitGameButton?.onClick.AddListener(() => OnQuit?.Invoke());
        }

        public event Action OnQuit;

        public virtual async Awaitable Show(CancellationToken token)
        {
            gameObject.SetActive(true);
            await confirmButton.WaitForClickAsync(token);
            gameObject.SetActive(false);
        }
    }
}