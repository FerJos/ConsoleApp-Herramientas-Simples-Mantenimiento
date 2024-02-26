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

void runAdminCMD(string cmdLine, bool pauseCMD)
{
    Process proceso = new Process();
    proceso.StartInfo.FileName = "cmd.exe";
    proceso.StartInfo.WorkingDirectory = Environment.GetEnvironmentVariable("SystemRoot") + @"\System32";
    proceso.StartInfo.Arguments = "/c" + cmdLine;
    proceso.StartInfo.Verb = "runas";
    proceso.StartInfo.UseShellExecute = true;

    if (pauseCMD)
    {
        proceso.StartInfo.Arguments += "& pause";
    }

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

bool userAcceptInput(string question)
{
    Console.Write(question + " (S/N): ");

    string userInput = Console.ReadLine() ?? "";
    userInput = userInput.ToUpper();
    switch (userInput)
    {
        case "S":
            return true;
        case "N":
            return false;
        default:
            return userAcceptInput(question);
    }
}

void runProgram()
{
    Console.Beep();

    Console.WriteLine("ConsoleAppTITools1 - Herramientas para el mantenimiento y/o reparación.\n");
    Console.WriteLine("!) Documentación de los comandos de Windows.");
    Console.WriteLine("0) Salir del programa.");
    Console.WriteLine("1)* sfc - Comprobador de recursos.");
    Console.WriteLine("2)* CHKDSK - Corregir errores del disco.");
    Console.WriteLine("3)* CHKDSK - Corregir errores del disco, encontrar sectores defectuosos y recupera la información legible.");
    Console.WriteLine("4)* CHKDSK - Desmontar la unidad, corregir errores del disco, encontrar sectores defectuosos y recupera la información legible.");
    Console.WriteLine("5)* DISM - {/CheckHealth | /ScanHealth | /RestoreHealth}.");
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
    Console.WriteLine("19) Carpetas compartidas.");
    Console.WriteLine("20)* Herramienta de eliminación de software malintencionado de ©Microsoft Windows.");
    Console.WriteLine("21) Editor de directivas de grupo local.");
    Console.WriteLine("22) Windows Store Reset.");
    Console.WriteLine("23) ipConfig /All");
    Console.WriteLine("24) Salir del programa y reiniciar el equipo (Se perderá cualquier trabajo no guardado).");

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
            Console.WriteLine("Esta herramienta requiere permisos elevados. Se abrirá una nueva ventana cuando el UAC otorgue los permisos requeridos.");
            Console.WriteLine("No cierre esta ventana.");
            if (userAcceptInput("¿Desea que el comprobador de recursos intente reparar los archivos con problemas?"))
            {
                Console.WriteLine("# SCANNOW");
                runAdminCMD("sfc.exe /SCANNOW", true);
            }
            else
            {
                Console.WriteLine("# VERIFYONLY");
                runAdminCMD("sfc.exe /VERIFYONLY", true);
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

            runAdminCMD("chkdsk.exe " + userInput + ": /f", true);

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "3":
            userInput = getLetterVolume();

            Console.WriteLine("Esta herramienta requiere permisos elevados. Se abrirá una nueva ventana cuando el UAC otorgue los permisos requeridos.");
            Console.WriteLine("No cierre esta ventana.");

            runAdminCMD("chkdsk.exe " + userInput + ": /f /r /b", true);

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "4":
            userInput = getLetterVolume();

            Console.WriteLine("Esta herramienta requiere permisos elevados. Se abrirá una nueva ventana cuando el UAC otorgue los permisos requeridos.");
            Console.WriteLine("No cierre esta ventana.");

            runAdminCMD("chkdsk.exe " + userInput + ": /f /r /b /x", true);

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "5":
            Console.WriteLine("Esta herramienta requiere permisos elevados. Se abrirá una nueva ventana cuando el UAC otorgue los permisos requeridos.");
            Console.WriteLine("No cierre esta ventana.");

            Console.WriteLine("# CheckHealth. Paso 1/4.");
            runAdminCMD("DISM.exe /Online /Cleanup-Image /CheckHealth", true);
            Console.WriteLine("# ScanHealth. Paso 2/4.");
            runAdminCMD("DISM.exe /Online /Cleanup-Image /ScanHealth", true);
            Console.WriteLine("# RestoreHealth. Paso 3/4.");
            if (userAcceptInput("¿Ejecutar '/RestoreHealth' para realizar operaciones de reparación automáticamente? Esta operación puede tardar varios minutos."))
            {
                runAdminCMD("DISM.exe /Online /Cleanup-Image /RestoreHealth", true);
            }
            Console.WriteLine("# startComponentCleanup. Paso 4/4.");
            if (userAcceptInput("¿Ejecutar '/startComponentCleanup' para limpiar los componentes reemplazados y reducir el tamaño del almacén de componentes?"))
            {
                if (userAcceptInput("¿Ejecutar '/ResetBase' para restablecer la base de componentes reemplazados? ADVERTENCIA: Las actualizaciones de Windows instaladas no se pueden desinstalar si ejecuta '/startComponentCleanup' con '/ResetBase'."))
                {
                    runAdminCMD("DISM.exe /Online /Cleanup-Image /startComponentCleanup /ResetBase", true);
                }
                else
                {
                    runAdminCMD("DISM.exe /Online /Cleanup-Image /startComponentCleanup", true);
                }
            }

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
            runAdminCMD("WMIC.exe", false);

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
            runAdminCMD("diskpart.exe", false);

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
            runAdminCMD("hdwwiz.exe", false);

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "19":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("fsmgmt.msc");

            Console.Clear();
            runProgram();
            break;
        case "20":
            Console.WriteLine("Esta herramienta requiere permisos elevados. Se abrirá una nueva ventana cuando el UAC otorgue los permisos requeridos.");
            Console.WriteLine("No cierre esta ventana.");
            runAdminCMD("mrt.exe", false);

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "21":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("gpedit.msc");

            Console.Clear();
            runProgram();
            break;
        case "22":
            Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
            Console.WriteLine("No cierre esta ventana.");
            runCMD("WSReset.exe");

            Console.Clear();
            runProgram();
            break;
        case "23":
            runCMD("ipconfig /all");

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            runProgram();
            break;
        case "24":
            Console.WriteLine("El equipo se reiniciará dentro de 5 segundos...");
            runCMD("shutdown /r /f /t 005");
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


Console.Title = "ConsoleAppTITools1";
runProgram();