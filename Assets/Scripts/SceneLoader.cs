using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private Animator transitionanim;
    [SerializeField]
    private Image image;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time/2.0f <= 0.005)
        {
            image.enabled = false;
        }
    }

    public void LoadCityView()
    {
        image.enabled = true;
        Debug.Log("Loading City View");
        StartCoroutine(LoadScene("City View"));
    }

    public void LoadStreetView()
    {
        image.enabled = true;
        Debug.Log("Loading Street View");
        StartCoroutine(LoadScene("Street View"));
    }

    IEnumerator LoadScene(string sceneName)
    {
        transitionanim.SetTrigger("End");

        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(sceneName);
        
        transitionanim.SetTrigger("Start");
    }
}
