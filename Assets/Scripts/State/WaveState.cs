using System.Collections;
using UnityEngine;
namespace slaughter.de.Managers
{
    public class WaveState : State
    {
        private float startTime;
        
        public override IEnumerator Start()
        {
            // Deine Implementierung der Logik für den WaveState
            yield return Wave(); // Starte die Wave Coroutine
        }

        public override IEnumerator Wave()
        {
            Debug.Log("Start Wave.");
            WaveManager.Instance.StartNextWave();

            float remainingTime = WaveManager.Instance.waveDuration;
            while (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                WaveManager.Instance.SetWaveTimer(remainingTime);
                yield return null;
            }

            WaveManager.Instance.StopWave();
            Debug.Log("End Wave.");

            GameManager.Instance.SetState(new ItemSelectionState());
        }
    }
}
