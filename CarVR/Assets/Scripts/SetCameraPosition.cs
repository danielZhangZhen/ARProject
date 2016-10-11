using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SetCameraPosition : MonoBehaviour {



    void Start()
    {
        //if (SceneManager.GetActiveScene().name == "Audi_ZhanTing")
        //{
            if (BackController.Instance.cameraPos != Vector3.zero)
            {
                transform.position = BackController.Instance.cameraPos;
                transform.eulerAngles = BackController.Instance.eulerAngles;
            }
        //}
    }
}
