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
    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadFadeOutEvent += DestoryPlayer;
        EventHandler.AfterSceneLoadFadeInEvent += SpawnPlayer;
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
}
