using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected static Vector3[] directions = new Vector3[] { 
Vector3.right, Vector3.up, Vector3.left, Vector3.down };

    [Header("Set in Inspector: Enemy")] // b
    public float maxHealth = 1; // c
    public float knockbackSpeed = 10;
    public float knockbackDuration = 0.25f;
    public float invincibleDuration = 0.5f;
    public GameObject[] randomItemDrops;
    public GameObject guaranteedltemDrop = null;



    [Header("Set Dynamically: Enemy")]
    public float health; // c
    protected Animator anim; // c
    protected Rigidbody rigid; // c
    protected SpriteRenderer sRend; // c
    public bool invincible = false;
    public bool knockback = false;

    private float invincibleDone = 0;
    private float knockbackDone = 0;
    private Vector3 knockbackVel;

    protected virtual void Awake()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        sRend = GetComponent<SpriteRenderer>();
    }
    protected virtual void Update()
    { // b
      // Проверить состояние неуязвимости и необходимость выполнить отскок
        if (invincible && Time.time > invincibleDone) invincible = false;
        sRend.color = invincible ? Color.red : Color.white;
        if (knockback)
        {
            rigid.velocity = knockbackVel;
            if (Time.time < knockbackDone) return;
        }
        anim.speed = 1; // c
        knockback = false;
    }
    void OnTriggerEnter(Collider colld)
    { // d
        if (invincible) return; // Выйти, если Дрей пока неуязвим
        DamageEffect dEf = colld.gameObject.GetComponent<DamageEffect>();
        if (dEf == null) return; // Если компонент DamageEffect отсутствует - выйти
        health -= dEf.damage; // Вычесть величину ущерба из уровня здоровья
        if (health <= 0)
        {
                Die();
        }
        invincible = true; // Сделать Дрея неуязвимым
        invincibleDone = Time.time + invincibleDuration;
        if (dEf.knockback)
        { // Выполнить отбрасывание
          // Определить направление отскока
            Vector3 delta = transform.position - colld.transform.root.position;
            if (Mathf.Abs(delta.x) >= Mathf.Abs(delta.y))
            {
                // Отбрасывание по горизонтали
                delta.x = (delta.x > 0) ? 1 : -1;
                delta.y = 0;
            }
            else
            {
                // Отбрасывание по вертикали
                delta.x = 0;
                delta.y = (delta.y > 0) ? 1 : -1;
            }
            // Применить скорость отбрасывания к компоненту Rigidbody
            knockbackVel = delta * knockbackSpeed;
            rigid.velocity = knockbackVel;
            // Установить режим knockback и время прекращения отбрасывания
            knockback = true;
            knockbackDone = Time.time + knockbackDuration;
            anim.speed = 0;
        }
    }
    void Die()
    { // f
        GameObject go;
        if (guaranteedltemDrop != null)
        {
            go = Instantiate<GameObject>(guaranteedltemDrop);
            go.transform.position = transform.position;
        }
        else if (randomItemDrops.Length > 0)
        { // b
            int n = Random.Range(0, randomItemDrops.Length);
            GameObject prefab = randomItemDrops[n];
            if (prefab != null)
            {
                go = Instantiate<GameObject>(prefab);
                go.transform.position = transform.position;
            }
        }
        Destroy(gameObject);
    }
}
