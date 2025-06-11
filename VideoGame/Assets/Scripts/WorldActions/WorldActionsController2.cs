using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class WorldActionsController2 : MonoBehaviour
{

    public Transform gateOne2;
    public Transform gateTwo2;
    public Transform activateBlocksBirdQuests;
    public Transform woodOne2;
    public Transform woodTwo2;
    public Transform woodThree2_1;
    public Transform woodThree2_2;

    private Dictionary<Transform, float> initialHeights = new Dictionary<Transform, float>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialHeights[gateOne2] = gateOne2.position.y;
        initialHeights[gateOne2] = gateTwo2.position.y;
        initialHeights[woodOne2] = woodOne2.position.y;
        initialHeights[woodTwo2] = woodTwo2.position.y;
        initialHeights[woodThree2_1] = woodThree2_1.position.y;
        initialHeights[woodThree2_2] = woodThree2_2.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        MoveObject(gateOne2, "birdPosition", 2f, 5f);
        MoveObject(gateTwo2, "openGateTwo2", 2f, 5f);
        ActivateObject(activateBlocksBirdQuests, "fimErro");
        ActivateObject(activateBlocksBirdQuests, "fimAcerto");
        MoveObject(woodOne2, "moveWoodOne2", 2f, 5f);
        MoveObject(woodTwo2, "moveWoodTwo2", 2f, 5f);
        MoveObject(woodThree2_1, "moveWoodThree2", 2f, 5f);
        MoveObject(woodThree2_2, "moveWoodThree2", 2f, 7f);
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
        //ResetGatePosition(finalGateOne);
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
