using HomeScene;
using UnityEngine;

public class GameState: MonoBehaviour
{
    public static GameState instance;
    [SerializeField] GameObject _gameoverDialogPrefab;
    private void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("Has more than one GameState");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void Gameover()
    {
        PlayerStateMachine playerCtrl = FindObjectOfType<PlayerStateMachine>();
        PlayerThirdPersonInput playerInput = FindObjectOfType<PlayerThirdPersonInput>();
        if(playerCtrl)
            playerCtrl.enabled = false;
        if(playerInput)
            playerInput.listening = false;

        // show window is game over
        Canvas canvas = FindObjectOfType<Canvas>();
        GameObject panel = Instantiate(_gameoverDialogPrefab, canvas.transform);
        GameoverDialog dialog = panel.GetComponent<GameoverDialog>();
        dialog.ShowDialog();
    }
}
