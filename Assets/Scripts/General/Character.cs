using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本属性")]
    public int maxHealth;
    public int currentHealth;

    [Header("受伤相关")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool isInvulnerable;

    public UnityEvent<Transform> onTakeDamage;
    public UnityEvent OnDie;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
            {
                isInvulnerable = false;
            }
        }

    }

    //受伤
    public void TakeDamage(Attack attack)
    {
        if (isInvulnerable)
            return;

        currentHealth -= attack.damege;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDie?.Invoke();
        }
        else
        {
            onTakeDamage?.Invoke(attack.transform);
        }
            

        TriggerInvulnerable();
    }

    //无敌
    private void TriggerInvulnerable()
    {
        if (!isInvulnerable)
        {
            isInvulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
            
    }

}
