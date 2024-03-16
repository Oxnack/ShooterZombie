using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    public void ExitGame()
    {
        _player.GetComponent<PlayerController.Player>().Hp.GetAttack(150);
    }
}
