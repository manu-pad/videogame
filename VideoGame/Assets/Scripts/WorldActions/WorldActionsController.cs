using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldActionsController : MonoBehaviour
{
    public Transform gateOne;
    public Transform gateTwo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        MoveGate(gateOne, "openGateOne", 2f, 5f);
        MoveGate(gateTwo, "openGateTwo", 2f, 10f);


    }

    void MoveGate(Transform gate, string variableName, float speed, float targetHeight)
    {
        // Verifica se a vari�vel est� ativa
        if (VariableManager.Instance.GetVariable(variableName))
        {
            // Move o port�o no eixo Y
            gate.position += new Vector3(0, speed * Time.deltaTime, 0);

            // Quando o port�o atingir a altura desejada
            if (gate.position.y >= targetHeight)
            {
                // Desativa a vari�vel para parar o movimento
                VariableManager.Instance.SetVariable(variableName, false);
            }
        }
    }


}
