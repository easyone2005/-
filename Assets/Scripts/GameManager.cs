using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text textScore = null;
    [SerializeField]
    private Text textLife = null;
    [SerializeField]
    private Text textHighScore = null;
    [SerializeField]
    private GameObject enemyCroissant = null;
    [SerializeField]
    private GameObject enemyHotdog = null;

    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }

    private int score = 0;
    private int life = 3;
    private int highScore = 0;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HIGHSCORE", 500);
        MinPosition = new Vector2(-7f, -14f);
        MaxPosition = new Vector2(7f, 14f);
        StartCoroutine(SpawnCroissant());
        StartCoroutine(SpawnHotdog());
        UpdateUI();
    }

    public void UpdateUI()
    {
        textScore.text = string.Format("SCORE\n{0}", score);
        textLife.text = string.Format("LIFE\n{0}", life);
        textHighScore.text = string.Format("HIGHSCORE\n{0}", highScore);
    }

    public void AddScore(int addScore)
    {
        score += addScore;
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HIGHSCORE", highScore);
        }
        UpdateUI();
    }

    public int GetLife()
    {
        return life;
    }

    public void Dead()
    {
        life--;
        if(life <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        UpdateUI();
    }

    private IEnumerator SpawnCroissant()
    {
        float randomX = 0f;
        float randomDelay = 0f;

        while (true)
        {
            randomX = Random.Range(-7f, 7f);
            randomDelay = Random.Range(1f, 5f);
            for(int i = 0; i < 5; i++)
            {
                Instantiate(enemyCroissant, new Vector2(randomX, 20f), Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
            }            
            yield return new WaitForSeconds(randomDelay);
        }
    }

    private IEnumerator SpawnHotdog()
    {
        float randomY = 0f;
        float randomDelay = 0f;

        yield return new WaitForSeconds(5f);

        while (true)
        {
            randomY = Random.Range(0f, 7f);
            randomDelay = Random.Range(1f, 5f);
            for (int i = 0; i < 5; i++)
            {
                Instantiate(enemyHotdog, new Vector2(7f, randomY), Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(randomDelay);
        }
    }
}
