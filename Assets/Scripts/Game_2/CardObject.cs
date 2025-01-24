using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cardSprite;
    private Card assignedCard;

    private bool isFollowingMouse = false;
    private bool isDiscarded = false;
    private bool isCovered = true;
    private bool isFlipped = false;

    private Vector3 originalPosition;
    private Vector3 rotationVector = new Vector3(0f, 180f, 0f);
    public Card AssignedCard => assignedCard;
    public bool IsCovered => isCovered;
    public bool IsDiscarded => isDiscarded;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (AssignedCard == null)
            return;

        CheckForCoverage();

        if (isFollowingMouse)
        {
            Vector3 newPos = Vector3.zero;
            newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = transform.position.z;
            transform.position = newPos;
        }
        else
            transform.position = originalPosition;
    }

    private void CheckForCoverage()
    {
        var coverege = Physics.OverlapSphere(transform.position, 0.4f);
        bool foundCover = false;
        foreach (var c in coverege)
        {
            if (c.gameObject.layer == gameObject.layer + 1)
            {
                foundCover = true;
            }
        }

        if (foundCover != isCovered)
        {
            Flip();
            isCovered = foundCover;
        }
    }

    private void Flip()
    {
        if (isFlipped)
            transform.DORotate(Vector3.zero, 0.5f, RotateMode.Fast);
        else
            transform.DORotate(rotationVector, 0.5f, RotateMode.Fast);

        cardSprite.sprite = assignedCard.sprite;
        isFlipped = !isFlipped;
    }

    public void AssignCard(Card newCard)
    {
        if (newCard == null)
            return;

        assignedCard = newCard;
        if (isFlipped)
            Flip();
    }

    public void ToggleFollowingMouse(bool state)
    {
        //Debug.Log("Toggle works for " + name + ": state = " + state);
        isFollowingMouse = state;

        if (state)
            CardGameManager.Instance.SetHoldingCard(this);
        else
            CardGameManager.Instance.SetHoldingCard(null);
    }

    public CardObject DiscardTheCard(Vector3 newPosition, CardObject previousCard)
    {
        CardGameManager.Instance.DiscardTheCard(this);
        ToggleFollowingMouse(false);
        originalPosition = newPosition;
        isDiscarded = true;

        if (previousCard != null)
            Destroy(previousCard.gameObject);

        return this;
    }
}
