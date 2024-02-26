// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Text;

void runCMD(string cmdLine)
{
    Process proceso = new Process();
    proceso.StartInfo.FileName = "cmd.exe";
    proceso.StartInfo.WorkingDirectory = Environment.GetEnvironmentVariable("USERPROFILE");
    proceso.StartInfo.Arguments = "/c" + cmdLine;
    proceso.StartInfo.UseShellExecute = false;

    try
    {
        proceso.Start();
        proceso.WaitForExit();
        Console.WriteLine("\n\n__________\nEl proceso a finalizado con el siguiente código: " + proceso.ExitCode);
    }
    catch (Exception errExc)
    {
        Console.WriteLine("\n\n__________\nEl proceso no se pudo iniciar con el siguiente error: " + errExc.Message);
    }
}

void runAdminCMD(string cmdLine)
{
    Process proceso = new Process();
    proceso.StartInfo.FileName = "cmd.exe";
    proceso.StartInfo.WorkingDirectory = Environment.GetEnvironmentVariable("SystemRoot") + @"\System32";
    proceso.StartInfo.Arguments = "/c" + cmdLine + "& pause";
    proceso.StartInfo.Verb = "runas";
    proceso.StartInfo.UseShellExecute = true;

    try
    {
        proceso.Start();
        proceso.WaitForExit();
        Console.WriteLine("\n\n__________\nEl proceso a finalizado con el siguiente código: " + proceso.ExitCode);
    }
    catch (Exception errExc)
    {
        Console.WriteLine("\n\n__________\nEl proceso no se pudo iniciar con el siguiente error: " + errExc.Message);
    }
}

string getLetterVolume()
{
    Console.Write("Escriba la letra de la unidad a la que desea trabajar (sin incluir los dos puntos): ");

    string userInput = Console.ReadLine() ?? "c";
    if (userInput == " " || userInput.Length != 1)
    {
        userInput = "C";
    }
    else
    {
        userInput = userInput.ToUpper();
    }

    return userInput;
}

string getDecision(string question)
{
    Console.Write($"{question}" + " (S/N): ");

    string userInput = Console.ReadLine() ?? "";
    userInput = userInput.ToUpper();

    if (userInput != "S" &&  userInput != "N")
    {
        return getDecision(question);
    }
    return userInput;
}

