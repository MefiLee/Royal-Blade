using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyButton : MonoBehaviour
{

    public void OnExitButton() //종료버튼
    {
         Application.Quit();
    }

    public void OnSetSkillButton(int a) // 스킬코드를 설정 후 메인씬으로 진입
    {
        /*  SkillCode
            1. 번개
            2. 쉴드
            3. 무기 강화
            4. 날개 
        */
        SkillManager.SC = a;
        SceneManager.LoadScene("MainScene");
    }
}


