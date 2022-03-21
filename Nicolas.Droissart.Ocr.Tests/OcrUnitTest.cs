using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Nicolas.Droissart.Ocr.Tests;

public class OcrUnitTest 
{ 
    [Fact] 
    public async Task ImagesShouldBeReadCorrectly() 
    { 
        var executingPath = GetExecutingPath(); 
        var images = new List<byte[]>(); 
        foreach (var imagePath in 
                 Directory.EnumerateFiles(Path.Combine(executingPath, "images"))) 
        { 
            var imageBytes = await File.ReadAllBytesAsync(imagePath); 
            images.Add(imageBytes); 
        } 
 
        var ocrResults = new Ocr().Read(images); 
 
        Assert.Equal(ocrResults[0].Text, "Dans de nombreuses technologies, il\nexiste des certifications. Le monde\nMicrosoft en propose de nombreuses pour\n");
        Assert.InRange(ocrResults[0].Confidence, 0.9, 1); 
        Assert.Equal(ocrResults[1].Text, @"ARRÉTONS DE PARLER DE\nDÉVELOPPEUR .NET !\n");
        Assert.InRange(ocrResults[1].Confidence, 0.9, 1); 
        Assert.Equal(ocrResults[2].Text, @"Le développeur .Net, ou plus largement\nsur les technologies Microsoft, n'est\npas un profil rare. La popularité de C#");
        Assert.InRange(ocrResults[2].Confidence, 0.9, 1); 
    } 
    private static string GetExecutingPath() 
    { 
        var executingAssemblyPath = 
            Assembly.GetExecutingAssembly().Location; 
        var executingPath = 
            Path.GetDirectoryName(executingAssemblyPath); 
        return executingPath; 
    } 
}