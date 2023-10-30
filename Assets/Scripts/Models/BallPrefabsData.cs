using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BallPrefabs", order = 1)]
public class BallPrefabsData : ScriptableObject
{
    public GameObject[] ballPrefabs;
}