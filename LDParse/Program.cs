using LDParse;
const string filePath = @"file_path_here.";

Parser parser = new Parser(filePath);
parser.Parse();


Console.ReadKey();
