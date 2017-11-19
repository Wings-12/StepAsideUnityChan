using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //cornPrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    private int startPros = -160;
    //ゴール地点
    private int goalPros = 120;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;

    //※ユニティちゃんが通り過ぎて画面外に出たアイテムを直ちに破棄してください（課題１）
    //アイテムが見える可能性のあるz軸の最大値（課題１）
    //private float visiblePosZ = -6.5f;(要らないと思う)


    //課題１内容：コイン・コーン・車（以下アイテムと呼ぶ）を生成するオブジェクト（課題１）
    //private GameObject ItemGeneratorObject; (要らないと思う)
    private GameObject unityChan;

    // Use this for initialization
    void Start()
    {
        //一定の距離ごとにアイテムを生成
        for (int i = startPros; i < goalPros; i += 15)
        {
            //どのアイテムを出すのかをランダムに設定
            int num = Random.Range(0, 10);
            if (num <= 1)
            {
                //コーンをx軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    GameObject cone = Instantiate(conePrefab) as GameObject;
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
                }
            }
            else
            {
                //レーンごとにアイテムを生成
                for (int j = -1; j < 2; j++)
                {
                    //アイテムの種類を決める
                    int item = Random.Range(1, 11);
                    //アイテムを置くZ座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-5, 6);
                    //60%コイン配置:30%車配置:10%何もなし
                    if (1 <= item && item <= 6)
                    {
                        //コインを生成
                        GameObject coin = Instantiate(coinPrefab) as GameObject;
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
                    }
                    else if (7 <= item && item <= 9)
                    {
                        //車を生成
                        GameObject car = Instantiate(carPrefab) as GameObject;
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
                    }
                }
            }
        }
        //課題１内容：コイン・コーン・車（以下アイテムと呼ぶ）を生成するオブジェクト（課題１）
        // シーン中のunitychanオブジェクトを取得（課題１）
     this.unityChan = GameObject.Find("unitychan");


        // シーン中のItemGeneratorオブジェクトを取得（課題１）
        //this.ItemGeneratorObject = GameObject.Find("ItemGenerator"); (使うかどうか保留中)
        //(↑コードいれても81行目付近のif (this.transform.position.z < other.gameObject.)のotherに赤波がでる)

    }

    // Update is called once per frame
    void Update()
    {
        //課題１内容：コイン・コーン・車（以下アイテムと呼ぶ）を生成するオブジェクト（課題１）
        //アイテムが画面外に出た場合（課題１）コメント：ゲームを再生したときにアイテムが画面外で破棄されているかわからない
        //if (this.transform.position.z < this.visiblePosZ)
        //上記コメントのため、次はUnityちゃんを通り過ぎたときにアイテムをが破棄されるようにする
        if (this.transform.position.z < this.unityChan.GetComponent<Transform>().position.z)
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
