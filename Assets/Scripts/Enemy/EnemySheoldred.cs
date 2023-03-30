using TMPro;
public class EnemySheoldred : EnemyBase
{
    public string nameOfThis = "Sheoldred";
    public TMP_Text nameDisplay;

    private void Start()
    {
        nameDisplay.text = nameOfThis;
    }
}
