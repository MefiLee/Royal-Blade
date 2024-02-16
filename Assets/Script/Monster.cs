using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private int HP; // 몬스터 체력 변수

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        HP = GameManager.instance.Stage * 5 + GameManager.instance.Round; // 임의로 정한 체력 산정 식, 스테이지와 라운드를 바탕으로 계산. 갈수록 높아짐
    }

    // Update is called once per frame
    void Update()
    {
        if(HP<=0)
        {
            DestroyImmediate(gameObject);
        }
    }

   


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack")) //공격에 닿을 시 처리
        {
            if(GameManager.instance.IsAttack == true){
                HP-=GameManager.instance.Damage;
                GameManager.instance.FeverPercent++;
                GameManager.instance.IsAttack = false;
            }

        }

        if (other.CompareTag("Shield")) //쉴드스킬에 닿을 시 데미지 주기(임시로 삭제처리)
        {
            HP-=9999;
        }
        if (other.CompareTag("Lightning")) //번개스킬에 닿을 시 데미지 주기
        {
            HP-=9999;
        }

        if (other.CompareTag("Defence")) // 방어에 닿을 시 처리
        {
            rigidbody.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            GameManager.instance.IsDefenceSuccess = true;
            

        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Defence")) //방어에 닿아있을 시 추가처리
        {
            rigidbody.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            GameManager.instance.IsDefenceSuccess = true;
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.collider.gameObject.CompareTag("Player")) //플레이어 방어상태가 아닐 때 닿았을 때 처리
        {
            if(GameManager.instance.IsGround == true && GameManager.instance.p.IsDefence == false){
                GameManager.instance.Life--;
                
                rigidbody.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            }
        }
    }





    private void OnDestroy() // 처치 시 처리
    {
        GameManager.instance.Gold +=10;
        GameManager.instance.Score += 100;
        GameManager.instance.Combo ++;
        GameManager.instance.Round ++;

    }

}
