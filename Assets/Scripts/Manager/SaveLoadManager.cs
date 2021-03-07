using UnityEngine;
using System.IO;
using System.Text;


public class SaveLoadManager :Singleton<SaveLoadManager>
{
    private string _saveFilePath="Assets/Save/";
    private string _saveFileName="Save.json";

    [SerializeField] GameObject enemyPrefab;


  
private void Update() {
    if(Input.GetKeyDown(KeyCode.K))
    {
        Save();
    }else if(Input.GetKeyDown(KeyCode.L))
    {
        Load();
    }

}
 


 private void OnEnable() {
     EventHandler.AfterSceneLoadFadeInEvent+=Load;

 }

  void OnDisable() {
     EventHandler.AfterSceneLoadFadeInEvent-=Load;
     
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
       FileStream save= File.Create(_saveFilePath+_saveFileName);
       AddText(save,JsonUtility.ToJson(saveDate));
       
      save.Close();
    }




    public void Load()
    {
       FileStream save= File.OpenRead(_saveFilePath+_saveFileName);
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

    }


    public void ClearSave()
    {
        FileStream save= File.Create(_saveFilePath+_saveFileName);
        save.Close();
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
