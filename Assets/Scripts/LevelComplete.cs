using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject star1, star2, star3;
    [SerializeField] private GameObject nextButton, homeButton, restartButton;
    [SerializeField] private GameObject frame, backPanel;

    [Header("Score Texts")]
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    private Vector3 framePosition;

    void Start()
    {
        framePosition = frame.transform.localPosition;

       
        var cg = backPanel.GetComponent<CanvasGroup>();
        if (cg == null) cg = backPanel.AddComponent<CanvasGroup>();
        cg.alpha = 0f;
        frame.SetActive(false);
    }

  
    public void ShowLevelComplete(int mistakes, int score, int bestScore)
    {
        gameObject.SetActive(true);

        Debug.Log($"[LevelComplete] ShowLevelComplete called! Mistakes: {mistakes}, Score: {score}, BestScore: {bestScore}");
        AudioManager.instance.PlaySFX(AudioManager.instance.winSFX);


        var bgImage = backPanel.GetComponent<UnityEngine.UI.Image>();
        if (bgImage != null)
        {
            var color = bgImage.color;
            color.a = 0f;
            bgImage.color = color;
            LeanTween.value(backPanel, 0f, 0.8f, 0.3f)
                .setOnUpdate((float val) =>
                {
                    var c = bgImage.color;
                    c.a = val;
                    bgImage.color = c;
                })
                .setIgnoreTimeScale(true);
        }

       
        if (finalScoreText != null)
        {
            finalScoreText.text = $" {score}";
            Debug.Log($"[LevelComplete] finalScoreText updated: {finalScoreText.text}");
        }
        else
        {
            Debug.LogWarning("[LevelComplete] finalScoreText is NOT assigned in the inspector!");
        }

        if (bestScoreText != null)
        {
            bestScoreText.text = $"{bestScore}";
            Debug.Log($"[LevelComplete] bestScoreText updated: {bestScoreText.text}");
        }
        else
        {
            Debug.LogWarning("[LevelComplete] bestScoreText is NOT assigned in the inspector!");
        }

      
        LeanTween.moveLocal(frame, new Vector3(0, 20, 0), 0.5f)
            .setEase(LeanTweenType.easeOutBounce)
            .setIgnoreTimeScale(true)
            .setOnComplete(() => StarsAnimation(mistakes));
    }


    private void StarsAnimation(int mistakes)
    {
        
        float starScale = 2.5f;
        star1.transform.localScale = new Vector3(starScale, starScale, starScale);
        star2.transform.localScale = new Vector3(starScale, starScale, starScale);
        star3.transform.localScale = new Vector3(starScale, starScale, starScale);

       
        int starsEarned = 0;
        if (mistakes <= 2) starsEarned = 3;
        else if (mistakes <= 4) starsEarned = 2;
        else starsEarned = 1;

       
        if (starsEarned >= 1)
            AnimateStar1(() =>
            {
                if (starsEarned >= 2)
                    LeanTween.delayedCall(0.3f, () => AnimateStar2(() =>
                    {
                        if (starsEarned >= 3)
                            LeanTween.delayedCall(0.3f, () => AnimateStar3());
                    })).setIgnoreTimeScale(true);
            });
    }

    private void AnimateStar1(System.Action onComplete = null)
    {
        star1.SetActive(true);
        LeanTween.alpha(star1.GetComponent<RectTransform>(), 1f, 0.3f)
            .setIgnoreTimeScale(true);
        LeanTween.scale(star1, Vector3.one, 0.5f)
            .setEase(LeanTweenType.easeOutBounce)
            .setIgnoreTimeScale(true)
            .setOnComplete(() => { onComplete?.Invoke(); });
    }

    private void AnimateStar2(System.Action onComplete = null)
    {
        star2.SetActive(true);
        LeanTween.alpha(star2.GetComponent<RectTransform>(), 1f, 0.3f)
            .setIgnoreTimeScale(true);
        LeanTween.scale(star2, Vector3.one, 0.5f)
            .setEase(LeanTweenType.easeOutBounce)
            .setIgnoreTimeScale(true)
            .setOnComplete(() => { onComplete?.Invoke(); });
    }

    private void AnimateStar3(System.Action onComplete = null)
    {
        star3.SetActive(true);
        LeanTween.alpha(star3.GetComponent<RectTransform>(), 1f, 0.3f)
            .setIgnoreTimeScale(true);
        LeanTween.scale(star3, Vector3.one, 0.5f)
            .setEase(LeanTweenType.easeOutBounce)
            .setIgnoreTimeScale(true)
            .setOnComplete(() => { onComplete?.Invoke(); });
    }



    public void Restart()
    {
        LeanTween.scale(frame, Vector3.zero, 0.3f)
            .setEase(LeanTweenType.easeInCubic)
            .setIgnoreTimeScale(true)
            .setOnComplete(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
    }
}