using UnityEngine;
using UnityEngine.UI;

public class UI_TurnUpdate : MonoBehaviour
{
    [SerializeField] private Text turnText;

    private void Start()
    {
        CardGameManager.Instance.turnIncresed += UpdateText;
    }

    private void UpdateText()
    {
        turnText.text = CardGameManager.Instance.Turn.ToString();
    }

    private void OnDestroy()
    {
        CardGameManager.Instance.turnIncresed -= UpdateText;
    }
}
