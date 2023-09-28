public class Parameter
{
    public ParameterType ParameterType { get; private set; }
    public float Value { get; private set; }

    public Parameter(ParameterType parameterType, float value)
    {
        ParameterType = parameterType;
        Value = value;
    }
    
    public void Inc(float value)
    {
        Value += value;
    }
}