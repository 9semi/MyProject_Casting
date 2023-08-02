using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] static string nextScene;
    [SerializeField] Image backGround;
    [SerializeField] Sprite[] sprite;
    [SerializeField] Text text;
    [SerializeField] Text textNormal;
    [SerializeField] Image progressBar;
    int randNum;
    
    private void Start()
    {
        // 혹시 실행되고 있는 이펙트 소리가 있다면 찾아서 끈다.
        AudioManager.INSTANCE.StopAllEffect();

        randNum = Random.Range(0, 1);
        backGround.sprite = sprite[randNum];
        progressBar.fillAmount = 0.0f;
        AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.loadingscene, true);
        StartCoroutine(LoadScene());
    }
    public static void LoadScene(string sceneName)
    {        
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }   
    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            if (op.progress >= 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            int a = (int)(progressBar.fillAmount * 100);
            if (a > 0 && a <= 33)
            {
                textNormal.text = "Loading...";
            }
            else if (a > 33 && a <= 67)
            {
                textNormal.text = "Downloading...";
            }
            else if (a > 67 && a <= 100)
            {
                textNormal.text = "Please wait...";
            }

            text.text = a + "%";
        }
    }
}