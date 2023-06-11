using UnityEngine;
using System.Collections.Generic;

// test asdf

// collider nodes
public class Blade: MonoBehaviour 
{
  private Transform[] _bladeNodes;
  private Vector3[] _prevBladeNodePos;
  public bool isBladeActive = false;
  private bool _isPrevBladeActive = false;
  private void Awake()
  {
    _bladeNodes = GetComponentsInChildren<Transform>();
    _prevBladeNodePos = new Vector3[_bladeNodes.Length];

    //Debug.Log("Blade nodes: " + _bladeNodes.Length);
  }

  private void InitializePrevBladeNotPos()
  {
    for (int i = 0; i < _bladeNodes.Length; i++)
    {
      _prevBladeNodePos[i] = _bladeNodes[i].position;
    }
  }

  private void Update()
  {
    //Debug.Log($"prevBladeNodePos[0]: {_prevBladeNodePos[0]} bladeNodes[0]: {_bladeNodes[0].position}");

    if (isBladeActive && !_isPrevBladeActive)
    {
      InitializePrevBladeNotPos();
    }

    if (isBladeActive) CollideCheck();
    _isPrevBladeActive = isBladeActive;
  }

  public void CollideCheck()
  {
    // we will need to know where on the blade the collision happened
    // ex) you hit a player and a wall at the same time, if wall is closer to the hilt, 
    // then the player should take no damage

    bool hit;
    RaycastHit hitInfo;
    for (int i = 0; i < _bladeNodes.Length; i++)
    {
      // from previous position to current
      hit = Physics.Raycast(_prevBladeNodePos[i], 
          _bladeNodes[i].position - _prevBladeNodePos[i], 
          out hitInfo);
      if (hit)
      {
        Debug.DrawLine(_prevBladeNodePos[i], _bladeNodes[i].position, Color.green, 1f);
        Debug.Log("Hit: " + hitInfo.collider.gameObject.name);
        // hitInfo.collider.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
      }
      else 
      {
        Debug.Log("No hit");
        Debug.DrawLine(_prevBladeNodePos[i], _bladeNodes[i].position, Color.red, 1f);
      }
      _prevBladeNodePos[i] = _bladeNodes[i].position;
    }
  }


  // I wonder if it would be cool to use the raycasting in order to determine weapon speed
  // you can then add the speed as a variable to your damage calculation
  // the greater the difference in positions, the more damage you do


  // would we want to decrease or increase the damage based on how long the hitboxes have been active?
  // we could have different bools to track different damage levels, but maybe a fixed padding
  // would work and make tracking the damage easier
  
}