using System.Collections;
using slaughter.de.Managers;
using UnityEngine;

namespace slaughter.de.State
{
    public class WaveState : State
    {
        private float startTime;

        public override IEnumerator Start()
        {
            Debug.Log("WaveState startet.");

            // Abonniere das OnWaveEnded-Ereignis vom WaveManager
            WaveManager.Instance.OnWaveEnded += HandleWaveEnded;

            yield return Wave(); // Starte die Wave Coroutine
        }

        public override IEnumerator Wave()
        {
            Debug.Log("Start Wave.");
            
            
            WaveManager.Instance.StartNextWave();

            var remainingTime = WaveManager.Instance.waveDuration;
            while (remainingTime > 0 && GameManager.Instance.GetCurrentStateType() == typeof(WaveState))
            {
                remainingTime -= Time.deltaTime;
                WaveManager.Instance.SetWaveTimer(remainingTime);
                yield return null;
            }

            // Prüfe, ob der Zustand noch aktiv ist, bevor du zum nächsten Zustand wechselst
            if (GameManager.Instance.GetCurrentStateType() == typeof(WaveState))
            {
                WaveManager.Instance.StopWave();
                Debug.Log("End Wave.");
                GameManager.Instance.SetState(new ItemSelectionState());
            }
        }

        public void StopRunningCoroutine(MonoBehaviour owner)
        {
            base.StopRunningCoroutine(owner);
            // Hänge Abmeldung vom OnWaveEnded-Ereignis hier an
            WaveManager.Instance.OnWaveEnded -= HandleWaveEnded;
        }

        private void HandleWaveEnded()
        {
            // Zustand wechseln, wenn die Welle endet
            if (GameManager.Instance.GetCurrentStateType() == typeof(WaveState))
            {
                Debug.Log("End Wave (von WaveManager-Ereignis)");
                GameManager.Instance.SetState(new ItemSelectionState());
            }
        }
    }
}