using UnityEngine;

/** Not yet implemented **/

public class InputManager : MonoBehaviour 
{
  public bool Dodge()
  {
    return Input.GetKeyDown(KeyCode.LeftShift);
  }
  
}