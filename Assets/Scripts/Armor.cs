using UnityEngine;

public class Armor : MonoBehaviour
{
    public float maxCondition = 100;
    public float currentCondition;
    public float absorbPercentage;
    private bool isBroke;

    void Start()
    {
        currentCondition = maxCondition;
    }

    void Update()
    {
        if(currentCondition <= 0 && !isBroke)
        {
            isBroke = true;
            absorbPercentage = 0;
        }
    }

    public void DoDelta(float delta)
    {
        currentCondition = Mathf.Clamp(currentCondition - delta, 0, maxCondition);
    }

}
