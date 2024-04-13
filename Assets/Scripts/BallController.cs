using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Speed = 2000f;
    public int currentOwner;
    public enum OwnerName : int { NoOne = 0, Player = 1, Enemy = 2 }
    public bool isAtackingEnemy, isAtackingPlayer;
    [SerializeField] private GameObject vfxPlayer, vfxEnemy;

    [SerializeField] private GameObject player, enemy;

    public Vector3 Direction = new Vector3(1, 0, 1);
    [SerializeField] private Material normalMt, enemyMt, playerMt;
    Renderer renderer;
    private Vector3 unitVector, velocity;
    private Rigidbody rigidBody;
    GameManager gameManager;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        normalMt = renderer.material;
        currentOwner = (int)OwnerName.NoOne;
        gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(Direction.normalized * Speed * Time.deltaTime, ForceMode.Impulse);
    }
    private void Update()
    {
        if (gameManager.isGameEnd)
        {
            rigidBody.velocity = Vector3.zero;
            return;
        }

        if (isAtackingEnemy)
        {
            vfxPlayer.SetActive(true);
            velocity = unitVector * 40;
            rigidBody.velocity = velocity;
        }
        else if (isAtackingPlayer)
        {
            vfxEnemy.SetActive(true);
            velocity = unitVector * 40;
            rigidBody.velocity = velocity;
        }
        else
        {
            rigidBody.velocity += Vector3.up * 5f * Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        rigidBody.velocity = rigidBody.velocity.normalized * Speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (currentOwner == (int)OwnerName.Player)
            {
                isAtackingEnemy = true;
                unitVector = gameManager.CalcurateUnitVector(enemy.transform.position, this.transform.position);
            }
            else if (isAtackingPlayer == false)
            {
                renderer.material = playerMt;
                currentOwner = (int)OwnerName.Player;
            }
            else
            {
                Debug.Log("Enemy kill");
                gameManager.Damage(true);
            }
        }
        else if (other.gameObject.name == "Enemy")
        {
            if (currentOwner == (int)OwnerName.Enemy)
            {
                isAtackingPlayer = true;
                unitVector = gameManager.CalcurateUnitVector(player.transform.position, this.transform.position);

            }
            else if (isAtackingEnemy == false)
            {
                renderer.material = enemyMt;
                currentOwner = (int)OwnerName.Enemy;
            }
            else
            {
                Debug.Log("Danege kurae");
                gameManager.Damage(false);
            }
        }
        else if (other.gameObject.tag == "wall")
        {
            isAtackingEnemy = false;
            vfxPlayer.SetActive(false);
            isAtackingPlayer = false;
            vfxEnemy.SetActive(false);
        }
    }
}
