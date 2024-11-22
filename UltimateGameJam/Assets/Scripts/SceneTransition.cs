using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string menuName;
    [SerializeField] private string instructionSceneName;
    [SerializeField] private string gameName;

    public void GoToScene(string name){
        SceneManager.LoadScene(name);
    }
}
