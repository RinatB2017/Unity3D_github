using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScript : MonoBehaviour
{
    public GameObject field;
    public Text totalScoreText;
    public GameObject lastScore;
    public Text lastScoreText;

    private void Awake()
    {
        Cursor.visible = false;
        Helper.Set2DCameraToObject(field);
        if (!PlayerPrefs.HasKey("totalScore"))
        {
            PlayerPrefs.SetInt("totalScore", 10000);
            PlayerPrefs.SetString("totalName", "chipenstain");
        }
        totalScoreText.text = PlayerPrefs.GetString("totalName") + "-" + PlayerPrefs.GetInt("totalScore");
        if (!PlayerPrefs.HasKey("lastScore"))
        {
            lastScore.SetActive(false);
        }
        else
        {
            lastScore.SetActive(true);
            lastScoreText.text = PlayerPrefs.GetString("lastName") + "-" + PlayerPrefs.GetInt("lastScore");
        }
    }

    private void Update()
    {
#if UNITY_STANDALONE || UNITY_WEBGL
        if (Input.anyKeyDown && !(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Delete)) && !Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Delete))
        {
            Debug.Log("delete saves");
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("totalScore", 10000);
            PlayerPrefs.SetString("totalName", "chipenstain");
        }
#endif
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
#if UNITY_ANDROID
        if (Input.touchCount > 0) SceneManager.LoadScene(1);
#endif
    }
}
