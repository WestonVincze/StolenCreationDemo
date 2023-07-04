using UnityEngine;

public class PlayerCursor : MonoBehaviour 
{
  void Start() 
  {
    // hides the cursor and locks to the center of the screen so that the default mouse cursor doesn't mo
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;
  }
}