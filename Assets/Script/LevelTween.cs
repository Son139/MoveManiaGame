using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTween : MonoBehaviour
{
    public static LevelTween instance;
    [SerializeField]
    GameObject panelCompletedLevel, homeButton, replayButton, nextLevel, itemCollected, itemImg, colorWheel,
    starFilled1, starFilled2, starFilled3, starEmpty1, starEmpty2, starEmpty3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //private void OnDestroy()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    ResetAnimation();
    //}

    public void LevelComplete()
    {
        RectTransform rectTransform = colorWheel.GetComponent<RectTransform>();
        Vector2 originalSize = rectTransform.sizeDelta;
        Vector2 targetSize = new Vector2(3000f, 3000f);

        LeanTween.value(gameObject, 0f, 1f, 0.3f) // Thời gian phóng to trong 3 giây
            .setEase(LeanTweenType.easeInOutQuad) // Sử dụng loại dễ dàng mượt mà
            .setOnUpdate((float t) =>
            {
                rectTransform.sizeDelta = Vector2.Lerp(originalSize, targetSize, t);
            });
            //.setOnComplete(() =>
            //{
            //    LeanTween.rotateAround(colorWheel, Vector3.forward, -360, 10f)
            //        .setLoopClamp();
            //});
        LeanTween.moveLocal(panelCompletedLevel, new Vector3(0f, 0f, 0f), 0.5f).setDelay(.1f).setEase(LeanTweenType.easeOutCirc);
        LeanTween.scale(homeButton, new Vector3(1f, 1f, 1f), 2f).setDelay(.3f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(nextLevel, new Vector3(1f, 1f, 1f), 2f).setDelay(.4f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(replayButton, new Vector3(1f, 1f, 1f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.alpha(itemImg.GetComponent<RectTransform>(), 1f, .4f).setDelay(0.5f);
        LeanTween.alpha(itemCollected.GetComponent<RectTransform>(), 4f, .5f).setDelay(0.5f);
    }

    public void ResetAnimation()
    {
        LeanTween.cancel(colorWheel);

        RectTransform rectTransform = colorWheel.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(0f, 0f);

        LeanTween.cancel(panelCompletedLevel);
        panelCompletedLevel.transform.localPosition = new Vector3(0f, -1000f, 0f); // Thay thế -1000f bằng vị trí ban đầu của panelCompletedLevel

        LeanTween.cancel(homeButton);
        homeButton.transform.localScale = Vector3.zero;

        LeanTween.cancel(replayButton);
        replayButton.transform.localScale = Vector3.zero;

        LeanTween.cancel(starEmpty1);
        starEmpty1.transform.localScale = Vector3.zero;

        LeanTween.cancel(starEmpty2);
        starEmpty2.transform.localScale = Vector3.zero;

        LeanTween.cancel(starEmpty3);
        starEmpty3.transform.localScale = Vector3.zero;

        LeanTween.cancel(starFilled1);
        starFilled1.transform.localScale = Vector3.zero;

        LeanTween.cancel(starFilled2);
        starFilled2.transform.localScale = Vector3.zero;

        LeanTween.cancel(starFilled3);
        starFilled3.transform.localScale = Vector3.zero;

        LeanTween.cancel(nextLevel);
        nextLevel.transform.localScale = Vector3.zero;

        LeanTween.cancel(itemImg.GetComponent<RectTransform>());
        itemImg.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);

        LeanTween.cancel(itemCollected.GetComponent<RectTransform>());
        itemCollected.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
    }
}
