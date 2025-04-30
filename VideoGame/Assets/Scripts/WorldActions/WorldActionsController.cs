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
        // Verifica se a variável está ativa
        if (VariableManager.Instance.GetVariable(variableName))
        {
            // Move o portão no eixo Y
            gate.position += new Vector3(0, speed * Time.deltaTime, 0);

            // Quando o portão atingir a altura desejada
            if (gate.position.y >= targetHeight)
            {
                // Desativa a variável para parar o movimento
                VariableManager.Instance.SetVariable(variableName, false);
            }
        }
    }


}
