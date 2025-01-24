using System;
using System.Collections.Generic;
using UnityEngine;

public class CardGameManager : Singleton<CardGameManager>
{
    [SerializeField] private List<Card> cards;
    [SerializeField] private List<CardObject> cardObjects;

    private CardObject holdingCard = null;
    private bool isGameStarted = false;
    private float timer = 0f;
    private int currentTurn = 0;

    public CardObject HoldingCard => holdingCard;
    public bool IsGameStarted => isGameStarted;
    public int Turn => currentTurn;
    public float Timer => timer;
    public Action turnIncresed;
    public Action playerWin;
    public Action playerLose;

    private void Start()
    {
        SetTheGame();
    }

    private void Update()
    {
        if (!isGameStarted)
            return;

        timer += Time.deltaTime;

        if (cards.Count <= 0)
        {
            isGameStarted = false;
            playerLose?.Invoke();
            DeckField.Instance.gameObject.SetActive(false);
        }

        if (cardObjects.Count <= 0)
        {
            isGameStarted = false;
            playerWin?.Invoke();
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 10f))
            if (hit.transform != null)
            {
                var card = hit.transform.GetComponent<CardObject>();
                if (card != null)
                {
                    if (!card.IsCovered && !card.IsDiscarded)
                        MoveTheCard(card);
                }
            }
    }

    private void SetTheGame()
    {
        for (int i = 0; i < cardObjects.Count; i++)
        {
            cardObjects[i].AssignCard(cards[UnityEngine.Random.Range(0, cards.Count)]);

            cards.Remove(cardObjects[i].AssignedCard);
        }
        isGameStarted = true;
        DeckField.Instance.gameObject.SetActive(true);
    }

    private void MoveTheCard(CardObject card)
    {
        if (holdingCard == null)
            card.ToggleFollowingMouse(Input.GetMouseButton(0));
        else
            holdingCard.ToggleFollowingMouse(Input.GetMouseButton(0));

    }

    public void SetHoldingCard(CardObject card)
    {
        holdingCard = card;
    }
    public Card GetCardFromTheDeck()
    {
        if (cards.Count <= 0)
            return null;

        Card newCard = cards[UnityEngine.Random.Range(0, cards.Count)];
        cards.Remove(newCard);
        return newCard;
    }

    public void DiscardTheCard(CardObject card)
    {
        if (card == null)
            return;

        cardObjects.Remove(card);
        Debug.Log(cardObjects.Count);
    }

    public void AddTurn()
    {
        currentTurn++;
        turnIncresed?.Invoke();
    }

}

