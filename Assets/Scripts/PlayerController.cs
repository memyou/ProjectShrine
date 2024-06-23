using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //向いている方向
    public Transform orientation;

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
    enum State
    {
        idle,
        walking,
        running
    }

    State state;

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
        KeyCheck();
        AnimCheck();
        PlayAudio();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    //プレイヤー移動処理
    void MovePlayer()
    {
        switch (state)
        {
            case State.walking:
                Move(walkSpeed);
                break;
            case State.running:
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
            state = State.running;
        }
        else if (Input.GetButton("Horizontal") | Input.GetButton("Vertical"))
        {
            //state=walking
            state = State.walking;
        }
        else
        {
            //state=idle
            state = State.idle;
        }
    }

    void PlayAudio()
    {
        //何らかのキーを押下した時、stateに応じて足音を鳴らす
        if (Input.anyKeyDown)
        {
            switch (state)
            {
                case State.walking:
                    audioSource.PlayOneShot(walkSE);
                    break;
                case State.running:
                    audioSource.PlayOneShot(runSE);
                    break;
            }
        }

        //state=idleの時は足音停止
        if (state == State.idle) { audioSource.Stop(); }

    }

    //アニメーション遷移
    void AnimCheck()
    {
        switch (state)
        {
            case State.walking:
                animator.SetBool("walk", true);
                animator.SetBool("run", false);
                break;
            case State.running:
                animator.SetBool("walk", false);
                animator.SetBool("run", true);
                break;
            case State.idle:
                animator.SetBool("walk", false);
                animator.SetBool("run", false);
                break;
        }
    }
}
