using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ArmoryCutscene : MonoBehaviour
{

    public GameObject player;
    public Camera cutsceneCam;
    public Image fadeImage;
    public float fadeDuration;

    private bool playCutsceneOnce = false;
    
    void Start()
    {
        cutsceneCam.gameObject.SetActive(false);
        Color color = fadeImage.color;
        color.a = 0;
        fadeImage.color = color;    
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !playCutsceneOnce)
        {
            StartCoroutine(FadeInAndOut());
            StartCoroutine(StopScutscene());
            playCutsceneOnce=true;
        }
    }

    IEnumerator FadeInAndOut()
    {
        yield return StartCoroutine(FadeImage(fadeImage, 0, 1f, fadeDuration));
        yield return new WaitForSeconds(1);
        player.SetActive(false);
        cutsceneCam.gameObject.SetActive(true);
        yield return StartCoroutine(FadeImage(fadeImage, 1, 0, fadeDuration));
    }

    IEnumerator StopScutscene()
    {
        yield return new WaitForSeconds(10);
        yield return StartCoroutine(FadeImage(fadeImage, 0, 1f, fadeDuration));
        yield return new WaitForSeconds(1);
        player.SetActive(true);
        cutsceneCam.gameObject.SetActive(false);
        yield return StartCoroutine(FadeImage(fadeImage, 1, 0, fadeDuration));
    }

    IEnumerator FadeImage(Image image, float startOpacity, float targetOpacity, float duration)
    {
        float elapsedTime = 0;
        Color color = fadeImage.color;
        color.a = startOpacity;

        while(elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startOpacity, targetOpacity, elapsedTime / duration);
            image.color = color;
            yield return null;
        }
        color.a = targetOpacity;
        image.color = color;    
    }

}
