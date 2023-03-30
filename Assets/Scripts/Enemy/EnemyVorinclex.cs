using TMPro;

public class EnemyVorinclex : EnemyBase
{
    public string nameOfThis = "Vorinclex";
    public TMP_Text nameDisplay;

    private void Start()
    {
        nameDisplay.text = nameOfThis;
    }
}
