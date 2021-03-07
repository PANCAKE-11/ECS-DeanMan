using UnityEngine;
using System.IO;
using System.Text;
using System.Linq;
public class SaveLoadManager :Singleton<SaveLoadManager>
{
    private string _saveFilePath="Assets/Save/";
    private string _saveFileName="Save.json";


    private void OnEnable() {
        // EventHandler.AfterSceneLoadFadeInEvent+=Load;
        // EventHandler.BeforeSceneUnloadEvent+=Save;
    }
    private void OnDisable() {
        // EventHandler.AfterSceneLoadFadeInEvent-=Load;
        // EventHandler.BeforeSceneUnloadEvent-=Save;

    }
    public void Save()
    {

        Save saveDate=new Save();

      //  saveDate.player=PlayerController.Instance.transform;

       // saveDate.playerHealth=PlayerController.Instance.Healeth;
        GameObject[] enemys=GameObject.FindGameObjectsWithTag("Enemy");
         saveDate.Enemy=enemys.ToList<GameObject>();
        print(enemys.Length);

       FileStream save= File.Create(_saveFilePath+_saveFileName);
       AddText(save,JsonUtility.ToJson(saveDate));
       
       foreach( var enemy in enemys)
       {
           Destroy(enemy);
       }
    }




    public void Load()
    {
       FileStream save= File.OpenRead(_saveFilePath+_saveFileName);
       Save saveData=JsonUtility.FromJson<Save>(ReadText(save));
      
       foreach(var enemy in saveData.Enemy)
       {
           GameObject.Instantiate(enemy,enemy.transform.position,enemy.transform.rotation);
       }

      //s Transform playerTransform= PlayerController.Instance.transform;
        // playerTransform.position=saveData.player.position;
        // playerTransform.rotation=saveData.player.rotation;
        // PlayerController.Instance.Healeth=saveData.playerHealth;

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
