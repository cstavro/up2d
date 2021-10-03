using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropController : MonoBehaviour {
    public CharacterController2D controller;
    public NextPieceController nextPieceController;
    public GameObject dropperPlaceholder;
    
    private float hMove = 0f;
    private bool isDrop = false;
    private float nextDrop = 0f;

    private float yLock;
    
    [SerializeField] private Transform pieceContainer;
    [SerializeField] [Range(0f, 100f)] private float speed = 20f;
    [SerializeField] private float dropDelay = 3f;
    [SerializeField] private List<DroppableItem> droppableItems;
    [SerializeField] private int nextPiece;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    
    private void Start() {
        randomizeNextDrop();
        yLock = Camera.main.WorldToScreenPoint(transform.position).y;
    }

    void Update() {
        hMove = Input.GetAxisRaw("Drop Horizontal") * speed;
        if (Input.GetButtonDown("Drop") 
        && Time.timeSinceLevelLoad >= nextDrop) {
            isDrop = true;
        }

        var p = transform.position;
        p.y = Camera.main.ScreenToWorldPoint(dropperPlaceholder.transform.position).y;
        transform.position = p;
    }

    private void FixedUpdate() {
        controller.Move(hMove * Time.fixedDeltaTime, false, false);
        var p = transform.position;
        p.x = Mathf.Clamp(p.x, minX, maxX);
        transform.position = p;
        if (isDrop) {
            Drop(nextPiece);
            isDrop = false;
        }
    }

    private void randomizeNextDrop() {
        nextPiece = Random.Range(0, droppableItems.Count);
        nextPieceController.ShowNextPiece(droppableItems[nextPiece].gameObject);
    }
   
    private void Drop(int pieceId) {
        nextDrop = Time.timeSinceLevelLoad + dropDelay;
        
        float rotate = Random.Range(0f, 360f);
        var currentPiece = Instantiate(droppableItems[pieceId], pieceContainer);
        currentPiece.transform.position = transform.position;
        currentPiece.transform.Rotate(Vector3.forward, rotate);
        
        randomizeNextDrop();
        
    }

    public void Reset() {
        gameObject.SetActive(true);
        randomizeNextDrop();
    }

    public void EndGame() {
        gameObject.SetActive(false);
    }
}
