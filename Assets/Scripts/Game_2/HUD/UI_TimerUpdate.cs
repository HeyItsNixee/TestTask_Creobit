using UnityEngine;
using UnityEngine.UI;

public class UI_TimerUpdate : MonoBehaviour
{
    [SerializeField] private Text hours;
    [SerializeField] private Text minutes;
    [SerializeField] private Text seconds;

    private void Update()
    {
        UpdateTimeText();
    }

    private void UpdateTimeText()
    {
        int h = 0, m = 0, s = 0;
        h = (int)CardGameManager.Instance.Timer / 3600;
        m = (int)CardGameManager.Instance.Timer / 60;
        s = (int)CardGameManager.Instance.Timer;

        if (h < 10 && h >= 0)
            hours.text = "0" + h.ToString();
        else
            hours.text = h.ToString();

        if (m < 10 && m >= 0)
            minutes.text = "0" + m.ToString();
        else
            minutes.text = m.ToString();

        if (s < 10 && s >= 0)
            seconds.text = "0" + s.ToString();
        else
            seconds.text = s.ToString();
    }

}
