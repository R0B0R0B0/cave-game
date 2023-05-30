using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public enum Cameras
{
    Base,Far,Battle,Rock
}

public class GameManager : MonoBehaviour
{
    public LevelManager LevelManager = new();
    public FightManager FightManager = new();

    #region Awake - Singleton & Dont destroy on load
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            Destroy(this);
            return;
        }
        Instance = this;

       // DontDestroyOnLoad(this);

        SceneManager.activeSceneChanged += OnSceneLoaded;
    }
    #endregion

    [Header("Cinemachine")]
    [SerializeField]
    CinemachineVirtualCamera[] cameras;
    //CinemachineVirtualCamera Far;
   // CinemachineVirtualCamera Fight;

    [Header("Level Management")]
    public Transform startPosition;
    [Header("Prefabs")]
    public GameObject playerPrefab;


    public GameObject player;

    public bool hasPlayer;
    public float health;

    public void OnEndPositionTriggered(int exitID)
    {
        ChangeScene("Level 2");
    }

    void OnLevelStart()
    {
        if (hasPlayer) { return; }
        startPosition = GameObject.FindGameObjectWithTag("Respawn").transform;
        if (startPosition != null)
        {
            player = Instantiate(player, startPosition);
        }
        else
        {
            player = Instantiate(player,Vector3.zero,Quaternion.identity);
        }

    }

    /// <summary>
    /// This is called when the scene is loaded
    /// </summary>
    public void OnSceneLoaded(Scene current, Scene next)
    {
        OnLevelStart();
    }

    /// <summary>
    /// This checks if next level can be loaded and calls LevelManager to load the next scene
    /// </summary>
    /// <param name="name"></param>
    void ChangeScene(string name)
    {
        //Check if objectives are filled if any
        
        LevelManager.LoadScene(name);
    }

    public void ChangeView(Cameras camera)
    {
        if (!CameraController.IsActiveCamera(cameras[(int)camera]))
        {
            CameraController.SwitchCamera(cameras[(int)camera]);
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            CameraController.Register(cameras[i]);
        }
        CameraController.SwitchCamera(cameras[0]);
    }

    private void OnDisable()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            CameraController.UnRegister(cameras[i]);
        }
    }

}
