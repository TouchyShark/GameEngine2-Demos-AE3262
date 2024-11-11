using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    public float maxHealth = 100;
    public float chipSpeed = 2f;
    public Image frontHelathBar;
    public Image backHelathBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;   
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health,0,maxHealth);
        UpdateHealthUI();
        if (Input.GetKeyDown(KeyCode.A)) 
        {
            TakeDamage(Random.Range(5, 10));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            RestoreHealth(Random.Range(5, 10));
        }
    }
    public void UpdateHealthUI()
    {
        Debug.Log(health);
        float fillF = frontHelathBar.fillAmount;
        float fillB = backHelathBar.fillAmount;
        float hFraction = health/maxHealth;
        if(fillB > hFraction) 
        {
            frontHelathBar.fillAmount = hFraction;
            backHelathBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHelathBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if(fillF < hFraction)
        {
            backHelathBar.color = Color.green;
            backHelathBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHelathBar.fillAmount = Mathf.Lerp(fillF,backHelathBar.fillAmount,percentComplete);
        }
    }

    public void TakeDamage(float damage) 
    {
        health -= damage;
        lerpTimer = 0f;
    }
    public void RestoreHealth(float healAmount) 
    {
        health += healAmount;
        lerpTimer = 0f;
    }
}
