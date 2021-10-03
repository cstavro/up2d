using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DroppableItem : MonoBehaviour {
    private Rigidbody2D rigidBody;
    private float minHeight = -10f;
    [SerializeField] public bool doPreviewScale = false;
    [SerializeField] public Vector3 previewScaler = Vector3.one;

    private Coroutine checkPhysicsCoroutine;
    
    private void Start() {
        this.rigidBody = GetComponent<Rigidbody2D>();

        checkPhysicsCoroutine = StartCoroutine(physicsEnabler());
    }

    private void OnDestroy() {
        if (!(checkPhysicsCoroutine is null)) {
            StopCoroutine(checkPhysicsCoroutine);
        }
    }

   
    private void FixedUpdate() {
        if (transform.position.y < minHeight) {
            Destroy(gameObject);
        }
    }

    private IEnumerator physicsEnabler() {
        Vector3 screenPosition;
        while (true) {
            screenPosition = Camera.main.WorldToScreenPoint(transform.position);

            rigidBody.bodyType = screenPosition.y < -100f ? 
                RigidbodyType2D.Static : 
                RigidbodyType2D.Dynamic;
            
            yield return new WaitForSeconds(1);
        }
    }
}
