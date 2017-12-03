using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //（追加）

public class UnityChanController : MonoBehaviour {
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;
    //Unityちゃんを移動させるコンポーネントを入れる（追加）
    private Rigidbody myRigidbody;
   
    //前進するための力（追加）
    private float forwardForce = 800.0f;
    //左右に移動するための力（追加）
    private float turnForce = 500.0f;
    //ジャンプするための力（追加）
    private float upForce = 600.0f;

    //左右の移動できる範囲（追加）
    private float movableRange = 3.4f;

    //動きを減速させる係数（追加）
    private float coefficient = 0.95f;

    //ゲーム終了の判定（追加）
    private bool isEnd = false;
    //ゲーム終了時に表示するテキスト（追加）
    private GameObject stateText;

    //スコアを表示するテキスト（追加）
    private GameObject scoreText;
    //得点（追加）
    private int score = 0;

    //左ボタン押下の判定（追加）
    private bool isLButtonDown = false;
    //右ボタン押下の判定（追加）
    private bool isRButtonDown = false;

    // <summary>無敵モード</summary> 　　←使わない
    //[SerializeField] bool m_godMode;

    // Use this for initialization
    void Start () {

        //Animatorコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //走るアニメーションを開始
        this.myAnimator.SetFloat("Speed", 1);

        //Rigidbodyコンポーネントを取得（追加）
        this.myRigidbody = GetComponent<Rigidbody>();

        //シーン中のstateTextオブジェクトを取得（追加）
        this.stateText = GameObject.Find("GameResultText");

        //シーン中のscoreTextオブジェクトを取得（追加）
        this.scoreText = GameObject.Find("ScoreText");

        //現在再生されているアニメーション情報を取得(animator.GetCurrentAnimatorStateInfo(0)の使い方が違う)
        //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);


}

// Update is called once per frame
void Update () {

        //ゲーム終了ならUnityちゃんの動きを減衰する（追加）
        if (this.isEnd)
        {
            this.forwardForce *= this.coefficient;
            this.turnForce *= this.coefficient;
            this.upForce *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;

            // クリックされたらシーンをロードする（追加）
            if (Input.GetMouseButtonDown(0))
                {
                    //GameSceneを読み込む（追加）
                    SceneManager.LoadScene("GameScene");
                }

        }

        //Unityちゃんに前方向の力を加える（追加）
        this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);

        //Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる（追加）
        if ((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x)
        {
            //左に移動（追加）
            this.myRigidbody.AddForce(-this.turnForce, 0, 0);
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange)
        {
            //右に移動（追加）
            this.myRigidbody.AddForce(this.turnForce, 0, 0);
        }

        //Jumpステートの場合はJumpにfalseをセットする（追加）
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        //ジャンプしていない時にスペースが押されたらジャンプする（追加）
        if (Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f)
        {
            //ジャンプアニメを再生（追加）
            this.myAnimator.SetBool("Jump", true);
            //Unityちゃんに上方向の力を加える（追加）
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }

        //Hikickしていないときの設定
        if(this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hikick"))
            {
            this.myAnimator.SetBool("Hikick", false);
            }
        //dキーの入力判定(オリジナル)
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.myAnimator.SetBool("Hikick", true);
            //入力があった時にアニメーション再生
            //this.myAnimator.SetTrigger("HikickTrigger");
            this.gameObject.transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);

            //攻撃判定生成
           
        }

    }

        //トリガーモードで他のオブジェクトと接触した場合の処理（追加）
        void OnTriggerEnter(Collider other)
        {
        //障害物に衝突した場合（追加）
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
            {
        //OnTriggerEnter関数内でアニメーションの状態を見て、Hikickの状態だったらDestroy
        //（もっと簡単な方法があるのでそちらを採用する）
        //if (animator.GetCurrentAnimationStateInfo(0))
        //    {

        //    }

            if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hikick"))
                {
                    GetComponent<ParticleSystem>().Play();
                    Destroy(other.gameObject);
                }
            else
                {
                    this.isEnd = true;
                    //stateTextにGAME OVERを表示（追加）
                    this.stateText.GetComponent<Text>().text = "GAME OVER";

                    // クリックされたらシーンをロードする（追加）
                    //if (Input.GetMouseButtonDown(0))
                    //{
                    //    //GameSceneを読み込む（追加）
                    //    SceneManager.LoadScene("GameScene");
                    //}

                }
        }
              
                //else
                //{
                //    this.isEnd = true;

                //    //stateTextにGAME OVERを表示（追加）
                //    this.stateText.GetComponent<Text>().text = "Game Over";
                //}

            //ゴール地点に到達した場合（追加）
            if (other.gameObject.tag == "GoalTag")
                {
                    this.isEnd = true;

                //stateTextにGAME CLEARを表示（追加）
                this.stateText.GetComponent<Text>().text = "CLEAR!!";
            }

            //コインに衝突した場合（追加）
            if (other.gameObject.tag == "CoinTag")
            {

                //パーティクルを再生（追加）
                GetComponent<ParticleSystem>().Play();

                //接触したコインのオブジェクトを破棄（追加）
                Destroy(other.gameObject);

                // スコアを加算(追加)
                this.score += 10;
                //ScoreText獲得した点数を表示(追加)
                this.scoreText.GetComponent<Text>().text = "Score" + score + "pt";
            }

        }

    //ジャンプボタンを押した場合の処理（追加）
    public void GetMyJumpButtonDown()
    {
        if (this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }
    }
    //左ボタンを押し続けた場合の処理（追加）
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }
    //左ボタンを離した場合の処理（追加）
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }
    //右ボタンを押し続けた場合の処理（追加）
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }
    //右ボタンを離した場合の処理（追加）
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }
}
