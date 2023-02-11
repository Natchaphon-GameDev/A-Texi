using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float timeRemaining = 10;
    [SerializeField] private bool timerIsRunning = false;
    [SerializeField] private TextMeshProUGUI timeText;

    [Header("Game Panel")] 
    [SerializeField] private RectTransform gameOverPanel; 
    [SerializeField] private RectTransform gameMenuPanel; 
    [SerializeField] private RectTransform gamePlayPanel; 
    [SerializeField] private Canvas manualPanel; 
    
    [Header("Money")]
    [SerializeField] private TextMeshProUGUI moneyText;
    
    [Header("AddOn")]
    [SerializeField] private TextMeshProUGUI AddMoneyText;
    [SerializeField] private TextMeshProUGUI AddTimeText;


    [Header("Camera Setup")]
    public Vector3 menuCam;
    public Vector3 playerCam;

    public event Action OnMenu;
    public event Action OnRestart;

    private int money;
    public static GameManager instance { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        
        SetMenuCam();
    }
    private void Update()
    {
        HandleTimer();
        moneyText.text = money.ToString();
    }

    private void SetMenuCam()
    {
        GameVariable.isPlayerCanMove = false;
        timerIsRunning = false;
        StartCoroutine(WaitForMenu());
        gameOverPanel.gameObject.SetActive(false);
        gamePlayPanel.gameObject.SetActive(false);
        OnMenu?.Invoke();
    }

    private IEnumerator WaitForMenu()
    {
        yield return new WaitForSeconds(3.3f);
        gameMenuPanel.gameObject.SetActive(true);
        DOTweenModuleUI.DOFade(gameMenuPanel.GetComponent<CanvasGroup>(),1,1.5f);
    }

    private void GameEnded()
    {
        gamePlayPanel.gameObject.SetActive(false);
        GameVariable.isPlayerCanMove = false;
        gameOverPanel.gameObject.SetActive(true);
        DOTweenModuleUI.DOFade(gameOverPanel.GetComponent<CanvasGroup>(),1,1.5f);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }

    public void SetPlayerCam()
    {
        DOTweenModuleUI.DOFade(manualPanel.GetComponent<CanvasGroup>(),1,1.5f);
        GameVariable.isPlayerCanMove = true;
        timerIsRunning = true;
        gameMenuPanel.gameObject.SetActive(false);
        gamePlayPanel.gameObject.SetActive(true);
        DOTweenModuleUI.DOFade(gamePlayPanel.GetComponent<CanvasGroup>(),1,1.5f);
        StartCoroutine(WaitForManual());

        OnRestart?.Invoke();
    }
    
    private IEnumerator WaitForManual()
    {
        yield return new WaitForSeconds(5);
        DOTweenModuleUI.DOFade(manualPanel.GetComponent<CanvasGroup>(),0,3);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void AddMoney(int money)
    {
        this.money += money;
        AddMoneyText.text = money.ToString("0") + "$";
        StartCoroutine(WaitForAddMoney());
    }
    
    private IEnumerator WaitForAddMoney()
    {
        var temp = AddMoneyText.GetComponent<CanvasGroup>();

        DOTweenModuleUI.DOFade(temp, 1, .5f);
        yield return new WaitForSeconds(.5f);
        DOTweenModuleUI.DOFade(temp, 0, .5f);
        yield return new WaitForSeconds(.5f);
        DOTweenModuleUI.DOFade(temp, 1, .5f);
        yield return new WaitForSeconds(.5f);
        DOTweenModuleUI.DOFade(temp, 0, .5f);
    }

    public void AddTime(float time)
    {
        timeRemaining += time;
        AddTimeText.text = time.ToString("0") + "s";
        StartCoroutine(WaitForAddTime());
    }
    
    private IEnumerator WaitForAddTime()
    {
        var temp = AddTimeText.GetComponent<CanvasGroup>();

        DOTweenModuleUI.DOFade(temp, 1, .5f);
        yield return new WaitForSeconds(.5f);
        DOTweenModuleUI.DOFade(temp, 0, .5f);
        yield return new WaitForSeconds(.5f);
        DOTweenModuleUI.DOFade(temp, 1, .5f);
        yield return new WaitForSeconds(.5f);
        DOTweenModuleUI.DOFade(temp, 0, .5f);
    }
    
    private void HandleTimer()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                GameEnded();
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
    
    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
