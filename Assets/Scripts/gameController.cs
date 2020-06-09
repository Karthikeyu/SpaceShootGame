using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{

    public GameObject uiPanel;
    private Animator animator;
    public GameObject jet;
    public Camera[] cameras;
    
    
    // Start is called before the first frame update
    void Start()
    {

        cameras[1].enabled = false;
        animator = uiPanel.GetComponent<Animator>();
        

       
    }

    // Update is called once per frame
    void Update()
    {
        if(jet == null)
        {
            Debug.Log(null);
            if (!cameras[1].enabled)
                cameras[1].enabled = true;
            animator.Play("GOTransfrom");
        }

    }

    public void quit()
    {

        Application.Quit();
    }


    public void restart()
    {
        
        

        animator.Play("GOTransformback");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
