// See https://aka.ms/new-console-template for more information
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;

using (var engine = new Engine())
{

    string timeSlotFile = @"H:\Videos\Captures\Ghost in the Shell (1995)\timestamps.txt";
    string timeSlots = File.ReadAllText(timeSlotFile);
    string[] text = timeSlots.Split(Environment.NewLine);
    string[] timestamps;
    List<double> duration = new(); //always in pairs
    foreach (string line in text)
    {
        timestamps = line.Split('-');
        foreach (string timestamp in timestamps)
        {
            if (!String.Equals(timestamp, ""))
            {
                if (timestamp.Length == 5)
                {
                    duration.Add(TimeSpan.Parse(@"00:" + timestamp).TotalSeconds);
                }
                else if (timestamp.Length == 7)
                {
                    duration.Add(TimeSpan.Parse("0" + timestamp).TotalSeconds);
                }
                else 
                {
                    duration.Add(TimeSpan.Parse(timestamp).TotalSeconds);
                }
            }
        }
    }

    string file = @"H:\Videos\Captures\Ghost in the Shell (1995)\Ghost in the Shell (1995).mp4";
    var inputFile = new MediaFile { Filename = file };
    engine.GetMetadata(inputFile);
    var outputName = @"H:\Videos\Extracted\Ghost in the Shell (1995)\Ghost in the Shell (1995)-";
    var outputExtension = ".mp4";
    double Duration = inputFile.Metadata.Duration.TotalSeconds;
    int contador = 1;

    int i = 0;
    while (true)
    {
        var options = new ConversionOptions();
        var outputFile = new MediaFile(outputName + contador.ToString("000") + outputExtension);
        options.CutMedia(TimeSpan.FromSeconds(duration[i]), TimeSpan.FromSeconds(duration[i + 1] - duration[i]));
        engine.Convert(inputFile, outputFile, options);
        i += 2;
        if (i >= duration.Count - 1)
        {
            break;
        }
        contador++;
    }

}