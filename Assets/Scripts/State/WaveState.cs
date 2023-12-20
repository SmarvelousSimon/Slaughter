using System.Collections;
using UnityEngine;
namespace slaughter.de.Managers
{
    public class WaveState : State
    {
        float startTime;

        public override IEnumerator Start()
        {
            // Deine Implementierung der Logik für den WaveState
            yield return Wave(); // Starte die Wave Coroutine
        }

        /// <summary>
        /// This method represents a wave in the game.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used in a coroutine.
        /// </returns>
        /// <remarks>
        /// This method starts a wave, sets the wave timer, and stops the wave after a certain duration.
        /// It also changes the game state to Item Selection State after the wave ends.
        /// </remarks>
        public override IEnumerator Wave()
        {
            Debug.Log("Start Wave.");
            WaveManager.Instance.StartNextWave();

            float remainingTime = WaveManager.Instance.waveDuration;
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

    }
}
