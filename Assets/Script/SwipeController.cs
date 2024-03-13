using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour
{
    [SerializeField] int maxPage;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPageRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    //float dragThreshould;

    [SerializeField] Image[] barImage;
    [SerializeField] Sprite barClosed, barOpen;

    [SerializeField] Button previousBtn, nextBtn;
    Vector3 initialPosition;
    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPageRect.localPosition;
        initialPosition = levelPageRect.localPosition;
        UpdateBar();
        UpdateArrowButton();
    }

    Vector3 CalculateTargetPosition(int targetPage)
    {
        float newX = initialPosition.x + (pageStep.x * (targetPage - 1));
        return new Vector3(newX, levelPageRect.localPosition.y, levelPageRect.localPosition.z);
    }

    private void Start()
    {
        levelPageRect.GetComponent<ScrollRect>().enabled = false;
    }
    public void NextBtn()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }

    public void PreviousBtn()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }

    void MovePage()
    {
        levelPageRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        UpdateBar();
        UpdateArrowButton();
    }

    void UpdateBar()
    {
        foreach (var item in barImage)
        {
            item.sprite = barClosed;
        }
        barImage[currentPage - 1].sprite = barOpen;
    }

    void UpdateArrowButton()
    {
        nextBtn.interactable = true;
        previousBtn.interactable = true;
        if (currentPage == 1) previousBtn.interactable = false;
        else if (currentPage == maxPage) nextBtn.interactable = false;
    }

    public void ArrowButtonClick(int pageIndex)
    {
        currentPage = pageIndex + 1;
        targetPos = CalculateTargetPosition(currentPage);
        MovePage();
    }
}
