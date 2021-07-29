using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collecting : MonoBehaviour
{
    private Transform Player;
    private Rigidbody2D currentRb;

    [SerializeField] private int ForceUp;
    [SerializeField] private int VelMov;

    private bool goToPlayer;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        currentRb = GetComponent<Rigidbody2D>();
        applyForceUp();
        StartCoroutine("GoToPlayer");
    }

    private void applyForceUp()
    {
        currentRb.AddForce(Vector2.up * ForceUp, ForceMode2D.Impulse);
    }

    private IEnumerator GoToPlayer()
    {
        yield return new WaitForSecondsRealtime(1.25f);
        Destroy(currentRb);
        goToPlayer = true;
    }

    private void Update()
    {
        if (goToPlayer)
        {
            Vector2 playerPos = new Vector2(Player.position.x, Player.position.y + 1f);
            transform.position = Vector2.MoveTowards(transform.position, playerPos, VelMov * Time.deltaTime);

            Vector2 colPos = new Vector2(transform.position.x, transform.position.y);
            if (colPos == playerPos)
            {
                Destroy(gameObject);
            }
        }
    }
}
