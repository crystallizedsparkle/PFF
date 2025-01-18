/*


    Palette File Format (.PFF)
    
    First 3 bytes are the file signature,
    the RGB color is 3 bytes

*/

// obtain the directory of the executable

namespace PFF;

class MainProgram
{

    static void Main(string[] args)
    {
        string filePath = AppDomain.CurrentDomain.BaseDirectory;

        // create the signature
        byte[] signature = new byte[] { (byte)'P', (byte)'F', (byte)'F' };
        // get the rgb info
        byte[] rgbValues = GetRGB();
        // create a new byte array with the length of both
        byte[] file = new byte[signature.Length + rgbValues.Length];

        // merge the byte arrays into the one declared above
        Array.Copy(signature, 0, file, 0, signature.Length);
        Array.Copy(rgbValues, 0, file, signature.Length, rgbValues.Length);

        Console.WriteLine("Input name of file to create (without extension)");
        string? fileName = Console.ReadLine();

        if (fileName == null)
            fileName = "nullstring"; // failsafe

        File.WriteAllBytes(filePath + fileName + ".pff", file);

        Console.WriteLine($"File \"{fileName}.pff\" created.");

        Thread.Sleep(2000);

        Console.WriteLine("Closing...");
    }

    static byte[] GetRGB()
    {
        // declare the names of RGB values
        string[] rgbNames = ["Red", "Green", "Blue"];

        // 3 bytes for each color
        byte[] rgb = new byte[3];
        // loop through
        for (int i = 0; i < rgb.Length; i++)
        {
            // reset these variables for the while loop
            string? input = string.Empty;
            bool isNumber = false;
            int result = 0;

            while (input == string.Empty || !isNumber || !(result >= 0 && result <= 255))
            {
                Console.WriteLine($"Input for RGB value {i + 1}, ({rgbNames[i]})");
                input = Console.ReadLine();

                if (input == null)
                    input = string.Empty; // failsafe

                // check if it is a number or not
                isNumber = int.TryParse(input, out result);

                if (!isNumber || !(result < 255 && result > 0))
                {
                    // remind user
                    Console.WriteLine("Must be a number 0-255");
                }
            }
            // add the byte
            rgb[i] = (byte)result;
        }
        return rgb;
    }
}
