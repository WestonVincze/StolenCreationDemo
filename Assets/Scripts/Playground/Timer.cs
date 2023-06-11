using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playground
{
  public class Timer : MonoBehaviour
  {
    public delegate void TimerDone();

    private float _timeLeft = 0.0f;

    private TimerDone _onTimerFinished;

    public void setTimer(float time, TimerDone onTimerFinished = null)
    {
      _timeLeft = time;
      _onTimerFinished = onTimerFinished;
    }

    void Update()
    {
      if (_timeLeft == 0) return;

      _timeLeft -= Time.deltaTime;
      if (_timeLeft <= 0.0f)
      {
        _timeLeft = 0.0f;
        _onTimerFinished();
      }
    }
  }
}