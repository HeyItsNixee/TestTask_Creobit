using UnityEngine;

public class DeckField : Singleton<DeckField>
{
    [SerializeField] private CardObject cardPrefab;

    private void Update()
    {
        if (!CardGameManager.Instance.IsGameStarted)
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 10f))
            if (hit.transform != null)
            {
                var deck = hit.transform.GetComponent<DeckField>();
                if (deck != null && Input.GetMouseButtonDown(0))
                {
                    var newCard = Instantiate(cardPrefab);
                    newCard.AssignCard(CardGameManager.Instance.GetCardFromTheDeck());
                    newCard.transform.position = DiscardField.Instance.transform.position;
                    DiscardField.Instance.DiscardTheCard(newCard);
                }
            }
    }
}
