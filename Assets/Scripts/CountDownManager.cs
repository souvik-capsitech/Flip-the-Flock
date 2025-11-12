using UnityEngine;
using TMPro;
using DG.Tweening;

public class CountdownManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public GameObject countdownPanel;

    public float scaleDuration = 0.4f;

    public System.Action OnCountdownFinished;

    private void Start()
    {
        StartCoroutine(StartCountdown());
    }

    private System.Collections.IEnumerator StartCountdown()
    {
        int count = 3;

        while (count > 0)
        {
            countdownText.text = count.ToString();

            countdownText.transform.localScale = Vector3.zero;

           
            countdownText.transform.DOScale(1f, scaleDuration)
                .SetEase(Ease.OutBack);

            yield return new WaitForSeconds(1f);
            count--;
        }

      
        countdownText.text = "GO!";
        countdownText.transform.localScale = Vector3.zero;

        countdownText.transform.DOScale(1f, scaleDuration)
            .SetEase(Ease.OutBack);

        yield return new WaitForSeconds(0.7f);

      
        countdownPanel.SetActive(false);

     
        OnCountdownFinished?.Invoke();
    }
}
