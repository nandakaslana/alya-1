using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    public GameObject deathAnimation;

    void Start()
    {
        PlayerController.OnDeath += OnDeath;
    }
    
    public void OnDeath()
    {
        Debug.Log("Death");
        if (deathAnimation != null)
        {
            StartCoroutine(HandleDeathEffect());
        }
    }
    private IEnumerator HandleDeathEffect()
    {
        deathAnimation.SetActive(true);
        yield return new WaitForSeconds(2f);
        deathAnimation.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

