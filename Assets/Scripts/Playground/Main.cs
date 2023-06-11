using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playground
{
  public class Main : MonoBehaviour
  {
    private Timer _timer;

    void Start()
    {
      _timer = GetComponent<Timer>();
      _timer.setTimer(1.0f, () => { Debug.Log("Timer done"); });
    }

    void Update()
    {

    }
  }
}