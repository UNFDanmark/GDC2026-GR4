using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public int maxPlayerHealth = 100;
    public int playerHealth = 100;

    public Slider healthBar;
    public TextMeshProUGUI healthText;
    
    public List<GameObject> enemies = new List<GameObject>();
    
        void Start()
    {
        instance = this;
        
        player = GameObject.FindGameObjectWithTag("Player");
        
        playerHealth = maxPlayerHealth;
        UpdateHealth();
    }

    public void damage(int damage)
    {
        if (playerHealth - damage <= 0)
        {
            die();
        }
        playerHealth -= damage;

        UpdateHealth();
    }

    private void UpdateHealth()
    {
        healthBar.value = playerHealth / maxPlayerHealth;
        healthText.text = playerHealth + " / " + maxPlayerHealth;
    }

    public void die()
    {
        SceneManager.LoadScene("DeathScreen");
    }
}
