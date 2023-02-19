using UnityEngine;

public class DieToWin : MonoBehaviour
{
    private GameObject _player;
    private GameManager GameManager;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == _player)
        {
            GameManager.ResetLevel();
        }
    }
}
