using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreConfig", menuName = "ScriptableObjects/ScoreConfig")]
public class ScoreConfig : ScriptableObject
{
    [Serializable]
    public class Pair
    {
        public string tag;
        public int score;
    }

    [SerializeField]
    private List<Pair> scores = new List<Pair>();

    public int GetScoreFor(string tag) => scores.Find(s => s.tag == tag).score;
}
