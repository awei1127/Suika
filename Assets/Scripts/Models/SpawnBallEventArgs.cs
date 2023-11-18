using System;
using UnityEngine;

public class SpawnBallEventArgs : EventArgs
{
    public BallCollisionHandler spawnedBallCollisionHandler;
    public BallPositionHandler spawnedBallPositionHandler;
}
