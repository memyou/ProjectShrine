using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //向いている方向
    public Transform orientation;

    public GameObject player;

    //振り返っていないかどうかを判定するレイの情報とbool
    Ray checkLookBack_ray;
    bool isLookBack;
    public LayerMask lookbackLayer;

    //rigidbody
    Rigidbody rb;

    //animator
    Animator animator;

    //audioSource
    AudioSource audioSource;

    //移動
    Vector3 velocity; //移動速度
    Vector3 moveInput; //移動入力値
    Vector3 moveDirection; //実際の移動距離

    public float walkSpeed = 1f; //歩行速度
    public float runSpeed = 3f; //走行速度

    //足音
    public AudioClip walkSE;
    public AudioClip runSE;


    //プレイヤーの状態
    public enum MoveState
    {
        idle,
        walking,
        running
    }

    MoveState state;

    void Start()
    {
        //コンポーネント取得
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        //回転を制限
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (!isLookBack)
        {
            CheckRay();

            KeyCheck();
            AnimCheck();
            PlayAudio();
        }
    }

    void FixedUpdate()
    {
        if (!isLookBack)
        {
            MovePlayer();
        }

    }

    //プレイヤー移動処理
    void MovePlayer()
    {
        switch (state)
        {
            case MoveState.walking:
                Move(walkSpeed);
                break;
            case MoveState.running:
                Move(runSpeed);
                break;
        }

    }

    //実際の移動処理
    void Move(float speed)
    {
        //入力値
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //向いている方向
        moveDirection = orientation.forward * moveInput.z + orientation.right * moveInput.x;

        //移動速度計算
        var clampedInput = Vector3.ClampMagnitude(moveDirection, 1f);

        velocity = clampedInput * speed;

        //移動速度制限
        velocity -= rb.velocity;
        velocity = new Vector3(Mathf.Clamp(velocity.x, -speed, speed), 0f, Mathf.Clamp(velocity.z, -speed, speed));

        //プレイヤー移動処理
        rb.AddForce(rb.mass * velocity / Time.fixedDeltaTime, ForceMode.Force);

        //プレイヤーの視線に合わせて体の向きを変更
        transform.rotation = orientation.rotation;
    }

    void KeyCheck()
    { //入力におけるプレイヤーの移動アクションの遷移
        if (Input.GetButton("Horizontal") & Input.GetKey(KeyCode.Space)
            | Input.GetButton("Vertical") & Input.GetKey(KeyCode.Space))
        {
            //state=running
            state = MoveState.running;
        }
        else if (Input.GetButton("Horizontal") | Input.GetButton("Vertical"))
        {
            //state=walking
            state = MoveState.walking;
        }
        else
        {
            //state=idle
            state = MoveState.idle;
        }
    }

    void CheckRay()
    {
        //レイ設定
        checkLookBack_ray = new Ray(orientation.transform.position, orientation.transform.forward);

        //振り返っていないかどうかを判断する
        if (Physics.Raycast(checkLookBack_ray, 2f, lookbackLayer))
        {
            Debug.Log("振り返った");
            isLookBack = true;
        }
    }

    void PlayAudio()
    {
        //state=idleの時は足音停止
        if (state == MoveState.idle) { audioSource.Stop(); }

        //何らかのキーを押下した時、stateに応じて足音を鳴らす
        if (Input.anyKeyDown)
        {
            audioSource.Stop();

            switch (state)
            {
                case MoveState.walking:
                    audioSource.PlayOneShot(walkSE);
                    break;
                case MoveState.running:
                    audioSource.PlayOneShot(runSE);
                    break;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space)) { audioSource.Stop(); audioSource.PlayOneShot(walkSE); }
    }

    //アニメーション遷移
    void AnimCheck()
    {
        switch (state)
        {
            case MoveState.walking:
                animator.SetBool("walk", true);
                animator.SetBool("run", false);
                break;
            case MoveState.running:
                animator.SetBool("walk", false);
                animator.SetBool("run", true);
                break;
            case MoveState.idle:
                animator.SetBool("walk", false);
                animator.SetBool("run", false);
                break;
        }
    }

    //playerの行動状況を取得
    public MoveState GetMoveState() { return state; }

    //振り返り判定
    public bool GetIsLookBack() { return isLookBack; }
    public void SetIsLookBack(bool isLookBack) { this.isLookBack = isLookBack; }
}
