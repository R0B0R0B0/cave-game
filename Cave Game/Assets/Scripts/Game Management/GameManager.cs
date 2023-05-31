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
    [Header("Musics")]
    public AudioClip[] musics;
    public AudioSource source;

    public GameObject player;

    public bool hasPlayer;
    public float health;

    private void Start()
    {
        ChangeMusic(0);
        OnLevelStart();
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

    public void ChangeView(Cameras camera)
    {
        if (!CameraController.IsActiveCamera(cameras[(int)camera]))
        {
            CameraController.SwitchCamera(cameras[(int)camera]);
        }
    }

    public void ChangeMusic(int index)
    {
        source.Stop();
        source.clip = musics[index];
        source.Play();
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

        source.Stop();
    }

}
