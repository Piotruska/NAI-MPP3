// using SingleLayerNN;
//
// Network network = new Network();
// network.set_trainDataFilePath("C:\\Users\\piotr\\Desktop\\GitHub\\NAI\\NAI-MPP3\\SingleLayerNN\\SingleLayerNN\\lang.train.csv");
// network.set_testDataFilePath("C:\\Users\\piotr\\Desktop\\GitHub\\NAI\\NAI-MPP3\\SingleLayerNN\\SingleLayerNN\\lang.test.csv");
// network.GetDataFromFiles();
// network.IniciateNetworkLayers();
// network.TrainNetwork();
// Console.WriteLine("Network Efficiency: "+network.getNetworkEfficiency());

using System;
using SingleLayerNN;  // Ensure this namespace includes the Network, Layer, and FileReader classes

public class Program
{
    private static Network network = new Network(0.1,0.05);
    private static FileReader fileReader = new FileReader();

    public static void Main(string[] args)
    {
        InitializeNetwork();
        network._learningRate = 0.1;  
        network._errorRate = 0.05;  
        bool exitApp = false;
        while (!exitApp)
        {
            Console.Clear();
            Console.WriteLine("Neural Network Console Application");
            Console.WriteLine("1. Train Network");
            Console.WriteLine("2. Show Accuracy");
            Console.WriteLine("3. Change Learning Rate and Error Threshold");
            Console.WriteLine("4. Reset Network");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    TrainNetwork();
                    break;
                case "2":
                    ShowAccuracy();
                    break;
                case "3":
                    ChangeLearningRateAndErrorThreshold();
                    break;
                case "4":
                    network = new Network(network._learningRate,network._errorRate);
                    InitializeNetwork();
                    break;
                case "5": exitApp = true;
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    private static void InitializeNetwork()
    {
        network._fileReader = new FileReader();
        network._trainDataFilePath = "C:\\Users\\piotr\\Desktop\\GitHub\\NAI\\NAI-MPP3\\SingleLayerNN\\SingleLayerNN\\lang.train.csv";  
        network._testDataFilePath = "C:\\Users\\piotr\\Desktop\\GitHub\\NAI\\NAI-MPP3\\SingleLayerNN\\SingleLayerNN\\lang.test.csv";
        network.GetDataFromFiles();
        network.IniciateNetworkLayers();
    }

    private static void TrainNetwork()
    {
        Console.WriteLine("Training started...");
        network.TrainNetwork();  // Assuming Train is a method in Network that performs training
        Console.WriteLine("Training completed!");
    }

    private static void ShowAccuracy()
    {
        double accuracy = network.getNetworkEfficiency();  // Assuming CalculateAccuracy calculates and returns the accuracy
        Console.WriteLine($"Last calculated accuracy: {accuracy:P2}");
    }

    private static void ChangeLearningRateAndErrorThreshold()
    {
        Console.Write($"Enter new learning rate (current: {network._learningRate:N2}): ");
        if (double.TryParse(Console.ReadLine(), out double newLearningRate))
        {
            
        }
        else
        {
            Console.WriteLine("Invalid input for learning rate.");
        }

        Console.Write($"Enter new error threshold (current: {network._errorRate:N2}): ");
        if (double.TryParse(Console.ReadLine(), out double newErrorThreshold))
        {
            
        }
        else
        {
            Console.WriteLine("Invalid input for error threshold.");
        }

        network.updateValues(newLearningRate,newErrorThreshold);
    }
}
