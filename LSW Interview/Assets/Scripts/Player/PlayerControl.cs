using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Store Store;

    private SpriteRenderer Skin;
    private NavMeshAgent Player;

    private float xPos;
    private float yPos;

    private Camera Camera;
    private Animator ActualAnim;

    public bool StorePressed;
    public bool CollectiblePressed;
    public bool Collecting;
    public bool ForRight;
    public bool ForLeft;

    public Collectible TempCollectible;

    private bool firstClick;
    [SerializeField] private GameObject InitialInstruction;

    private void Start()
    {
        Player = GetComponent<NavMeshAgent>();

        Player.updateRotation = false;
        Player.updateUpAxis = false;

        Camera = Camera.main;

        ObtainCurrentSprite(GetComponentInChildren<SpriteRenderer>());
        ObtainCurrentAnim(GetComponentInChildren<Animator>());
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0) || !Input.GetMouseButton(0)) { return; }

        if(StorePressed) { return; }

        if (Collecting) { return; }

        CollectiblePressed = false;

        xPos = Input.mousePosition.x;
        yPos = Input.mousePosition.y;

        MovePlayer(xPos ,yPos);
      
        if (!firstClick) { InitialInstruction.SetActive(false); firstClick = true; }
    }
    private void LateUpdate()
    {
        updateWalkAnimOff();
    }
    private void MovePlayer(float newXpos, float newYpos)
    {
        Vector2 newPosPlayer = Camera.ScreenToWorldPoint(new Vector2(newXpos, newYpos));

        UpdateRotPlayer(newPosPlayer.x);
        UpdatePosPlayer(newPosPlayer);
    }
    public void MovePlayerToStore(float newXpos, float newYpos)
    {
        Vector2 newPosPlayer = new Vector2(newXpos, newYpos);

        UpdateRotPlayer(newPosPlayer.x);
        UpdatePosPlayer(newPosPlayer);
    }
    public void MovePlayerToCollectible(float newXpos, float newYpos)
    {
        Vector2 newPosPlayer = new Vector2(newXpos, newYpos);

        UpdateRotPlayer(newPosPlayer.x);
        UpdatePosPlayer(newPosPlayer);
    }
    private void UpdateRotPlayer(float newXpos)
    {
        float xPosActual = transform.position.x;
        
        if(newXpos < xPosActual)
        {
            Skin.flipX = true;
        }else if(newXpos > xPosActual)
        {
            Skin.flipX = false;
        }
    }
    private void UpdatePosPlayer(Vector2 newPosPlayer)
    {
        Player.SetDestination(newPosPlayer);

        updateWalkAnimOn();
    }
    public void ObtainCurrentAnim(Animator CurrentAnim)
    {
        ActualAnim = CurrentAnim;
    }
    public void ObtainCurrentSprite(SpriteRenderer CurrentSprite)
    {
        Skin = CurrentSprite;
    }
    private void updateWalkAnimOn()
    {
        if (!Player.isStopped)
        {
            ActualAnim.SetBool("Walking", true);
        }
    }
    private void updateWalkAnimOff()
    {
        if (Player.destination == transform.position)
        {
            ActualAnim.SetBool("Walking", false);

            if (StorePressed)
            {
                DetectSideToArrive();

                Store.InStore();

                CollectiblePressed = false;
            }
            if (CollectiblePressed)
            {
                DetectSideToArrive();

                ActualAnim.SetBool("Attacking", true);
                Collecting = true;
                TempCollectible.availableToCollect = false;
                CollectiblePressed = false;
            }
        }
    }
    private void DetectSideToArrive()
    {
        if (ForLeft)
        {
            Skin.flipX = false;

            ForLeft = false;
        }
        else if (ForRight)
        {
            Skin.flipX = true;

            ForRight = false;
        }
    }
    public void CollectingOff()
    {
        Collecting = false;
        ActualAnim.SetBool("Attacking",false);
    }

}
