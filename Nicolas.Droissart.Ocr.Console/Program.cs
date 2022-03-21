using Nicolas.Droissart.Ocr;

var ocr = new Ocr();
var images = new List<byte[]>();


foreach (var input_path in args)
{
    images.Add(File.ReadAllBytes(input_path));
}

var ocrResults = ocr.Read(images);

foreach (var ocrResult in ocrResults) 
{ 
    System.Console.WriteLine($"Confidence :{ocrResult.Confidence}"); 
    System.Console.WriteLine($"Text :{ocrResult.Text}"); 
} 
