using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private Vector3 StartPosition;
    public CharacterController2D controller;
    public TMP_Text currentHeightText;
    public TMP_Text maxHeightText;

    private int maxHeight;

    private float hMove = 0f;
    [SerializeField] [Range(0f, 100f)] private float runSpeed = 20f;
    private bool isJump = false;

    // Update is called once per frame
    void Update() {
        hMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump")) {
            this.isJump = true;
        }
    }

    private void FixedUpdate() {
        controller.Move(hMove * Time.fixedDeltaTime, false, isJump);
        isJump = false;

        var h = Math.Max(0, (int)transform.position.y - 1);
        currentHeightText.text = $"{h} m";
        
        maxHeight = Math.Max(maxHeight, h);
        maxHeightText.text = $"{maxHeight} m";
    }

    public void Reset() {
        transform.position = StartPosition;
        gameObject.SetActive(true);

        maxHeight = 0;
    }

    public void EndGame() {
        gameObject.SetActive(false);
    }
}
