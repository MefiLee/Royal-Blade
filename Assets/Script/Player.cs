using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public int JumpPower;

    public bool IsGround;
    public bool IsDefence;

    public GameObject DefenceObject;
    public GameObject AttackObject;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        IsDefence = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.IsGround && GameManager.instance.IsDefenceSuccess == true){
            GameManager.instance.Combo = 0;
        }
        
    }

    public void Jump()
    {
        GameManager.instance.IsGround = false;
        rigidbody.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
    }


    public void Defence()
    {
        DefenceObject.SetActive(true);
        IsDefence = true;

        Invoke("ReloadDefence",0.5f);
    }

    public void Attack()
    {
        AttackObject.SetActive(true);
        GameManager.instance.IsAttack = true;

        Invoke("ReloadAttack",0.1f);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Ground"))
        {
            GameManager.instance.IsGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Ground"))
        {
            GameManager.instance.IsGround = false;
        }
    }

    public void Waiting(){}

    public void ReloadAttack()
    {
        AttackObject.SetActive(false);
        GameManager.instance.IsAttack = false;

    }


    public void ReloadDefence()
    {
        DefenceObject.SetActive(false);
        IsDefence = false;
        GameManager.instance.DefenceButton.interactable = true;
    }

    public void Down()
    {
        rigidbody.AddForce(Vector2.down * JumpPower, ForceMode2D.Impulse);
    }

    public void Attacked(){
        
    }
}
