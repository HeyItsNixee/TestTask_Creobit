using UnityEngine;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public Sprite sprite;
    public CardGrade grade;
    public int value;
    public Card previousPossible;
    public Card nextPossible;
}
