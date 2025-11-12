using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [Header("Card Setup")]
    public int cardID;
    public Image front_Side;
    public Image back_Side;
    public bool isFlipped;
    public bool isMatched = false;
    public bool isEverRevealed = false;
    [Header("Animation Settings")]
    public float flipDuration = 0.5f;

    private bool isAnimating = false;


    public void SetupCard(Sprite Front_Sprite, int id)
    {
        cardID = id;
        front_Side.sprite = Front_Sprite;

      
        front_Side.gameObject.SetActive(false);
        back_Side.gameObject.SetActive(true);

        isFlipped = false;
        isAnimating = false;
        transform.rotation = Quaternion.identity;
    }


    public void OnClick()
    {
        if (!GameManager.instance.CanSelect())
            return;

        if (ScoreManager.instance != null && ScoreManager.instance.IsGameOver())
            return;

        if (isAnimating)
            return;

        if (!isFlipped)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.clickSFX);

            FlipToFront();
            GameManager.instance.OnReveal(this);
        }
    }

    private void FlipToFront()
    {
        if (isAnimating) return;
        isAnimating = true;

        
        transform.DORotate(new Vector3(0, 90, 0), flipDuration / 2)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
              
                back_Side.gameObject.SetActive(false);
                front_Side.gameObject.SetActive(true);

                
                transform.DORotate(new Vector3(0, 180, 0), flipDuration / 2)
                    .SetEase(Ease.InOutQuad)
                    .OnComplete(() =>
                    {
                       
                        transform.rotation = Quaternion.identity;
                        isFlipped = true;
                        isAnimating = false;
                    });
            });
    }

    private void FlipToBack()
    {
        if (isAnimating) return;
        isAnimating = true;

        transform.DORotate(new Vector3(0, 90, 0), flipDuration / 2)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
             
                front_Side.gameObject.SetActive(false);
                back_Side.gameObject.SetActive(true);

                transform.DORotate(Vector3.zero, flipDuration / 2)
                    .SetEase(Ease.InOutQuad)
                    .OnComplete(() =>
                    {
                        isFlipped = false;
                        isAnimating = false;
                    });
            });
    }

 
    public void FlipCard()
    {
        if(!isEverRevealed)
            isEverRevealed = true;

        if (!isFlipped)
            FlipToFront();
        else
            FlipToBack();
    }

    public void HideCard()
    {
       
        if (isAnimating) return;
        if (!isFlipped) return;

        FlipToBack();
    }
}
