using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.text = $"Score: {Singleton.Instance.score}";
    }

    public void UpdateScore()
    {
        Singleton.Instance.score++;
        scoreText.text = $"Score: {Singleton.Instance.score}";
    }
}
