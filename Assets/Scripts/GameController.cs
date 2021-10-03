using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour {
    public PlayerMovement playerController;
    public DropController dropController;
    public Camera camera;
    public GameObject pieceContainer;
    public GameObject GameOver;
    public GameObject TitleCard;
    public GameObject Hud;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p")) {
            Debug.Log("Restarting game..");
            RestartGame();
        }

        if (playerController.transform.position.y < -10f) {
            EndGame();
        }
    }

    void RestartGame() {
        GameOver.SetActive(false);
        playerController.Reset();
        dropController.Reset();
        camera.transform.position = new Vector3(0, 7.64f, -10f);

        var camera2d = camera.GetComponent<CameraController2D>();
        camera2d.targetCameraLocation = camera.transform.position; 

        foreach(Transform child in pieceContainer.transform) {
            Destroy(child.gameObject);
        }
        
        TitleCard.SetActive(false);
        Hud.SetActive(true);
    }

    void EndGame() {
        var score = GameOver.transform.Find("MaxHeightValue").GetComponent<TMP_Text>();
        score.text = playerController.maxHeightText.text;
        GameOver.SetActive(true);
        playerController.EndGame();
        dropController.EndGame();
    }
}
