using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    Player playerObject;
    public Text seconds;
    public GameObject gameOverUI;
    int flag = 0;    //flag to check when GameOverMethod gets called.
	// Use this for initialization
	void Start () {
        gameOverUI.SetActive(false);
        playerObject = FindObjectOfType<Player>();
        playerObject.onPlayerDestroyed += GameOverMethod;
    }
	
	// Update is called once per frame
	void Update () {
		if(flag == 1)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);  
            }
        }
	}

    public void GameOverMethod()
    {
        flag = 1;
        gameOverUI.SetActive(true);
        seconds.text = Mathf.RoundToInt(Time.timeSinceLevelLoad).ToString();  // Seconnds from the start of the scene to be displayed, when player is destroyed
    }
}