void runProgram()
{
    Console.WriteLine("ConsoleAppTITools1 - Herramientas para el mantenimiento y/o reparación.\n");
    Console.WriteLine("!) Documentación de los comandos de Windows.");
    Console.WriteLine("0) Salir del programa.");
    Console.WriteLine("1)* sfc - Comprobador de recursos.");
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
    Console.WriteLine("13) Microsoft Management Console.");
    Console.WriteLine("14)* [EN DESUSO] Windows Management Instrumentation Console");
    Console.WriteLine("15) Administración de discos.");
    Console.WriteLine("16)* DISKPART.");
    Console.WriteLine("17) Herramienta de diagnóstico de DirectX.");
    Console.WriteLine("18)* Asistente para agregar hardware.");
    Console.WriteLine("19) Windows Store Reset.");
    Console.WriteLine("20) ipConfig /All");
    Console.WriteLine("21) Salir del programa y apagar el equipo inmediatamente (Se perderá cualquier trabajo no guardado).");

    Console.WriteLine("\n   (*) Herramienta que requiere permisos administrativos.");
    Console.Write("Elija una opción: ");

    string userInput = Console.ReadLine() ?? "0";
    if (userInput == "" || userInput.ToLower() == "exit")
    {
        userInput = "0";
    }
    Console.Clear();

    switch (userInput)
    {
        case "!":
            Console.WriteLine("Está aplicación de consola ejecuta las herramientas integradas del sistema operativo ©Microsoft Windows para realizar mantenimiento y/o reparación al equipo.\nA continuación se muestra la documentación de algunos de los comandos utilizados.\n");
            Console.WriteLine("* sfc - https://learn.microsoft.com/es-mx/windows-server/administration/windows-commands/sfc");
            Console.WriteLine("* chkdsk - https://learn.microsoft.com/es-mx/windows-server/administration/windows-commands/chkdsk?tabs=event-viewer");
            Console.WriteLine("* DISM - https://learn.microsoft.com/es-mx/windows-hardware/manufacture/desktop/what-is-dism?view=windows-11");
            Console.WriteLine("* cleanmgr - https://learn.microsoft.com/es-mx/windows-server/administration/windows-commands/cleanmgr");
            Console.WriteLine("* ipconfig - https://learn.microsoft.com/es-mx/windows-server/administration/windows-commands/ipconfig");
            Console.WriteLine("* shutdown - https://learn.microsoft.com/es-mx/windows-server/administration/windows-commands/shutdown");

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "0":
            Console.WriteLine("Saliendo del programa...");
            break;
        case "1":
            userInput = getDecision("¿Desea que el comprobador de recursos intente reparar los archivos con problemas?");

            Console.WriteLine("Esta herramienta requiere permisos elevados. Se abrirá una nueva ventana cuando el UAC otorgue los permisos requeridos.");
            Console.WriteLine("No cierre esta ventana.");
            if (userInput == "S")
            {
                Console.WriteLine("# SCANNOW");
                runAdminCMD("sfc.exe /SCANNOW");
            }
            else
            {
                Console.WriteLine("# VERIFYONLY");
                runAdminCMD("sfc.exe /VERIFYONLY");
            }

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "2":
            userInput = getLetterVolume();

            Console.WriteLine("Esta herramienta requiere permisos elevados. Se abrirá una nueva ventana cuando el UAC otorgue los permisos requeridos.");
            Console.WriteLine("No cierre esta ventana.");

            runAdminCMD("chkdsk.exe " + userInput + ": /f");

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "3":
            userInput = getLetterVolume();

            Console.WriteLine("Esta herramienta requiere permisos elevados. Se abrirá una nueva ventana cuando el UAC otorgue los permisos requeridos.");
            Console.WriteLine("No cierre esta ventana.");

            runAdminCMD("chkdsk.exe " + userInput + ": /f /r /b");

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "4":
            userInput = getLetterVolume();

            Console.WriteLine("Esta herramienta requiere permisos elevados. Se abrirá una nueva ventana cuando el UAC otorgue los permisos requeridos.");
            Console.WriteLine("No cierre esta ventana.");

            runAdminCMD("chkdsk.exe " + userInput + ": /f /r /b /x");

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "5":
            Console.WriteLine("Esta herramienta requiere permisos elevados. Se abrirá una nueva ventana cuando el UAC otorgue los permisos requeridos.");
            Console.WriteLine("No cierre esta ventana.");

            Console.WriteLine("# ScanHealth. Paso 1/4.");
            runAdminCMD("DISM.exe /Online /Cleanup-Image /ScanHealth");
            Console.WriteLine("# CheckHealth. Paso 2/4.");
            runAdminCMD("DISM.exe /Online /Cleanup-Image /CheckHealth");
            Console.WriteLine("# RestoreHealth. Paso 3/4.");
            runAdminCMD("DISM.exe /Online /Cleanup-Image /RestoreHealth");
            Console.WriteLine("# startComponentCleanup. Paso 4/4.");
            runAdminCMD("DISM.exe /Online /Cleanup-Image /startComponentCleanup");

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "6":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("cleanmgr.exe");

            Console.Clear();
            runProgram();
            break;
        case "7":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("SystemPropertiesProtection.exe");

            Console.Clear();
            runProgram();
            break;
        case "8":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("control.exe");

            Console.Clear();
            runProgram();
            break;
        case "9":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("msinfo32.exe");

            Console.Clear();
            runProgram();
            break;
        case "10":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("eventvwr.msc");

            Console.Clear();
            runProgram();
            break;
        case "11":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("MdSched.exe");

            Console.Clear();
            runProgram();
            break;
        case "12":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("lusrmgr.msc");

            Console.Clear();
            runProgram();
            break;
        case "13":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("mmc.exe");

            Console.Clear();
            runProgram();
            break;
        case "14":
            Console.WriteLine("Esta herramienta requiere permisos elevados. Se abrirá una nueva ventana cuando el UAC otorgue los permisos requeridos.");
            Console.WriteLine("No cierre esta ventana.");
            runAdminCMD("WMIC.exe");

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "15":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("diskmgmt.msc");

            Console.Clear();
            runProgram();
            break;
        case "16":
            Console.WriteLine("Esta herramienta requiere permisos elevados. Se abrirá una nueva ventana cuando el UAC otorgue los permisos requeridos.");
            Console.WriteLine("No cierre esta ventana.");
            runAdminCMD("diskpart.exe");

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "17":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("dxdiag.exe");

            Console.Clear();
            runProgram();
            break;
        case "18":
            Console.WriteLine("Esta herramienta requiere permisos elevados. Se abrirá una nueva ventana cuando el UAC otorgue los permisos requeridos.");
            Console.WriteLine("No cierre esta ventana.");
            runAdminCMD("hdwwiz.exe");

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "19":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("WSReset.exe");

            Console.Clear();
            runProgram();
            break;
        case "20":
            runCMD("ipconfig /all");

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "21":
            Console.WriteLine("El equipo se apagará en breve...");
            runCMD("shutdown /f /p");
            break;
        default:
            Console.WriteLine("Opción inválida. Ingrese una opción válida.");

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
    }
}


runProgram();