using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int levelNumber;
    [System.NonSerialized] public bool levelBeaten;
    private GameObject _win, _player;
    private Vector2 _playerPos;

    private void Awake()
    {
        _win = GameObject.FindGameObjectWithTag("Win");
        _player = GameObject.FindGameObjectWithTag("Player");

        _playerPos = _player.transform.position;
    }

    public void ResetLevel()
    {
        levelBeaten = true;
        _player.transform.position = _playerPos;
        _win.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void ChangeScene(int id)
    {
        levelBeaten = false;
        levelNumber = id;
        SceneManager.LoadScene(id);
    }
}
