using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    [SerializeField] private int highPointsTreshold;
    [SerializeField] private int lowPointsTreshold;
    [SerializeField] private int dyingScorePenalty;
    [SerializeField] private string[] levels;
    [SerializeField] private Image scoreImage;
    [SerializeField] private Sprite positiveRatingImage;
    [SerializeField] private Sprite neutralRatingImage;
    [SerializeField] private Sprite negativeRatingImage;
    [HideInInspector] public GameObject player;
    public int score = 0;
    private int currentLevel = 0;

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start() =>
        ScoreChange();

    public void Respawn()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.transform.position = LevelManager.instance.spawnPos;
        score -= dyingScorePenalty;
        ScoreChange();

        if (score < 0)
            score = 0;
    }

    public void ScoreChange()
    {
        if (score >= highPointsTreshold) //Update UI element to represent a high score
            scoreImage.sprite = positiveRatingImage;
        else if (score > lowPointsTreshold) //Update UI element to represent a medium score
            scoreImage.sprite = neutralRatingImage;
        else //Update UI element to represent a low score
            scoreImage.sprite = negativeRatingImage;
    }

    public void LoadNextLevel()
    {
        if (currentLevel < levels.Length)
        {
            SceneManager.LoadScene(levels[currentLevel + 1]);
            currentLevel++;
        }
        else
            Application.Quit();
    }
}
