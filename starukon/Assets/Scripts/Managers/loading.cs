using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loading : MonoBehaviour
{
    public GameObject loadingInfo;
    public GameObject loadingIcon;
    public GameObject background;
    private AsyncOperation async;

    private void Awake()
    {
        Cursor.visible = false;
        Helper.Set2DCameraToObject(background);
#if UNITY_ANDROID
        loadingInfo.GetComponent<Text>().text = "TOUCH TO START";
#endif
    }

    IEnumerator Start()
    {
        async = SceneManager.LoadSceneAsync(2);
        yield return true;
        async.allowSceneActivation = false;
        loadingInfo.SetActive(true);
        loadingIcon.SetActive(false);
    }

    private void Update()
    {
#if UNITY_STANDALONE || UNITY_WEBGL
        if (Input.anyKeyDown) async.allowSceneActivation = true;
#endif
#if UNITY_ANDROID
        if (Input.touchCount > 0) async.allowSceneActivation = true;
#endif
    }
}
