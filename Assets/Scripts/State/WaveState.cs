using System.Collections;
using slaughter.de.Managers;
using UnityEngine;

namespace slaughter.de.State
{
    public class WaveState : State
    {
        public override IEnumerator Start()
        {
            // Abonniere das OnWaveEnded-Ereignis vom WaveManager
            yield return Wave(); // Starte die Wave Coroutine
        }

        public override IEnumerator Wave()
        {
            Debug.Log("Start Wave.");
            
            GameManager.Instance.StartNextWave();

            var remainingTime = GameManager.Instance.waveDuration;
            while (remainingTime > 0 && GameManager.Instance.GetCurrentStateType() == typeof(WaveState))
            {
                remainingTime -= Time.deltaTime;
                GameManager.Instance.SetWaveTimer(remainingTime);
                yield return null;
            }

            // Prüfe, ob der Zustand noch aktiv ist, bevor du zum nächsten Zustand wechselst
            if (GameManager.Instance.GetCurrentStateType() == typeof(WaveState))
            {
                GameManager.Instance.StopWave();
                Debug.Log("End Wave.");
                StateManager.Instance.SetState(new ItemSelectionState());
            }
        }
    }
}