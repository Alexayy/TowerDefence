using TMPro;
using UnityEngine;

public class EnemyUrabrask : EnemyBase
{
    /* Using this as an extension to base class to classify,
     far more is imagined and planned, but due to time and skill,
     that remains as a wish. */
    [Header("Enemy specifics")]
    public string nameOfThis = "Urabrask";
    public TMP_Text nameDisplay;

    private void Start()
    {
        nameDisplay.text = nameOfThis;
    }
}
