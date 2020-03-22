using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text totalScore;
    public Text score;
    public Text speed;
    public Text level;
    public Image[] lifes;
    public Slider lifeBar;
    public Slider enemyLifeBar;

    public GameObject ballImage;

    public static float time;
    private float controlTime = 2f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time <= controlTime)
        {
            level.gameObject.SetActive(true);
        }
        else level.gameObject.SetActive(false);
    }

    public IEnumerator ChangeLifeBar(float value)
    {
        float newValue = lifeBar.value + value;
        for (float time = 0.0f; time <= 1; time += 0.5f * Time.deltaTime)
        {
            lifeBar.value = Mathf.Lerp(lifeBar.value, newValue, time);
            yield return null;
        }
        lifeBar.value = newValue;
    }

    public IEnumerator ChangeLifeBar()
    {
        for (float time = 0.0f; time <= 1; time += 0.5f * Time.deltaTime)
        {
            lifeBar.value = Mathf.Lerp(lifeBar.value, 0, time);
            yield return null;
        }
        lifeBar.value = 0f;
    }

    public IEnumerator ChangeEnemyLifeBar(float value)
    {
        float newValue = enemyLifeBar.value + value;
        for (float time = 0.0f; time <= 1; time += 0.5f * Time.deltaTime)
        {
            enemyLifeBar.value = Mathf.Lerp(enemyLifeBar.value, newValue, time);
            yield return null;
        }
        enemyLifeBar.value = newValue;
    }

    public IEnumerator ChangeEnemyLifeBar()
    {
        for (float time = 0.0f; time <= 1; time += 0.5f * Time.deltaTime)
        {
            enemyLifeBar.value = Mathf.Lerp(enemyLifeBar.value, 0, time);
            yield return null;
        }
        enemyLifeBar.value = 0f;
    }
}