using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField]
    private ScoreConfig _scoreConfig;

    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    private int _score = 0;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetScore(0);
    }

    public void AddScore(string tag)
    {
        int score = _scoreConfig.GetScoreFor(tag);
        _score += score;
        _scoreText.text = _score.ToString();
    }

    private void SetScore(int number)
    {
        _score = number;
        _scoreText.text = _score.ToString();
    }

    private void Start()
    {
        _scoreText.text = _score.ToString();
    }

    public int GetScore()
    {
        return _score;
    }
}
