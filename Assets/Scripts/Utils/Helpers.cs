using UnityEngine;
using System;
using System.Collections.Generic;

public static class Helpers
{
  public static void ValidateComponents(string parentName, List<(Component c, Type t)> components)
  {
    foreach (var componentTuple in components)
    {
      Component component = componentTuple.c;
      Type expectedType = componentTuple.t;

      if (component == null)
      {
        Debug.LogError($"{parentName} could not find the required component {expectedType.Name}");
      }
    }
  }
}