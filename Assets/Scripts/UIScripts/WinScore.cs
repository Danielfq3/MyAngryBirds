using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinScore : MonoBehaviour
{
    [SerializeField]
    private ScoreCounter _scoreCounter;

    void Start()
    {
        int score = _scoreCounter.GetComponent<ScoreCounter>().GetScore();
        gameObject.GetComponent<TextMeshProUGUI>().text = "SCORE: " + score.ToString();
    }
}
