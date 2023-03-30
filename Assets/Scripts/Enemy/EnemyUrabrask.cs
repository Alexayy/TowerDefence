using TMPro;

public class EnemyUrabrask : EnemyBase
{
    public string nameOfThis = "Urabrask";
    public TMP_Text nameDisplay;

    private void Start()
    {
        nameDisplay.text = nameOfThis;
    }
}
