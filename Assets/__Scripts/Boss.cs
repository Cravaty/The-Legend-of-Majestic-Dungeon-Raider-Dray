using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy, IFacingMover
{
    [Header("Set in Inspector: Skeletos")]
    public float speed = 2f;
    public float timeThinkMin = 1f;
    public float timeThinkMax = 4f;
    public Transform player;
    public Transform boss;
    public GameObject won;
    public static GameObject wowon;

    public int povorot = 0;
    [Header("Set Dynamically: Skeletos")]
    public int facing = 0;
    public float timeNextDecision = 0;
    private InRoom inRm; // b
    protected override void Awake()
    { // c
        base.Awake();
        inRm = GetComponent<InRoom>();
        wowon = won;
    }

    override protected void Update()
    {
        base.Update();
        if (knockback) return;
        
       
        DecideDirection();

        
        rigid.velocity = directions[facing] * speed;
        
    }
   void LateUpdate()
    { }
    void DecideDirection()
    {
        facing = GetFacingForDray();
    }

    int GetFacingForDray()
    {
        if (Mathf.Round(player.position.x) > Mathf.Round(boss.position.x) && Mathf.Round(player.position.y) == Mathf.Round(boss.position.y))
            povorot = 0;
        if (Mathf.Round(player.position.y) > Mathf.Round(boss.position.y) && Mathf.Round(player.position.x) == Mathf.Round(boss.position.x))
            povorot = 1;
        if (Mathf.Round(player.position.x) < Mathf.Round(boss.position.x) && Mathf.Round(player.position.y) == Mathf.Round(boss.position.y))
            povorot = 2;
        if (Mathf.Round(player.position.y) < Mathf.Round(boss.position.y) && Mathf.Round(player.position.x) == Mathf.Round(boss.position.x))
            povorot = 3;
        Debug.Log(povorot);
        return povorot;
    }

    public static void Won()
    {
        wowon.SetActive(true);
    }
   
    // Реализация интерфейса IFacingMover
    public int GetFacing()
    {
        return facing;
    }
    public bool moving { get { return true; } } // d
    public float GetSpeed()
    {
        return speed;
    }
    public float gridMult
    {
        get { return inRm.gridMult; }
    }
    public Vector2 roomPos
    {
        get { return inRm.roomPos; }
        set { inRm.roomPos = value; }
    }
    public Vector2 roomNum
    {
        get { return inRm.roomNum; }
        set { inRm.roomNum = value; }
    }
    public Vector2 GetRoomPosOnGrid(float mult = -1)
    {
        return inRm.GetRoomPosOnGrid(mult);
    }
}
