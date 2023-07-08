using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using CONST;

public class SceneTransitionManager : MonoBehaviour
{
    public void LoadTo(CONST.SCENE.Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

}
