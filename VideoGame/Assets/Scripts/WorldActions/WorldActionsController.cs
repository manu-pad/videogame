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

    }

    // Update is called once per frame
    void Update()
    {
        MoveGate(gateOne, "openGateOne", 2f, 5f);
        MoveGate(gateTwo, "openGateTwo", 2f, 5f);
        MoveGate(gateThree, "openGateThree", 2f, 5f);
        MoveGate(gateFour, "openGateFour", 2f, 4.5f);
        MoveGate(gateFive, "openGateFive", 2f, 5f);
        MoveGate(gateSix, "openGateSix", 2f, 5f);
        MoveGate(finalGateOne, "finalGateOne", 2f, 5f);

    }

    void MoveGate(Transform gate, string variableName, float speed, float moveAmount)
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
