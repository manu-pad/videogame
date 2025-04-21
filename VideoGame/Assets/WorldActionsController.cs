using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldActionsController : MonoBehaviour
{
    public Transform gateOne;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueFlagsManager.Instance.GetFlag("dicasTutorial"))
        {
            DicasController.Instance.SetDica("Pressione [TAB] para interagir!");
        }
        if (Input.GetKeyDown(KeyCode.Tab)) //se calhar não é a melhor forma mas manter assim
        {
            DicasController.Instance.SetDica("");
            DialogueFlagsManager.Instance.SetFlag("dicasTutorial", false);
        }


        if (DialogueFlagsManager.Instance.GetFlag("finalDialogoUm"))
        {
            gateOne.position += new Vector3(0, 5f, 0); // sobe 5 unidades no eixo Y
            DialogueFlagsManager.Instance.SetFlag("finalDialogoUm", false); // impede de subir de novo
        }

    }
   
}
