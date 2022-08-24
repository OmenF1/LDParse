using LDParse;
const string filePath = @"C:\Users\Owen\Documents\Dev\LD Reverse Eng\Hungaroring-mclaren_720s_gt3-0-2022.08.21-21.35.29.ld";

Parser parser = new Parser(filePath);
parser.Parse();


Console.ReadKey();
