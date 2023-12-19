using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void play()
    {
       // SoundManager.instance.PlaySound(interactSound);
          SceneManager.LoadScene(PlayerPrefs.GetInt("level", 1));
    }
    public void Quit()
    {
        Application.Quit(); //Quits the game (only works in build)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode (will only be executed in the editor)
#endif
    }
}