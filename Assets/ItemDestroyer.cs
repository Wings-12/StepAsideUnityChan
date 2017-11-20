using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroyer : MonoBehaviour
{

    //課題１内容：コイン・コーン・車（以下アイテムと呼ぶ）を生成するオブジェクトをUnityちゃんがを
    //通り過ぎて画面外に行ったら破棄する（課題１）
    //unityChanのオブジェクトにアクセスするための変数
    private GameObject unityChan;


    // Use this for initialization
    void Start ()
    {
        //課題１内容：コイン・コーン・車（以下アイテムと呼ぶ）を生成するオブジェクト（課題１）
        // シーン中のunitychanオブジェクトを取得（課題１）
        this.unityChan = GameObject.Find("unitychan");

    }

    // Update is called once per frame
    void Update ()
    {
        //課題１内容：コイン・コーン・車（以下アイテムと呼ぶ）を生成するオブジェクト（課題１）
        //アイテムが画面外に出た場合（課題１）コメント：ゲームを再生したときにアイテムが画面外で破棄されているかわからない
        //if (this.transform.position.z < this.visiblePosZ)
        //上記コメントのため、次はUnityちゃんを通り過ぎたときにアイテムをが破棄されるようにする
        if (this.transform.position.z < this.unityChan.GetComponent<Transform>().position.z -6.5f)
        {
            //アイテムを破棄する（課題１）
            //if (this.gameObject.tag == "ItemGeneratorTag")
            //{
            //（Unityちゃんが）通過したアイテムのオブジェクトを破棄（課題１）
            Destroy(this.gameObject);
            //}
        }

    }
}
