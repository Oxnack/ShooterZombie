using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyCavas : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private TextMeshProUGUI _bestScore;



    private void OnEnable()
    {
        DestroyAllZombies();
        _bestScore.text = "Убийства: " + PlayerPrefs.GetInt("kills").ToString();
        Time.timeScale = 0;
    }

    public void GetStart()
    {
        Time.timeScale = 1;
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
