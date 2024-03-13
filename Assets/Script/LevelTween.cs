using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTween : MonoBehaviour
{
    [SerializeField]
    GameObject panelCompletedLevel, homeButton, replayButton, nextLevel, itemCollected, itemImg, colorWheel;

    void Start()
    {
        RectTransform rectTransform = colorWheel.GetComponent<RectTransform>();
        Vector2 originalSize = rectTransform.sizeDelta;
        Vector2 targetSize = new Vector2(3000f, 3000f);

        LeanTween.value(gameObject, 0f, 1f, 0.3f) // Thời gian phóng to trong 3 giây
            .setEase(LeanTweenType.easeInOutQuad) // Sử dụng loại dễ dàng mượt mà
            .setOnUpdate((float t) => {
                rectTransform.sizeDelta = Vector2.Lerp(originalSize, targetSize, t);
            })
            .setOnComplete(() =>
            {
                LeanTween.rotateAround(colorWheel, Vector3.forward, -360, 10f)
                    .setLoopClamp();
            });
        LevelComplete();
    }

    void LevelComplete()
    {
        LeanTween.moveLocal(panelCompletedLevel, new Vector3(0f, 0f, 0f), 0.5f).setDelay(.1f).setEase(LeanTweenType.easeOutCirc);
        LeanTween.scale(homeButton, new Vector3(1f, 1f, 1f), 2f).setDelay(.3f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(nextLevel, new Vector3(1f, 1f, 1f), 2f).setDelay(.4f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(replayButton, new Vector3(1f, 1f, 1f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.alpha(itemImg.GetComponent<RectTransform>(), 1f, .4f).setDelay(0.5f);
        LeanTween.alpha(itemCollected.GetComponent<RectTransform>(), 4f, .5f).setDelay(0.5f);
    }
}
