using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static GameManager instance;
    public GameObject cardPrefab;
    public Transform gridParent;
    public List<Sprite> cardSprites; 

    private List<int> cardID = new List<int>();
    private Card firstCard, secondCard;
    private bool canSelect = true;
    public GameObject matchParticlePrefab;
    private int totalPairs;
    private int matchedPairs;
    public bool gameStarted = false;
    public ThemeData forestTheme;
    public ThemeData arcticTheme;
    private ThemeData activeTheme;
    public Image backgroundImage;
    public List<Card> allCards = new List<Card>();

    void Awake()
    {
        instance = this;

        if (ThemeManager.instance.currentTheme == ThemeManager.ThemeType.Forest)
            activeTheme = forestTheme;
        else
            activeTheme = arcticTheme;

        cardSprites = new List<Sprite>(activeTheme.cardSprites);
    }

    void Start()
    {
        totalPairs = 6;
        matchedPairs = 0;

        
        if (backgroundImage != null)
            backgroundImage.sprite = activeTheme.background;

       
        CountdownManager countdown = FindObjectOfType<CountdownManager>();
        countdown.OnCountdownFinished += () =>
        {
            gameStarted = true;
            GenerateCards();
        };
    }


    public void GenerateCards()
    {
       cardID.Clear();

        for(int i = 0;i<6;i++)
        {
            cardID.Add(i);
            cardID.Add(i);
            
        }

        for (int i=0;i<cardID.Count; i++)
        {
            int randomPos = Random.Range(i, cardID.Count);
            (cardID[i], cardID[randomPos]) = (cardID[randomPos], cardID[i]);
        }

        for(int i=0;i<cardID.Count;i++)
        {
            GameObject cardObject = Instantiate(cardPrefab, gridParent);
            Card card = cardObject.GetComponent<Card>();

            int id = cardID[i];
            card.SetupCard(cardSprites[id], id);

            allCards.Add(card);
        }
        StartCoroutine(ShowAllCardsTemporarily());
    }

    public void OnReveal(Card clickedCard)
    {
        if (!gameStarted) return;
        if (!canSelect) return;

        if(firstCard == null)
        {
            firstCard = clickedCard;
        }

        else if (secondCard == null  && clickedCard != firstCard)
        {
            secondCard = clickedCard;
            canSelect = false;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.5f);

        if (firstCard == null || secondCard == null)
        {
            Debug.LogError(" One of the cards is null before checking match!");
            canSelect = true;
            yield break;
        }

        if (ScoreManager.instance == null)
        {
            Debug.LogError(" ScoreManager instance is missing in the scene!");
            canSelect = true;
            yield break;
        }

        
        if (firstCard.cardID == secondCard.cardID)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.matchSFX);
            bool bothNew = !firstCard.isEverRevealed && !secondCard.isEverRevealed;
            int pointsToAdd = bothNew ? 2 : 1;

            ScoreManager.instance.AddScore(pointsToAdd);
            matchedPairs++;
            Vector3 midPoint = (firstCard.transform.position + secondCard.transform.position) / 2f;
            ShowMatchEffect(midPoint);
            firstCard = null;
            secondCard = null;
            if (matchedPairs >= totalPairs)
            {
                yield return new WaitForSeconds(0.5f);
                FindObjectOfType<LevelComplete>().ShowLevelComplete(
          ScoreManager.instance.GetMistakeCount(),
          ScoreManager.instance.GetScore(),
          ScoreManager.instance.GetBestScore()
      );
                Time.timeScale = 0f;
            }
        
        }
        else
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.failSFX);
            ScoreManager.instance.AddMistake();

        
            if (firstCard != null) firstCard.HideCard();
            if (secondCard != null) secondCard.HideCard();

            firstCard = null;
            secondCard = null;
        }

        canSelect = true;
    }

    IEnumerator ShowAllCardsTemporarily()
    {
       
        canSelect = false;

   
        foreach (Transform child in gridParent)
        {
            Card card = child.GetComponent<Card>();
            if (card != null)
                card.FlipCard(); 
        }

        
        yield return new WaitForSeconds(2f);

        
        foreach (Transform child in gridParent)
        {
            Card card = child.GetComponent<Card>();
            if (card != null)
                card.FlipCard(); 
        }

     
        yield return new WaitForSeconds(0.5f);

      
        canSelect = true;
    }


    public bool CanSelect()
    {
        return canSelect;
    }

    public void ShowMatchEffect(Vector3 worldPos)
    {
        if (matchParticlePrefab == null)
            return;

        
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(
            RectTransformUtility.WorldToScreenPoint(null, worldPos)
        );

        spawnPos.z = 0f;

        Instantiate(matchParticlePrefab, spawnPos, Quaternion.identity);
    }


    public void useEagleEye()
    {
        StartCoroutine(EagleEyeRoutine());
    }

    private IEnumerator EagleEyeRoutine()
    {
        if (!gameStarted) yield break;

        ScoreManager.instance.AddScore(-1);
        List <Card> tempList = new List<Card>();

        foreach (Card c in allCards)
        {
            if(!c.isMatched && !c.isFlipped)
            {
                tempList.Add(c);
                c.FlipCard();
            }
        }
        yield return new WaitForSeconds(2f);

        
        foreach (Card c in tempList)
        {
            if (!c.isMatched && c.isFlipped)
            {
                c.FlipCard();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
