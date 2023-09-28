using System.Collections.Generic;
using System.Linq;

public class Parameters
{
    private List<Parameter> _parameters = new List<Parameter>();

    public Parameters(List<Parameter> parameters)
    {
        _parameters = parameters;
    }
    
    public Parameter Get(ParameterType parameterType) => _parameters.First(x => x.ParameterType == parameterType);
}