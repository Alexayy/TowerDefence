using TMPro;

public class EnemyJinGitaxias : EnemyBase
{
    public string nameOfThis = "Jin-Gitaxias";
    public TMP_Text nameDisplay;

    private void Start()
    {
        nameDisplay.text = nameOfThis;
    }
}
