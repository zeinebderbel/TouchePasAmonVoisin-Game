using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playScene : MonoBehaviour
{
    //public string[] nameScenes;

    public void openscene(string scenename)
    {
        SceneManager.LoadScene(scenename, LoadSceneMode.Single);
    }

	public void quit()
	{
        Application.Quit();
    }
}
