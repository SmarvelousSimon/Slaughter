using slaughter.de.Movement;
using UnityEngine;

namespace slaughter.de
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        private PlayerMovement _playerMovement;
        private SpriteRenderer _spriteRenderer;

        // Start is called before the first frame update
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _playerMovement = GetComponent<PlayerMovement>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (_playerMovement.moveDir.x != 0 || _playerMovement.moveDir.y != 0)
            {
                _animator.SetBool("Move", true);
                SetSpriteDirection();
            }
            else
            {
                _animator.SetBool("Move", false);
            }
        }

        private void SetSpriteDirection()
        {
            if (_playerMovement.lastViewXDirection < 0)
                _spriteRenderer.flipX = true;
            else
                _spriteRenderer.flipX = false;
        }
    }
}