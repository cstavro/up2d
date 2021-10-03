using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPieceController : MonoBehaviour {

    private GameObject currentPiece;
    private float yLock;
    public GameObject nextPiecePosition;

    private void Start() {
        yLock = Camera.main.WorldToScreenPoint(transform.position).y;
    }

    private void Update() {
        var p  = Camera.main.ScreenToWorldPoint(nextPiecePosition.transform.position);
        p.z = 0f;
        transform.position = p;
    }

    public void ShowNextPiece(GameObject nextPiece) {
        if (currentPiece) {
            currentPiece.gameObject.SetActive(false);
            Destroy(currentPiece);
        }

        currentPiece = Instantiate(nextPiece.gameObject, transform);
        currentPiece.transform.localPosition = Vector3.zero;
        
        var di = currentPiece.GetComponent<DroppableItem>();
        if (di.doPreviewScale) {
            currentPiece.transform.localScale = di.previewScaler;
        }

        var rb = currentPiece.gameObject.GetComponent<Rigidbody2D>();
        rb.simulated = false;
    }
}
