using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 移動タイプの定義
    public enum MoveType
    {
        Straight, // 直線移動
        Spiral    // 螺旋移動
    }

    [Header("タイプ設定 (Type)")]
    [Tooltip("移動タイプを選択")]
    [SerializeField] private MoveType moveType;

    [Header("タイプ設定 (Type)")]
    [Tooltip("飛行タイプかどうか (True: 重力無効, False: 重力有効)")]
    [SerializeField] private bool isFlying;

    [Header("移動設定 (Movement)")]
    [Tooltip("最小移動速度")]
    [SerializeField] private float minSpeed = 3f;
    [Tooltip("最大移動速度")]
    [SerializeField] private float maxSpeed = 6f;

    [Header("回転設定 (Rotation)")]
    [Tooltip("回転速度（螺旋移動用）")]
    [SerializeField] private float rotationSpeed = 100f;
        
    private Transform target;

    // 実際の移動速度（内部計算用）
    private float currentSpeed;
    private float factor = 0.5f;

    // 回転方向（1: 時計回り, -1: 反時計回り）
    private float rotateDir;

    private Rigidbody rb;

    void Start()
    {
        // Rigidbodyコンポーネントを取得
        rb = GetComponent<Rigidbody>();

        // Rigidbodyがある場合、タイプに合わせて重力を設定
        if (rb != null)
        {
            // 飛行タイプなら重力OFF, 地上タイプなら重力ON
            rb.useGravity = !isFlying;
        }

        // "Player"タグを持つオブジェクトを探してターゲットに設定
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }

        // 開始時にランダムな速度を決定
        currentSpeed = Random.Range(minSpeed, maxSpeed);
        // 開始時にランダムな回転方向を決定 (50%確率で 1 または -1)
        rotateDir = Random.value < 0.5f ? 1f : -1f;
    }

    void Update()
    {
        // ターゲットがいる場合のみ移動
        if (target != null)
        {
            // タイプに応じて移動関数を切り替え
            switch (moveType)
            {
                case MoveType.Straight:
                    MoveStraight();
                    break;
                case MoveType.Spiral:
                    MoveSpiral();
                    break;
            }
        }
    }

    // 直線移動
    void MoveStraight()
    {
        float step = currentSpeed * factor * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    // 螺旋移動
    void MoveSpiral()
    {
        // 1. ターゲットに近づく
        float step = currentSpeed * factor * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        // 2. ターゲットの周りを回る
        // rotateDirが 1 なら右回り、-1 なら左回り
        transform.RotateAround(target.position, Vector3.up, rotationSpeed * rotateDir * Time.deltaTime);

        // 3. 常にターゲットの方を向く
        transform.LookAt(target);
    }
}