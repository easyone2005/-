using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private Text textHighScore = null;

    private void Start()
    {
        textHighScore.text = string.Format("HIGHSCORE\n{0}",
            PlayerPrefs.GetInt("HIGHSCORE", 500));
    }
    public void OnClickRetry()
    {
        SceneManager.LoadScene("Main");
    }
}
