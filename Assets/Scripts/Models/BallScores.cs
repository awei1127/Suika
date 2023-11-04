using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BallScores", menuName = "ScriptableObjects/BallScores", order = 1)]
public class BallScores : ScriptableObject
{
    public BallScore[] scores;
}

[System.Serializable]
public class BallScore
{
    public BallNumber ballNumber;
    public int score;
}