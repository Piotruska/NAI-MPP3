namespace SingleLayerNN;

public class Layer
{
    private string Name {get; set;}
    private List<Perceptron> _perceptrons = new List<Perceptron>();
    private readonly List<(double[], string)> _trainingData;
    private readonly List<(double[], string)> _testData;
    private int _numberOfPreceptrons = 0;
    private int[] _output;
    private double _errorThreashold;
    public double _learningRate {get; set;}

    public Layer(string name,List<(double[], string)> trainingData, List<(double[], string)> testData,double errorThreashold,double learningRate)
    {
        this.Name = name;
        _trainingData = trainingData;
        _testData = testData;
        _errorThreashold = errorThreashold;

    }
    
    public void IniciateLayer()
    {
        List<string> l = new List<string>();
        foreach (var x in _trainingData)
        {
            if (!l.Contains(x.Item2))
            {
                _perceptrons.Add(new Perceptron(("Perceptron - "+x.Item2),x.Item2,x.Item1.Length,0.5));
                Console.WriteLine("Perceptron - "+x.Item2 + " Created");
                l.Add(x.Item2);
                _numberOfPreceptrons++;
            }
        }
        _output = new int[_numberOfPreceptrons];

    }

    public void TrainPerceptrons()
    {
        while (!TestError())
        {
            foreach (var x in _trainingData)
            {
                foreach (var p in _perceptrons)
                {
                    p.Train(x);
                }
            }
        }
        
        
    }

    public bool TestError()
    {
        double errorCount = 0;
        foreach (var x in _testData)
        {
            if (!ClassifyVector(x.Item1).Equals(x.Item2))
            {
                errorCount++;
            }
        }

        errorCount = errorCount / (_testData.Count);
        Console.WriteLine("Error: "+ errorCount);
        return errorCount < _errorThreashold;
    }

    public string ClassifyVector(double[] x)
    {
        
        _output = new int[_numberOfPreceptrons];
        int index = 0;
        foreach (var p in _perceptrons)
        {
            _output[index]= p.Classify(x);
            index++;
        }
        if(1 != _output.Sum())
        {
            return "Error";
        }
        else
        {
            index = 0;
            foreach (var i in _output)
            {
                if (i == 1)
                {
                    break;
                }
                index++;
            }
            return _perceptrons[index].PositiveValue;
        }
        
    }
}