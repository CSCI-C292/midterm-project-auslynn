using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] GameObject player;
    Collider2D col;
    //Collider2D playerCollider;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        //playerCollider = player.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player" && this.name == "SceneChanger1")
        {
            SceneManager.LoadScene("Scene2");
        }
    }
}
