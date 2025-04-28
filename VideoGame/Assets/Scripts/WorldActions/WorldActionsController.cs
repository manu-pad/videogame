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
        MoveGate(gateOne, "openGateOne");

    }

    void MoveGate(Transform gate, string variableName)
    {
        // Verifica se a variável está ativa
        if (VariableManager.Instance.GetVariable(variableName))
        {
            // Move o portão no eixo Y
            gate.position += new Vector3(0, 2f * Time.deltaTime, 0);

            // Quando o portão atingir a altura desejada
            if (gate.position.y >= 5f)
            {
                // Desativa a variável para parar o movimento
                VariableManager.Instance.SetVariable(variableName, false);
            }
        }
    }

}
