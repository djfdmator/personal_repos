using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GoogleConnect : MonoSingleton<GoogleConnect>
{
    //DB
    public string saveData;

    //로그인 UI
    public GameObject LoginUI;

    //디버그용 label
    private string mStatusText = "Ready.";

    void Start()
    {
#if UNITY_ANDROID

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames() //구글에 저장된 게임 기능을 사용하기 위해 추가
            .Build();

        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.DebugLogEnabled = true;

        PlayGamesPlatform.Activate(); //iOS에서 호출되면 api가 제대로 작동하지 않는다. 고로 #if~ #endif 문으로 구별 지어주자

#elif UNITY_IOS
 
        GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
 
#endif
    }

    #region Log In & Out
    //로그인 함수
    public void SignIn()
    {
        //Social.localUser를 하면 Android 든 IOS 든 알아서 문제없이 작동한다.
        //그러나 구분해야 하는 경우가 있다면
        //Android는 PlayGamesPlatform 사용
        //iOS는 GameCenterPlatform 사용
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                // to do ...
                // 로그인 성공 처리
                mStatusText = "Success";
            }
            else
            {
                // to do ...
                // 로그인 실패 처리
                mStatusText = "Failed";
            }
        });
    }

    public void SignOut()
    {
        PlayGamesPlatform.Instance.SignOut(); 
    }
    #endregion

    //Save
    #region Save

    public void SaveToCloud(string _data)
    {
        StartCoroutine(Save(_data));
    }

    IEnumerator Save(string _data)
    {
        while(!Social.localUser.authenticated)
        {
            SignIn();
            yield return new WaitForSeconds(2f);
        }

        string id = Social.localUser.id;
        string fileName = string.Format("{0}_TEST", id);
        saveData = _data;

        OpenFileSaveORLoad(fileName, true);
    }

    void OnSavedGameOpenedToSave(SavedGameRequestStatus _status, ISavedGameMetadata _data)
    {
        if(_status == SavedGameRequestStatus.Success)
        {
            mStatusText = "Success";
            byte[] b = Encoding.UTF8.GetBytes(string.Format(saveData));
            SaveGame(_data, b, DateTime.Now.TimeOfDay);
        }
        else
        {
            //Fail
            mStatusText = "Fail";
        }
    }

    void SaveGame(ISavedGameMetadata _data, byte[] _byte, TimeSpan _playTime)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();

        builder = builder.WithUpdatedPlayedTime(_playTime).WithUpdatedDescription("Saved at " + DateTime.Now);

        SavedGameMetadataUpdate metadataUpdate = builder.Build();
        savedGameClient.CommitUpdate(_data, metadataUpdate, _byte, OnSavedGameWritten);
    }

    void OnSavedGameWritten(SavedGameRequestStatus _status, ISavedGameMetadata _data)
    {
        if(_status == SavedGameRequestStatus.Success)
        {
            //Save Complete
            mStatusText = "Save Complete";
        }
        else
        {
            //Save Failed
            mStatusText = "Save Failed";
        }
    }

    #endregion

    //Load
    #region Load
    
    public void LoadFromCloud()
    {
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        while(!Social.localUser.authenticated)
        {
            SignIn();
            yield return new WaitForSeconds(2f);
        }

        string id = Social.localUser.id;
        string fileName = string.Format("{0}_TEST", id);

        OpenFileSaveORLoad(fileName, false);
    }

    void OnSavedGameOpenedToRead(SavedGameRequestStatus _status, ISavedGameMetadata _data)
    {
        if(_status == SavedGameRequestStatus.Success)
        {
            mStatusText = "Success";
            LoadGameData(_data);
        }
        else
        {
            //Fail
            mStatusText = "Fail";
        }
    }

    void LoadGameData(ISavedGameMetadata _data)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(_data, OnSavedGameDataRead);
    }

    void OnSavedGameDataRead(SavedGameRequestStatus _status, byte[] _byte)
    {
        if(_status == SavedGameRequestStatus.Success)
        {
            mStatusText = "Load Success";
            string data = Encoding.Default.GetString(_byte);
            //데이터 세팅
        }
        else
        {
            //Load Fail
            mStatusText = "Load Fail";
        }
    }

    #endregion

    public void OpenFileSaveORLoad(string _fileName, bool isSaved)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        if(isSaved)
        {
            //Save
            savedGameClient.OpenWithAutomaticConflictResolution(_fileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpenedToSave);
        }
        else
        {
            //Load
            savedGameClient.OpenWithAutomaticConflictResolution(_fileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpenedToRead);
        }
    }

    private void OnGUI()
    {

        GUI.Label(new Rect(20, 20, Screen.width, Screen.height * 0.25f),
          mStatusText);

        //자동 로그인 됬는지 확인
        if (Social.localUser.authenticated)
        {
            //됨
            Destroy(LoginUI);
            mStatusText = "Success";
            //TODO : 데이터 로드
            //TODO : 게임 스타트 붙이기
        }
        else
        {
            //안됨
            LoginUI.SetActive(true);
            mStatusText = "Failed";
        }
    }


    #region leaderboard
    //리더보드 컨트롤
    public void UnlockAchievement(int score)
    {
        if (score >= 100)
        {
#if UNITY_ANDROID
            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.leaderboard_test, 100f, null);
#elif UNITY_IOS
            Social.ReportProgress("Score_100", 100f, null);
#endif
        }
    }

    public void ShowAchievementUI()
    {
        // Sign In 이 되어있지 않은 상태라면
        // Sign In 후 업적 UI 표시 요청할 것
        if (Social.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    // Sign In 성공
                    // 바로 업적 UI 표시 요청
                    Social.ShowAchievementsUI();
                    return;
                }
                else
                {
                    // Sign In 실패 처리
                    return;
                }
            });
        }

        Social.ShowAchievementsUI();
    }
    #endregion

    void Update()
    {

    }
}
