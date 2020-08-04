using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;

    public static GameManager Instance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = new GameManager();
            }
            return m_Instance;
        }
    }

    public enum SceneStatus { NONE, TITLE, PLAY }
    public GameObject TitleScene;
    public GameObject PlayScene;

    public SceneStatus curScene
    {
        get { return m_curScene; }
        set
        {
            if (m_curScene != value)
            {
                m_curScene = value;
                TitleScene.SetActive(false);
                PlayScene.SetActive(false);

                switch (m_curScene)
                {
                    case SceneStatus.TITLE:
                        TitleScene.SetActive(true);
                        break;
                    case SceneStatus.PLAY:
                        PlayScene.SetActive(true);
                        break;
                    default:
                        TitleScene.SetActive(true);
                        break;
                }
            }
        }
    }
    private SceneStatus m_curScene = SceneStatus.NONE;

    #region Database
    //플레이 중인 날짜
    public int playDay;
    public int curPlayDay;

    public string[] purposeList;
    public string[] addressList;
    public string[] manNames;
    public string[] wemenNames;
    #endregion

    private void Awake()
    {
        m_Instance = this;

        TitleScene = transform.Find("Title").gameObject;
        PlayScene = transform.Find("Play").gameObject;
    }

    private void Start()
    {
        curScene = SceneStatus.TITLE;
    }

    public void GotoScene(SceneStatus sceneStatus)
    {
        curScene = sceneStatus;
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
