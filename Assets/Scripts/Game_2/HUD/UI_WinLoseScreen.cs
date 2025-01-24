using UnityEngine;

public class UI_WinLoseScreen : MonoBehaviour
{
    [SerializeField] private GameObject Background;
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject LoseScreen;


    private void Start()
    {
        HideAll();
        CardGameManager.Instance.playerWin += ShowWin;
        CardGameManager.Instance.playerLose += ShowLose;
    }

    private void ShowWin()
    {
        Background.SetActive(true);
        WinScreen.SetActive(true);
    }

    private void ShowLose()
    {
        Background.SetActive(true);
        LoseScreen.SetActive(true);
    }

    private void HideAll()
    {
        Background.SetActive(false);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
    }

    private void OnDestroy()
    {
        CardGameManager.Instance.playerWin -= ShowWin;
        CardGameManager.Instance.playerLose -= ShowLose;
    }
}
