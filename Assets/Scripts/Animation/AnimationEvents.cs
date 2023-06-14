using UnityEngine;
using Player;

public class AnimationEvents : MonoBehaviour
{
  private PlayerDodge _playerDodge;
  private PlayerJump _playerJump;

  private void Start()
  {
    _playerDodge = GetComponentInParent<PlayerDodge>();
    _playerJump = GetComponentInParent<PlayerJump>();
  }

  private void TriggerDodge()
  {
    _playerDodge.TriggerDodge();
  }

  private void TriggerJump()
  {
    _playerJump.TriggerJump();
  }

}