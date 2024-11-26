using slaughter.de.Actors.Player;
using UnityEngine;

namespace slaughter.de.CameraScripts
{
    public class CameraScript : MonoBehaviour
    {
        [SerializeField] private float cameraDistance = 15;

        private Transform _player;


        public void Awake()
        {
            enabled = false;
        }

        public void Update()
        {
            var position = _player.position;
            position.z = -cameraDistance;
            transform.position = position;
        }

        public void RegisterPlayer(PlayerController player)
        {
            _player = player.transform;
            enabled = true;
        }
    }
}