using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float speed = 750;
    public int currentOwner;
    public enum OwnerName : int { NoOne = 0, Player = 1, Enemy = 2 }
    public bool isAtackingEnemy, isAtackingPlayer;

    public GameObject vfxPlayer, vfxEnemy;

    [SerializeField] private GameObject player, enemy;

    public Vector3 Direction = new Vector3(1, 0, 1);
    [SerializeField] private Material normalMt, enemyMt, playerMt;
    Renderer renderer;
    private Vector3 unitVector, velocity;
    private Rigidbody rigidBody;
    float sfxTimer;
    GameManager gameManager;
    public bool isPermittedControlPlayer, isPermittedControlEnemy;
    void Start()
    {
        Direction = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
        renderer = GetComponent<Renderer>();
        normalMt = renderer.material;
        currentOwner = (int)OwnerName.NoOne;
        gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(Direction.normalized * speed * Time.deltaTime, ForceMode.Impulse);
        if (enemy == null) enemy = GameObject.FindGameObjectWithTag("Enemy").gameObject;
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        if (gameManager.isGameEnd)
        {
            rigidBody.velocity = Vector3.zero;
            return;
        }

        if (isPermittedControlPlayer)
        {
            rigidBody.velocity = Vector3.zero;
            return;
        }

        if (isAtackingEnemy)
        {
            InvokeSFXinUpdate();
            vfxPlayer.SetActive(true);
            velocity = unitVector * 35;
            rigidBody.velocity = velocity;
        }
        else if (isAtackingPlayer)
        {
            InvokeSFXinUpdate();
            vfxEnemy.SetActive(true);
            velocity = unitVector * 35;
            rigidBody.velocity = velocity;
        }
        else
        {
            rigidBody.velocity += Vector3.up * 2f * Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        rigidBody.velocity = rigidBody.velocity.normalized * speed * Time.deltaTime;
        if (collision.gameObject.tag == "wall")
        {
            isAtackingEnemy = false;
            vfxPlayer.SetActive(false);
            isAtackingPlayer = false;
            vfxEnemy.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (currentOwner == (int)OwnerName.Player)
            {
                isAtackingEnemy = true;
                unitVector = gameManager.CalcurateUnitVector(enemy.transform.position, this.transform.position);
            }
            else if (isAtackingPlayer == false)
            {
                ChangeOwnerPlayer(true, false);
            }
            else
            {
                Debug.Log("Enemy kill");
                gameManager.Damage(true);
            }
        }
        else if (other.gameObject.tag == "Enemy")
        {
            if (currentOwner == (int)OwnerName.Enemy)
            {
                isAtackingPlayer = true;
                unitVector = gameManager.CalcurateUnitVector(player.transform.position, this.transform.position);

            }
            else if (isAtackingEnemy == false)
            {
                ChangeOwnerPlayer(false, false);
            }
            else
            {
                Debug.Log("Danege kurae");
                gameManager.Damage(false);
            }
        }
    }

    public void ChangeOwnerPlayer(bool isPlayerBall, bool callFromExCode)
    {
        if (!callFromExCode) SoundManager.instance.PlaySE(4);
        if (isPlayerBall)
        {
            renderer.material = playerMt;
            currentOwner = (int)OwnerName.Player;
        }
        else
        {
            renderer.material = enemyMt;
            currentOwner = (int)OwnerName.Enemy;
        }
    }

    void InvokeSFXinUpdate()
    {
        sfxTimer += Time.deltaTime;
        if (sfxTimer > .5f)
        {
            sfxTimer = 0;
            SoundManager.instance.PlaySE(1);
        }
    }
}
