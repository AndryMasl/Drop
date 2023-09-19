using System;


class Drop
{
    private double rnd;

    public string Model (string flag, double TP, double FP)
    {
        var rand = new Random();
        this.rnd = rand.NextDouble();
        if (flag == "Drop") //отдельная функция
        {
            if (this.rnd < TP) return "Drop";
            else return "LiePeople";
        }
        if (flag == "nonDrop")
        {
            if (this.rnd < FP) return "LieDrop";
            else return "People";
        }
        return "Failed";
    }

    public double Simulate(int num_clients, double proportion_drop, double TP, double FP)
    {
        int num_drop = Convert.ToInt32(num_clients * proportion_drop);
        int num_people = num_clients - num_drop;

        int num_drop2 = 0;
        int num_people2 = 0;
        string? result_model = null;

        for (int i = 0; i < num_drop; i++)
        {
            result_model = this.Model("Drop", TP, FP);
            if (result_model == "Drop") num_drop2++;
        }
        for (int i = 0; i < num_people; i++)
        {
            result_model = this.Model("nonDrop", TP, FP);
            if (result_model == "LieDrop") num_people2++;
        }
        double relation = Convert.ToDouble(num_people2) / (num_drop2 + num_people2);

        Console.WriteLine($"Drop: {num_drop2}");
        Console.WriteLine($"LieDrop: {num_people2}");
        Console.WriteLine($"Probability: {relation}");
        return relation;
    }

    public double SimulateTwice (int num_clients)
    {
        double relation = this.Simulate(num_clients, 0.0002, 0.9, 0.001);
        return relation = this.Simulate(num_clients, (1 - relation), 0.85, 0.02);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Drop drop = new Drop();
        drop.SimulateTwice(1000000);
    }
}

