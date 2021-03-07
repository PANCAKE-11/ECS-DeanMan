using UnityEngine;
using System.IO;
using System.Text;
using Cinemachine;

public class SaveLoadManager :Singleton<SaveLoadManager>
{
    private string _saveFilePath="Assets/Save/";
    private string _EnemySaveFileName="Save.json";

    [SerializeField] GameObject enemyPrefab;

    private GameObject player;
    private GameObject cameras;
    [SerializeField] private So_PlayerProperties playerProperties;


    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _camerasPrefab;

  
private void Update() {
    if(Input.GetKeyDown(KeyCode.K))
    {
        Save();
    }else if(Input.GetKeyDown(KeyCode.L))
    {
         Load();
    }
}
 

    public void Save()
    {

        EnemySave saveDate=new EnemySave();

      //  saveDate.player=PlayerController.Instance.transform;

       // saveDate.playerHealth=PlayerController.Instance.Healeth;
        GameObject[] enemys=GameObject.FindGameObjectsWithTag("Enemy");
        for(int i=0;i<enemys.Length;i++)
        {
            saveDate.Enemyposition.Add(enemys[i].transform.position);
            saveDate.EnemyRotation.Add(enemys[i].transform.rotation);
            if(enemys[i].GetComponent<Enemy>())
           {  saveDate.EnemyDeadFlag.Add(false);
             saveDate.EnemyHealth.Add(enemys[i].GetComponent<Enemy>().Health);}
            else
            { saveDate.EnemyDeadFlag.Add(true);
             saveDate.EnemyHealth.Add(-1);}

        }
       FileStream save= File.Create(_saveFilePath+_EnemySaveFileName);
       AddText(save,JsonUtility.ToJson(saveDate));

      save.Close();

    if (player != null && cameras != null)
        {
            playerProperties.position = player.transform.position;
            playerProperties.roatition = player.transform.rotation;
            playerProperties.HealthValue=PlayerController.Instance.Healeth;
        }
    }

    public void Load()
    {
       FileStream save= File.OpenRead(_saveFilePath+_EnemySaveFileName);
       EnemySave saveData=JsonUtility.FromJson<EnemySave>(ReadText(save));

        GameObject[] enemys=GameObject.FindGameObjectsWithTag("Enemy");
      if(ReadText(save)!="")
     { for(int i=0;i<enemys.Length;i++)
      {
         enemys[i].transform.position=saveData.Enemyposition[i];
          enemys[i].transform.rotation=saveData.EnemyRotation[i];
          if(enemys[i].GetComponent<Enemy>())
        {  enemys[i].GetComponent<Enemy>().Health=saveData.EnemyHealth[i];
          enemys[i].GetComponent<Enemy>().die=saveData.EnemyDeadFlag[i];}
      }}
      save.Close();

      SpawnPlayerAndCarmera();

    }
   public void SpawnPlayerAndCarmera()
    {
        if(!player&&!cameras)
        {player = GameObject.Instantiate(_playerPrefab, playerProperties.position, playerProperties.roatition);
        cameras = GameObject.Instantiate(_camerasPrefab, Vector3.one, Quaternion.identity);
        cameras.transform.SetParent(transform);
         CinemachineVirtualCamera _virtualCamera = cameras.transform.GetComponentInChildren<CinemachineVirtualCamera>();
        _virtualCamera.Follow = player.transform;
        _virtualCamera.LookAt = player.transform;}
        else{
            player.SetActive(true);
            cameras.SetActive(true);
            player.transform.position=playerProperties.position;
            player.transform.rotation=playerProperties.roatition;
        }
       PlayerController.Instance.Healeth=playerProperties.HealthValue;
    }
    public void ClearSave()
    {
        FileStream save= File.Create(_saveFilePath+_EnemySaveFileName);
        save.Close();
          playerProperties.position = Vector3.zero;
            playerProperties.roatition = Quaternion.identity;

    }
     private  void AddText(FileStream fs, string value)
    {
        byte[] info = new UTF8Encoding(true).GetBytes(value);
        fs.Write(info, 0, info.Length);
    }

    private string ReadText(FileStream fs)
    {
             int fsLen = (int)fs.Length;
                byte[] heByte = new byte[fsLen];
                int r = fs.Read(heByte, 0, heByte.Length);
                string myStr = System.Text.Encoding.UTF8.GetString(heByte);
                return myStr;
    }
}
