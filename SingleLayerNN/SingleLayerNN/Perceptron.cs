using System;

namespace SingleLayerNN;

public class Perceptron
{
    public string Name { get; private set; }
    public string PositiveValue { get; private set; }
    private double[] weightVector;
    private double bias;
    public double learningRate;
    private int vectorLength;

    public Perceptron(string name, string positiveValue, int vectorLength, double learningRate)
    {
        Name = name;
        PositiveValue = positiveValue;
        this.vectorLength = vectorLength;
        this.learningRate = learningRate;
        weightVector = new double[vectorLength];
        InitializeWeights();
        bias = 0;  
    }

    private void InitializeWeights()
    {
        Random rand = new Random();
        for (int i = 0; i < vectorLength; i++)
        {
            weightVector[i] = rand.NextDouble() * 0.02 - 0.01; 
        }
    }

    public void Train((double[] inputs, string actualValue) data)
    {
        double[] inputs = data.inputs;
        string actualValue = data.actualValue;

        int desiredOutput = actualValue.Equals(PositiveValue) ? 1 : 0;
        int perceptronOutput = Classify(inputs);

        if (perceptronOutput != desiredOutput)
        {
            for (int i = 0; i < vectorLength; i++)
            {
                weightVector[i] += learningRate * (desiredOutput - perceptronOutput) * inputs[i];
            }
            bias += learningRate * (desiredOutput - perceptronOutput);
        }
    }

    public int Classify(double[] inputs)
    {
        double activation = bias;
        for (int i = 0; i < vectorLength; i++)
        {
            activation += inputs[i] * weightVector[i];
        }
        return activation >= 0 ? 1 : 0;
    }
}