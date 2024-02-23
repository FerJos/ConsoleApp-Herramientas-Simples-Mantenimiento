// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Text;

void runCMD(string cmdLine, bool runAsRoot)
{
    Process proceso = new Process();
    proceso.StartInfo.FileName = "cmd.exe";
    proceso.StartInfo.Arguments = "/c" + cmdLine;
    if (runAsRoot)
    {
        proceso.StartInfo.Arguments += "& pause";
        proceso.StartInfo.Verb = "runas";
        proceso.StartInfo.UseShellExecute = true;
    }
    else
    {
        proceso.StartInfo.UseShellExecute = false;
    }

    try
    {
        proceso.Start();
        proceso.WaitForExit();
        Console.WriteLine("\n\n__________\nEl proceso a finalizado con el siguiente código: " + proceso.ExitCode);
    }
    catch (Exception errExc)
    {
        Console.WriteLine("Algo salió mal al intentar ejecutar el proceso. ERROR: " + errExc.Message);
    }
}

void runProgram()
{
    Console.WriteLine("Herramientas básicas para la reparación del SO.\n");
    Console.WriteLine("0) Salir del programa.");
    Console.WriteLine("1)* Comprobador de recursos. Comando sfc /SCANNOW.");
    Console.WriteLine("2)* CHKDSK - Corregir errores del disco.");
    Console.WriteLine("3)* CHKDSK - Corregir errores del disco, encontrar sectores defectuosos y recupera la información legible.");
    Console.WriteLine("4)* CHKDSK - Desmontar la unidad, corregir errores del disco, encontrar sectores defectuosos y recupera la información legible.");
    Console.WriteLine("5)* DISM - ScanHealth, CheckHealth, RestoreHealth y startComponentCleanup.");
    Console.WriteLine("6) Cleanmgr - Libera espacio en el disco.");
    Console.WriteLine("7) Abrir Protección del sistema.");
    Console.WriteLine("8) Abrir Panel de control.");
    Console.WriteLine("9) Abrir Información del sistema.");
    Console.WriteLine("10) Abrir Visor de eventos.");
    Console.WriteLine("11) Abrir Diagnóstico de memoria de Windows.");
    Console.WriteLine("12) Abrir Usuarios y grupos locales.");
    Console.WriteLine("13) ipConfig /All");
    Console.WriteLine("14) Apagar el equipo inmediatamente (Se perderá cualquier trabajo no guardado).");

    Console.WriteLine("\n(*) Herramienta que requiere permisos administrativos.");
    Console.Write("Elija una opción: ");

    string userInput = Console.ReadLine() ?? "";
    Console.Clear();

    switch (userInput)
    {
        case "1":
            Console.WriteLine("Esta herramienta requiere permisos elevados. Se abrirá una nueva ventana cuando el UAC otorgue los permisos requeridos.");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("sfc /SCANNOW", true);

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "2":
            Console.Write("Escriba la letra de la unidad para realizar la comprobación (dejar en blanco para seleccionar C automáticamente): ");
            userInput = Console.ReadLine() ?? "C";
            if (userInput.Length == 1 && userInput != " ")
            {
                userInput = userInput.ToUpper();
            }
            else
            {
                userInput = "C";
            }

            Console.WriteLine("Esta herramienta requiere permisos elevados. Se abrirá una nueva ventana cuando el UAC otorgue los permisos requeridos.");
            Console.WriteLine("No cierre esta ventana.");

            runCMD("chkdsk " + userInput + ": /f", true);

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "3":
            Console.Write("Escriba la letra de la unidad para realizar la comprobación (dejar en blanco para seleccionar C automáticamente): ");
            userInput = Console.ReadLine() ?? "C";
            if (userInput.Length == 1 && userInput != " ")
            {
                userInput = userInput.ToUpper();
            }
            else
            {
                userInput = "C";
            }

            Console.WriteLine("Esta herramienta requiere permisos elevados. Se abrirá una nueva ventana cuando el UAC otorgue los permisos requeridos.");
            Console.WriteLine("No cierre esta ventana.");

            runCMD("chkdsk " + userInput + ": /f /r /b", true);

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "4":
            Console.Write("Escriba la letra de la unidad para realizar la comprobación (dejar en blanco para seleccionar C automáticamente): ");
            userInput = Console.ReadLine() ?? "C";
            if (userInput.Length == 1 && userInput != " ")
            {
                userInput = userInput.ToUpper();
            }
            else
            {
                userInput = "C";
            }

            Console.WriteLine("Esta herramienta requiere permisos elevados. Se abrirá una nueva ventana cuando el UAC otorgue los permisos requeridos.");
            Console.WriteLine("No cierre esta ventana.");

            runCMD("chkdsk " + userInput + ": /f /r /b /x", true);

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "5":
            Console.WriteLine("Esta herramienta requiere permisos elevados. Se abrirá una nueva ventana cuando el UAC otorgue los permisos requeridos.");
            Console.WriteLine("No cierre esta ventana.");

            Console.WriteLine("# ScanHealth");
            runCMD("DISM.exe /Online /Cleanup-Image /ScanHealth", true);
            Console.WriteLine("# CheckHealth");
            runCMD("DISM.exe /Online /Cleanup-Image /CheckHealth", true);
            Console.WriteLine("# RestoreHealth");
            runCMD("DISM.exe /Online /Cleanup-Image /RestoreHealth", true);
            Console.WriteLine("# startComponentCleanup");
            runCMD("DISM.exe /Online /Cleanup-Image /startComponentCleanup", true);

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "6":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("cleanmgr", false);

            Console.Clear();
            runProgram();
            break;
        case "7":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("SystemPropertiesProtection.exe", false);

            Console.Clear();
            runProgram();
            break;
        case "8":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("control.exe", false);

            Console.Clear();
            runProgram();
            break;
        case "9":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("msinfo32.exe", false);

            Console.Clear();
            runProgram();
            break;
        case "10":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("eventvwr.msc", false);

            Console.Clear();
            runProgram();
            break;
        case "11":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("MdSched.exe", false);

            Console.Clear();
            runProgram();
            break;
        case "12":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("lusrmgr.msc", false);

            Console.Clear();
            runProgram();
            break;
        case "13":
            runCMD("ipconfig /all", false);

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "14":
            Console.WriteLine("El equipo se apagará en breve...");
            runCMD("shutdown /f /p", false);
            break;
        default:
            Console.WriteLine("Saliendo del programa...");
            break;
    }
}


Console.OutputEncoding = Encoding.UTF8;
runProgram();