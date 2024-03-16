using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Enemy;

public class LobbyCavas : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private TextMeshProUGUI _bestScore;
    [SerializeField] private GameObject _ZomSpawner;



    private void OnEnable()
    {
        _player.GetComponent<PlayerController.Player>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        DestroyAllZombies();
        _bestScore.text = "Убийства: " + PlayerPrefs.GetInt("kills").ToString();
        _ZomSpawner.SetActive(false);
        Time.timeScale = 0;
    }

    public void GetStart()
    {
        _player.GetComponent<PlayerController.Player>().enabled = true;
        Time.timeScale = 1;
        _ZomSpawner.SetActive(true);
        gameObject.SetActive(false);
        gameObject.GetComponent<LobbyCavas>().enabled = false;
    }
    void DestroyAllZombies()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");

        foreach (GameObject zombie in zombies)
        {
            Destroy(zombie);
        }
    }

}
