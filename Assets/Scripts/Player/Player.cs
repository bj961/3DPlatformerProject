using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCameraController cameraController;
    public Equipment equip;
    


    public ItemData itemData;
    public Action addItem;

    public Transform dropPosition;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        cameraController = GetComponent<PlayerCameraController>();
        equip = GetComponent<Equipment>();
    }

    private void Start()
    {
        if (GameManager.Instance.player == null)
        {
            GameManager.Instance.player = this;
        }
    }
}
