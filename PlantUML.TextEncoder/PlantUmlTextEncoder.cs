using System.IO.Compression;
using System.Text;

namespace PlantUML.TextEncoder;

public static class PlantUmlTextEncoder
{
    private const string Base64String   = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
    private const string PlantUmlString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-_";
        
    private static readonly List<byte> Base64DecodingAlphabet = Base64String.Select(Convert.ToByte).ToList();
    private static readonly List<byte> PlantUmlEncodingAlphabet = PlantUmlString.Select(Convert.ToByte).ToList();
    
    
    public static string Encode(string text)
    {
        var utf8Text = ConvertToUTF8(text);
        var compressedBytes = Deflate(utf8Text);
        
        var base64Encoded = Convert.ToBase64String(compressedBytes);
        var plantUmlEncoded = Base64ToPlantUml(base64Encoded);
        
        return plantUmlEncoded;
    }
    
    private static byte[] ConvertToUTF8(string diagramDescription)
    {
        var byteData = Encoding.Default.GetBytes(diagramDescription);
        return Encoding.Convert(Encoding.Default, Encoding.UTF8, byteData);
    }
    
    private static byte[] Deflate(byte[] inputData)
    {
        using var inputStream = new MemoryStream(inputData);
        using var outputStream = new MemoryStream();
        using var deflateStream = new DeflateStream(outputStream, CompressionMode.Compress);
        
        inputStream.CopyTo(deflateStream);
        deflateStream.Close();

        return outputStream.ToArray();
    }
    
    private static string Base64ToPlantUml(string base64)
    {
        string plantUml = "";
        
        foreach (var c in base64)
        {
            var index = Base64DecodingAlphabet.IndexOf((byte)c);
            var transformedByte = PlantUmlEncodingAlphabet[index];
            plantUml += Convert.ToChar(transformedByte);
        }

        return plantUml;
    }
}