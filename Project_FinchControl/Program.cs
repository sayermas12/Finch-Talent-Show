using System;
using System.IO;
using System.Collections.Generic;
using FinchAPI;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Control
    // Description: Talent Show
    // Application Type: Console
    // Author: Mason Sayer
    // Dated Created: 
    // Last Modified: 
    //
    // **************************************************

    class Program
    {
        private enum Command
        {
            none,
            moveforward,
            movebackward,
            stopmotors,
            wait,
            turnright,
            turnleft,
            ledon,
            ledoff,
            done

        }
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();

            DisplayMainMenu();

            DisplayClosingScreen();
        }
        static void SetTheme()
        {
            string dataPath = @"Data\Theme.txt";
            string foregroundColorString;
            ConsoleColor foregroundColor;

            foregroundColorString = File.ReadAllText(dataPath);

            Enum.TryParse(foregroundColorString, out foregroundColor);

            Console.ForegroundColor = foregroundColor;

        }
        static void DisplayMainMenu()
        {

            DisplayScreenHeader("Main Menu");
            //
            // 
            //
            Finch finchRobot = new Finch();
            bool finchRobotConnected = false;
            bool quitApplication = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Main Menu");
                //
                //get menu choice from user
                //

                Console.WriteLine("a) Connect Finch Robot");
                Console.WriteLine("b) Talent Show");
                Console.WriteLine("c) Data Recorder");
                Console.WriteLine("d) Alarm System");
                Console.WriteLine("e) User Programming");
                Console.WriteLine("f) Disconnect Finch Robot");
                Console.WriteLine("g) Login");
                Console.WriteLine("q) Quit");
                Console.Write("Enter Choice:");
                menuChoice = Console.ReadLine().ToUpper();

                //
                // process menu choice
                //

                switch (menuChoice)
                {
                    case "A":
                        finchRobotConnected = DisplayConnectFinchRobot(finchRobot);
                        break;
                    case "B":
                        DisplayTalentShow(finchRobot);
                        break;
                    case "C":
                        DisplayDataRecorder(finchRobot);
                        break;
                    case "D":
                        DisplayAlarmSystem(finchRobot);
                        break;
                    case "E":
                        DisplayUserProgramming(finchRobot);
                        break;
                    case "F":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;
                    case "G":
                        DisplayLoginOption();
                        break;
                    case "Q":
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a menu choice. ");
                        DisplayContinuePrompt();
                        break;

                }
            } while (!quitApplication);
        }
        #region Login

        static bool DisplayLoginOption(bool login = false)
        {
            string menuChoice;
            bool quitMenu = false;
            DisplayScreenHeader("Login/Register");

            do
            {
                Console.WriteLine("a) Login");
                Console.WriteLine("b) Register");
                Console.WriteLine("c) Quit");
                menuChoice = Console.ReadLine().ToUpper();

                switch (menuChoice)
                {
                    case "A":
                        DisplayLogin(login);
                        if (login == true)
                        {
                            break;
                        }
                        break;
                    case "B":
                        DisplayRegistration(login);
                        if (login == true)
                        {
                            break;
                        }
                        break;
                    case "C":
                        quitMenu = true;
                        break;
                    default:
                        Console.WriteLine("Please Enter a proper term");
                        break;
                }
            } while (!quitMenu);
            return (login);
        }
        static bool DisplayLogin(bool login)
        {
            bool userNameFound = false;
            bool passWordFound = false;
            string dataPath = @"C:\Users\hidde\Desktop\Desktop\CIT110\Finch Talent Show\Project_FinchControl\Data\Username.txt";
            DisplayScreenHeader("Login");

            File.ReadAllText(dataPath);
            do
            {
                Console.Write("Username:");
                string userName = Console.ReadLine();
                string Usernames = File.ReadAllText(dataPath);
                if (Usernames == userName)
                {
                    userNameFound = true;
                }
                else
                {
                    Console.WriteLine("Not a valid Username");
                }
            } while (!userNameFound);
            do
            {
                Console.Write("Password:");
                string passWord = Console.ReadLine();
                string Passwords = File.ReadAllText(dataPath);
                if (Passwords == passWord)
                {
                    login = true;
                }
                else
                {
                    Console.WriteLine("Not a valid Password");
                }
            } while (!passWordFound);

            
            DisplayContinuePrompt();


            return (login);
        }
        static bool DisplayRegistration(bool login)
        {

            string dataPath = @"C:\Users\hidde\Desktop\Desktop\CIT110\Finch Talent Show\Project_FinchControl\Data\Username.txt";
            string dataPath2 = @"C:\Users\hidde\Desktop\Desktop\CIT110\Finch Talent Show\Project_FinchControl\Data\Password.txt";
            string userName;
            string passWords;
            DisplayScreenHeader("Register");

            Console.WriteLine();
            Console.WriteLine("Please Enter new Username and Password");

            Console.Write("Username: ");
            userName = Console.ReadLine();
            File.AppendAllText(dataPath, userName);

            Console.Write("Password: ");
            passWords = Console.ReadLine();
            File.WriteAllText(dataPath2, passWords);

            login = true;

            return (login);
        }

        #endregion
        #region User Programming

        static void DisplayWriteUserProgrammingData(List<Command> commands)
        {
            string dataPath = @"Data\Data.txt";
            List<string> commandsString = new List<string>();

            DisplayScreenHeader("Write Commands to data file");

            //
            //create a list of command strings
            //

            foreach (Command command in commands)
            {
                commandsString.Add(command.ToString());
            }
            Console.WriteLine("Press any key to save");
            Console.ReadKey();

            File.WriteAllLines(dataPath, commandsString.ToArray());

            Console.WriteLine();
            Console.WriteLine("Commands Saved");

            DisplayContinuePrompt();
        }
        static void DisplayUserProgramming(Finch finchRobot)
        {
            string menuChoice;
            bool quitApplication = false;

            (int motorSpeed, int ledBrightness, int waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;


            List<Command> commands = new List<Command>();

            do
            {

                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //

                Console.WriteLine("a) Set Command Parameters");
                Console.WriteLine("b) Add Commands");
                Console.WriteLine("c) View Commands");
                Console.WriteLine("d) Execute Commands");
                Console.WriteLine("e) Save Commands to Text File");
                Console.WriteLine("f) Load Commands from text files");
                Console.WriteLine("q) Quit");
                Console.Write("Enter Choice:");
                menuChoice = Console.ReadLine().ToLower();



                //

                // process user menu choice

                //

                switch (menuChoice)

                {

                    case "a":
                        commandParameters = DisplayGetCommandParameters();
                        break;
                    case "b":
                        DisplayGetFinchCommands(commands);
                        break;
                    case "c":
                        DisplayFinchCommands(commands);
                        break;
                    case "d":
                        DisplayExecuteCommands(finchRobot, commands, commandParameters);
                        break;
                    case "e":
                        DisplayWriteUserProgrammingData(commands);
                        break;
                    case "f":
                        commands = DisplayReadUserProgrammingData();
                        break;
                    case "q":

                        quitApplication = true;

                        break;



                    default:

                        Console.WriteLine();

                        Console.WriteLine("Please enter a letter for the menu choice.");

                        DisplayContinuePrompt();

                        break;

                }



            } while (!quitApplication);

        }
        static void DisplayExecuteCommands(Finch finchRobot, List<Command> commands,(int motorSpeed, int ledBrightness, int waitSeconds) commandParameters)
        {
            int motorSpeed = commandParameters.motorSpeed;
            int ledBrightness = commandParameters.ledBrightness;
            int waitSeconds = commandParameters.waitSeconds * 1000;
            DisplayScreenHeader("Execute Finch Commands");

            // info and pause

            foreach (Command command in commands)
            {
                switch (command)
                {
                    case Command.none:
                        break;
                    case Command.moveforward:
                        finchRobot.setMotors(motorSpeed, motorSpeed);
                        break;
                    case Command.movebackward:
                        finchRobot.setMotors(-motorSpeed, -motorSpeed);
                        break;
                    case Command.stopmotors:
                        finchRobot.setMotors(0, 0);
                        break;
                    case Command.wait:
                        finchRobot.wait(waitSeconds);
                        break;
                    case Command.turnright:
                        finchRobot.setMotors(0,motorSpeed);
                        break;
                    case Command.turnleft:
                        finchRobot.setMotors(motorSpeed,0);
                        break;
                    case Command.ledon:
                        finchRobot.setLED(ledBrightness, ledBrightness, ledBrightness);
                        break;
                    case Command.ledoff:
                        finchRobot.setLED(0, 0, 0);
                        break;
                    case Command.done:
                        break;
                    default:
                        break;
                }
            }

            DisplayContinuePrompt();
        }
        static List<Command> DisplayReadUserProgrammingData()
        {
            string dataPath = @"Data\Data.txt";
            List<Command> commands = new List<Command>();
            string[] commandsString;

            DisplayScreenHeader("Read Commands from Data File");

            Console.WriteLine("Ready to read commands from the data file.");
            Console.WriteLine();

            commandsString = File.ReadAllLines(dataPath);

            Command command;
            foreach (string commandString in commandsString)
            {
                Enum.TryParse(commandString, out command);

                commands.Add(command);
            }

            Console.WriteLine();
            Console.WriteLine("Commands read from data file complete");

            DisplayContinuePrompt();

            return commands;
        }
        static void DisplayGetFinchCommands(List<Command> commands)
        {
            Command command = Command.none;
            string userResponse;

            DisplayScreenHeader("Finch Robot Commands");

            Console.WriteLine("Commands: moveforward, movebackward, stopmotors, wait, turnright, turnleft, ledon, ledoff, done");

            while (command != Command.done)
            {
                Console.Write("Enter Command:");
                userResponse = Console.ReadLine();
                Enum.TryParse(userResponse, out command);

                commands.Add(command);
            }
            // echo commands
            DisplayContinuePrompt();
        }
        static void DisplayFinchCommands(List<Command> commands)
        {
            DisplayScreenHeader("Display Finch Commands");

            foreach (Command command in commands)
            {
                Console.WriteLine(command);
            }
            DisplayContinuePrompt();
        }
        static (int motorSpeed, int ledBrightness, int waitSeconds) DisplayGetCommandParameters ()
        {
             (int motorSpeed, int ledBrightness, int waitSeconds) commandParameters;
        commandParameters.motorSpeed = 0;
        commandParameters.ledBrightness = 0;
        commandParameters.waitSeconds = 0;         

        DisplayScreenHeader("Command Parameters");

        //todo validate

        Console.Write("Enter Motor Speed [1-255]:");
        commandParameters.motorSpeed = int.Parse(Console.ReadLine());

        Console.Write("Enter LED brightness [1-255]:");
        commandParameters.ledBrightness = int.Parse(Console.ReadLine());

        Console.Write("Enter wait time [1-255]:");
        commandParameters.waitSeconds = int.Parse(Console.ReadLine());

                return commandParameters;
        

        }



    #endregion
        #region Data
    static void DisplayDataRecorder(Finch finchRobot)
        {
            double dataPointFrequency;
            int numberOfDataPoints;
            string userResponse;

            DisplayScreenHeader("Data Recorder");

            dataPointFrequency = DisplayGetDataPointFrequency();
            numberOfDataPoints = DisplayGetNumberOfDataPoints();

            //
            // instance the array
            //
            double[] temperatures = new double[numberOfDataPoints];
            double[] light = new double[numberOfDataPoints];

            DisplayGetData(numberOfDataPoints, dataPointFrequency, finchRobot, temperatures);
            DisplayGetLightData(numberOfDataPoints, dataPointFrequency, finchRobot, light);

            DisplayData(temperatures);
            DisplayLightData(light);


            DisplayMainMenuPrompt();
        }
        static int DisplayGetNumberOfDataPoints()
        {
            int numberOfDataPoints;

            DisplayScreenHeader("Number of Data Points");

            Console.Write("Enter Number of Data Points: ");
            int.TryParse(Console.ReadLine(), out numberOfDataPoints);

            DisplayContinuePrompt();

            return numberOfDataPoints;
        }
        static double DisplayGetDataPointFrequency()
        {
            double dataPointFrequency;
            string userResponse;

            DisplayScreenHeader("Data Point Frequency");

            Console.Write("Enter Data Point Frequency: ");

            double.TryParse(Console.ReadLine(), out dataPointFrequency);

            DisplayContinuePrompt();

            return dataPointFrequency;
        }
    #endregion
        #region Temp
    static void DisplayData(double[] temperatures)
        {
            DisplayScreenHeader("Temperature Data");

            for (int index = 0; index < temperatures.Length; index++)
            {
                Console.WriteLine($"Temperature {index + 1}: {temperatures[index]}");

            }

            DisplayContinuePrompt();
        }

        static void DisplayGetData(int numberOfDataPoints, double datapointFrequency, Finch finchRobot, double[] temperatures)
        {

            DisplayScreenHeader("Get Temperature Data");

            //provide the user info and prompt
            for (int index = 0; index < numberOfDataPoints; index++)
            {
                //
                // record data
                //
                temperatures[index] = finchRobot.getTemperature();
                int milliseconds = (int)(datapointFrequency * 1000);
                finchRobot.wait(milliseconds);

                //
                // echo data
                //
                Console.WriteLine($"Temperature {index + 1}: {temperatures[index]}");

            }

            DisplayContinuePrompt();
        }
        #endregion
        #region Light
        static void DisplayLightData(double[] light)
        {
            DisplayScreenHeader("Light Data");

            for (int index = 0; index < light.Length; index++)
            {
                Console.WriteLine($"Light {index + 1}: {light[index]}");

            }

            DisplayContinuePrompt();
        }

        static void DisplayGetLightData(int numberOfDataPoints, double datapointFrequency, Finch finchRobot, double[] light)
        {

            DisplayScreenHeader("Get Light Data");
            //provide the user info and prompt
            for (int index = 0; index < numberOfDataPoints; index++)
            {

                //record data


                light[index] = finchRobot.getRightLightSensor();
                int milliseconds = (int)(datapointFrequency * 1000);
                finchRobot.wait(milliseconds);

                //
                // echo data
                //
                Console.WriteLine($"Light {index + 1}: {light[index]}");

            }

            DisplayContinuePrompt();
        }
        #endregion
        #region Alarm
        static void DisplayAlarmSystem(Finch finchRobot)
        {
            string alarmType;
            int maxSeconds;
            double threshold;
            bool thresholdExceeded;
            DisplayScreenHeader("Alarm System");

            alarmType = DisplayGetAlarmType();
            maxSeconds = DisplayGetMaxSeconds();
            threshold = DisplayGetThreshold(finchRobot, alarmType);
            //warn user and pause

            thresholdExceeded = MonitorCurrentLightLevel(finchRobot, threshold, maxSeconds, alarmType);

            if (thresholdExceeded)
            {
                if (alarmType == "light")
                {
                    Console.WriteLine("Maximum  light level Exceeded");
                }
                else
                {
                    Console.WriteLine("Maximum Temperature level Exceeded");
                }
            }
            else
            {
                Console.WriteLine("Maximum Monitoring Time Exceeded");
            }
            DisplayContinuePrompt();
        }
        static bool MonitorCurrentLightLevel(Finch finchRobot, double threshold, int maxSeconds, string alarmType)
        {
            bool thresholdExceeded = false;
            double currentTemperatureLevel;
            double seconds = 0;
            switch (alarmType)
            {
                case "light":
                    while (!thresholdExceeded && seconds <= maxSeconds)
                    {
                        currentTemperatureLevel = finchRobot.getRightLightSensor();
                        DisplayScreenHeader("Monitoring Light Levels");
                        Console.WriteLine($"Maximum Light Level: {threshold}");
                        Console.WriteLine($"Current Light Level: {currentTemperatureLevel}");

                        if (currentTemperatureLevel > threshold) thresholdExceeded = true;

                        finchRobot.wait(500);
                        seconds += 0.5;
                    }
                    break;
                case "temperature":
                    while (!thresholdExceeded && seconds <= maxSeconds)
                    {
                        currentTemperatureLevel = finchRobot.getTemperature();
                        DisplayScreenHeader("Monitoring Temperature Levels");
                        Console.WriteLine($"Maximum Temperature Level: {threshold}");
                        Console.WriteLine($"Current Temperature Level: {currentTemperatureLevel}");

                        if (currentTemperatureLevel > threshold) thresholdExceeded = true;

                        finchRobot.wait(500);
                        seconds += 0.5;
                    }

                    break;

                default:
                    Console.WriteLine("This isn't even possible to get here, how did you do that?");
                    DisplayContinuePrompt();
                    break;
            }



            return thresholdExceeded;
        }
        static int DisplayGetMaxSeconds()
        {
            // todo VALIDATE USER INPUT
            Console.Write("Seconds to Monitor: ");
            return int.Parse(Console.ReadLine());
        }
        static double DisplayGetThreshold(Finch dave, string alarmType)
        {
            double threshold = 0;
            DisplayScreenHeader("Threshold Value");

            switch (alarmType)
            {
                case "light":
                    Console.WriteLine($"Current Light Level {dave.getRightLightSensor()}");
                    Console.Write("Enter Maximum Light Level [0 - 255]: ");
                    threshold = double.Parse(Console.ReadLine());
                    break;
                case "temperature":
                    Console.WriteLine($"Current Temperature Level {dave.getTemperature()}");
                    Console.Write("Enter Maximum temperature level: ");
                    threshold = double.Parse(Console.ReadLine());
                    break;

                default:
                    Console.WriteLine("Please enter either light or temperature");
                    break;
            }

            DisplayContinuePrompt();
            return threshold;
        }
        static string DisplayGetAlarmType()
        {
            string AlarmType;
            Console.WriteLine("Alarm Type [light or temperature]");
            AlarmType = Console.ReadLine();
            switch (AlarmType)
            {
                case "light":
                    break;
                case "temperature":
                    break;
                default:
                    Console.WriteLine("Please Enter light or temperature");
                    break;
            }
            return AlarmType;
        }
        #endregion
        #region Menu Methods

        static void DisplayTalentShow(Finch finchRobot)
        {
            DisplayScreenHeader("Talent Show");

            Console.WriteLine("Finch Robot is ready to show you their talent.");
            DisplayContinuePrompt();

            finchRobot.setMotors(255, 0);
            finchRobot.wait(250);
            finchRobot.setMotors(-255, 0);
            finchRobot.wait(250);
            finchRobot.setMotors(255, 0);
            finchRobot.wait(1000);

            finchRobot.setLED(0, 0, 255);

            finchRobot.setMotors(0, -255);
            finchRobot.wait(250);
            finchRobot.setMotors(0, 255);
            finchRobot.wait(250);
            finchRobot.setMotors(0, -255);
            finchRobot.wait(1000);

            finchRobot.setLED(0, 255, 0);

            finchRobot.setMotors(255, 255);
            finchRobot.wait(700);
            finchRobot.setMotors(-255, -255);
            finchRobot.wait(250);
            finchRobot.setMotors(255, 255);
            finchRobot.wait(250);
            finchRobot.setMotors(-255, -255);
            finchRobot.wait(250);
            finchRobot.setMotors(255, 255);
            finchRobot.wait(250);
            finchRobot.setMotors(-255, -255);
            finchRobot.wait(300);
            finchRobot.setMotors(0, 0);
            finchRobot.wait(10);
            finchRobot.setMotors(-255, -255);
            finchRobot.wait(1000);

            finchRobot.setLED(255, 0, 0);

            finchRobot.setMotors(0, 0);
            finchRobot.wait(30);

            finchRobot.setMotors(0, -255);
            finchRobot.wait(500);
            finchRobot.setMotors(0, 255);
            finchRobot.wait(500);
            finchRobot.setMotors(0, -255);
            finchRobot.wait(250);
            finchRobot.setMotors(0, 255);
            finchRobot.wait(250);
            finchRobot.setMotors(0, -255);
            finchRobot.wait(1000);

            finchRobot.setLED(0, 0, 255);

            finchRobot.setMotors(0, 0);
            finchRobot.wait(30);

            finchRobot.setMotors(255, 0);
            finchRobot.wait(500);
            finchRobot.setMotors(-255, 0);
            finchRobot.wait(500);
            finchRobot.setMotors(255, 0);
            finchRobot.wait(250);
            finchRobot.setMotors(-255, 0);
            finchRobot.wait(250);
            finchRobot.setMotors(255, 0);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);
            //********************************************

            finchRobot.setLED(0, 255, 0);

            finchRobot.noteOn(293);
            finchRobot.wait(250);
            finchRobot.noteOn(349);
            finchRobot.wait(250);
            finchRobot.noteOn(587);
            finchRobot.wait(1000);

            finchRobot.noteOn(293);
            finchRobot.wait(250);
            finchRobot.noteOn(349);
            finchRobot.wait(250);
            finchRobot.noteOn(587);
            finchRobot.wait(1000);

            finchRobot.noteOn(659);
            finchRobot.wait(700);
            finchRobot.noteOn(698);
            finchRobot.wait(250);
            finchRobot.noteOn(659);
            finchRobot.wait(250);
            finchRobot.noteOn(698);
            finchRobot.wait(250);
            finchRobot.noteOn(659);
            finchRobot.wait(250);
            finchRobot.noteOn(523);
            finchRobot.wait(300);
            finchRobot.noteOn(440);
            finchRobot.wait(1000);

            finchRobot.noteOff();
            finchRobot.wait(70);

            finchRobot.noteOn(440);
            finchRobot.wait(500);
            finchRobot.noteOn(293);
            finchRobot.wait(500);
            finchRobot.noteOn(349);
            finchRobot.wait(250);
            finchRobot.noteOn(392);
            finchRobot.wait(250);
            finchRobot.noteOn(440);
            finchRobot.wait(1000);
            finchRobot.noteOff();
            finchRobot.wait(50);
            finchRobot.noteOn(440);
            finchRobot.wait(500);
            finchRobot.noteOn(293);
            finchRobot.wait(1000);
            finchRobot.noteOff();
            finchRobot.wait(500);

            DisplayContinuePrompt();
        }

        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("Ready to disconnect the Rinch Robot.");
            DisplayContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine();
            Console.WriteLine("Finch Robot is now disconnected.");

            DisplayContinuePrompt();
        }

        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            bool finchRobotConnected = false;

            DisplayScreenHeader("Connect to Finch Robot");

            Console.WriteLine("Ready to connect to the Finch robot. Be sure to connect the usb cable to the robot and computer");
            DisplayContinuePrompt();

            finchRobotConnected = finchRobot.connect();
        
            if (finchRobotConnected)
            {
                finchRobot.setLED(0, 255, 0);
                finchRobot.noteOn(15000);
                finchRobot.wait(1000);
                finchRobot.noteOff();

                Console.WriteLine();
                Console.WriteLine("Finch robot is now connected");
                finchRobotConnected = true;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Unable to connect to the Finch robot");
            }

            DisplayContinuePrompt();

            return finchRobotConnected;
        }

        /// <summary>
        /// display welcome screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }
        #endregion
        #region HELPER METHODS

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        static void DisplayMainMenuPrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}
