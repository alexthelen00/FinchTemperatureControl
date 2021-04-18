using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Robot Temperature Retreiver
    // Description: User inputs how far the finch should travel, the finch records the temperature 
    //          and returns to the original location.
    // Application Type: Console
    // Author: Thelen, Alex
    // Dated Created: 4/10/2021
    // Last Modified: 4/18/2021
    //
    // **************************************************

    class Program
    {
        
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }

        
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.DarkRed;
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Main Menu                                 *
        /// *****************************************************************
        /// </summary>
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            Finch finchRobot = new Finch();

            do
            {
                DisplayScreenHeader("Main Menu");

                
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Run Program");
                Console.WriteLine("\tc) Instructions");
                Console.WriteLine("\td) Disconnect Finch Robot");
                Console.WriteLine("\te) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        RunProgram(finchRobot);
                        break;

                    case "c":
                        DisplayInstructions(finchRobot);
                        break;

                    case "d":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "e":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        



        #region APP

        static void RunProgram(Finch finchRobot)
        {

            
            DisplayScreenHeader("Enter Desired Finch Variables");

            Console.WriteLine("\tWelcome to the Finch Temperature Retreiver!");
            Console.WriteLine("\t");
            Console.WriteLine("\tThis application alows the user to input a time and speed for the finch to travel.");
            Console.WriteLine("\tOnce the finch has gathered the information it will travel to the specified loaction,");
            Console.WriteLine("\tgather the temperature, and return to the starting point.");
            Console.WriteLine("\t");


            DisplayContinuePrompt();



            int Distance;
            bool validResponse;

            do
            {


                validResponse = true;

                DisplayScreenHeader("\tTime To Travel");


                

                Console.WriteLine("\tEnter the time in seconds you would like the finch robot to travel.");


                if (!int.TryParse(Console.ReadLine(), out Distance))
                {
                    validResponse = false;

                    Console.WriteLine();
                    Console.WriteLine("It appears you entered an invalid number.");

                    Console.WriteLine();
                    Console.WriteLine("\tPress any key to continue.");
                    Console.ReadKey();


                }


            } while (!validResponse);



            


            int Speed;

            do
            {

                

                validResponse = true;

                DisplayScreenHeader("\tSpeed To Travel");


                

                Console.WriteLine("\tEnter the speed you would like the motors to turn (1-255)");


                if (!int.TryParse(Console.ReadLine(), out Speed))
                {
                    validResponse = false;

                    Console.WriteLine();
                    Console.WriteLine("It appears you entered an invalid number.");

                    Console.WriteLine();
                    Console.WriteLine("\tPress any key to continue.");
                    Console.ReadKey();


                }


            } while (!validResponse);


            
            

            Console.Clear();


            int time = Distance * 1000;
            finchRobot.setMotors(Speed, Speed);
            finchRobot.wait(time);
            finchRobot.setMotors(0, 0);

            finchRobot.setLED(0, 0, 240);
            finchRobot.wait(2000);

            int TemperatureSensorValue = 0;

            TemperatureSensorValue = ((int)finchRobot.getTemperature());

            finchRobot.setLED(0, 245, 0);
            finchRobot.wait(4000);

            finchRobot.setMotors(0, 255);
            finchRobot.wait(2000);
            finchRobot.setMotors(0, 0);
            finchRobot.wait(1000);
            finchRobot.setMotors(Speed, Speed);
            finchRobot.wait(time);
            finchRobot.setMotors(0, 0);


            Console.Clear();

            DisplayScreenHeader("Temperature Results");

            Console.WriteLine();
            Console.WriteLine($"\tTemperature at Current Location: {TemperatureSensorValue.ToString("n2")} degrees Celsius");


            Console.WriteLine($"\tTemperature at Current Location: {(TemperatureSensorValue * 9) / 5 + 32} degrees Fahrenheit");


           
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();

        }

        #endregion

        #region Instructions

        static void DisplayInstructions(Finch finchRobot)
        {

            DisplayScreenHeader("User Instructions");

            Console.WriteLine("\tWelcome to the Finch Temperature Retreiver!");
            Console.WriteLine("\t");
            Console.WriteLine("\tThis application alows the user to input a time and speed for the finch to travel.");
            Console.WriteLine("\tOnce the finch has gathered the information it will travel to the specified loaction,");
            Console.WriteLine("\tgather the temperature, and return to the starting point.");
            Console.WriteLine("\t");
            Console.WriteLine("\tThe user must first connect to the Finch using menu choice (a)");
            Console.WriteLine("\tNext the user should choose menu choice (b) and follow the steps to run the application.");
            Console.WriteLine("\tAfter the conditions are set when the program finally runs a blue light will turn on when");
            Console.WriteLine("\tthe temperature is being collected and then it will turn green before the robot returns to its position");
            Console.WriteLine("\tOnce the robot has returned to its first position the temperature at that location will be displayed");
            Console.WriteLine("\tThanks for Viewing!");





            DisplayMenuPrompt("Menu");

        }


        #endregion

        #region FINCH ROBOT MANAGEMENT

        /// <summary>
        /// *****************************************************************
        /// *               Disconnect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tAbout to disconnect from the Finch robot.");
            DisplayContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnect.");

            DisplayMenuPrompt("Main Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *                  Connect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayScreenHeader("Connect Finch Robot");

            Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
            DisplayContinuePrompt();

            robotConnected = finchRobot.connect();

            

            DisplayMenuPrompt("Main Menu");

            
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();

            return robotConnected;
        }

        #endregion

        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        
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
