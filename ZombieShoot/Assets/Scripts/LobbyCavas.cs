using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyCavas : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private TextMeshProUGUI _bestScore;

    private void Start()
    {
        _bestScore.text = "��������: " + PlayerPrefs.GetInt("kills").ToString();
    }

}
