using System;
using UnityEngine;

public class SameBallCollidedEventArgs : EventArgs
{
    public Vector3 midpoint;
    public BallNumber ballNumber;
}
