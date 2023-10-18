using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static bool IsTimePass(DateTime dateTime, float sec) => DateTime.Now.Ticks - dateTime.Ticks > sec * TimeSpan.TicksPerSecond ? true : false;
}
