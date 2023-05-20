using System.IO;
internal class Program
{
    class Product
    {
        public string Code {get; set;}
        public string Description {get; set;}
        public Decimal Price {get; set;}

        public Product (string c, string d, decimal p)
        {
            Code = c;
            Description = d;
            Price = p;
        }

    }

    class ProductDB
    {
        public static void SaveProducts(List<Product> products)
        {
            StreamWriter textOut = new StreamWriter(new FileStream("products.txt", FileMode.Create, FileAccess.Write));

            foreach(Product p in products)
            {
                textOut.Write(p.Code +"|");
                textOut.Write(p.Description +"|");
                textOut.Write(p.Price +"|");
                textOut.WriteLine();
            }
            textOut.Close();

        }

        public static void SaveProductsBinary(List<Product> products)
        {
            BinaryWriter binaryOut = new BinaryWriter(new FileStream("products.bin", FileMode.Create, FileAccess.Write));

            foreach(Product p in products)
            {
                binaryOut.Write(p.Code);
                binaryOut.Write(p.Description);
                binaryOut.Write(p.Price);
            }
            binaryOut.Close();

        }

        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            StreamReader textIn = new StreamReader(new FileStream("products.txt", FileMode.Open, FileAccess.Read));

            while (textIn.Peek() != -1)
            {
                string row = textIn.ReadLine();
                string[] columns = row.Split('|');
                Product product = new Product(columns[0], columns[1], Convert.ToDecimal(columns[2]));
                products.Add(product);
            }
            textIn.Close();
            return products;
        }

        public static List<Product> GetProductsBinary()
        {
            List<Product> products = new List<Product>();
            BinaryReader binaryIn = new BinaryReader(new FileStream("products.bin",FileMode.Open,FileAccess.Read));

            while (binaryIn.PeekChar() != -1)
            {
                Product product = new Product(binaryIn.ReadString(), binaryIn.ReadString(), binaryIn.ReadDecimal());
                products.Add(product);
            }
            return products;
        }
    }
    private static void Main(string[] args)
    {
        List<Product> products = new List <Product>();
        {
            products.Add (new Product("124543", "caja", 1222));
            products.Add (new Product("312312", "juego", 120));
            products.Add (new Product("312242", "celular", 8900));
            products.Add (new Product("657523", "microondas", 8916));
            products.Add (new Product("235423", "refrigerador", 7844));   
        }
        ProductDB.SaveProducts(products);
        ProductDB.SaveProductsBinary(products);
        
        List<Product> productsReaded;
        productsReaded = ProductDB.GetProducts();

        List<Product> productsReadedBinary;
        productsReadedBinary = ProductDB.GetProductsBinary();

        foreach (Product P in productsReaded)
        {
            Console.WriteLine(P.Code + " " + P.Description + " " + P.Price);
        }

        foreach (Product P in productsReadedBinary)
        {
            Console.WriteLine(P.Code + " " + P.Description + " " + P.Price);
        }
    }
}