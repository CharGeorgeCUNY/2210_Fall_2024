using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance;

    public static GameManager GetGameManager()
    {
        if(_Instance == null)
        {
            _Instance = FindObjectOfType<GameManager>();
        }
        return _Instance;
    }
    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
       //List<Bullet> bullets = new List<Bullet>( GameObject.FindObjectsOfType<Bullet>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementScore()
    {
        score++;
        Debug.Log(score);
    }
}
