using TMPro;

public class EnemyEleshNorn : EnemyBase
{
    public string nameOfThis = "Elesh Norn";
    public TMP_Text nameDisplay;

    private void Start()
    {
        nameDisplay.text = nameOfThis;
    }
}
