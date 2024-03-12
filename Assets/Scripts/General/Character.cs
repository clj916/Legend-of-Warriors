using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("��������")]
    public int maxHealth;
    public int currentHealth;

    [Header("�������")]
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

    //����
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

    //�޵�
    private void TriggerInvulnerable()
    {
        if (!isInvulnerable)
        {
            isInvulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
            
    }

}
