using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _currency;
    [SerializeField] private TMP_Text _lives;

    [SerializeField] private Button _basillicaTurret;
    [SerializeField] private Button _bayTurret;
    [SerializeField] private Button _drosTurret;
    [SerializeField] private Button _hunterTurret;
    [SerializeField] private Button _furnaceTurret;
    
    [SerializeField] private Shop _shop;

    [Header("Pause UI")] 
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private Button _androidPauseButton;
    [SerializeField] private Button _panelClosePauseMenu;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _quitButton;
    
    [SerializeField] private TMP_Text _endText;
    [SerializeField] private TMP_Text _endButtonText;

    private void Awake()
    {
        _basillicaTurret.onClick.RemoveAllListeners();
        _bayTurret.onClick.RemoveAllListeners();
        _drosTurret.onClick.RemoveAllListeners();
        _hunterTurret.onClick.RemoveAllListeners();
        _furnaceTurret.onClick.RemoveAllListeners();
        
        _basillicaTurret.onClick.AddListener(SpawnBasillicaTurret);
        _bayTurret.onClick.AddListener(SpawnBayTurret);
        _drosTurret.onClick.AddListener(SpawnDrosTurret);
        _hunterTurret.onClick.AddListener(SpawnHunterTurret);
        _furnaceTurret.onClick.AddListener(SpawnFurnaceTurret);
    
        _continueButton.onClick.RemoveAllListeners();
        _continueButton.onClick.AddListener(delegate
        {
            _pausePanel.SetActive(false);
            GameManager.Instance.ResumeGame();
        });
        
        _quitButton.onClick.RemoveAllListeners();
        _quitButton.onClick.AddListener(Application.Quit);
        
#if UNITY_ANDROID
        _androidPauseButton.gameObject.SetActive(true);
        _androidPauseButton.onClick.RemoveAllListeners();
        _androidPauseButton.onClick.AddListener(delegate
        {
            GameManager.Instance.PauseGame();
            _endText.text = "Pause";
            _pausePanel.SetActive(true);
        });
#endif
        
        _panelClosePauseMenu.onClick.RemoveAllListeners();
        _panelClosePauseMenu.onClick.AddListener(delegate
        {
            _pausePanel.SetActive(false);
            GameManager.Instance.ResumeGame();
        });
    }

    private void Start()
    {
        _pausePanel.SetActive(false);
    }

    private void Update()
    {
        _currency.text = $"${GameManager.Currency.ToString()}";
        _lives.text = $"Lives: {GameManager.Health.ToString()}";

#if UNITY_STANDALONE_WIN || UNITY_WEBGL || UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GameManager.Instance.PauseGame();
            _endText.text = "Pause";
            _pausePanel.gameObject.SetActive(true);
        }
#endif

    }

    public void SpawnBasillicaTurret() { _shop.PurchaseBasillicaTurret(); }
    public void SpawnBayTurret() { _shop.PurchaseBayTurret(); }
    public void SpawnDrosTurret() { _shop.PurchasePitterTurret(); }
    public void SpawnHunterTurret() { _shop.PurchaseHunterTurret(); }
    public void SpawnFurnaceTurret() { _shop.PurchaseFurnaceTurret(); }

    public void YouWin()
    {
        
    }

    public void YouLose()
    {
        if (GameManager.Health <= 0)
        {
            _continueButton.gameObject.SetActive(false);
            GameManager.Instance.PauseGame();
            _endText.text = "You Lose";
            _endButtonText.text = "Try Again";
            StartCoroutine(IWillReturnToHomeScreen());
        }
    }

    IEnumerator IWillReturnToHomeScreen()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Start");
    }
}
