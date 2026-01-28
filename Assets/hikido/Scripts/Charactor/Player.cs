using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : CharactorBase
{
    [SerializeField] PlayerHP _plaerHP;
    [SerializeField] GameObject _camera;
    [SerializeField] Rigidbody _rb;
    [SerializeField] private float sensitivity = 30;
    [SerializeField] private float clampAngle = 80f;
    private float xRotation = 0f;
    private float yRotation = 0f;   

    protected override void Start() 
    {
        base.Start();
        _plaerHP.GetComponent<PlayerHP>();   
        _rb.GetComponent<Rigidbody>();
        //_playertransform = GetComponent<Transform>();
    }

    private void Update()
    {
        //常に更新
        currentmoveSpeed = gameManegerSO.statusMoveSpeed;
        currentmoveJump = gameManegerSO.statusMoveJump;

        //TODO：テストコードで後で削除
        if (Input.GetKey("p")) { TestTeakeDamage(); }
    }

    private void FixedUpdate()
    {
        //攻撃
        Attack();

        //移動
        HandleMove();

        //ジャンプ
        HandleJump();

        //マウスでのカメラ
        CameraControl();
    }

    //カメラコントロール
    private void CameraControl()
    {
        float mx = UnityEngine.Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float my = UnityEngine.Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        yRotation += mx;
        xRotation -= my;
        xRotation = Mathf.Clamp(xRotation, -clampAngle, clampAngle);

        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        _camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    //playerの移動
    private void HandleMove() 
    {
        if (UnityEngine.Input.GetKey("w"))
        {
            transform.position += transform.forward * currentmoveSpeed * Time.deltaTime; //前移動
            animator.SetBool("Walk", true);                                                              
        }
        else { animator.SetBool("Walk", false); }
        if (UnityEngine.Input.GetKey("s"))
        {
            transform.position -= transform.forward * currentmoveSpeed * Time.deltaTime; //後ろ移動
            animator.SetBool("back", true);
        }
        else { animator.SetBool("back", false); }
        if (UnityEngine.Input.GetKey("a"))
        {
            transform.position -= transform.right * currentmoveSpeed * Time.deltaTime;   //左移動
            animator.SetBool("Left",true);
        }
        else { animator.SetBool("Left", false); }
        if (UnityEngine.Input.GetKey("d"))
        {
            transform.position += transform.right * currentmoveSpeed * Time.deltaTime;   //右移動
            animator.SetBool("Right", true);
        }
        else {　animator.SetBool("Right", false); }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown("space"))
        {
            _rb.AddForce(transform.up * currentmoveJump);
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
    }

    //攻撃
    private void Attack() 
    {
        const string AttackParam = "Attack";
        if (Input.GetMouseButtonDown(0)) 
        {
            animator.SetTrigger(AttackParam);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy") 
        {
            //TODO:enemyのダメージを受け取る
            int _hitDamage = (int)enemyDamage;
            StartCoroutine(_plaerHP.HitDamage(_hitDamage));
        }
    }

    //testcode
    private void TestTeakeDamage() 
    {
        int _hitDamage = (int)enemyDamage;
        StartCoroutine(_plaerHP.HitDamage(_hitDamage));
    }

}
