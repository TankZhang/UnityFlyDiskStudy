using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{

    //游戏状态枚举
    public enum GameState
    {
        START,
        GAME,
        END
    }
    //当前游戏状态
    private GameState gameState;
    private GameObject m_startUI;
    private GameObject m_gameUI;
    private GameObject m_endUI;
    private GUIText gUIScore;
    private GUIText gUITime;
    private GUIText gUITotalScore;
    private AudioSource audioSource;
    private Weapon weapon;
    private FlyDiskManagement flyDiskManagement;
    private int score = 0;
    private const int timeARound = 20;
    private float time = timeARound;
    private bool timeFlag = false;

    void Start()
    {
        m_startUI = GameObject.Find("StartUI");
        m_gameUI = GameObject.Find("GameUI");
        m_endUI = GameObject.Find("EndUI");
        gUIScore = GameObject.Find("GameScore").GetComponent<GUIText>();
        gUITime = GameObject.Find("GameTime").GetComponent<GUIText>();
        gUITotalScore = GameObject.Find("GameTotalScore").GetComponent<GUIText>();
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        weapon = GameObject.Find("Weapon").GetComponent<Weapon>();
        flyDiskManagement = GameObject.Find("FlyDiskParent").GetComponent<FlyDiskManagement>();
        ChangeGameState(GameState.START);
    }
    public void StartTime()
    {
        timeFlag = true;
        time = timeARound;
    }

    void Update()
    {
        if (timeFlag)
        {
            time -= Time.deltaTime;
            gUITime.text = "时间：" + Mathf.Round(time) + "秒";
            if (time <= 0)
            {
                timeFlag = false;
                ChangeGameState(GameState.END);
            }
        }
    }
    public void AddScore()
    {
        score++;
        gUIScore.text = "分数：" + score + "分";
    }

    public void ChangeGameState(GameState state)
    {
        gameState = state;
        m_startUI.SetActive(false);
        m_gameUI.SetActive(false);
        m_endUI.SetActive(false);
        switch (state)
        {
            case GameState.START:
                m_startUI.SetActive(true);
                audioSource.Stop();
                weapon.ChangeEnable(false);
                break;
            case GameState.GAME:
                m_gameUI.SetActive(true);
                audioSource.Play();
                weapon.ChangeEnable(true);
                StartTime();
                flyDiskManagement.StartCreateFlydisk();
                break;
            case GameState.END:
                m_endUI.SetActive(true);
                audioSource.Stop();
                weapon.ChangeEnable(false);
                flyDiskManagement.StopCreateFlydisk();
                gUITotalScore.text = "总分数：" + score + "分";
                score = 0;
        		gUIScore.text = "分数：" + score + "分";
				flyDiskManagement.RemoveFlydisk();
                break;
        }
    }
}
