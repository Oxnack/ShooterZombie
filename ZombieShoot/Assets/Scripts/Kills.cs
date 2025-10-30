using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Kills
{
    public int kills = 0;
    public TextMeshProUGUI textKills;

    public void GetKill(int kill)
    {
        if (PlayerPrefs.GetInt("kills") <= 0)
        {
            PlayerPrefs.SetInt("kills", 0);
        }

        kills += kill;
        textKills.text = "Убийства: " + kills.ToString();
        if (kills > PlayerPrefs.GetInt("kills"))
        {
            Save();
            Debug.Log("kills: " + PlayerPrefs.GetInt("kills").ToString());
        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt("kills", kills);
    }

    public int GetKills()
    {
        return PlayerPrefs.GetInt("kills");
    }
}
