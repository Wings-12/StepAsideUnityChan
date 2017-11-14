﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour {
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;
    //Unityちゃんを移動させるコンポーネントを入れる（追加）
    private Rigidbody myRigidbody;
    //前進するための力（追加）
    private float forwardForce = 800.0f;

    //左右に移動するための力（追加）
    private float turnForce = 500.0f;

    //左右の移動できる範囲（追加）
    private float movableRange = 3.4f;

    // Use this for initialization
    void Start () {

        //Animatorコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //走るアニメーションを開始
        this.myAnimator.SetFloat("Speed", 1);

        //Rigidbodyコンポーネントを取得（追加）
        this.myRigidbody = GetComponent<Rigidbody>();
}

// Update is called once per frame
void Update () {

        //Unityちゃんに前方向の力を加える（追加）
        this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);

        //Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる（追加）

    }
}
