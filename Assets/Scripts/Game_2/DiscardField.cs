using UnityEngine;

public class DiscardField : Singleton<DiscardField>
{
    [SerializeField] private GameObject hintPosition;

    private void Start()
    {
        hintPosition.SetActive(true);
    }

    private CardObject lastGotCard = null;
    public CardObject LastDiscardedCard => lastGotCard;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (CardGameManager.Instance.HoldingCard == null)
            return;

        if (other.gameObject == CardGameManager.Instance.HoldingCard.gameObject)
        {
            if (lastGotCard == null)
            {
                AcceptTheCard();
            }
            else
            {
                if (lastGotCard.AssignedCard.value == CardGameManager.Instance.HoldingCard.AssignedCard.nextPossible.value ||
                    lastGotCard.AssignedCard.value == CardGameManager.Instance.HoldingCard.AssignedCard.previousPossible.value)
                    AcceptTheCard();
                else
                    return;
            }
        }
    }

    private void AcceptTheCard()
    {
        lastGotCard = CardGameManager.Instance.HoldingCard.DiscardTheCard(transform.position, lastGotCard);
        if (hintPosition.activeSelf)
            hintPosition.SetActive(false);

        CardGameManager.Instance.AddTurn();
    }

    public void DiscardTheCard(CardObject newCard)
    {
        if (lastGotCard != null)
            Destroy(lastGotCard.gameObject);

        lastGotCard = newCard;

        if (hintPosition.activeSelf)
            hintPosition.SetActive(false);

        CardGameManager.Instance.AddTurn();
    }

}
