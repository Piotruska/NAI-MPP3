namespace SingleLayerNN;

public class Network
{
    public List<Layer> _layers = new ();
    public FileReader _fileReader = new ();
    public List<(double[], string)> _trainingData = new ();
    public List<(double[], string)> _testData = new ();
    public string _trainDataFilePath = "";
    public string _testDataFilePath = "";
    public double _learningRate = 0.5;
    public double _errorRate = 0.5;

    public void set_trainDataFilePath(string trainDataFilePath)
    {
        _trainDataFilePath = trainDataFilePath;
    }
    
    public void set_testDataFilePath(string testDataFileName)
    {
        _testDataFilePath = testDataFileName;
    }

    public void updateValues(double learningRate,double _errorRate)
    {
        _learningRate = learningRate;
    }
    public void GetDataFromFiles()
    {
        _trainingData = _fileReader.ReadDataFile(_trainDataFilePath);
        _testData = _fileReader.ReadDataFile(_testDataFilePath);
    }

    public void IniciateNetworkLayers()
    {
        
        _layers.Add(new Layer("Layer1",_trainingData,_testData,_errorRate,_learningRate));
        Console.WriteLine("Layer1 Created");
        foreach (var l in _layers)
        {
            l.IniciateLayer();
        }
    }

    public void TrainNetwork()
    {
        foreach (var l in _layers)
        {
            l.TrainPerceptrons();
        }
    }
    public string ClassifyVector(double[] x)
    {
        return _layers[0].ClassifyVector(x);
    }

    public double getNetworkEfficiency()
    {
        double correctCount = 0;
        foreach (var x in _testData)
        {
            if (ClassifyVector(x.Item1).Equals(x.Item2))
            {
                correctCount++;
            }
        }

        double correctAvg = correctCount / (_testData.Count);
        
        return correctAvg;
    }
    
    
}