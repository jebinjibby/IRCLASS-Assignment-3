using System;


    class Program
    {
        static void Main(string[] args)
        {
            double[] values = new double[2];
            Console.WriteLine("Welcome to our application \nType in the number corresponding to the type of section:\n1. Flat Bar\n2. Angle Section\n3. T-Section\n4. Bulb Section");
            String choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("You have chosen Flat Bar");
                    Console.WriteLine("Enter the height of the bar(in mm): ");
                    double height = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter the thickness of the bar(in mm): ");
                    double thickness = Convert.ToDouble(Console.ReadLine());
                    values = flatStiffener(height, thickness);
                    Console.WriteLine($"THE CROSS-SECTIONAL AREA IS {Math.Round(values[0],3)} sq.mm AND THE SECOND MOMENT OF AREA ABOUT NA IS {Math.Round(values[1],3)} cm^4");
                    runagainexception();
                    break;
                case "2":
                case "3":
                    if(choice == "2") {
                        Console.WriteLine("You have chosen Angle Section");
                    }
                    else
                    {
                        Console.WriteLine("You have chosen T-Section");
                    }
                    
                    Console.WriteLine("Enter the width of the flange(in mm): ");
                    double bf = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter the thickness of the flange(in mm): ");
                    double tf = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter the height of the web(in mm): ");
                    double hw = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter the thickness of the web(in mm): ");
                    double tw = Convert.ToDouble(Console.ReadLine());
                    values = angleStiffener(bf,tf,hw,tw);
                    Console.WriteLine($"THE CROSS-SECTIONAL AREA IS {Math.Round(values[0], 3)} sq.mm AND THE SECOND MOMENT OF AREA ABOUT NA IS {Math.Round(values[1], 3)} cm^4");
                    runagainexception();
                    break;
                case "4":
                    Console.WriteLine("You have chosen Bulb Section");
                    Console.WriteLine("Enter the height of the web(in mm): ");
                    double bulbHeight = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter the thickness of the web(in mm): ");
                    double bulbWidth = Convert.ToDouble(Console.ReadLine());
                    values = bulbSection(bulbHeight, bulbWidth);
                    Console.WriteLine("Your bulb section has been converted to an angled section to calculate area and MOI.");
                    Console.WriteLine($"THE CROSS-SECTIONAL AREA IS {Math.Round(values[0], 3)} sq.mm AND THE SECOND MOMENT OF AREA ABOUT NA IS {Math.Round(values[1], 3)} cm^4");
                    runagainexception();
                    break;
                default:
                    Console.WriteLine("Sorry !\nWe got into some error \nMaybe you did not enter the correct number or put an extra space before pressing enter.");
                    runagainexception();
                    break;
            }


            Console.ReadLine();
        }
        public static double[] flatStiffener(double height, double thickness)
        {
            double csa, MOI;
            double[] values = new double[2];

            csa = height * thickness;
            MOI = thickness * (Math.Pow(height, 3)) / 12;

            values[0] = csa;
            values[1] = MOI/10000;

            return values;
        }
        public static double[] angleStiffener(double bf, double tf, double hw, double tw)
        {
            double A1, A2, Y1, Y2, Y_bar, Ixx1, Ixx2, Ixx,csa;
            double[] moments = new double[2];

            A1 = bf * tf;
            A2 = hw * tw;
            Y1 = hw + (tf / 2);
            Y2 = hw / 2;
            csa = A1+A2;

            Y_bar = (((A1 * Y1) + (A2 * Y2)) / (A1 + A2));


            Ixx1 = ((tw * Math.Pow(hw, 3)) / 12) + ((Y_bar - (hw / 2)) * (Y_bar - (hw / 2)) * (hw * tw));
            Ixx2 = ((bf * Math.Pow(tf, 3)) / 12) + ((hw + (tf / 2) - Y_bar) * (hw + (tf / 2) - Y_bar) * (bf * tf));

            Ixx = Ixx1 + Ixx2;

            moments[0] = Math.Round(csa, 2);
            moments[1] = Math.Round(Ixx/10000, 2);

            return moments;
        }
        public static double[] bulbSection(double hw, double tw)
        {
            double angleLength, angleBreadth, webThickness, flangeThickness, alpha;
            double[] fromAngleStiffener = new double[2];

            if (hw <= 120)
            {
                alpha = 1.1 + Math.Pow((120 - hw), 2) / 3000;//alpha values are as per IRS rules
            }
            else
            {
                alpha = 1;
            }
            //All formulae derived from Rulebook
            angleLength = hw + 2 - hw / 9.2; //hw
            angleBreadth = alpha * (tw - 2 + (hw / 6.7));//bf
            flangeThickness = (hw / 9.2) - 2;//tf
            webThickness = tw;//tw

            fromAngleStiffener = angleStiffener(angleBreadth,flangeThickness,angleLength, webThickness);

            return fromAngleStiffener;
        }
        public static void runagainexception()
        {
            String runAgain;
            Console.Write("\nTo run the program again, enter 'y' \nIf you want to quit enter 'n':");
            runAgain = Console.ReadLine();
            if (runAgain == "y")
            {
                Main(null);
            }
            else if(runAgain == "n")
            {
                Console.WriteLine("\nThank You for using our application");
                Environment.Exit(0);
            }
            else
            {
              Console.WriteLine("\nPlease choose the correct option!!");
              runagainexception();
            }
        }
    }