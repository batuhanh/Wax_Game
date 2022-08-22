using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Basic LevelManager CLass that have some levelstart levelwin levelfailed type methods 
public class LevelManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private GameObject[] levels;
    [SerializeField] private int currentLevel;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private Animator armAnim;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        
        currentLevel = PlayerPrefs.GetInt("level", 0);
        LoadCurrentlevel();
        eventManager.CallLevelStartedEvent();

    }
    private void LoadCurrentlevel() //Loading current level according to data from Playerprefs
    {
        foreach (GameObject l in levels)
        {
            l.SetActive(false);
        }
        levels[currentLevel % levels.Length].SetActive(true);
        levelText.text = "Lv. " + currentLevel.ToString();
    }
    private void LevelCompleted()//Increasing currentlevel and opening Win Menu
    {
        currentLevel++;
        PlayerPrefs.SetInt("level", currentLevel);
        winMenu.SetActive(true);
        armAnim.SetTrigger("Win");
    }
    public void RestartScene() // Restarting scene to load new or existing level
    {
        SceneManager.LoadScene(0);
    }
    private void LevelFailed()
    {
     
    }
    void OnEnable()
    {
        EventManager.myLevelCompleted += LevelCompleted;
        EventManager.myLevelFailed += LevelFailed;

    }
    void OnDisable()
    {
        EventManager.myLevelCompleted -= LevelCompleted;
        EventManager.myLevelFailed -= LevelFailed;

    }
}
