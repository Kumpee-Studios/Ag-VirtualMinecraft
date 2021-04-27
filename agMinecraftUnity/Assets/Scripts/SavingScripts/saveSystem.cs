using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class saveSystem
{
    public static void savePlayer(followPlayer camera, playerController controller, playerInventory inventory, interactionController interaction)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.info"; //creating path and filename, extension doesn't matter, persistent path is universal across OS
        FileStream stream = new FileStream(path, FileMode.Create); //creates file at path given
        saveInformation information = new saveInformation(camera, controller, inventory, interaction); //creating the save information

        formatter.Serialize(stream, information); //turning information into binary
        stream.Close();
    }

    public static saveInformation loadData()
    {
        string path = Application.persistentDataPath + "/player.info";
        if(File.Exists(path)) //if it exists, go to the path and open file, deserialize it and save it as a saveinformation
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open); //opens file
            if (stream.Length != 0)
            {
                saveInformation info = formatter.Deserialize(stream) as saveInformation; //casting the data into a saveinformation object
                stream.Close(); //don't forget to close the streamreader
                return info;
            }
            else
            {
                stream.Close(); //need this in 2 places to avoid errors
                return null;
            }
        }
        else //temp solution: bundle project with file included that has starting position and seed count etc
        { //if it doesn't exist or it's their first time playing, might have to adjust this later
            Debug.LogError("Save file not found");
            return null;
        }

    }
    public static void deleteData()
    {
        string path = Application.persistentDataPath + "/player.info"; //referencing path and filename for deletion
        if (File.Exists(path)) //assuming this will throw nasty error if not caught
        { //check file exists, if it does delete it. Means they either started new game or are playing for first time
            File.Delete(path);
        }
    }
}
