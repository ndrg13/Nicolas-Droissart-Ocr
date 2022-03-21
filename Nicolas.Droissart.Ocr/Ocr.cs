using System.Reflection;
using Tesseract;

namespace Nicolas.Droissart.Ocr;

public class Ocr 
{ 
    private static string GetExecutingPath() 
    { 
        var executingAssemblyPath = Assembly.GetExecutingAssembly().Location; 
        var executingPath = Path.GetDirectoryName(executingAssemblyPath); 
        return executingPath; 
    } 
    public List<OcrResult> Read(IList<byte[]> images)
    {
        var tasks = new List<Task>();

        foreach (var image in images)
        {
            var task = Task.Run(() =>
            {
                using var engine = new TesseractEngine(Path.Combine(GetExecutingPath(), @"tessdata"), "fra", EngineMode.Default);
                using var pix = Pix.LoadFromMemory(image);
                var test = engine.Process(pix);
                var Text = test.GetText();
                var Confidence = test.GetMeanConfidence();
                
                return new OcrResult{Text = Text, Confidence = Confidence};
            });
            
            tasks.Add(task);
        }
        
        Task.WaitAll(tasks.ToArray());
        return tasks.Select(task => ((Task<OcrResult>) task).Result).ToList();
    }
}


