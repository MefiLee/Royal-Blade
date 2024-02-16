using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance // 외부에서 싱글톤을 가져올 때 사용할 변수
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<GameManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }
    private static GameManager m_instance; // 싱글톤이 할당될 static 변수


    /*  SkillCode
        1. 윈드슬래시
        2. 쉴드
        3. 무기 강화
        4. 날개  */
    public int SkillCode;
    
    public int Stage; // 스테이지 
    public int Round; // 라운드
    public int Combo; // 콤보
    public int FeverPercent; //피버 퍼센트

    public int Score; // 점수


    public int Gold; // 골드
    public int Damage; // 데미지

    public int Life;

    
    public Text Stagetext;
    public Text Roundtext;
    public Text Combotext;
    public Text FeverPercenttext;

    public Text Scoretext;

    
    public Text Goldtext;
    public Text Damagetext;


    public GameObject LifeIcon1;
    public GameObject LifeIcon2;
    public GameObject LifeIcon3;

    public GameObject GameOverPanel;

    public Button DefenceButton;

    public Player p;

    public bool Fly;
    
    public bool IsAttack;
    public bool IsDefenceSuccess;
    public bool IsGround;

    public GameObject MonsterPrefab;
    public GameObject MonsterSpawner;

    public GameObject Shield;
    public GameObject Weapon;
    public GameObject Lightning;

    
    public Text OverScoretext;


    void Awake()
    {

        Stage = 1;
        Round = 1;
        Combo = 0;
        FeverPercent = 0;

        Damage = 1;
        Gold = 0;
        Score = 0;
        
        Life = 3;

        SkillCode = SkillManager.SC; //로비씬에서 선택한 스킬 불러오기
        
        IsAttack = false;
        Fly = false;
    }


    void Start()
    {
        
        IsDefenceSuccess = false;
    }


    void Update()
    {

        Scoretext.text = Score.ToString();
        Stagetext.text = Stage.ToString();
        Roundtext.text = Round.ToString();
        
        
        Damagetext.text = Damage.ToString();
        Combotext.text = Combo.ToString();
        FeverPercenttext.text = FeverPercent.ToString();
        Goldtext.text = Gold.ToString();

        if(Combo == 100) // 콤보 100 달성 시 
        {
            Damage *= 2;
            Combo= 999;
            Invoke("ReloadCombo", 5f); // 5초후 콤보 0으로 설정
        }


        if(Life<3){
            LifeIcon3.SetActive(false);
        }
        if(Life<2){
            LifeIcon2.SetActive(false);
        }
        if(Life<1){
            LifeIcon1.SetActive(false);
            GameOverPanel.SetActive(true);

            
            OverScoretext.text = "Score : " + Score.ToString();
        }


        if(Round >=4){
            Stage ++;
            Round = 1;
        }

        if(IsDefenceSuccess == true){
            p.Down();
            IsDefenceSuccess = false;
        }

        if(Fly == true){ IsGround = true; }

    }

    public void OnUpgradeButton() //무기업그레이드버튼
    {
        if(Gold>0){
            Gold--;
            Damage++;
        }
    }

    public void OnJumpButton() // 점프 버튼
    {   
        if(IsGround == true){
            p.Jump();
        }
    }

    public void OnDefenceButton() // 방어 버튼
    {
        DefenceButton.interactable = false;
        p.Defence();
    }


    public void OnAttackButton() //공격버튼
    {
        p.Attack();
    }

    public void OnSkillButton()
    {
        if(SkillCode == 1)// 번개
        { 
            Lightning.SetActive(true);
            Invoke("ReloadLightning",0.1f);
        }
        else if(SkillCode == 2)// 쉴드
        { 
            Shield.SetActive(true);
            Invoke("ReloadShield",3f);
        }
        else if(SkillCode == 3)// 무기 강화
        { 
            Weapon.transform.localScale = new Vector3(Weapon.transform.localScale.x, 15f, Weapon.transform.localScale.z);
            
            Invoke("ReloadEnchant",3f);

        }
        else if(SkillCode == 4)// 날개
        { 
            Fly = true;
            Invoke("ReloadFly",5f);
        }
    }


    public void OnSummonMonsterButton(){ // 몬스터 소환
        GameObject instance = Instantiate(MonsterPrefab, MonsterSpawner.transform.position, Quaternion.identity);
    }

    public void OnExitButton() // 게임오버시 나가기
    {
        SceneManager.LoadScene("LobbyScene");

    }
    void ReloadCombo()
    {
        Combo = 0;
    }

    void ReloadFly(){
        Fly = false;
    }
    
    void ReloadShield(){
        Shield.SetActive(false);
    }

    void ReloadEnchant(){
        Weapon.transform.localScale = new Vector3(Weapon.transform.localScale.x, 1.5f, Weapon.transform.localScale.z);
    }

    void ReloadLightning(){
        Lightning.SetActive(false);
    }
}
