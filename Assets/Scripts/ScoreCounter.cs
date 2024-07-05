using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScoreCounter : MonoBehaviour
{
    private HealthForObjects[] FindAllObjects()
    {
        return Object.FindObjectsByType<HealthForObjects>(FindObjectsSortMode.None);
    }

    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private int _score = 0;

    private void Start()
    {
        _scoreText.text = _score.ToString();
        HealthForObjects.OnObjectDestroyed += OnObjectDestroyed;
    }

    private void OnObjectDestroyed()
    {
        _score += 25;
;       _scoreText.text = _score.ToString();
    }
}
