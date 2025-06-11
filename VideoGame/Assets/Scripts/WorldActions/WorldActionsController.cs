using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class WorldActionsController : MonoBehaviour
{
    public Transform gateOne;
    public Transform gateTwo;
    public Transform gateThree;
    public Transform gateFour;
    public Transform gateFive;
    public Transform gateSix;
    public Transform finalGateOne;
    //level2
    public Transform gateOne2;
    public Transform gateTwo2;
    public Transform activateBlocksBirdQuests;
    public Transform woodOne2;
    public Transform woodTwo2;
    public Transform woodThree2_1;
    public Transform woodThree2_2;
    //level3
    public Transform woodTwo3;
    public Transform woodThree3;
    public Transform woodFour3;

    private Dictionary<Transform, float> initialHeights = new Dictionary<Transform, float>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialHeights[gateOne] = gateOne.position.y;
        initialHeights[gateTwo] = gateTwo.position.y;
        initialHeights[gateThree] = gateThree.position.y;
        initialHeights[gateFour] = gateFour.position.y;
        initialHeights[gateFive] = gateFive.position.y;
        initialHeights[gateSix] = gateSix.position.y;
        initialHeights[finalGateOne] = finalGateOne.position.y;
        //level2
        initialHeights[gateOne2] = gateOne2.position.y;
        initialHeights[gateOne2] = gateTwo2.position.y;
        initialHeights[woodOne2] = woodOne2.position.y;
        initialHeights[woodTwo2] = woodTwo2.position.y;
        initialHeights[woodThree2_1] = woodThree2_1.position.y;
        initialHeights[woodThree2_2] = woodThree2_2.position.y;
        //level3
        initialHeights[woodTwo3] = woodTwo3.position.y;
        initialHeights[woodThree3] = woodThree3.position.y;
        initialHeights[woodFour3] = woodFour3.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        MoveObject(gateOne, "openGateOne", 2f, 5f);
        MoveObject(gateTwo, "openGateTwo", 2f, 5f);
        MoveObject(gateThree, "openGateThree", 2f, 5f);
        MoveObject(gateFour, "openGateFour", 2f, 4.5f);
        MoveObject(gateFive, "openGateFive", 2f, 5f);
        MoveObject(gateSix, "openGateSix", 2f, 5f);
        MoveObject(finalGateOne, "finalGateOne", 2f, 5f);
        //level2
        MoveObject(gateOne2, "birdPosition", 2f, 5f);
        MoveObject(gateTwo2, "openGateTwo2", 2f, 5f);
        ActivateObject(activateBlocksBirdQuests, "fimErro");
        ActivateObject(activateBlocksBirdQuests, "fimAcerto");
        MoveObject(woodOne2, "moveWoodOne2", 2f, 5f);
        MoveObject(woodTwo2, "moveWoodTwo2", 2f, 5f);
        MoveObject(woodThree2_1, "moveWoodThree2", 2f, 5f);
        MoveObject(woodThree2_2, "moveWoodThree2", 2f, 7f);
        //level3
        MoveObject(woodTwo3, "moveWoodTwo3", 2f, 5f);
        MoveObject(woodThree3, "moveWoodThree3", 2f, 5f);
        MoveObject(woodFour3, "moveWoodFour3", 2f, 5f);

    }

    void MoveObject(Transform gate, string variableName, float speed, float moveAmount)
    {
        if (VariableManager.Instance.GetVariable(variableName))
        {
            // Altura inicial salva em uma variável estática ou dicionário, para cada portão
            if (!initialHeights.ContainsKey(gate))
            {
                initialHeights[gate] = gate.position.y;
            }

            float targetY = initialHeights[gate] + moveAmount;

            gate.position += new Vector3(0, speed * Time.deltaTime, 0);

            if (gate.position.y >= targetY)
            {
                VariableManager.Instance.SetVariable(variableName, false);
            }
        }
    }


    //ativa os objetos 
    void ActivateObject(Transform obj, string variableName)
    {
        if (VariableManager.Instance.GetVariable(variableName))
        {
            if (!obj.gameObject.activeSelf)
            {
                obj.gameObject.SetActive(true);
            }

            VariableManager.Instance.SetVariable(variableName, false);
        }
    }



    public void ResetGates()
    {
        ResetGatePosition(gateOne);
        ResetGatePosition(gateTwo);
        ResetGatePosition(gateThree);
        ResetGatePosition(gateFour);
        ResetGatePosition(gateFive);
        ResetGatePosition(gateSix);
        ResetGatePosition(finalGateOne);
    }

    private void ResetGatePosition(Transform gate)
    {
        if (initialHeights.ContainsKey(gate))
        {
            Vector3 pos = gate.position;
            pos.y = initialHeights[gate];
            gate.position = pos;
        }
    }

}
