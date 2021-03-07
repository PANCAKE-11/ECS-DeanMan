using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _camerasPrefab;

    [SerializeField] private So_PlayerProperties properties;
    private GameObject player;
    private GameObject cameras;

    public bool StartMenu;
    public Texture2D normalCursor;
    public Texture2D AimCursor;


    
    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadFadeOutEvent += DestoryPlayer;
        EventHandler.AfterSceneLoadFadeInEvent += SpawnPlayer;
        StartMenu=true;
    }

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadFadeOutEvent -= DestoryPlayer;
        EventHandler.AfterSceneLoadFadeInEvent -= SpawnPlayer;
    }
    /// <summary>
    /// 主角相关的存储
    /// </summary>
    public void SpawnPlayer()
    {

        player = GameObject.Instantiate(_playerPrefab, properties.position, properties.roatition);
        cameras = GameObject.Instantiate(_camerasPrefab, Vector3.one, Quaternion.identity);
        CinemachineVirtualCamera _virtualCamera = cameras.transform.GetComponentInChildren<CinemachineVirtualCamera>();
        _virtualCamera.Follow = player.transform;
        _virtualCamera.LookAt = player.transform;
    }
    private void Start() {
        Cursor.SetCursor(normalCursor,Vector2.zero,CursorMode.Auto);
        SpawnPlayer();
    }

    public void DestoryPlayer()
    {
        if (player != null && cameras != null)
        {
            properties.position = player.transform.position;
            properties.roatition = player.transform.rotation;
            Destroy(player);
            Destroy(cameras);
        }
    }

    public void SetNormalCursor()
    {
        Cursor.SetCursor(normalCursor,Vector2.zero,CursorMode.Auto);

    }

    
    public void SetAimCursor()
    {
        Cursor.SetCursor(AimCursor,Vector2.zero,CursorMode.Auto);

    }

   
}
